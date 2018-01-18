// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.Geometry;

namespace Fmines.Ui
{
    public class ConsoleSettings
    {
        private readonly ConsoleColor _backgroundColor;
        private readonly int _cursorSize;
        private readonly ConsoleColor _foregroundColor;
        private readonly Size _windowSize;

        private ConsoleSettings(
            int cursorSize,
            Size windowSize,
            ConsoleColor backgroundColor,
            ConsoleColor foregroundColor)
        {
            _cursorSize = cursorSize;
            _windowSize = windowSize;
            _backgroundColor = backgroundColor;
            _foregroundColor = foregroundColor;
            Console.CancelKeyPress += CancelKeyPressed;
        }

        public static ConsoleSettings Capture()
        {
            return new ConsoleSettings(
                Console.CursorSize,
                new Size(Console.WindowWidth, Console.WindowHeight),
                Console.BackgroundColor,
                Console.ForegroundColor);
        }

        private void CancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            Restore();
        }


        public void Restore()
        {
            Console.ForegroundColor = _foregroundColor;
            Console.BackgroundColor = _backgroundColor;
            Console.CursorSize = _cursorSize;
            Console.SetWindowSize(_windowSize.Width, _windowSize.Height);
            Console.Clear();
            Console.CancelKeyPress -= CancelKeyPressed;
        }
    }
}