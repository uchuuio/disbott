using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Disbott.Views
{
    [Name("Remind Me")]
    public class RemindMeModule : ModuleBase
    {
        private System.Threading.Timer timer;
        //public delegate void MyDel(string note);
        //public event MyDel MyEvent;

        [Command("datenote")]
        [Remarks("adds a new note for a date")]
        public async Task DateNote(int year, int month, int day, int hour, int minuite, int second, [Remainder]string newnote)
        {
            var msg = Context.Message;
            var discordId = msg.Author.Username;

            var currentTime = DateTime.Now;
            var userTime = new DateTime(year, month, day, hour, minuite, second);
            var timeToWait = userTime.Subtract(currentTime);
            TimeSpan timeToGo = timeToWait;

            await ReplyAsync($"{discordId} added a new note for {userTime.ToString()}");
            await ReplyAsync($"time to wait is {timeToWait.ToString()}");

            if (timeToGo < TimeSpan.Zero)
            {
               await ReplyAsync("Time Passed Fam");
            }
            this.timer = new System.Threading.Timer(x =>
            {
                this.ReplyWithNote($"@{discordId} {newnote} {userTime.ToString()} \r\n The time is currently {DateTime.Now.ToString()}");
            }, null, timeToGo, Timeout.InfiniteTimeSpan);
        }

        public async void ReplyWithNote(string note)
        {
            await ReplyAsync(note);
        }
    }
}
