using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal static class CoverField
    {
        [Pure]
        public static Covers UncoverDeep(
            this Covers covers,
            MineField mineField,
            Point point,
            Size size)
        {
            if (!covers.IsCovered(point)) return covers;
            if (!mineField.IsEmptyAt(point)) return covers.Uncover(point);
            return point
                .Neighbours(size)
                .Aggregate(
                    covers.Uncover(point),
                    (current, neighbor) => current.UncoverDeep(mineField, neighbor, size));
        }

        [Pure]
        public static Covers UncoverMines(
            this Covers covers,
            MineField mineField)
        {
            return
                mineField
                    .Mines()
                    .Aggregate(covers, (current, mine) => current.Uncover(mine));
        }
    }
}