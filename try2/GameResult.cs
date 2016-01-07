// // This code is distributed under MIT license. 
// // Copyright (c) 2015-2016 George Mamaladze
// // See license.txt or http://opensource.org/licenses/mit-license.php

namespace Fmines
{
    internal class GameResult
    {
        public GameResult(bool hasFailed, int coveredCount)
        {
            HasFailed = hasFailed;
            CoveredCount = coveredCount;
        }

        public bool HasFailed { get; }
        public int CoveredCount { get; }

        public bool IsGameOver()
        {
            return HasFailed || CoveredCount == 0;
        }
    }
}