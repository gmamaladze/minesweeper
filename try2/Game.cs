using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal static class Game
    {
        [Pure]
        public static GameResult Evaluate(
            Options options, 
            IReadOnlyDictionary<Point, Content> mineField,
            IImmutableDictionary<Point, Cover> covers)
        {
            return new GameResult(mineField
                .Where(pair => pair.Value == Content.Boom)
                .Select(pair => pair.Key)
                .Intersect(
                    covers
                        .Where(pair => pair.Value == Cover.Uncovered)
                        .Select(pair => pair.Key))
                .Any(), covers
                    .Count(pair => pair.Value == Cover.CoveredUnflagged));
        }
    }
}