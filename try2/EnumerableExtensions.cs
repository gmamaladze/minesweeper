using System;
using System.Collections.Generic;

namespace try2
{
    public static class EnumerableExtensions
    {
        public static void ForAll<T>(this IEnumerable<T> elements, Action<T> action)
        {
            foreach (var element in elements)
            {
                action(element);
            }
        }
    }
}