using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
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
                .All8
                .Select(point.Next);
        }

        public static IEnumerable<Point> Neighbours(this Point point, Size range)
        {
            return point
                .Neighbours()
                .Where(p => p.IsInRange(range));
        }

    }
}