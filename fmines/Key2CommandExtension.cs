// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using Fmines.Geometry;

namespace Fmines
{
    internal static class Key2CommandExtension
    {
        public static readonly Command Undo = (s, m) => Game.Undo(s);
        public static readonly Command SwitchFlag = (s, m) => Game.SwitchFlag(s);
        public static readonly Command Uncover = Game.Uncover;
        public static readonly Command Quit = Game.Quit;

        public static readonly Command MoveRight = (s, m) => Game.MoveCursor(s, m, Direction.Right);
        public static readonly Command MoveLeft = (s, m) => Game.MoveCursor(s, m, Direction.Left);
        public static readonly Command MoveDown = (s, m) => Game.MoveCursor(s, m, Direction.Down);
        public static readonly Command MoveUp = (s, m) => Game.MoveCursor(s, m, Direction.Up);

        public static readonly Command NullCommand = (s, m) => s;

        private static readonly IReadOnlyDictionary<ConsoleKey, Command> Commands =
            new Dictionary<ConsoleKey, Command>
            {
                {ConsoleKey.UpArrow, MoveUp},
                {ConsoleKey.DownArrow, MoveDown},
                {ConsoleKey.LeftArrow, MoveLeft},
                {ConsoleKey.RightArrow, MoveRight},
                {ConsoleKey.Spacebar, SwitchFlag},
                {ConsoleKey.Enter, Uncover},
                {ConsoleKey.Q, Quit},
                {ConsoleKey.U, Undo}
            };


        public static Command ToCommand(this ConsoleKey key)
        {
            Command command;
            var found = Commands.TryGetValue(key, out command);
            return
                found
                    ? command
                    : NullCommand;
        }
    }
}