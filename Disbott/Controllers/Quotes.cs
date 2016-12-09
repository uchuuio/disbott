using System;
using System.Threading.Tasks;

using Discord.Commands;
using LiteDB;
using System.Linq;
using Disbott.Models.Objects;

namespace Disbott.Controllers
{
    public class QuotesController
    {
        public static bool AddQuoteMethod(string name, string newquote)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<QuoteSchema>("quotes");

                var newQuote = new QuoteSchema
                {
                    Name = name,
                    Quote = newquote
                };

                quotes.Insert(newQuote);
            }

            return true;
        }

        public static Tuple<string, string> GetQuoteMethod(string name)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<QuoteSchema>("quotes");

                var result = quotes.Find(x => x.Name.Equals(name));

                var userquotes = result as QuoteSchema[] ?? result.ToArray();

                int totalUserQuotes = userquotes.Length;

                if (totalUserQuotes == 0)
                {
                    return new Tuple<string, string>(name, null);
                }

                Random rand = new Random();
                int randIndex = rand.Next(1, (totalUserQuotes + 1));
                int convertedRandIndex = randIndex - 1;
                var quote = userquotes[convertedRandIndex].Quote;

                return new Tuple<string, string>(name, quote);
            }
        }

        public static bool DeleteQuoteMethod(string quote)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<QuoteSchema>("quotes");

                quotes.Delete(x => x.Quote.Equals(quote));

                return true;
            }
        }
    }
}