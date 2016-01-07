namespace try2
{
    /// <summary>
    /// Shortcut for Func[GameState, MineField, GameState]
    /// </summary>
    /// <param name="gameState"></param>
    /// <param name="mineField"></param>
    /// <returns></returns>
    internal delegate GameState Command(GameState gameState, MineField mineField);
}