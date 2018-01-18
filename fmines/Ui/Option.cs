// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using Fmines.Geometry;

namespace Fmines.Ui
{
    public class Option
    {
        public Option(Size size, int minesCount, string description = "Custom")
        {
            if (minesCount < 10) throw new ArgumentOutOfRangeException(nameof(minesCount));
            MinesCount = minesCount;
            Description = description;
            Size = size;
        }

        public Size Size { get; }
        public int MinesCount { get; }

        public string Description { get; }

        public override string ToString()
        {
            return $"{Description} \t {MinesCount} mines, {Size} tile grid";
        }
    }
}