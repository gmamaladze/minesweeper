using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace try2
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