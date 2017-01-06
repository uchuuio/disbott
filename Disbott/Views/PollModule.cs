using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Disbott.Controllers;
using Disbott.Models.Objects;
using Disbott.Properties;
using System;
using System.Threading;
using System.IO;

namespace Disbott.Views
{
    [Name("Poll")]
    public class PollModule : ModuleBase
    {
        private System.Threading.Timer timer;

        public async void EndPollNote(PollSchema poll)
        {
            bool timerexists = PollController.FindPoll(poll.Question);

            if (timerexists == true)
            {
                await ReplyAsync($"id: {poll.Id} - '{poll.Question}' has finished \r Yes Votes: {poll.Yes} \r No Votes: {poll.No}");

                PollController.stopPollRunning(poll.Question);

                this.timer.Dispose();
            }
            else
            {
                this.timer.Dispose();
            }

            
        }

        [Command("newpoll")]
        [Remarks("Starts a new Poll")]
        public async Task NewPoll(string time, [Remainder]string pollName)
        {
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            //Set all the date time info
            var currentTime = DateTime.Now;
            var userTime = DateTime.Parse(time);
            var timeToWait = userTime.Subtract(currentTime);
            TimeSpan timeToGo = timeToWait;

            PollSchema pollInfo = PollController.AddNewPoll(discordId, pollName, userTime);

            // Handle the timer if its in the past
            if (timeToGo < TimeSpan.Zero)
            {
                await ReplyAsync("Time Passed Fam");
            }

            //EVENT HANDLER FOR THE TIMER REACHING THE TIME
            this.timer = new System.Threading.Timer(x =>
            {
                this.EndPollNote(pollInfo);
            }, null, timeToGo, Timeout.InfiniteTimeSpan);

            //Message to confirm the poll has been set up
            await ReplyAsync($"{discordId} has started a new poll. [id = {pollInfo.Id}] \n {pollName} \n You have until {userTime} to vote! \n Use 'votepoll [id] [yes/no]'");

        }

        [Command("currentpolls")]
        [Remarks("Gets all the current polls")]
        public async Task CurrentPolls()
        {
            // Search the db for current reminders (active)
            string currentPolls = PollController.ReurnCurrentPolls();
            if (currentPolls == "")
            {
                await ReplyAsync("There are no active Polls!");
            }
            else
            {
                await ReplyAsync(currentPolls);
            }
        }

        [Command("votepoll")]
        [Remarks("Votes on a poll")]
        public async Task VoteOnPoll(string id, string vote)
        {
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            string hasVoted = PollController.VoteOnPoll(id, vote, discordId);

            await ReplyAsync(hasVoted);
        }

        [Command("deletepoll")]
        [Remarks("Deletes a poll")]
        public async Task DeletePoll(string id)
        {
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            bool hasDeleted = PollController.DeletePoll(Convert.ToInt32(id), discordId);

            if (hasDeleted == true)
            {
                await ReplyAsync($"{id} was deleted");
            }

            else
            {
                await ReplyAsync($"You cannot delete someone elses poll! Not cool...");
            }
        }

        [Command("deleteallpolls")]
        [Remarks("WARNING DELETES ALL POLLS")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task deleteAllPolls()
        {
            File.Delete(Constants.pollPath);

            await ReplyAsync("All polls deleted, you monster");
        }
    }
}
