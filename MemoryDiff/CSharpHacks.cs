﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryDiff
{
    static class CSharpHacks
    {
        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> kvp,
                                             out TKey key, out TValue value)
        {
            key = kvp.Key;
            value = kvp.Value;
        }
    }
}
