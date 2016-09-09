using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott.Modules
{
    [Module]
    public class Utils
    {
        [Command("ping"), Description("Responds Pong")]
        public async Task Ping(IUserMessage msg)
        {
            await msg.Channel.SendMessageAsync("pong");
        }

        [Command("about"), Description("Tells user about Disbott")]
        public async Task About(IUserMessage msg)
        {
            await msg.Channel.SendMessageAsync("Hello, I\'m Disbott. A bot for Discord. Find out more about me here https://disbott.pagu.co");
        }

        [Command("info"), Description("Tells user the current Disbott info")]
        public async Task Info(IUserMessage msg)
        {
            await msg.Channel.SendMessageAsync("Disbott C# Edition, Version 2.0.0-alpha.1 -- https://github.com/tomopagu/disbott/tree/c%23");
        }
    }
}
