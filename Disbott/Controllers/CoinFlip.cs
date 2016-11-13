using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott.Controllers
{
    public static class CoinFlip
    {
        public static string Flip()
        {
            Random rnd = new Random();
            int flipped = rnd.Next(1, 3);
            if (flipped == 1)
            {
                return "Heads";
            }
            else
            {
                return "Tails";
            }
        }
    }
}
