using System;

namespace try2
{
    internal class Board
    {
        private static readonly Icon MineIcon = new Icon('¤', ConsoleColor.White);
        private static readonly Icon FlagIcon = new Icon('►', ConsoleColor.Red);
        private static readonly Icon CoverIcon = new Icon('░', ConsoleColor.DarkGray);

        private static readonly Icon[] WarningIcons =
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

        public static void Draw(DrawParams drawParams, MineField mines, Covers covers)
        {
            mines
                .Size
                .AllPoints()
                .ForAll(p =>
                {
                    Cell.Draw(
                        drawParams,
                        covers.IsCovered(p)
                            ? covers.HasFlag(p)
                                ? FlagIcon
                                : CoverIcon
                            : mines.HasMineAt(p)
                                ? MineIcon
                                : WarningIcons[mines.WarningsAt(p)],
                        p);
                });
        }
    }
}