using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disbott.Models
{
    public static class Enums
    {
        public class BlackJack
        {
            public enum cardValue
            {
                Ace,
                Two,
                Three,
                Four,
                Five,
                Six,
                Seven,
                Eight,
                Nine,
                Ten,
                Jack,
                Queen,
                King
            }
            public enum cardSuit
            {
                Spades,
                Hearts,
                Diamonds,
                Clubs
            }
        }
    }
}
