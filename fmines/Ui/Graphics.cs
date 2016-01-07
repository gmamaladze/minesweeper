// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.Geometry;

namespace Fmines.Ui
{
    internal class Graphics : IDisposable
    {
        private readonly ConsoleSettings _originalSettings;

        public Graphics(Size size, Point offset, Scale scale, ConsoleSettings originalSettings)
        {
            _originalSettings = originalSettings;
            Size = size;
            Offset = offset;
            Scale = scale;
        }

        public Size Size { get; }

        public Point Offset { get; }

        public Scale Scale { get; }

        public void Dispose()
        {
            _originalSettings.Restore();
        }

        public static Graphics Init(Size size)
        {
            var graphics = new Graphics(
                size,
                new Point(2, 2),
                new Scale(4, 2),
                ConsoleSettings.Capture());
            Console.CursorSize = 100;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            ResizeWindow(graphics);
            Console.Clear();
            return graphics;
        }

        private static void ResizeWindow(Graphics graphics)
        {
            var actualSize = new Size(Console.WindowWidth, Console.WindowHeight);
            var requiredSize = graphics.Transform((graphics.Size + new Point(1, 1)));
            if (!requiredSize.FitsInto(actualSize))
            {
                Console.SetWindowSize(requiredSize.Width, requiredSize.Height);
            }
        }

        public Size Transform(Size size)
        {
            return size*Scale + Offset;
        }

        public Point Transform(Point point)
        {
            return point*Scale + Offset;
        }
    }
}