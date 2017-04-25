// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Fmines.Geometry;

namespace Fmines
{
    internal class Covers
    {
        private readonly IImmutableDictionary<Point, bool> _covers;

        private Covers(IImmutableDictionary<Point, bool> covers)
        {
            _covers = covers;
        }

        public static Covers Create(Size size)
        {
            return new Covers(
                size
                    .AllPoints()
                    .ToImmutableDictionary(p => p, p => false));
        }

        public Covers SwitchFlag(Point point)
        {
            bool hasFlag;
            var found = _covers.TryGetValue(point, out hasFlag);
            return !found
                ? this
                : new Covers(_covers.SetItem(point, !hasFlag));
        }

        public Covers Uncover(Point point)
        {
            return
                IsCovered(point)
                    ? new Covers(_covers.Remove(point))
                    : this;
        }

        public Covers UncoverRange(IEnumerable<Point> points)
        {
            return new Covers(_covers.RemoveRange(points));
        }

        public bool IsCovered(Point point)
        {
            return _covers.ContainsKey(point);
        }

        public bool HasFlag(Point point)
        {
            return _covers[point];
        }

        public int CoverCount()
        {
            return _covers.Values.Count();
        }
    }
}