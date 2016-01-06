using System.Collections.Immutable;
using System.Linq;

namespace try2
{
    internal class Covers
    {
        private readonly IImmutableDictionary<Point, bool> _covers;

        public Covers(Size size)
            : this(size
                .AllPoints()
                .ToImmutableDictionary(p => p, p => false))
        {
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