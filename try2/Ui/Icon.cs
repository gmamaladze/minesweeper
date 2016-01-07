using System;

namespace try2
{
    internal class Icon
    {
        public Icon(char symbol, ConsoleColor color)
        {
            Symbol = symbol;
            Color = color;
        }

        public char Symbol { get; }

        public ConsoleColor Color { get; }
    }
}