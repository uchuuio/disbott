﻿using System.Configuration;
using System.ComponentModel;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
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
            await
                ReplyAsync("Disbott C# Edition, Version 3.0.0-alpha.2 -- https://github.com/tomopagu/disbott/tree/c%23");
        }
    }
}
