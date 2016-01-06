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
            IImmutableDictionary<Point, Cover> covers)
        {
            return new GameResult(
                IsAnyMineUncovered(mineField, covers), 
                GetRemainingCoveredCount(covers, options));
        }

        private static int GetRemainingCoveredCount(IImmutableDictionary<Point, Cover> covers, Options options)
        {
            var total = options.Size.Height*options.Size.Width;
            var uncoveredOrFlagged = covers
                .Count(pair =>
                {
                    var cover = pair.Value;
                    return (cover == Cover.CoveredFlagged) || (cover == Cover.Uncovered);
                });
            return total - uncoveredOrFlagged;
        }

        private static bool IsAnyMineUncovered(MineField mineField, IImmutableDictionary<Point, Cover> covers)
        {
            return mineField
                .Mines()
                .Intersect(
                    covers
                        .Where(pair => pair.Value == Cover.Uncovered)
                        .Select(pair => pair.Key))
                .Any();
        }
    }
}