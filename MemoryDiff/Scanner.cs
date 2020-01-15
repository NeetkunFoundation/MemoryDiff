using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryDiff
{
    enum ScanState
    {
        Ready,
        Scanning,
        Complete
    }

    static class ScanStateToString
    {
        internal static string ToString(this ScanState state, int count = 0)
        {
            switch (state)
            {
                case ScanState.Ready:
                    return "準備完了";
                case ScanState.Scanning:
                    return "解析中... (件数: " + count + ")";
                case ScanState.Complete:
                    return "検索完了 (件数: " + count + ")";
            }
            return "";
        }
    }

    class Scanner
    {
        static Windows.RegionPageProtection[] ProtectionExclusions { get; } = new[] {
            Windows.RegionPageProtection.PAGE_GUARD,
            Windows.RegionPageProtection.PAGE_NOACCESS
        };

        static Windows.RegionPageProtection[] ProtectionInclusions { get; } = new[] {
            Windows.RegionPageProtection.PAGE_EXECUTE_READ
        };

        Process Target { get; }
        IntPtr TargetHandle => Target.Handle;

        public Scanner(Process target)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public async Task Watch<T>(IList<IntPtr> addresses, int intervalInMillis, CancellationToken token,
            Action<ulong, T> handler = null, Action<IDictionary<ulong, T>> allHandler = null, T t = default(T))
        {
            await Log.WriteLineAsync("ウォッチを開始しました...");
            var values = new Dictionary<ulong, T>();
            var baseAddr = (ulong)Target.MainModule.BaseAddress.ToInt64();
            while (!token.IsCancellationRequested)
            {
                foreach (var address in addresses)
                {
                    int size;
                    switch (Type.GetTypeCode(t?.GetType() ?? typeof(T)))
                    {
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                            size = sizeof(byte);
                            break;
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                            size = sizeof(short);
                            break;
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Single:
                            size = sizeof(int);
                            break;
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Double:
                            size = sizeof(long);
                            break;
                        default:
                            size = 16;
                            break;
                    }
                    byte[] data = new byte[size];
                    if (Windows.ReadProcessMemory(TargetHandle, address, data, data.Length, out int _))
                    {
                        var key = (ulong)address;
                        T value = Convert<T>(data, 0, t);
                        if (!values.ContainsKey(key) || !Equals(values[key], value))
                        {
                            handler?.Invoke(key, value);
                            values[key] = value;
                        }
                    }
                }
                allHandler?.Invoke(values);
                await Task.Delay(intervalInMillis).ConfigureAwait(false);
            }
            await Log.WriteLineAsync("ウォッチを停止しました...");
        }

        public async Task<List<IntPtr>> FindMatches<T>(T value, ISet<IntPtr> exclusions = null, Action<ulong, T> handler = null)
        {
            await Log.WriteLineAsync("スキャンしています...");
            var matches = new List<IntPtr>();

            // var start = (ulong)Target.MainModule.BaseAddress;
            // var end = 0x145000000UL;

            var start = 0UL;
            var end = 0x7FFFFFFEFFFFUL;

            await Log.WriteLineAsync($"検索範囲: 0x{start:X08} to 0x{end:X08}");
            await Log.WriteLineAsync("リトルエンディアン: " + BitConverter.IsLittleEndian);
            return await Task.Run(async () =>
            {
                var current = start;
                while (current < end && !Target.HasExited)
                {
                    var structByteCount = Windows.VirtualQueryEx(
                       TargetHandle,
                       (IntPtr)current,
                       out Windows.MEMORY_BASIC_INFORMATION64 region,
                       (uint)Marshal.SizeOf(typeof(Windows.MEMORY_BASIC_INFORMATION64)));
                    if (structByteCount == 0)
                    {
                        await Log.WriteLineAsync("Page query returned 0 bytes");
                        break;
                    }

                    if (region.RegionSize > 0
                       && region.State == (uint)Windows.RegionPageState.MEM_COMMIT
                       && CheckProtection(region.Protect))
                    {
                        var regionStart = region.BaseAddress;
                        if (start > regionStart)
                            regionStart = start;

                        var regionEnd = region.BaseAddress + region.RegionSize;
                        if (end < regionEnd)
                            regionEnd = end;

                        if (Target.HasExited)
                            break;

                        var regionSize = regionEnd - regionStart;
                        var data = new byte[regionSize];
                        Windows.ReadProcessMemory(TargetHandle,
                           (IntPtr)regionStart, data, data.Length, out int _);

                        // Matching
                        for (var offset = 0; offset < data.Length; offset += sizeof(float))
                        {
                            T found = Convert<T>(data, offset, value);
                            if (Equals(found, value))
                            {
                                await Log.WriteLineAsync($"0x{regionStart + (ulong)offset:X8}: {found}");
                                await Log.WriteLineAsync($"絶対アドレス: 0x{regionStart + (ulong)offset:X8}");
                                await Log.WriteLineAsync($"チャンク: 0x{regionStart:X8} から 0x{regionEnd:X8}");
                                await Log.WriteLineAsync($"オフセット: 0x{offset:X8}");
                                var address = (IntPtr)(regionStart + (ulong)offset);
                                if (!exclusions.Contains(address))
                                {
                                    matches.Add(address);
                                    handler?.Invoke((ulong)address, found);
                                }
                            }
                        }
                    }
                    current = region.BaseAddress + region.RegionSize;
                }

                return matches;
            });
        }

        static bool CheckProtection(uint flags)
        {
            var protection = (Windows.RegionPageProtection)flags;
            if (ProtectionExclusions.Where(exclusion => protection.HasFlag(exclusion)).Any())
                return false;
            if (ProtectionInclusions.Where(inclusion => protection.HasFlag(inclusion)).Any())
                return true;
            return true;
        }

        static T Convert<T>(byte[] data, int offset, object value = null)
        {
            switch (Type.GetTypeCode(value?.GetType() ?? typeof(T)))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return (T)(object)data[offset];
                case TypeCode.Int16:
                    return (T)(object)BitConverter.ToInt16(data, offset);
                case TypeCode.UInt16:
                    return (T)(object)BitConverter.ToUInt16(data, offset);
                case TypeCode.Int32:
                    return (T)(object)BitConverter.ToInt32(data, offset);
                case TypeCode.UInt32:
                    return (T)(object)BitConverter.ToUInt32(data, offset);
                case TypeCode.Int64:
                    return (T)(object)BitConverter.ToInt64(data, offset);
                case TypeCode.UInt64:
                    return (T)(object)BitConverter.ToUInt64(data, offset);
                case TypeCode.Single:
                    return (T)(object)BitConverter.ToSingle(data, offset);
                case TypeCode.Double:
                    return (T)(object)BitConverter.ToDouble(data, offset);
                default:
                    // TODO: Support other types.
                    return (T)(object)data;
            }
        }

    }
}
