using System;
using System.Threading.Tasks;

using Discord.Commands;
using LiteDB;
using System.Linq;
using Disbott.Models.Objects;

namespace Disbott.Views
{
    [Name("Quotes")]
    public class QuotesModule : ModuleBase
    {
        [Command("addquote")]
        [Remarks("adds a new quote")]
        public async Task AddQuote(string name, [Remainder]string newquote)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<Quote>("quotes");

                var newQuote = new Quote
                {
                    Name = name,
                    Quotes = newquote
                };

                quotes.Insert(newQuote);
            }

            await ReplyAsync($"Added quote for {name} saying, {newquote}", true);
        }

        [Command("quote")]
        [Remarks("Gets a quote from a person")]
        public async Task Quote(string name)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<Quote>("quotes");

                var result = quotes.Find(x => x.Name.Equals(name));

                var userquotes = result as Quote[] ?? result.ToArray();

                int totalUserQuotes = userquotes.Length;

                if (totalUserQuotes == 0)
                {
                    await ReplyAsync("This person has no quotes");
                }
                else
                {
                    Random rand = new Random();
                    int randIndex = rand.Next(1, (totalUserQuotes + 1));
                    int convertedRandIndex = randIndex - 1;

                    await ReplyAsync($"{name} said {userquotes[convertedRandIndex].Quotes}", true);
                }
            }
        }

        [Command("deletequote")]
        [Remarks("Removes quote from a person")]
        public async Task DeleteQuote([Remainder]string quote)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<Quote>("quotes");

                quotes.Delete(x => x.Quotes.Equals(quote));

                await ReplyAsync($"Deleted quote {quote}", true);
            }
        }
    }
}