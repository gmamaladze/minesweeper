using System;

namespace minesweeper
{
	public class Game
	{
		int columns = 4;
		int rows = 4;
		int topmargin = 4;
		int bottommargin = 2;
		int numberOfMines = 4;
		bool mineHit = false;
		bool[,] flipped;
		bool[,] flagged;
		bool[,] mines;
		int [,] warnings;

		int userCurrentRow;
		int userCurrentColumn;
		int totalLeft;
		int flagsLeft;

		public Game ()
		{
			flipped = new bool[rows, columns];
			flagged = new bool[rows, columns];
			mines = new bool[rows, columns];
			warnings = new int[rows, columns];
			totalLeft = rows * columns;
			flagsLeft = numberOfMines;

			var newMines = 0;
			var random = new Random();
			while (newMines < numberOfMines) 
			{
				int randomColumn = random.Next(0, columns);
				int randomRow = random.Next(0, rows);
				if(mines[randomRow, randomColumn] == true)
					continue;
				mines[randomRow, randomColumn] = true;
				Spreadwarnings(randomRow, randomColumn);
				newMines++;
			}
		}

		public void Run ()
		{
			try {
				while (!Finished) {
					userCurrentRow = -1;
					userCurrentColumn = -1;
					Draw ();
					userCurrentRow = ReadNumericKey("Row to change:");
					Draw ();
					userCurrentColumn = ReadNumericKey("Column to change:");
					Draw ();
					Console.WriteLine();
					var key = ReadKey("f for flag everything else for flip:");
					if(key == "f")	
						Flag();
					else
						Flip();
					Draw();
					if(flagsLeft == 0)
						UserMessage("No flags left");

				}
			} catch (ApplicationException) {
				UserMessage("User quit game!");
				return;

			}
			if(totalLeft == 0)
				UserMessage("You made it, now the children can play here!!");
			else
				UserMessage("Oooh nooo!");
		}

		void Spreadwarnings (int row, int col)
		{
			int colOffset = col - 1;
			if(colOffset < 0)
				colOffset = 0;

			int rowOffset = row - 1;
			if(rowOffset < 0)
				rowOffset = 0;

			int rowMax = row + 1;
			if(rowMax >= rows)
				rowMax = rows - 1;

			int colMax = col + 1;
			if(colMax >= columns)
				colMax = columns - 1;
			var startCol = colOffset;
			for(; rowOffset <= rowMax; rowOffset++)
			{
				colOffset = startCol;
				for(;colOffset <= colMax; colOffset++)
					warnings[rowOffset, colOffset]++;
			}
		}

		void Flag ()
		{
			// Can't flag if already flipped
			if(flipped[userCurrentRow, userCurrentColumn])
				return;
			if(flagsLeft == 0)
				return;
			flagged[userCurrentRow, userCurrentColumn] = !flagged[userCurrentRow, userCurrentColumn];
			totalLeft--;
			flagsLeft--;
		}

		bool Flip ()
		{
			// If already flagged, unflag
			if (flagged [userCurrentRow, userCurrentColumn]) {
				flagged [userCurrentRow, userCurrentColumn] = false;
				totalLeft++;
				flagsLeft++;
			}
			// If we hit a mine game is over
			if (mines [userCurrentRow, userCurrentColumn]) {
				mineHit = true;
			}

			SpreadFlip(userCurrentRow, userCurrentColumn);

			return !mineHit;
		}

		void SpreadFlip (int row, int col)
		{
			if(col < 0)
				return;
			if(col >= columns)
				return;

			if(row < 0)
				return;
			if(row >= rows)
				return;

			if(IsFlipped(row, col))
				return;
			// the actual flip
			flipped[row, col] = true;
			totalLeft--;

			if(warnings[row, col] != 0)
				return;
			SpreadFlip(row - 1, col - 1);
			SpreadFlip(row - 1, col);
			SpreadFlip(row - 1, col + 1);

			SpreadFlip(row, col - 1);
			SpreadFlip(row, col + 1);

			SpreadFlip(row + 1, col - 1);
			SpreadFlip(row + 1, col);
			SpreadFlip(row + 1, col + 1);
		}

