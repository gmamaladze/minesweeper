using System.Collections.Generic;
using System.Collections.Immutable;

namespace try2
{
    internal class GameState
    {
        public GameState()
            : this(new Stack<Covers>())
        {
        }

        private GameState(Stack<Covers> moves)
        {
            Moves = moves;
        }

        public Point CursorPosition { get; set; }
        public GameResult GameResult { get; set; }
        public Stack<Covers> Moves { get; }
    }
}