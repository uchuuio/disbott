using System;

namespace Disbott.Controllers
{
    public static class CoinFlipController
    {
        public static string Flip()
        {
            Random rnd = new Random();
            int flipped = rnd.Next(1, 3);
            if (flipped == 1)
            {
                return "Heads";
            }
            return "Tails";
        }
    }
}
