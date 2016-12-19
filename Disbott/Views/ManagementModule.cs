using System;
using System.Linq;
using System.Threading.Tasks;

using LiteDB;
using Discord.Commands;

using Disbott.Controllers;
using Disbott.Models.Objects;
using Disbott.Properties;
using Discord;

namespace Disbott.Views
{
    [Name("Management")]
    public class ManagementModule : ModuleBase
    {
        [Command("Kick")]
        [Remarks("Kicks a user")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickUser(string name)
        {
            
        }
    }
}
