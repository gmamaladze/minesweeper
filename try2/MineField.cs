using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal static class MineField
    {
        [Pure]
        public static IEnumerable<Point> CreateRandomMines(Options options)
        {
            return Enumerable
                .Repeat(new Random(), options.MinesCount)
                .Select(r =>
                    new Point(
                        r.Next(options.Size.Width),
                        r.Next(options.Size.Height)));
        }

        [Pure]
        public static ReadOnlyDictionary<Point, Content> Populate(IEnumerable<Point> mines, Size size)
        {
            var builder = new Dictionary<Point, Content>();
            foreach (var mine in mines)
            {
                builder[mine] = Content.Boom;
                mine
                    .Neighbours()
                    .Where(p => p.IsInRange(size))
                    .ForAll(neighbor => { builder[neighbor] = builder.GetAt(neighbor).Inc(); });
            }
            return new ReadOnlyDictionary<Point, Content>(builder);
        }


        [Pure]
        public static Content GetAt(this IReadOnlyDictionary<Point, Content> content, Point point)
        {
            Content result;
            var found = content.TryGetValue(point, out result);
            return !found
                ? Content.Empty
                : result;
        }
    }
}