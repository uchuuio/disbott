using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

using Discord;
using Discord.Commands;
using Disbott.Properties;
using Disbott.Controllers;

using Disbott.Models.Objects;

namespace Disbott.Views
{
    [Name("Message Count")]
    public class MessageCountModule : ModuleBase
    {
        [Command("message-count")]
        [Remarks("Displays Current Message Count for Current or Another Discord User")]
        public async Task Messagecount()
        {
            var msg = Context.Message;

            string result = MessageCount.GetMessages(msg);

            await ReplyAsync(result);
        }
    }
}
