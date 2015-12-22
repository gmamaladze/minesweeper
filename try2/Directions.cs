using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace try2
{
    internal static class Directions
    {
        [Pure]
        public static IEnumerable<Direction> All() => new[]
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right
        };
    }
}