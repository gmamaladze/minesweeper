using System;
using System.Collections.Generic;

namespace try2
{
    internal class Program
    {
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
                    var command = key.ToCommand();
                    gameState = command.Invoke(gameState, mineField);
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
    }
}