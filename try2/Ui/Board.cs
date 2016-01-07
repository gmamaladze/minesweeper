using System;

namespace try2
{
    internal static class Board
    {
        public static void Draw(Graphics graphics, MineField mines, Covers covers)
        {
            mines
                .Size
                .AllPoints()
                .ForAll(p =>
                {
                    Cell.Draw(
                        graphics,
                        covers.IsCovered(p)
                            ? covers.HasFlag(p)
                                ? Icon.Flag
                                : Icon.Cover
                            : mines.HasMineAt(p)
                                ? Icon.Mine
                                : Icon.Warning[mines.WarningsAt(p)],
                        p);
                });
        }
    }
}