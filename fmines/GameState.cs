// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System.Collections.Immutable;
using System.Linq;
using Fmines.Geometry;

namespace Fmines
{
    internal class GameState
    {
        private readonly IImmutableStack<Covers> _moves;

        private GameState(IImmutableStack<Covers> moves, Point cursorPosition)
        {
            _moves = moves;
            CursorPosition = cursorPosition;
        }

        public Point CursorPosition { get; }

        public static GameState Create(Covers covers)
        {
            return new GameState(ImmutableStack.Create(covers), new Point(0, 0));
        }

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