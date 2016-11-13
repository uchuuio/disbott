using System;
using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;
using Disbott.Controllers;

using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace Disbott.Views
{
    [Module]
    public class CoinflipCommand
    {
        [Command("flip"), Description("Flips a coin!")]
        public async Task flip(IUserMessage msg)
        {
            string result = CoinFlip.Flip();
            await msg.Channel.SendMessageAsync(result);
        }
    }
}
