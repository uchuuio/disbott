using System;
using System.Threading.Tasks;

using Discord.Commands;
using LiteDB;
using System.Linq;
using Disbott.Models.Objects;
using Disbott;

namespace Disbott.Controllers
{
    public class QuotesController
    {
        /// <summary>
        /// Adds a new quote to the db
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newquote"></param>
        /// <returns></returns>
        public static bool AddQuoteMethod(string name, string newquote)
        {
            using (var db = new LiteDatabase(Constants.quotePath))
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

        /// <summary>
        /// Gets a random quote from the db
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Tuple<string, string> GetQuoteMethod(string name)
        {
            using (var db = new LiteDatabase(Constants.quotePath))
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

        /// <summary>
        /// Deletes a quote from the db
        /// </summary>
        /// <param name="quote"></param>
        /// <returns></returns>
        public static bool DeleteQuoteMethod(string quote)
        {
            using (var db = new LiteDatabase(Constants.quotePath))
            {
                var quotes = db.GetCollection<QuoteSchema>("quotes");

                quotes.Delete(x => x.Quote.Equals(quote));

                return true;
            }
        }
    }
}