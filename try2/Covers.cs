using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace try2
{
    internal class Covers
    {
        private readonly IImmutableDictionary<Point, bool> _covers;

        public static Covers Create(Size size)
        {
            return new Covers(
                size
                    .AllPoints()
                    .ToImmutableDictionary(p => p, p => false));
        }

        private Covers(IImmutableDictionary<Point, bool> covers)
        {
            _covers = covers;
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

        public int UnflaggedCount()
        {
            return _covers.Values.Count(hasFlag => !hasFlag);
        }
    }
}