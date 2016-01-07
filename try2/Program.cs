using System;
using System.Collections.Generic;

namespace try2
{
    internal class Program
    {
        private static readonly IReadOnlyDictionary<ConsoleKey, Direction> Key2Direction =
            new Dictionary<ConsoleKey, Direction>
            {
                {ConsoleKey.UpArrow, Direction.Up},
                {ConsoleKey.RightArrow, Direction.Right},
                {ConsoleKey.DownArrow, Direction.Down},
                {ConsoleKey.LeftArrow, Direction.Left}
            };

        private static readonly IReadOnlyDictionary<ConsoleKey, Func<GameState, MineField, GameState>> Commands =
            new Dictionary<ConsoleKey, Func<GameState, MineField, GameState>>
            {
                {ConsoleKey.UpArrow, (s, m) => Game.MoveCursor(s, m, Key2Direction[ConsoleKey.UpArrow])},
                {ConsoleKey.DownArrow, (s, m) => Game.MoveCursor(s, m, Key2Direction[ConsoleKey.DownArrow])},
                {ConsoleKey.LeftArrow, (s, m) => Game.MoveCursor(s, m, Key2Direction[ConsoleKey.LeftArrow])},
                {ConsoleKey.RightArrow, (s, m) => Game.MoveCursor(s, m, Key2Direction[ConsoleKey.RightArrow])},
                {ConsoleKey.Spacebar, (s, m) => Game.SwitchFlag(s)},
                {ConsoleKey.Enter, Game.Uncover},
                {ConsoleKey.Q, Game.Quit},
                {ConsoleKey.U, (s, m) => Game.Undo(s)}
            };


        private static void Main(string[] args)
        {
            var options = OptionSelector.ReadOptions();
            var mineField = MineFiedlBuilder.GenerateRandom(options.MinesCount, options.Size);
            var gameState = Game.Start(mineField);


            using (var graphics = Graphics.Init(options.Size))
            {
                Title.Draw(graphics, "SPACE-falg  ENTER-open  Q-quit  U-undo");
                Grid.Draw(graphics);

                GameResult result;
                do
                {
                    Board.Draw(graphics, mineField, gameState.Covers());
                    Cursor.Draw(graphics, gameState.CursorPosition);
                    var key = Console.ReadKey(true).Key;
                    gameState = Execute(key, gameState, mineField);
                    result = gameState.Evaluate(mineField);
                } while (!result.IsGameOver());

                Board.Draw(graphics, mineField, gameState.Covers());
                Title.Draw(
                    graphics,
                    result.HasFailed
                        ? "Sorry, you lost this game. Better luck next time!"
                        : "Congratulations, you won the game!",
                    "Press any key to EXIT ..."
                    );
                Console.ReadKey(true);
            }
        }

        private static GameState Execute(ConsoleKey key, GameState gameState, MineField mineField)
        {
            Func<GameState, MineField, GameState> command;
            var found = Commands.TryGetValue(key, out command);
            if (found) gameState = command(gameState, mineField);
            return gameState;
        }
    }
}