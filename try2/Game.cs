using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal class Game
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
        public static MineField Init(Options options)
        {
            return
                new MineField(
                    options.Size,
                    CreateRandomMines(options));
        }


        [Pure]
        public static CoverField SwitchFlag(Point point, CoverField covers)
        {
            var result = covers;
            result.SwitchFlag(point);
            return result;
        }

        [Pure]
        public static CoverField UncoverDeep(Point point, CoverField covers)
        {
            var result = covers;
            Directions
                .All()
                .Select(point.Next)
                .ForAll(p => result = UncoverDeep(p, result));
            return result;
        }


        [Pure]
        public static GameResult Evaluate(Options options, MineField mineField, CoverField covers)
        {
            return new GameResult(
                hasFailed: 
                    mineField
                        .Mines
                        .Intersect(covers.FreePoints())
                        .Any(),
                coveredCount: 
                    covers
                        .All()
                        .Count(c => c == Cover.Covered));
        }
    }
}