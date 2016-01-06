using System.Collections.Generic;
using System.Collections.Immutable;

namespace try2
{
    internal class GameState
    {
        public GameState()
            : this(new Stack<IImmutableDictionary<Point, Cover>>())
        {
        }

        private GameState(Stack<IImmutableDictionary<Point, Cover>> moves)
        {
            Moves = moves;
        }

        public Point CursorPosition { get; set; }
        public GameResult GameResult { get; set; }
        public Stack<IImmutableDictionary<Point, Cover>> Moves { get; }
    }
}