using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDiff
{
    public static class Log
    {
        public delegate void LogHandler(string text);

        public static event LogHandler OnNext;

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
            OnNext(text);
        }

        public static async Task WriteLineAsync(string text)
        {
            await Console.Out.WriteLineAsync(text);
            OnNext(text);
        }

    }
}
