using System;
using System.Collections.Generic;

namespace try2
{
    internal class Direction : Point
    {
        private Direction(int dx, int dy)
            : base(dx, dy)
        {
            if (Math.Abs(dx) > 1) throw new ArgumentOutOfRangeException(nameof(dx));
            if (Math.Abs(dy) > 1) throw new ArgumentOutOfRangeException(nameof(dy));
        }

        public static Direction Up { get; } = new Direction(0, -1);
        public static Direction Down { get; } = new Direction(0, 1);
        public static Direction Left { get; } = new Direction(-1, 0);
        public static Direction Right { get; } = new Direction(1, 0);

        public static IEnumerable<Direction> All4 { get; } = new[] {Up, Down, Left, Right};

        public static IEnumerable<Direction> All8 { get; } = new[]
        {Up, Up + Right, Right, Right + Down, Down, Down + Left, Left, Left + Up};

        public static Direction operator +(Direction first, Direction second)
        {
            return new Direction(first.X + second.X, first.Y + second.Y);
        }
    }
}