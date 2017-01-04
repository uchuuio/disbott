using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Disbott.Controllers;
using Disbott.Models.Objects;
using Disbott.Properties;
using System;
using System.Threading;

namespace Disbott.Views
{
    [Name("Poll")]
    public class PollModule : ModuleBase
    {
        //PollController pollOption = new PollController();

        //[Command("newpoll")]
        //[Remarks("Starts A Poll")]
        //public async Task NewPoll([Remainder]string pollName)
        //{
        //    var msg = Context.Message;
        //    string createdPoll = pollOption.CreateNewPoll(msg, pollName);

        //    await ReplyAsync(createdPoll);
        //}
        private System.Threading.Timer timer;

        public async void EndPollNote(string question)
        {
            bool timerexists = PollController.FindPoll(question);

            if (timerexists == true)
            {
                PollSchema pollResults = PollController.GetPollResults(question);

                await ReplyAsync($"id: {pollResults.Id} - '{pollResults.Question}' has finished \r Yes Votes: {pollResults.Yes} \r No Votes: {pollResults.No}");

                PollController.stopPollRunning(question);

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

            PollController.AddNewPoll(discordId, pollName, userTime);

            // Handle the timer if its in the past
            if (timeToGo < TimeSpan.Zero)
            {
                await ReplyAsync("Time Passed Fam");
            }

            //EVENT HANDLER FOR THE TIMER REACHING THE TIME
            this.timer = new System.Threading.Timer(x =>
            {
                this.EndPollNote(pollName);
            }, null, timeToGo, Timeout.InfiniteTimeSpan);

            await ReplyAsync($"@everyone {discordId} has started a new poll. '{pollName}' \r You have until {userTime} to vote! \r Use 'votepoll [id] [yes/no]'");

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

            string hasVoted = PollController.VoteOnPoll(id, vote);

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

        //[Command("vote")]
        //[Remarks("Vote on a poll")]
        //public async Task Vote(string vote)
        //{
        //    if (vote == "yes" || vote == "no")
        //    {
        //        string createdPoll = pollOption.VoteOnPoll(vote);
        //        if (string.IsNullOrEmpty(createdPoll) == false)
        //        {
        //            await ReplyAsync(createdPoll);
        //        }
        //    }
        //    else
        //    {
        //        await ReplyAsync(Resources.error_Incorrect_Format_Poll);
        //    }
        //}

        //[Command("endpoll")]
        //[Remarks("End the current poll")]
        //public async Task EndPoll()
        //{
        //    var msg = Context.Message;
        //    string createdPoll = pollOption.EndPoll(msg);
        //    await ReplyAsync(createdPoll);
        //}

        //[Command("officerendpoll")]
        //[Remarks("Ends the poll as admin")]
        //[RequireUserPermission(GuildPermission.BanMembers)]
        //public async Task OfficerEndPoll()
        //{
        //    string createdPoll = pollOption.OfficerEndPoll();
        //    await ReplyAsync(createdPoll);
        //}
    }
}
