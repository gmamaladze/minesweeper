// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.Geometry;

namespace Fmines.Ui
{
    public static class Cursor
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