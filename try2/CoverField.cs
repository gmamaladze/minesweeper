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
            if (covers.GetAt(point) == Cover.Uncovered) return covers;
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
            MineField mineField,
            Point point,
            Size size)
        {
            if (covers.GetAt(point) == Cover.Uncovered) return covers;
            if (mineField.IsEmptyAt(point)) return covers.Uncover(point);
            return point
                .Neighbours(size)
                .Aggregate(
                    covers.Uncover(point),
                    (current, neighbor) => current.UncoverDeep(mineField, neighbor, size));
        }

        [Pure]
        public static IImmutableDictionary<Point, Cover> UncoverMines(
            this IImmutableDictionary<Point, Cover> covers,
            MineField mineField)
        {
            return
                mineField
                    .Mines()
                    .Aggregate(covers, (current, mine) => current.Uncover(mine));
        }
    }
    }