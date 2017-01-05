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
        // Creates the instance of the timer
        private System.Threading.Timer timer;

        // Method to run when the timer is finished 
        public async void ReplyWithNote(string userNote, string note)
        {
            // Check if the timer is still in the db
            bool timerExists = RemindMeController.FindReminder(note);

            //If the timer is still in the db awesome
            if (timerExists == true)
            {
                await ReplyAsync(userNote);
                RemindMeController.DeleteReminderEnd(note);
                this.timer.Dispose();
            }
            else
            {
                //End the instance of the timer
                this.timer.Dispose();
                //await ReplyAsync("THIS MESSAGE MEANS IT FKING WORKS");
            }
        }

        [Command("reminddatetime")]
        [Remarks("adds a new note for a date")]
        public async Task RemindDateTime(string date, [Remainder]string note)
        {
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;


            try
            {
                //Set all the date time info
                var currentTime = DateTime.Now;
                var userTime = DateTime.Parse(date);
                var timeToWait = userTime.Subtract(currentTime);
                TimeSpan timeToGo = timeToWait;

                // Add the reminder to the db
                var addReminder = RemindMeController.AddRemindMeHistory(userTime, note, discordId);

                await ReplyAsync($"Don't worry {discordId}! I will remind you at {userTime}");

                // Handle the timer if its in the past
                if (timeToGo < TimeSpan.Zero)
                {
                    await ReplyAsync("Time Passed Fam");
                }

                //EVENT HANDLER FOR THE TIMER REACHING THE TIME
                this.timer = new System.Threading.Timer(x =>
                {
                    this.ReplyWithNote($"{msg.Author.Mention} Remember: {note} \r\n This test was set up at {userTime} it is currently {DateTime.Now.ToString()}",note);
                }, null, timeToGo, Timeout.InfiniteTimeSpan);
                
            }
            catch(FormatException e)
            {
                Console.WriteLine(e);
                await ReplyAsync("Date was in an incorrect format. Use the format 'DD/MM/YYYY HH:MM:SS'(Must be in inverted commas) \r\n Or just type a date for a day");
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
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            //Set all the date time info
            TimeSpan timeToWait = new TimeSpan(hours, mins, seconds);
            TimeSpan timeToGo = timeToWait;
            DateTime setTime = DateTime.Now.Add(timeToGo);

            // Add reminder to the db
            var addReminder = RemindMeController.AddRemindMeHistory(setTime, note, discordId);

            await ReplyAsync($"Don't worry {discordId}! I will remind you in {timeToWait}");

            //EVENT HANDLER FOR THE TIMER REACHING THE TIME
            this.timer = new System.Threading.Timer(x =>
            {
                this.ReplyWithNote($"{msg.Author.Mention} Remember: {note}",note);
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="mins"></param>
        /// <param name="seconds"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [Command("remindall")]
        [Remarks("Reminds Everyone")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RemindAll(int hours, int mins, int seconds, [Remainder]string note)
        {
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            //Set all the date time info
            TimeSpan timeToWait = new TimeSpan(hours, mins, seconds);
            TimeSpan timeToGo = timeToWait;
            DateTime setTime = DateTime.Now.Add(timeToGo);

            // Add reminder to the db
            var addReminder = RemindMeController.AddRemindMeHistory(setTime, note, "Everyone");

            await ReplyAsync($"Yes sir {discordId}! I will remind everyone in {timeToWait}");

            //EVENT HANDLER FOR THE TIMER REACHING THE TIME
            this.timer = new System.Threading.Timer(x =>
            {
                this.ReplyWithNote($"@everyone Remember: {note}",note);
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        /// <summary>
        /// Ands a new remind to remind everyone on channel
        /// </summary>
        /// <param name="date"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [Command("reminddatetimeall")]
        [Remarks("adds a new note for a date")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task RemindAllDateTime(string date, [Remainder]string note)
        {
            // Discord user info
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            try
            {
                //Set all the date time info
                var currentTime = DateTime.Now;
                var userTime = DateTime.Parse(date);
                var timeToWait = userTime.Subtract(currentTime);
                TimeSpan timeToGo = timeToWait;
                
                // Add reminder to the db
                var addReminder = RemindMeController.AddRemindMeHistory(userTime, note, "Everyone");

                await ReplyAsync($"Yes Sir {discordId}! I will remind you at {userTime}");

                //Handle if time is in the past
                if (timeToGo < TimeSpan.Zero)
                {
                    await ReplyAsync("Time Passed Fam");
                }

                //EVENT HANDLER FOR THE TIMER REACHING THE TIME
                this.timer = new System.Threading.Timer(x =>
                {
                    this.ReplyWithNote($"@everyone Remember: {note}",note);
                }, null, timeToGo, Timeout.InfiniteTimeSpan);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                await ReplyAsync("Date was in an incorrect format. Use the format 'DD/MM/YYYY HH:MM:SS'(Must be in inverted commas) \r\n Or just type a date for a day");
            }
            catch (Exception e)
            {
                await ReplyAsync(e.Message);
            }
        }

        /// <summary>
        /// Gets all the reminders that are currently active. Requires admin
        /// </summary>
        /// <returns></returns>
        [Command("allreminders")]
        [Remarks("Gets all the current reminders")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetReminders()
        {
            // Search the db for current reminders (active)
            string currentReminders = RemindMeController.GetReminders();
            if (currentReminders == "")
            {
                await ReplyAsync("There are no active reminders!");
            }
            else
            {
                await ReplyAsync(currentReminders);
            }
        }

        /// <summary>
        /// Gets the users Reminders only
        /// </summary>
        /// <returns></returns>
        [Command("myreminders")]
        [Remarks("Gets your the current reminders")]
        public async Task GetMyReminders()
        {
            //Gets your discord user id
            var msg = Context.Message;
            var discordId = msg.Author.Username;


            // Search the db for current reminders (active)
            string currentReminders = RemindMeController.GetMyReminders(discordId);
            if (currentReminders == "")
            {
                await ReplyAsync("There are no active reminders!");
            }
            else
            {
                await ReplyAsync(currentReminders);
            }
        }

        //Allows admin overide for delete
        [Command("admindeletereminder")]
        [Remarks("deletes a reminder")]
        public async Task AdminDeleteReminder(string id)
        {
            // Delete item from the db
            RemindMeController.DeleteReminder(Convert.ToInt32(id), "Admin");
            await ReplyAsync($"{id} was deleted");
        }

        [Command("deletereminder")]
        [Remarks("deletes a reminder")]
        public async Task DeleteReminder(string id)
        {
            //Gets your discord user id
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            // Delete item from the db
            bool hasDeleted = RemindMeController.DeleteReminder(Convert.ToInt32(id),discordId);
            if (hasDeleted == true)
            {
                await ReplyAsync($"{id} was deleted");
            }

            else
            {
                await ReplyAsync($"You cannot delete someone elses reminders! Not cool...");
            }
        }
    }
}
