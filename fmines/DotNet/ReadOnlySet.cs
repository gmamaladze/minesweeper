// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections;
using System.Collections.Generic;

namespace Fmines.DotNet
{
    internal class ReadOnlySet<T> : IReadOnlySet<T>
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