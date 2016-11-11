using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using LiteDB;
using System.Linq;

namespace Disbott.Modules
{
    [Module]
    public class QuotesMethod
    {
        [Command("addquote"), Description("adds a new quote")]
        public async Task addquote(IUserMessage msg, string name, string newquote)
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

            await msg.Channel.SendMessageAsync($"Added quote for {name} saying, {newquote}", true);
        }

        [Command("quote"), Description("Gets a quote from a person")]
        public async Task quote(IUserMessage msg, string name)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<Quote>("quotes");

                var result = quotes.Find(x => x.Name.Equals(name));

                var userquotes = result as Quote[] ?? result.ToArray();

                int totalUserQuotes = userquotes.Length;

                if (totalUserQuotes == 0)
                {
                    await msg.Channel.SendMessageAsync("This person has no quotes");
                }
                else
                {
                    Random rand = new Random();
                    int randIndex = rand.Next(0, (totalUserQuotes - 1));

                    await msg.Channel.SendMessageAsync($"{name} said {userquotes[randIndex].Quotes}", true);
                }
            }
        }

        [Command("deletequote"), Description("Removes quote from a person")]
        public async Task deletequote(IUserMessage msg, string quote)
        {
            using (var db = new LiteDatabase(@"Quotes.db"))
            {
                var quotes = db.GetCollection<Quote>("quotes");

                var results = quotes.Delete(x => x.Quotes.Equals(quote));

                await msg.Channel.SendMessageAsync($"Deleted quote {quote}", true);
            }
        }
        
    

    }
}