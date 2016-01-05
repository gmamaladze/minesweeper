using System;

namespace try2
{
    internal static class CoverExtensions
    {
        public static Cover Opposite(this Cover cover)
        {
            switch (cover)
            {
                case Cover.CoveredUnflagged:
                    return Cover.CoveredFlagged;
                case Cover.Uncovered:
                    return Cover.Uncovered;
                case Cover.CoveredFlagged:
                    return Cover.CoveredUnflagged;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cover), cover, null);
            }
        }
    }
}