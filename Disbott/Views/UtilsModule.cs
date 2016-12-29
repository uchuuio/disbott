using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Disbott.Properties;

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
                    ReplyAsync(string.Format(Resources.response_info_Dev, Resources.url_Disbott));
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
            await Context.Client.ApiClient.LogoutAsync();
            Process.GetCurrentProcess().Kill();
        }
    }
}