		public bool Finished { get{ return mineHit || totalLeft == 0; }	}

		bool IsFlipped (int row, int column)
		{
			if(Finished)
				return true;
			return flipped[row, column];
		}

		void Draw()
		{
			PrintHeader();
			Console.SetCursorPosition(topmargin, topmargin);
			Console.Write("   ");
			for(var column = 0; column < columns; column++)
				Console.Write(column.ToString());
			for(var row = 0; row < rows; row++)
			{
				Console.CursorTop++;
				DrawColumns(row);
			}
			UserMessage("");
			Console.WriteLine("".PadRight(10));
		}

		void DrawColumns(int row)
		{
			var orgColor = Console.BackgroundColor;
  			Console.CursorLeft = topmargin;
			Console.Write(String.Format("{0}: ", row));
			for(var column = 0;column < columns; column++)
			{
				if(flagged[row, column] == true) /* == true not necessary*/
					Console.Write('F');
				else if(IsFlipped(row, column))
				{
					if(mines[row, column])
					{
						if(row == userCurrentRow && column == userCurrentColumn)
						{
							Console.BackgroundColor = ConsoleColor.DarkRed;
							Console.Write('*');
							Console.BackgroundColor = orgColor;
						}
						else
							Console.Write('*');
					}
					else
					{
						if(warnings[row, column] != 0)
							Console.Write(warnings[row, column]);
						else
							Console.Write('-');
					}
				}
				else
					Console.Write('.');
			}
		}

		void PrintHeader ()
		{
			Console.SetCursorPosition(0,0);
			Console.WriteLine("Press the Escape (Esc) key to quit".PadRight(120));
			if(userCurrentRow != -1)
				Console.WriteLine(String.Format("Row:    {0}", userCurrentRow));
			else
				Console.WriteLine(              "            ");
			if(userCurrentColumn != -1)
				Console.WriteLine(String.Format("Column: {0}", userCurrentColumn));
			else
				Console.WriteLine(              "            ");
		}

		int ReadNumericKey (string leadTeaxt)
		{
			int returnValue;
			while (true) {
				var input = ReadKey(leadTeaxt);
				Console.BackgroundColor = ConsoleColor.Black;
				if(Int32.TryParse(input, out returnValue))
					break;
				Console.BackgroundColor = ConsoleColor.DarkRed;
			}
			return returnValue;
		}

		void UserMessage(string message)
		{
			Console.SetCursorPosition (0, topmargin + rows + bottommargin);
			Console.WriteLine(message.PadRight(100));
		}

		string ReadKey (string leadText)
		{
			UserMessage(leadText);
			var key = Console.ReadKey ();
			if (key.Key == ConsoleKey.Escape)
				throw new ApplicationException("Quit");		
			return key.KeyChar.ToString();
		}

	    public static GameState Undo(GameState gameState)
	    {
	        return gameState.Undo();
	    }

	    public static GameState Quit(GameState gameState, MineField mineField)
	    {
	        var covers = gameState.Covers().UncoverMines(mineField);
	        return gameState.Do(covers);
	    }

	    public static GameState Uncover(GameState gameState, MineField mineField)
	    {
	        var covers = gameState.Covers().UncoverDeep(mineField, gameState.CursorPosition);
	        return gameState.Do(covers);
	    }

	    public static GameState SwitchFlag(GameState gameState)
	    {
	        var covers = gameState.Covers().SwitchFlag(gameState.CursorPosition);
	        return gameState.Do(covers);
	    }

	    public static GameState MoveCursor(GameState gameState, MineField mineField, Direction direction)
	    {
	        return gameState.Do(gameState.CursorPosition.Move(direction, mineField.Size));
	    }

	    public static GameState Start(MineField mineField)
	    {
	        return GameState.Create(Covers.Create(mineField.Size));
	    }
	}
}

