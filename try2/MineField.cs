// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using Fmines.DotNet;
using Fmines.Geometry;

namespace Fmines
{
    internal class MineField
    {
        private readonly IReadOnlySet<Point> _mines;
        private readonly IReadOnlyDictionary<Point, int> _warnings;

        public MineField(Size size, IReadOnlySet<Point> mines, IReadOnlyDictionary<Point, int> warnings)
        {
            Size = size;
            _mines = mines;
            _warnings = warnings;
        }

        public Size Size { get; }


        public bool HasMineAt(Point point)
        {
            return _mines.Contains(point);
        }

        public int WarningsAt(Point point)
        {
            int result;
            var found = _warnings.TryGetValue(point, out result);
            return found ? result : 0;
        }


        private bool HasWarningAt(Point point)
        {
            return _warnings.ContainsKey(point);
        }

        public bool IsEmptyAt(Point point)
        {
            return !HasMineAt(point) && !HasWarningAt(point);
        }

        public IEnumerable<Point> Mines()
        {
            return _mines;
        }
    }
}