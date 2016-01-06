using System;

namespace try2
{
    internal class Icon
    {
        public Icon(char symbol, ConsoleColor color)
        {
            _symbol = symbol;
            _color = color;
        }

        private readonly char _symbol;

        private readonly ConsoleColor _color;

        private readonly static Point CellOffset = new Point(2, 1);

        public void Draw(DrawParams drawParams, Point position)
        {
            var consolePosition = drawParams.Transform(position) + CellOffset;
            Console.SetCursorPosition(consolePosition.X, consolePosition.Y);
            Console.ForegroundColor = _color;
            Console.Write(_symbol);
        }
    }
}