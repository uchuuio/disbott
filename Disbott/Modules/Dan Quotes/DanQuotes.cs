using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using LiteDB;

namespace Disbott.Modules.Dan_Quotes
{
    [Module]
    public class DanQuotes
    {
        [Command("dan"), Description("Gets a Dan Quote!")]
        public async Task dan(IUserMessage msg)
        {
            using (var db = new LiteDatabase(@"DanQuotes.db"))
            {

            }
        }
    }
}
