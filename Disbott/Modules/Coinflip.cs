using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott.Modules
{
    [Module]
    public class Coinflip
    {
        [Command("flip"), Description("Flips a coin!")]
        public async Task flip(IUserMessage msg)
        {
            Random rnd = new Random();
            int flipped = rnd.Next(1, 3);
            if (flipped == 1)
            {
                await msg.Channel.SendMessageAsync("Heads");
            }
            else
            {
                await msg.Channel.SendMessageAsync("Tails");
            }
        }
    }
}
