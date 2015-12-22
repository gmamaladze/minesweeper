using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace try2
{
    internal static class PointExtensions
    {
        [Pure]
        public static Point Next(this Point point, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(point.X, point.Y - 1);
                case Direction.Down:
                    return new Point(point.X, point.Y + 1);
                case Direction.Left:
                    return new Point(point.X - 1, point.Y);
                case Direction.Right:
                    return new Point(point.X + 1, point.Y);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static IEnumerable<Point> Neighbours(this Point point)
        {
            yield return point.Next(Direction.Up);
            yield return point.Next(Direction.Up).Next(Direction.Right);
            yield return point.Next(Direction.Right);
            yield return point.Next(Direction.Right).Next(Direction.Down);
            yield return point.Next(Direction.Down);
            yield return point.Next(Direction.Down).Next(Direction.Left);
            yield return point.Next(Direction.Left);
            yield return point.Next(Direction.Left).Next(Direction.Up);
        }
    }
}