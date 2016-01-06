using System;

namespace try2
{
    internal class CustomConsoleSettings : IDisposable
    {
        private readonly int _originalCursorSize;
        private readonly Size _originalWindowSize;

        private CustomConsoleSettings(int originalCursorSize, Size originalWindowSize)
        {
            _originalCursorSize = originalCursorSize;
            _originalWindowSize = originalWindowSize;
            Console.CancelKeyPress += CancelKeyPressed;
        }

        public void Dispose()
        {
            RestoreOriginalSettings();
        }

        public static CustomConsoleSettings Init(DrawParams drawParams)
        {
            var actualSize = new Size(Console.WindowWidth, Console.WindowHeight);
            Console.CursorSize = 100;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            AdjustSizeIfNeeded(drawParams, actualSize);
            Console.Clear();
            return new CustomConsoleSettings(Console.CursorSize, actualSize);
        }

        private void CancelKeyPressed(object sender, ConsoleCancelEventArgs e)
        {
            RestoreOriginalSettings();
        }

        private static void AdjustSizeIfNeeded(DrawParams drawParams, Size actualSize)
        {
            var requiredSize = drawParams.Transform((drawParams.Size + new Point(1, 1)));
            if (!requiredSize.FitsInto(actualSize))
            {
                Console.SetWindowSize(requiredSize.Width, requiredSize.Height);
            }
        }

        private void RestoreOriginalSettings()
        {
            Console.ResetColor();
            Console.CursorSize = _originalCursorSize;
            Console.SetWindowSize(_originalWindowSize.Width, _originalWindowSize.Height);
            Console.Clear();
        }
    }
}