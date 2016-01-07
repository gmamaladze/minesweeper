using System;

namespace try2
{
    internal static class Cell
    {
        public static void Draw(Graphics graphics, Icon icon, Point position)
        {
            Console.CursorVisible = false;
            var consolePosition = graphics.Transform(position) + new Point(2, 1);
            Console.SetCursorPosition(consolePosition.X, consolePosition.Y);
            Console.ForegroundColor = icon.Color;
            Console.Write(icon.Symbol);
            Console.CursorVisible = true;
        }
    }
}