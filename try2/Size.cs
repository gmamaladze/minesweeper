using System;

namespace try2
{
    internal class Size
    {
        public Size(int width, int height)
        {
            if (width < 9) throw new ArgumentOutOfRangeException(nameof(width));
            if (height < 9) throw new ArgumentOutOfRangeException(nameof(height));
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public override string ToString()
        {
            return $"{Width}x{Height}";
        }

        public static Size operator +(Size left, Point right)
        {
            return new Size(left.Width + right.X, left.Height + right.Y);
        }

        public static Size operator *(Size left, Scale right)
        {
            return new Size(left.Width * right.X, left.Height * right.Y);
        }

    }
}