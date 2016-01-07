using System.Collections.Immutable;

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

        public GameState Move(Covers covers)
        {
            var moves = _moves.Push(covers);
            return new GameState(moves, CursorPosition);
        }

        public GameState Move(Point cursorPosition)
        {
            return new GameState(_moves, cursorPosition);
        }

        public GameState Undo()
        {
            Covers _;
            return new GameState(_moves.Pop(out _), CursorPosition);
        }

        public Covers Covers()
        {
            return _moves.Peek();
        }
    }
}