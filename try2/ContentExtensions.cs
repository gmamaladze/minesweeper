using System;
using System.Diagnostics.Contracts;

namespace try2
{
    internal static class ContentExtensions
    {
        [Pure]
        public static Content Inc(this Content content)
        {
            switch (content)
            {
                case Content.Empty:
                    return Content.One;
                case Content.One:
                    return Content.Two;
                case Content.Two:
                    return Content.Three;
                case Content.Three:
                    return Content.Four;
                case Content.Four:
                    return Content.Five;
                case Content.Five:
                    return Content.Six;
                case Content.Six:
                    return Content.Seven;
                case Content.Seven:
                    return Content.Eight;
                case Content.Eight:
                    throw new InvalidOperationException();
                case Content.Boom:
                    return Content.Boom;
                default:
                    throw new ArgumentOutOfRangeException(nameof(content), content, null);
            }
        }
    }
}