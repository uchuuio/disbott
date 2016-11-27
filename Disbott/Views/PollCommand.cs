using System.ComponentModel;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Disbott.Controllers;

namespace Disbott.Views
{
    [Module]
    public class PollCommand
    {
        Poll pollOption = new Poll();

        [Command("newpoll"), Description("Starts A Poll")]
        public async Task newpoll(IUserMessage msg, string pollName)
        {
            string createdPoll = pollOption.CreateNewPoll(msg, pollName);

            await msg.Channel.SendMessageAsync(createdPoll);
        }

        [Command("vote"), Description("Vote on a poll")]
        public async Task vote(IUserMessage msg, string vote)
        {
            if (vote == "yes" || vote == "no")
            {
                string createdPoll = pollOption.VoteOnPoll(vote);
                if (string.IsNullOrEmpty(createdPoll) == false)
                {
                    await msg.Channel.SendMessageAsync(createdPoll);
                }
            }
            else
            {
                await msg.Channel.SendMessageAsync("Please type 'yes' or 'no'");
            }
        }

        [Command("endpoll"), Description("End the current poll")]
        public async Task endpoll(IUserMessage msg)
        {
            string createdPoll = pollOption.EndPoll(msg);

            await msg.Channel.SendMessageAsync(createdPoll);

        }

        [Command("officerendpoll"), Description("Ends the poll as admin")]
        [RequirePermission(GuildPermission.BanMembers)]
        public async Task officerendpoll(IUserMessage msg)
        {
            string createdPoll = pollOption.OfficerEndPoll();

            await msg.Channel.SendMessageAsync(createdPoll);

        }
    }
}
