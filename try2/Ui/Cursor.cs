using System;

namespace try2
{
    internal static class Cursor
    {
        public static void Draw(DrawParams drawParams, Point position)
        {
            Console.CursorVisible = false;
            var screenPoint = drawParams.Transform(position) + new Point(2, 1);
            Console.CursorLeft = screenPoint.X;
            Console.CursorTop = screenPoint.Y;
            Console.CursorVisible = true;
        }
    }
}