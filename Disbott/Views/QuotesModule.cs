using System;
using System.Threading.Tasks;

using Discord.Commands;
using LiteDB;
using System.Linq;
using Disbott.Controllers;
using Disbott.Models.Objects;
using Disbott.Properties;

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
                await ReplyAsync(string.Format(Resources.response_Added_Quote,name,newquote), true);
            }
            else
            {
                await ReplyAsync(Resources.error_Cant_Add_Quote);
            }
        }

        [Command("quote")]
        [Remarks("Gets a quote from a person")]
        public async Task Quote(string name)
        {
            var quoteTuple = QuotesController.GetQuoteMethod(name);

            if (quoteTuple.Item2 == null)
            {
                await ReplyAsync(string.Format(Resources.error_User_No_Quotes, name));
            }
            await ReplyAsync(string.Format(Resources.response_Random_Quote, quoteTuple.Item1, quoteTuple.Item2), true);
        }

        [Command("deletequote")]
        [Remarks("Removes quote from a person")]
        public async Task DeleteQuote([Remainder]string quote)
        {
            var result = QuotesController.DeleteQuoteMethod(quote);
            if (result)
            {
                await ReplyAsync(string.Format(Resources.response_Deleted_Quote, quote),true);
            }
            else
            {
                await ReplyAsync(Resources.error_Could_Not_Delete);
            }
        }
    }
}