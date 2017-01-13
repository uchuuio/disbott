using System;

namespace Disbott.Controllers
{
    public static class CoinFlipController
    {
        /// <summary>
        /// Flips the coin
        /// </summary>
        /// <returns></returns>
        public static int Flip()
        {
            Random rnd = new Random();
            int flipped = rnd.Next(1, 3);
            return flipped;
        }

        /// <summary>
        /// Determines whether it is heads or tails
        /// </summary>
        /// <param name="flipped"></param>
        /// <returns></returns>
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
