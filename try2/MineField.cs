using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal class MineField : Field<Content>
    {
        public IEnumerable<Point> Mines { get; }

        public MineField(Size size, IEnumerable<Point> mines) 
            : base(size)
        {
            Mines = mines.ToArray();
            Mines.ForAll(Place);
        }

        private void Place(Point mine)
        {
            this[mine] = Content.Boom;
            mine
                .Neighbours()
                .ForAll(neighbor =>
                {
                    this[neighbor] = this[neighbor].Inc();
                });
        }
    }
}