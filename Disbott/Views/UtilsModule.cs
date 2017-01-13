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
            var urlLocation = ConfigurationManager.AppSettings["domain"];
            await
                ReplyAsync(string.Format(Resources.response_About,urlLocation));
        }

        [Command("info")]
        [Remarks("Tells user the current Disbott info")]
        public async Task Info()
        {
            #if DEBUG
                await
                    ReplyAsync(string.Format(Resources.response_Info_Dev, Resources.url_Disbott));
            #else
                // Ideally this should get the deployed version number and release url from github
                await
                    ReplyAsync(string.Format(Resources.response_Info_Dev, Resources.url_Disbott));
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
            string answer = Utils.ShowHelp(module);

            await ReplyAsync(answer);
        }

        [Command("upupdowndownleftrightleftrightba")]
        [Remarks("")]
        public async Task BoringStuff()
        {
            await ReplyAsync("ACTIVATING DANK MODE \r Type dank to end DANK MODE");
            Constants.DANKMODEACTIVATED = true;
        }

        [Command("dank")]
        [Remarks("")]
        public async Task EndBoringStuff()
        {
            if (Constants.DANKMODEACTIVATED == false)
            {
                await ReplyAsync("But dank mode isnt active?");
            }
            else
            {
                await ReplyAsync("Ended dank mode");
            }
            Constants.DANKMODEACTIVATED = false;
        }
    }
}
