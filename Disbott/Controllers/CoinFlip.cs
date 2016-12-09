using System;

namespace Disbott.Controllers
{
    public static class CoinFlipController
    {
        public static int Flip()
        {
            Random rnd = new Random();
            int flipped = rnd.Next(1, 3);
            return flipped;
        }

        public static string GetString(int flipped)
        {
            if (flipped == 1)
            {
                return "Heads";
            }
            return "Tails";
        }
    }
}
