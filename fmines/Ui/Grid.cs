// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.DotNet;
using Fmines.Geometry;

namespace Fmines.Ui
{
    public static class Grid
    {
        private static readonly string[] Template =
        {
            //0123456789012
            "┌───┬───┬───┐", //0
            "│   │   │   │", //1
            "├───┼───┼───┤", //2
            "│   │   │   │", //3
            "├───┼───┼───┤", //4
            "│   │   │   │", //5
            "└───┴───┴───┘" //6
        };

        public static void Draw(Graphics graphics)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            var gridSize = graphics.Size*graphics.Scale + new Point(1, 1);
            gridSize
                .AllPoints()
                .ForAll(point => Draw(point, gridSize, graphics.Offset));
            Console.ResetColor();
        }


        private static void Draw(Point point, Size gridSize, Point offset)
        {
            var position = point + offset;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(
                GetChar(
                    ToTemplateX(point.X, gridSize),
                    ToTemplateY(point.Y, gridSize)));
        }

        private static char GetChar(int x, int y)
        {
            return Template[y][x];
        }

        private static int ToTemplateY(int y, Size gridSize)
        {
            if (y == 0) return 0;
            if (y == gridSize.Height - 1) return 6;
            return y%4 + 2;
        }

        private static int ToTemplateX(int x, Size gridSize)
        {
            if (x == 0) return 0;
            if (x == gridSize.Width - 1) return 12;
            return x%8 + 4;
        }
    }
}