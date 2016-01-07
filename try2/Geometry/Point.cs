// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

namespace Fmines.Geometry
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
            return new Point(left.X*right, left.Y*right);
        }

        public static Point operator *(Point left, Scale right)
        {
            return new Point(left.X*right.X, left.Y*right.Y);
        }

        public override string ToString()
        {
            return $"{X}x{Y}";
        }
    }
}