using System;

namespace try2
{
    internal class Grid
    {
        private static readonly string[] _template =
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

        public static void Draw(DrawParams drawParams)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            var gridSize = drawParams.Size * drawParams.Scale + new Point(1,1);
            var windowSize = gridSize + drawParams.Offset*2;
            //Console.SetWindowSize(windowSize.Width, windowSize.Height);
            gridSize
                .AllPoints()
                .ForAll(point => Draw(point, gridSize, drawParams.Offset));
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
            return _template[y][x];
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