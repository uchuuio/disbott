using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Disbott.Properties;
using System.Collections.Generic;
using Disbott.Controllers;

namespace Disbott.Views
{
    [Name("Utils")]
    public class UtilsModule : ModuleBase
    {
        [Command("ping")]
        [Remarks("Responds Pong")]
        public async Task Ping()
        {
            await ReplyAsync(Resources.response_Pong);
        }

        [Command("about")]
        [Remarks("Tells user about Disbott")]
        public async Task About()
        {
            await
                ReplyAsync(string.Format(Resources.response_About, Resources.url_Web_Disbott));
        }

        [Command("info")]
        [Remarks("Tells user the current Disbott info")]
        public async Task Info()
        {
            #if DEBUG
                await
                    ReplyAsync(string.Format(Resources.response_Info_Dev, Resources.url_Github_Disbott));
            #else
                var currentRelease = await UtilsController.getReleaseData();
                await
                    ReplyAsync(string.Format(Resources.response_Info_Live, currentRelease[0], currentRelease[1]));
            #endif
        }

        [Command("Kill")]
        [Remarks("Kills the Disbott Instance")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Kill()
        {
            var guilds = await Context.Client.GetGuildsAsync();
            foreach (var guild in guilds)
            {
                var channel = guild.GetDefaultChannelAsync();
                await channel.Result.SendMessageAsync(Resources.response_Killed);
            }
            await Context.Client.DisconnectAsync();
            Process.GetCurrentProcess().Kill();
        }

        [Command("modules")]
        [Remarks("Gives a list of all modules")]
        public async Task Modules()
        {
            string[] Modules = { Resources.Desc_Coinflip, Resources.Desc_Giphy, Resources.Desc_Lol, Resources.Desc_Message_Count, Resources.Desc_Poll, Resources.Desc_Quotes, Resources.Desc_Remind_Me, Resources.Desc_Roll, Resources.Desc_Twitter };

            string reply = string.Join("\r", Modules);

            await ReplyAsync(reply);
        }

        [Command("help")]
        [Remarks("Get help about a module")]
        public async Task ModuleHelp(string module = null)
        {
            string answer = UtilsController.ShowHelp(module);

            await ReplyAsync(answer);
        }
    }
}
