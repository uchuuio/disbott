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
    public class Roll
    {
        [Command("roll"), Description("Rolls a Dice!")]
        public async Task roll(IUserMessage msg)
        {
            Random rnd = new Random();
            int rolling = rnd.Next(1, 7);
            string result = rolling.ToString();
            await msg.Channel.SendMessageAsync(result);
        }
    }
}
