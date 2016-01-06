using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace try2
{
    internal class Program
    {
        public static IReadOnlyDictionary<ConsoleKey, Direction> Key2Direction =
            new Dictionary<ConsoleKey, Direction>
            {
                {ConsoleKey.UpArrow, Direction.Up},
                {ConsoleKey.RightArrow, Direction.Right},
                {ConsoleKey.DownArrow, Direction.Down},
                {ConsoleKey.LeftArrow, Direction.Left}
            };

        private static void Main(string[] args)
        {
            var options = OptionsUi.ReadOptions();
            var mineField = MineFiedlBuilder.GenerateRandom(options);

            var gameState = new GameState
            {
                CursorPosition = new Point(0, 0),
                GameResult = new GameResult(false, options.Size.Width*options.Size.Height)
            };
            gameState.Moves.Push(new Covers(options.Size));


            var drawParams = new DrawParams(options.Size, new Point(2, 2), new Scale(4, 2));

            using (CustomConsoleSettings.Init(drawParams))
            {
                DrawTitle(new[] {"SPACE-open  ENTER-flag  Q-quit  U-undo"}, drawParams);
                GridUi.Draw(drawParams);

                while (!gameState.GameResult.IsGameOver())
                {
                    Draw(mineField, drawParams, gameState);
                    DrawCursor(gameState.CursorPosition, drawParams);
                    var key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            gameState.CursorPosition = Move(gameState.CursorPosition, key, drawParams.Size);
                            break;
                        case ConsoleKey.U:
                            if (gameState.Moves.Count > 1) gameState.Moves.Pop();
                            break;
                        case ConsoleKey.Q:
                            gameState.GameResult = new GameResult(true, 0);
                            break;
                        case ConsoleKey.Spacebar:
                            gameState.Moves.Push(gameState.Moves.Peek()
                                .UncoverDeep(mineField, gameState.CursorPosition, options.Size));
                            gameState.GameResult = Game.Evaluate(options, mineField, gameState.Moves.Peek());
                            break;
                        case ConsoleKey.Enter:
                            gameState.Moves.Push(gameState.Moves.Peek().SwitchFlag(gameState.CursorPosition));
                            break;
                    }
                }
                gameState.Moves.Push(gameState.Moves.Peek()
                    .UncoverMines(mineField));
                Draw(mineField, drawParams, gameState);


                DrawTitle(
                    new[]
                    {
                        gameState.GameResult.HasFailed
                            ? "Sorry, you lost this game. Better luck next time!"
                            : "Congratulations, you won the game!",
                        "Press any key to EXIT ..."
                    },
                    drawParams);
                Console.ReadKey(true);
            }
        }

        private static void DrawTitle(IEnumerable<string> lines, DrawParams drawParams)
        {
            var position = new Point(drawParams.Offset.X, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            var maxWidth = drawParams.Transform(drawParams.Size).Width;

            foreach (var text in lines)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(
                    text
                        .PadRight(maxWidth)
                        .Substring(0, maxWidth));
                position = position.Next(Direction.Down);
            }
        }

        private static void DrawCursor(Point position, DrawParams drawParams)
        {
            Console.CursorVisible = false;
            var screenPoint = position*drawParams.Scale + drawParams.Offset + new Point(2, 1);
            Console.CursorLeft = screenPoint.X;
            Console.CursorTop = screenPoint.Y;
            Console.CursorVisible = true;
        }

        public static Point Move(Point point, ConsoleKey key, Size size)
        {
            var next = point.Next(Key2Direction[key]);
            return next.IsInRange(size)
                ? next
                : point;
        }

        private static void Draw(MineField mines, DrawParams drawParams, GameState gameState)
        {
            drawParams
                .Size
                .AllPoints()
                .Select(p=>GetCellAt(p, mines, drawParams, gameState))
                .ForAll(cell => cell.Draw(drawParams));
        }

        private static Cell GetCellAt(Point point, MineField mines, DrawParams drawParams, GameState gameState)
        {
            var covers = gameState.Moves.Peek();

            return !covers.IsCovered(point)
                ? (mines.HasMineAt(point)
                    ? new Cell(MineIcon, point)
                    : new Cell(
                        WarningIcons[mines.WarningsAt(point)],
                        point))
                : (covers.HasFlag(point)
                    ? new Cell(FlagIcon, point)
                    : new Cell(CoverIcon, point));
        }


        private static readonly Icon MineIcon = new Icon('¤', ConsoleColor.White);
        private static readonly Icon EmptyIcon = new Icon(' ', ConsoleColor.Black);
        private static readonly Icon FlagIcon = new Icon('►', ConsoleColor.Red);
        private static readonly Icon CoverIcon = new Icon('░', ConsoleColor.DarkGray);

        private static readonly Icon[] WarningIcons = new[]
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
    }
}