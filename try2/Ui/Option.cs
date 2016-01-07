using System;

namespace try2
{
    internal class Option
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