using System;

namespace try2
{
    internal static class Title
    {
        public static void Draw(DrawParams drawParams, string line1, string line2 = "")
        {
            Console.CursorVisible = true;
            var lines = new[] {line1, line2};
            var position = new Point(drawParams.Offset.X, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            var maxWidth = drawParams.Transform(drawParams.Size).Width;

            foreach (var text in lines)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(
                    text
                        .PadRight(maxWidth)
                        .Substring(0, maxWidth));
                position = position.Next(Direction.Down);
            }
            Console.CursorVisible = true;
        }
    }
}