// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;

namespace Fmines.DotNet
{
    internal static class EnumerableExtensions
    {
        public static void ForAll<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
            {
                action(element);
            }
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static IReadOnlySet<T> ToReadOnlySet<T>(this ISet<T> set)
        {
            return new ReadOnlySet<T>(set);
        }

        public static IReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T> source)
        {
            return source
                .ToHashSet()
                .ToReadOnlySet();
        }
    }
}