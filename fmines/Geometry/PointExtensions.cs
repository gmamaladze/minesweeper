// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Fmines.Geometry
{
    internal static class PointExtensions
    {
        [Pure]
        public static Point Next(this Point point, Direction direction)
        {
            return point + direction;
        }

        public static IEnumerable<Point> Neighbours(this Point point)
        {
            return Direction
                .All
                .Select(point.Next);
        }

        public static IEnumerable<Point> Neighbours(this Point point, Size range)
        {
            return point
                .Neighbours()
                .Where(p => p.IsInRange(range));
        }

        public static Point Move(this Point point, Direction direction, Size size)
        {
            var next = point.Next(direction);
            return next.IsInRange(size)
                ? next
                : point;
        }
    }
}