using System.Collections.Immutable;
using System.Linq;

namespace try2
{
    internal class GameState
    {
        private readonly IImmutableStack<Covers> _moves;

        public static GameState Create(Covers covers)
        {
            return new GameState(ImmutableStack.Create(covers), new Point(0, 0));
        }
        private GameState(IImmutableStack<Covers> moves, Point cursorPosition)
        {
            _moves = moves;
            CursorPosition = cursorPosition;
        }

        public Point CursorPosition { get; }

        public GameState Do(Covers covers)
        {
            var moves = _moves.Push(covers);
            return new GameState(moves, CursorPosition);
        }

        public GameState Do(Point cursorPosition)
        {
            return new GameState(_moves, cursorPosition);
        }

        public GameState Undo()
        {
            var moves = _moves.Pop();
            return moves.IsEmpty 
                ? this 
                : new GameState(moves, CursorPosition);
        }

        public GameResult Evaluate(MineField mineField)
        {
            var covers = Covers();
            return new GameResult(
                mineField
                    .Mines()
                    .Any(mine => !covers.IsCovered(mine)),
                covers.UnflaggedCount());
        }


        public Covers Covers()
        {
            return _moves.Peek();
        }
    }
}