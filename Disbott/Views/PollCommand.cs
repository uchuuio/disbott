using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Disbott.Models.Objects;
using LiteDB;

namespace Disbott.Views
{
    [Module]
    public class PollCommand
    {
        Polls newPoll = new Polls()
        {
            PollName = "",
            Yes = 0,
            No = 0

        };

        [Command("newpoll"), Description("Starts A Poll")]
        public async Task newpoll(IUserMessage msg, string pollName)
        {
            newPoll.PollName = pollName;
            Timer timer = new Timer();
            
        }

        [Command("vote"), Description("Vote on a poll")]
        public async Task vote(IUserMessage msg, string vote)
        {
            
        }
    }
}
