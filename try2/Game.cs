using System.Diagnostics.Contracts;
using System.Linq;

namespace try2
{
    internal static class Game
    {
        [Pure]
        public static GameState Start(MineField mineField)
        {
            return GameState.Create(Covers.Create(mineField.Size));
        }

        [Pure]
        public static GameState Undo(GameState current)
        {
            return current.Undo();
        }

        [Pure]
        public static GameState Quit(GameState current, MineField mineField)
        {
            return current.Do(
                current
                    .Covers()
                    .UncoverRange(
                        mineField.Mines()));
        }

        [Pure]
        public static GameState Uncover(GameState current, MineField mineField)
        {
            return current.Do(
                current
                    .Covers()
                    .UncoverDeep(mineField, current.CursorPosition));
        }

        [Pure]
        public static GameState SwitchFlag(GameState current)
        {
            return current.Do(
                current
                    .Covers()
                    .SwitchFlag(current.CursorPosition));
        }

        [Pure]
        public static GameState MoveCursor(GameState current, MineField mineField, Direction direction)
        {
            return current.Do(
                current
                    .CursorPosition
                    .Move(direction, mineField.Size));
        }


        [Pure]
        private static Covers UncoverDeep(
            this Covers covers,
            MineField mineField,
            Point point)
        {
            if (!covers.IsCovered(point)) return covers;
            if (!mineField.IsEmptyAt(point)) return covers.Uncover(point);
            return point
                .Neighbours(mineField.Size)
                .Aggregate(
                    covers.Uncover(point),
                    (current, neighbor) => current.UncoverDeep(mineField, neighbor));
        }
    }
}