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

        public async Task Watch(IList<IntPtr> addresses, int intervalInMillis, CancellationToken token,
            Action<ulong, int> handler = null, Action<IDictionary<ulong, int>> allHandler = null)
        {
            Console.WriteLine("Watcher Started");
            var values = new Dictionary<ulong, int>();
            var baseAddr = (ulong)Target.MainModule.BaseAddress.ToInt64();
            while (!token.IsCancellationRequested)
            {
                values.Clear();
                foreach (var address in addresses)
                {
                    byte[] data = new byte[sizeof(int)];
                    if (Windows.ReadProcessMemory(TargetHandle, address, data, data.Length, out int _))
                    {
                        var value = BitConverter.ToInt32(data, 0);
                        handler?.Invoke((ulong)address, value);
                        values.Add((ulong)address, value);
                    }
                }
                allHandler?.Invoke(values);
                await Task.Delay(intervalInMillis).ConfigureAwait(false);
            }
        }

        public async Task<List<IntPtr>> FindMatches(float value, ISet<IntPtr> exclusions = null, Action<ulong> handler = null)
        {
            await Console.Out.WriteLineAsync("Scanning Addresses...");
            var matches = new List<IntPtr>();

            var start = 0UL; // (ulong)Target.MainModule.BaseAddress;
            var end = 0x7FFFFFFEFFFFUL; // (ulong)0x145000000;

            await Console.Out.WriteLineAsync($"Address Range: 0x{start:X08} to 0x{end:X08}");

            Console.WriteLine(BitConverter.IsLittleEndian);
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
                        await Console.Out.WriteLineAsync("Page query returned 0 bytes");
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
                            var found = BitConverter.ToSingle(data, offset);
                            if (found == value)
                            {
                                await Console.Out.WriteLineAsync($"0x{regionStart + (ulong)offset:X8}: {found}");
                                await Console.Out.WriteLineAsync("Match Address (Absolute): " + (regionStart + (ulong)offset));
                                await Console.Out.WriteLineAsync("Match Address Index: " + offset);
                                await Console.Out.WriteLineAsync("Match Address Base: " + regionStart);
                                var address = (IntPtr)(regionStart + (ulong)offset);
                                if (!exclusions.Contains(address))
                                {
                                    matches.Add(address);
                                    handler?.Invoke((ulong)address);
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

    }
}
