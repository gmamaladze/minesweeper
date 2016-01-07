// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;

namespace Fmines.DotNet
{
    internal interface IReadOnlySet<T> : IEnumerable<T>
    {
        bool Contains(T item);
    }
}