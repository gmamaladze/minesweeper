// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Fmines.DotNet;
using Fmines.Geometry;

namespace Fmines
{
    public class MineFiedlBuilder
    {
        [Pure]
        public static MineField GenerateRandom(int minesCount, Size size)
        {
            var mines = CreateRandomMines(minesCount, size);
            var warings = CalculateWarnings(mines, size);
            return new MineField(size, mines, warings);
        }

        [Pure]
        private static IReadOnlySet<Point> CreateRandomMines(int minesCount, Size size)
        {
            return Enumerable
                .Repeat(new Random(), minesCount)
                .Select(r => new Point(
                    r.Next(size.Width),
                    r.Next(size.Height)))
                .ToReadOnlySet();
        }

        [Pure]
        private static IReadOnlyDictionary<Point, int> CalculateWarnings(IEnumerable<Point> mines, Size size)
        {
            return
                mines
                    .SelectMany(m => m.Neighbours(size))
                    .GroupBy(p => p)
                    .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}