// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Fmines.Geometry
{
    internal static class SizeExtensions
    {
        [Pure]
        public static IEnumerable<Point> AllPoints(this Size size)
        {
            for (var i = 0; i < size.Width; i++)
            {
                for (var j = 0; j < size.Height; j++)
                {
                    yield return new Point(i, j);
                }
            }
        }
    }
}