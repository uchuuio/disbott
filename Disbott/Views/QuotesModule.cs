using System;
using System.Threading.Tasks;

using Discord.Commands;
using LiteDB;
using System.Linq;
using Disbott.Controllers;
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
            var addQuote = QuotesController.AddQuoteMethod(name, newquote);

            if (addQuote)
            {
                await ReplyAsync($"Added quote for {name} saying, {newquote}", true);
            }
            else
            {
                await ReplyAsync("Could not add quote");
            }
        }

        [Command("quote")]
        [Remarks("Gets a quote from a person")]
        public async Task Quote(string name)
        {
            var quoteTuple = QuotesController.GetQuoteMethod(name);

            if (quoteTuple.Item2 == null)
            {
                await ReplyAsync("This person has no quotes");
            }

            await ReplyAsync($"{quoteTuple.Item1} said {quoteTuple.Item2}", true);
        }

        [Command("deletequote")]
        [Remarks("Removes quote from a person")]
        public async Task DeleteQuote([Remainder]string quote)
        {
            var result = QuotesController.DeleteQuoteMethod(quote);
            if (result)
            {
                await ReplyAsync($"Deleted quote {quote}", true);
            }
            else
            {
                await ReplyAsync("Could not delete quote");
            }
        }
    }
}