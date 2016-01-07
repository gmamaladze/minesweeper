// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;

namespace Fmines.Geometry
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
            return new Size(left.Width*right.X, left.Height*right.Y);
        }

        public bool FitsInto(Size other)
        {
            return Width <= other.Width && Height <= other.Height;
        }

        protected bool Equals(Size other)
        {
            return Width == other.Width && Height == other.Height;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Size) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Width*397) ^ Height;
            }
        }
    }
}