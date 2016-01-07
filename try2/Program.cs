using System;
using System.Collections.Generic;
using System.Linq;

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
                {ConsoleKey.UpArrow, (s, m) => MoveCursor(s, m, ConsoleKey.UpArrow)},
                {ConsoleKey.DownArrow, (s, m) => MoveCursor(s, m, ConsoleKey.DownArrow)},
                {ConsoleKey.LeftArrow, (s, m) => MoveCursor(s, m, ConsoleKey.LeftArrow)},
                {ConsoleKey.RightArrow, (s, m) => MoveCursor(s, m, ConsoleKey.RightArrow)},
                {ConsoleKey.Spacebar, (s, m) => SwitchFlag(s)},
                {ConsoleKey.Enter, Uncover},
                {ConsoleKey.Q, Quit},
                {ConsoleKey.U, (s, m) => Undo(s)}
            };

        private static GameState Undo(GameState gameState)
        {
            return gameState.Undo();
        }

        private static GameState Quit(GameState gameState, MineField mineField)
        {
            var covers = gameState.Covers().UncoverMines(mineField);
            return gameState.Move(covers);
        }

        private static GameState Uncover(GameState gameState, MineField mineField)
        {
            var covers = gameState.Covers().UncoverDeep(mineField, gameState.CursorPosition);
            return gameState.Move(covers);
        }

        private static GameState SwitchFlag(GameState gameState)
        {

            var covers = gameState.Covers().SwitchFlag(gameState.CursorPosition);
            return gameState.Move(covers);
        }


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

        private static GameState MoveCursor(GameState gameState, MineField mineField, ConsoleKey key)
        {
            var cursorPosition = Move(gameState.CursorPosition, key, mineField.Size);
            return gameState.Move(cursorPosition);
        }

        private static void Main(string[] args)
        {
            var options = OptionsUi.ReadOptions();
            var mineField = MineFiedlBuilder.GenerateRandom(options);

            var gameState = GameState.Create(Covers.Create(options.Size));


            var drawParams = new DrawParams(options.Size, new Point(2, 2), new Scale(4, 2));


            using (CustomConsoleSettings.Init(drawParams))
            {
                DrawTitle(new[] {"SPACE-open  ENTER-flag  Q-quit  U-undo"}, drawParams);
                GridUi.Draw(drawParams);

                var result = Game.Evaluate(options, mineField, gameState.Covers());
                do {
                    Draw(mineField, drawParams, gameState);
                    DrawCursor(gameState.CursorPosition, drawParams);
                    var key = Console.ReadKey(true).Key;

                    Func<GameState, MineField, GameState> command;
                    var found = Commands.TryGetValue(key, out command);
                    if (!found) continue;
                    gameState = command(gameState, mineField);
                    result = Game.Evaluate(options, mineField, gameState.Covers());
                } while (!result.IsGameOver());

                Draw(mineField, drawParams, gameState);
                DrawTitle(
                    new[]
                    {
                        result.HasFailed
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
            var covers = gameState.Covers();
            var points = drawParams
                .Size
                .AllPoints();

            Draw(points, mines, covers, drawParams);
        }

        private static void Draw(IEnumerable<Point> points, MineField mines, Covers covers, DrawParams drawParams)
        {
            Console.CursorVisible = false;
            points
                .Select(p => new Cell(
                    GetIcon(
                        () => covers.IsCovered(p),
                        () => covers.HasFlag(p),
                        () => mines.HasMineAt(p),
                        () => mines.WarningsAt(p)),
                    p))
                .ForAll(cell => cell.Draw(drawParams));
            Console.CursorVisible = true;
        }

        private static Icon GetIcon(
            Func<bool> isCover,
            Func<bool> isFlag,
            Func<bool> isMine,
            Func<int> getWarnings)
        {
            return isCover()
                ? isFlag()
                    ? FlagIcon
                    : CoverIcon
                : isMine()
                    ? MineIcon
                    : WarningIcons[getWarnings()];
        }
    }
}