// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

namespace Fmines
{
    /// <summary>
    ///     Shortcut for Func[GameState, MineField, GameState]
    /// </summary>
    /// <param name="gameState"></param>
    /// <param name="mineField"></param>
    /// <returns></returns>
    public delegate GameState Command(GameState gameState, MineField mineField);
}