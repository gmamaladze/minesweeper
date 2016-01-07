using System.Collections;
using System.Collections.Generic;

namespace try2
{
    public class ReadOnlySet<T> : IReadOnlySet<T>
    {
        private readonly ISet<T> _set;

        public ReadOnlySet(ISet<T> set)
        {
            _set = set;
        }

        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}