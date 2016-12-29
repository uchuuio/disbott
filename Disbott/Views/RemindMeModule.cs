using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Disbott.Controllers;

namespace Disbott.Views
{
    [Name("Remind Me")]
    public class RemindMeModule : ModuleBase
    {
        private System.Threading.Timer timer;

        public async void ReplyWithNote(string note)
        {
            await ReplyAsync(note);
            RemindMeController.DeleteReminder(note);
        }

        [Command("reminddatetime")]
        [Remarks("adds a new note for a date")]
        public async Task RemindDateTime(string date, [Remainder]string note)
        {
            var msg = Context.Message;
            var discordId = msg.Author.Username;
            try
            {
                var addReminder = RemindMeController.AddRemindMeHistory(discordId, date, note);

                var currentTime = DateTime.Now;
                var userTime = DateTime.Parse(date);
                var timeToWait = userTime.Subtract(currentTime);
                TimeSpan timeToGo = timeToWait;

                await ReplyAsync($"Don't worry {discordId}! I will remind you at {userTime}");

                if (timeToGo < TimeSpan.Zero)
                {
                    await ReplyAsync("Time Passed Fam");
                }

                this.timer = new System.Threading.Timer(x =>
                {
                    this.ReplyWithNote($"{msg.Author.Mention} Remember: {note} \r\n This test was set up at {userTime} it is currently {DateTime.Now.ToString()}");
                }, null, timeToGo, Timeout.InfiniteTimeSpan);
                
            }
            catch(FormatException e)
            {
                await ReplyAsync("Date was in an incorrect format. Use the format 'DD/MM/YYYY HH:MM:SS'");
            }
            catch(Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("remindme")]
        [Remarks("Reminds you after a time")]
        public async Task RemindMe(int hours, int mins , int seconds, [Remainder]string note)
        {
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            TimeSpan timeToWait = new TimeSpan(hours, mins, seconds);
            TimeSpan timeToGo = timeToWait;

            var addReminder = RemindMeController.AddRemindMeHistory(discordId, timeToWait.ToString(), note);

            await ReplyAsync($"Don't worry {discordId}! I will remind you in {timeToWait}");

            this.timer = new System.Threading.Timer(x =>
            {
                this.ReplyWithNote($"{msg.Author.Mention} Remember: {note}");
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        [Command("remindall")]
        [Remarks("Reminds Everyone")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RemindAll(int hours, int mins, int seconds, [Remainder]string note)
        {
            var msg = Context.Message;
            var discordId = msg.Author.Username;
            TimeSpan timeToWait = new TimeSpan(hours, mins, seconds);
            TimeSpan timeToGo = timeToWait;

            var addReminder = RemindMeController.AddRemindMeHistory("Everyone", timeToWait.ToString(), note);

            await ReplyAsync($"Yes sir {discordId}! I will remind everyone in {timeToWait}");

            this.timer = new System.Threading.Timer(x =>
            {
                this.ReplyWithNote($"@everyone Remember: {note}");
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        [Command("remindalldatetime")]
        [Remarks("adds a new note for a date")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RemindAllDateTime(string date, [Remainder]string note)
        {
            var msg = Context.Message;
            var discordId = msg.Author.Username;
            try
            {
                var addReminder = RemindMeController.AddRemindMeHistory("everyone", date, note);

                var currentTime = DateTime.Now;
                var userTime = DateTime.Parse(date);
                var timeToWait = userTime.Subtract(currentTime);
                TimeSpan timeToGo = timeToWait;

                await ReplyAsync($"Yes Sir {discordId}! I will remind you at {userTime}");

                if (timeToGo < TimeSpan.Zero)
                {
                    await ReplyAsync("Time Passed Fam");
                }
                this.timer = new System.Threading.Timer(x =>
                {
                    this.ReplyWithNote($"@everyone Remember: {note}");
                }, null, timeToGo, Timeout.InfiniteTimeSpan);
            }
            catch (FormatException e)
            {
                await ReplyAsync("Date was in an incorrect format. Use the format 'DD/MM/YYYY HH:MM:SS'");
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        [Command("getreminders")]
        [Remarks("Gets all the current reminders")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetReminders()
        {
            string currentReminders = RemindMeController.GetReminders();

            await ReplyAsync(currentReminders);
        }

        [Command("deletereminder")]
        [Remarks("deletes a reminder")]
        public async Task DeleteReminder(string note)
        {
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            RemindMeController.DeleteReminder(note);

            await ReplyAsync($"{note} was deleted");
        }
    }
}
