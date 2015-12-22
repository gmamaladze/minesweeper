using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace try2
{
    internal class GameState
    {
        public Point CursorPosition { get; set; }
        public GameResult GameResult { get; set; }
        public CoverField CoverFields { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = OptionsUi.ReadOptions();
            var board = Game.Init(options);
            var drawParams = new DrawParams(options.Size, new Point(2,1), new Scale(4,2) );
            Grid.Draw(drawParams);
            var gameState = new GameState
            {
                CursorPosition = new Point(0, 0),
                CoverFields = new CoverField(options.Size),
                GameResult = new GameResult(false, options.Size.Width * options.Size.Height)
            };

            foreach (var key in Keys())
            {
                if (gameState.GameResult.GameOver()) break;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                        gameState.CursorPosition = Move(gameState.CursorPosition, key);
                        break;
                    case ConsoleKey.U:
                        break;
                    case ConsoleKey.Q:
                        gameState.GameResult = new GameResult(true, 0);
                        break;
                    case ConsoleKey.Spacebar:
                        gameState.CoverFields = Game.UncoverDeep(gameState.CursorPosition, gameState.CoverFields);
                        gameState.GameResult = Game.Evaluate(options, board, gameState.CoverFields);
                        break;
                    case ConsoleKey.Enter:
                        Game.SwitchFlag(gameState.CursorPosition, gameState.CoverFields);
                        break;
                }
                Draw(board, drawParams, gameState);
            }
        }

        public static IEnumerable<ConsoleKey> Keys()
        {
            while (true)
            {
                yield return Console.ReadKey().Key;
            }
        }

        public static Point Move(Point point, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    return point.Next(Direction.Up);
                case ConsoleKey.DownArrow:
                    return point.Next(Direction.Down);
                case ConsoleKey.LeftArrow:
                    return point.Next(Direction.Left);
                case ConsoleKey.RightArrow:
                    return point.Next(Direction.Right);
            }
            return point;
        }

        private static void Draw(MineField board, DrawParams drawParams, GameState gameState)
        {
            var cursor = gameState.CursorPosition;

            drawParams
                .Size
                .AllPoints()
                .Where(p => !p.Equals(cursor))
                .Where(p => gameState.CoverFields[p] != Cover.Free)
                .ForAll(p=> Draw(p, gameState.CoverFields[p], drawParams));

            board
                .GetSize()
                .AllPoints()
                .ForAll(p=>Draw(p, board[p], drawParams));
        }

        private static void Draw(Point p, Cover coverField, DrawParams drawParams)
        {
            var cellOffset = new Point(2, 1);
            var windowsPoint = p * drawParams.Scale + drawParams.Offset + cellOffset;
            Console.SetCursorPosition(windowsPoint.X, windowsPoint.Y);
            Console.Write(GetChar(coverField));
        }

        private static char GetChar(Cover coverField)
        {
            switch (coverField)
            {
                case Cover.Covered:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return '░';
                case Cover.Free:
                    return ' ';
                case Cover.Flagged:
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
                    return '░';
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