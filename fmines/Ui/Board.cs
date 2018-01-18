// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using Fmines.DotNet;
using Fmines.Geometry;

namespace Fmines.Ui
{
    public static class Board
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