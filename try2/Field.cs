using System.Collections.Generic;
using System.Linq;

namespace try2
{
    internal abstract class Field<T> where T : new()
    {
        protected Field(Size size)
            : this(new T[size.Width, size.Height])
        {

        }

        protected Field(T[,] cells)
        {
            Cells = cells;
        }

        protected T[,] Cells { get; }

        public T this[Point point]
        {
            get
            {
                if (!point.IsInRange(GetSize())) return new T();
                return Cells[point.X, point.Y];
            }
            protected set
            {
                if (!point.IsInRange(GetSize())) return;
                Cells[point.X, point.Y] = value;
            }
        }

        public Size GetSize() => new Size(Cells.GetLength(0), Cells.GetLength(1));

        public IEnumerable<T> All()
        {
            return GetSize()
                .AllPoints()
                .Select(p => this[p]);
        } 
    }
}