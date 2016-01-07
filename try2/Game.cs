using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal static class Game
    {
        [Pure]
        public static GameResult Evaluate(
            Options options,
            MineField mineField,
            Covers covers)
        {
            return new GameResult(
                mineField
                    .Mines()
                    .Any(mine => !covers.IsCovered(mine)), 
                covers.UnflaggedCount());
        }


        [Pure]
        public static Covers UncoverDeep(
            this Covers covers,
            MineField mineField,
            Point point)
        {
            if (!covers.IsCovered(point)) return covers;
            if (!mineField.IsEmptyAt(point)) return covers.Uncover(point);
            return point
                .Neighbours(mineField.Size)
                .Aggregate(
                    covers.Uncover(point),
                    (current, neighbor) => current.UncoverDeep(mineField, neighbor));
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