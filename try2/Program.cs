using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace try2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = OptionsUi.ReadOptions();
            var mines = MineField.Populate(MineField.CreateRandomMines(options), options.Size);

            var gameState = new GameState
            {
                CursorPosition = new Point(0, 0),
                GameResult = new GameResult(false, options.Size.Width*options.Size.Height)
            };
            gameState.Moves.Push(ImmutableDictionary<Point, Cover>.Empty);


            var drawParams = new DrawParams(options.Size, new Point(2, 2), new Scale(4, 2));

            using (CustomConsoleSettings.Init(drawParams))
            {
                Console.Write("  SPACE-open  ENTER-flag  Q-quit  U-undo ");
                Grid.Draw(drawParams);

                while (true)
                {
                    Draw(mines, drawParams, gameState);
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
                            break;
                        case ConsoleKey.Q:
                            gameState.GameResult = new GameResult(true, 0);
                            break;
                        case ConsoleKey.Spacebar:
                            gameState.Moves.Push(gameState.Moves.Peek()
                                .UncoverDeep(mines, gameState.CursorPosition, options.Size));
                            //qgameState.GameResult = Game.Evaluate(options, mines, gameState.Moves.Peek());
                            break;
                        case ConsoleKey.Enter:
                            gameState.Moves.Push(gameState.Moves.Peek().SwitchFlag(gameState.CursorPosition));
                            break;
                    }
                    if (gameState.GameResult.GameOver()) break;
                }
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


        public static IReadOnlyDictionary<ConsoleKey, Direction> Key2Direction = 
            new Dictionary<ConsoleKey, Direction> { 
            { ConsoleKey.UpArrow, Direction.Up},
            { ConsoleKey.RightArrow, Direction.Right},
                { ConsoleKey.DownArrow, Direction.Down},
                { ConsoleKey.LeftArrow, Direction.Left}
        };

        private static void Draw(IReadOnlyDictionary<Point, Content> mines, DrawParams drawParams, GameState gameState)
        {
            Draw(mines, drawParams, gameState, drawParams.Size.AllPoints());
        }

        private static void Draw(IReadOnlyDictionary<Point, Content> mines, DrawParams drawParams, GameState gameState,
            IEnumerable<Point> points)
        {
            Console.CursorVisible = false;
            var cursor = gameState.CursorPosition;
            var covers = gameState.Moves.Peek();

            foreach (var point in points)
            {
                var cover = covers.GetAt(point);
                if (cover == Cover.Uncovered)
                    Draw(point, mines.GetAt(point), drawParams);
                else
                    Draw(point, cover, drawParams);
            }
            Console.CursorVisible = true;
        }

        private static void Draw(Point p, Cover coverField, DrawParams drawParams)
        {
            var cellOffset = new Point(2, 1);
            var windowsPoint = p*drawParams.Scale + drawParams.Offset + cellOffset;
            Console.SetCursorPosition(windowsPoint.X, windowsPoint.Y);
            Console.Write(GetChar(coverField));
        }

        private static char GetChar(Cover coverField)
        {
            switch (coverField)
            {
                case Cover.CoveredUnflagged:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return '░';
                case Cover.Uncovered:
                    return ' ';
                case Cover.CoveredFlagged:
                    Console.ForegroundColor = ConsoleColor.Red;
                    return '►';
                default:
                    throw new ArgumentOutOfRangeException(nameof(coverField), coverField, null);
            }
        }

        private static void Draw(Point p, Content c, DrawParams drawParams)
        {
            var cellOffset = new Point(2, 1);
            var windowsPoint = p*drawParams.Scale + drawParams.Offset + cellOffset;
            Console.SetCursorPosition(windowsPoint.X, windowsPoint.Y);
            Console.Write(GetChar(c));
        }

        private static char GetChar(Content content)
        {
            switch (content)
            {
                case Content.Empty:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return ' ';
                case Content.One:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    return '1';
                case Content.Two:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    return '2';
                case Content.Three:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    return '3';
                case Content.Four:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    return '4';
                case Content.Five:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    return '5';
                case Content.Six:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    return '6';
                case Content.Seven:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return '7';
                case Content.Eight:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    return '8';
                case Content.Boom:
                    Console.ForegroundColor = ConsoleColor.White;
                    return '¤';
                default:
                    throw new ArgumentOutOfRangeException(nameof(content), content, null);
            }
        }
    }
}