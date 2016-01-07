// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.Geometry;

namespace Fmines.Ui
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