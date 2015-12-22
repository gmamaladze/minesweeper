using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal class CoverField : Field<Cover>
    {
        public CoverField(Size size) : base (size) {}

        private CoverField(Cover[,] covers) : base(covers) { }

        public void Uncover(Point point)
        {
            this[point] = Cover.Free;
        }

        public void SwitchFlag(Point point)
        {
            var cover = this[point];
            switch (cover)
            {
                case Cover.Covered:
                    this[point] = Cover.Flagged;
                    break;
                case Cover.Free:
                    break;
                case Cover.Flagged:
                    this[point] = Cover.Covered;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<Point> FreePoints()
        {
            return GetSize()
                .AllPoints()
                .Where(p => this[p] == Cover.Free);
        }

        public CoverField Clone()
        {
            return new CoverField((Cover[,])Cells.Clone());
        }
    }
}