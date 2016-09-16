using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott.Modules
{
    [Module]
    public class Dice
    {
        [Command("roll"), Description("Rolls a dice!")]
        public async Task Ping(IUserMessage msg)
        {
            Random rnd = new Random();
            int roll = rnd.Next(1,7);
            await msg.Channel.SendMessageAsync(roll);
        }
    }
}
