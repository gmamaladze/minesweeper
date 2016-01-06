using System.Collections.Generic;

namespace try2
{
    internal interface IReadOnlySet<T> : IEnumerable<T>
    {
        bool Contains(T item);
    }
}