using System;

namespace try2
{
    internal class Icon
    {
        public static readonly Icon Mine = new Icon('¤', ConsoleColor.White);
        public static readonly Icon Flag = new Icon('►', ConsoleColor.Red);
        public static readonly Icon Cover = new Icon('░', ConsoleColor.DarkGray);

        public static readonly Icon[] Warning =
        {
            new Icon(' ', ConsoleColor.Black),
            new Icon('1', ConsoleColor.Blue),
            new Icon('2', ConsoleColor.DarkGreen),
            new Icon('3', ConsoleColor.Yellow),
            new Icon('4', ConsoleColor.DarkBlue),
            new Icon('5', ConsoleColor.DarkGreen),
            new Icon('6', ConsoleColor.Cyan),
            new Icon('7', ConsoleColor.Gray),
            new Icon('8', ConsoleColor.Magenta)
        };

        public Icon(char symbol, ConsoleColor color)
        {
            Symbol = symbol;
            Color = color;
        }

        public char Symbol { get; }

        public ConsoleColor Color { get; }
    }
}