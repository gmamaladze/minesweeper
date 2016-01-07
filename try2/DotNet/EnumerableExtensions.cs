using System;
using System.Collections.Generic;

namespace try2
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