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
    }
}