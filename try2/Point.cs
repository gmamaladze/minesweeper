using System.Runtime.CompilerServices;

namespace try2
{
    internal class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool IsInRange(Size size)
        {
            return X >= 0 && Y >= 0 && X < size.Width && Y < size.Height;
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator *(Point left, int right)
        {
            return new Point(left.X * right, left.Y * right);
        }

        public static Point operator *(Point left, Scale right)
        {
            return new Point(left.X * right.X, left.Y * right.Y);
        }

    }
}