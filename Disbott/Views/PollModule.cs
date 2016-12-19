using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Disbott.Controllers;
using Disbott.Properties;

namespace Disbott.Views
{
    [Name("Poll")]
    public class PollModule : ModuleBase
    {
        PollController pollOption = new PollController();

        [Command("newpoll")]
        [Remarks("Starts A Poll")]
        public async Task NewPoll([Remainder]string pollName)
        {
            var msg = Context.Message;
            string createdPoll = pollOption.CreateNewPoll(msg, pollName);

            await ReplyAsync(createdPoll);
        }

        [Command("vote")]
        [Remarks("Vote on a poll")]
        public async Task Vote(string vote)
        {
            if (vote == "yes" || vote == "no")
            {
                string createdPoll = pollOption.VoteOnPoll(vote);
                if (string.IsNullOrEmpty(createdPoll) == false)
                {
                    await ReplyAsync(createdPoll);
                }
            }
            else
            {
                await ReplyAsync(Resources.error_Incorrect_Format_Poll);
            }
        }

        [Command("endpoll")]
        [Remarks("End the current poll")]
        public async Task EndPoll()
        {
            var msg = Context.Message;
            string createdPoll = pollOption.EndPoll(msg);
            await ReplyAsync(createdPoll);
        }

        [Command("officerendpoll")]
        [Remarks("Ends the poll as admin")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task OfficerEndPoll()
        {
            string createdPoll = pollOption.OfficerEndPoll();
            await ReplyAsync(createdPoll);
        }
    }
}
