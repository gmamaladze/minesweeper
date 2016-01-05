using System.Runtime.CompilerServices;

namespace try2
{
    internal class Point
    {
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Point)) return false;
            return Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

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

        public override string ToString()
        {
            return $"{X}x{Y}";
        }
    }
}