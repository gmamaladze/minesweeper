namespace try2
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