using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disbott.Models.Objects.BlackJack;
using Disbott.Models;

namespace Disbott.Controllers
{
    public static class BlackJack
    {
        public static List<Card> Deck;

        public static void CreateDeck()
        {
            Deck = new List<Card>();
            var suitCount = Enum.GetNames(typeof(Enums.BlackJack.cardSuit)).Length;
            var valCount = Enum.GetNames(typeof(Enums.BlackJack.cardValue)).Length;

            for (int i = 0; i < suitCount; i++)  
            {
                for (int o = 0; o < valCount; o++)      
                {
                    var card = new Card()                   
                    {
                        Suit = (Enums.BlackJack.cardSuit)i,             
                        Value = (Enums.BlackJack.cardValue)o
                    };

                    Deck.Add(card);
                }
            }
        }
    }
}
