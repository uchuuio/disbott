using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Disbott.Views
{
    [Name("Utils")]
    public class UtilsModule : ModuleBase
    {
        [Command("ping")]
        [Remarks("Responds Pong")]
        public async Task Ping()
        {
            await ReplyAsync("pong");
        }

        [Command("about")]
        [Remarks("Tells user about Disbott")]
        public async Task About()
        {
            await
                ReplyAsync("Hello, I\'m Disbott. A bot for Discord. Find out more about me here " +
                           ConfigurationManager.AppSettings["domain"]);
        }

        [Command("info")]
        [Remarks("Tells user the current Disbott info")]
        public async Task Info()
        {
            #if DEBUG
                await
                    ReplyAsync("Disbott C# Edition, Dev Version -- https://github.com/paguco/disbott/");
            #else
                // Ideally this should get the deployed version number and release url from github
                await
                    ReplyAsync("Disbott C# Edition, Version 3.0.0 -- https://github.com/tomopagu/disbott/");
            #endif
        }

        [Command("Kill")]
        [Remarks("Kills the Disbott Instance")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Kill()
        {
            await ReplyAsync("Killing self");
            await Context.Client.ApiClient.LogoutAsync();
            Process.GetCurrentProcess().Kill();
        }
    }
}
