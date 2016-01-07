using System;

namespace try2
{
    internal static class Cursor
    {
        public static void Draw(Graphics graphics, Point position)
        {
            Console.CursorVisible = false;
            var screenPoint = graphics.Transform(position) + new Point(2, 1);
            Console.CursorLeft = screenPoint.X;
            Console.CursorTop = screenPoint.Y;
            Console.CursorVisible = true;
        }
    }
}