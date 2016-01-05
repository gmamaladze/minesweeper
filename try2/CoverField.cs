using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal static class CoverField
    {
        [Pure]
        public static IImmutableDictionary<Point, Cover> Uncover(this IImmutableDictionary<Point, Cover> covers,
            Point point)
        {
            return covers.SetItem(point, Cover.Uncovered);
        }

        [Pure]
        public static IImmutableDictionary<Point, Cover> SwitchFlag(
            this IImmutableDictionary<Point, Cover> covers,
            Point point)
        {
            return covers.SetAt(
                point,
                covers.GetAt(point).Opposite());
        }

        [Pure]
        public static Cover GetAt(this IImmutableDictionary<Point, Cover> covers, Point point)
        {
            Cover result;
            var found = covers.TryGetValue(point, out result);
            return !found
                ? Cover.CoveredUnflagged
                : result;
        }

        [Pure]
        public static IImmutableDictionary<Point, Cover> SetAt(
            this IImmutableDictionary<Point, Cover> covers, Point point,
            Cover value)
        {
            return value == Cover.Uncovered
                ? covers.Remove(point)
                : covers.SetItem(point, value);
        }


        [Pure]
        public static IImmutableDictionary<Point, Cover> UncoverDeep(
            this IImmutableDictionary<Point, Cover> covers,
            IReadOnlyDictionary<Point, Content> mines,
            Point point,
            Size size)
        {
            if (!point.IsInRange(size)) return covers;
            if (covers.GetAt(point) == Cover.Uncovered) return covers;
            if (mines.GetAt(point) != Content.Empty) return covers.Uncover(point);
            return point
                .Neighbours()
                .Aggregate(
                    covers.Uncover(point),
                    (current, neighbor) => current.UncoverDeep(mines, neighbor, size));
        }
    }
}