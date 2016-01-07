// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.Geometry;

namespace Fmines.Ui
{
    internal static class Title
    {
        public static void Draw(Graphics graphics, string line1, string line2 = "")
        {
            Console.CursorVisible = true;
            var lines = new[] {line1, line2};
            var position = new Point(graphics.Offset.X, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            var maxWidth = graphics.Transform(graphics.Size).Width;

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