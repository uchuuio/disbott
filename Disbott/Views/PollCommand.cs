using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;
using Discord;
using Discord.Commands;
using Disbott.Models.Objects;
using LiteDB;
using System.Diagnostics;

namespace Disbott.Views
{
    [Module]
    public class PollCommand
    {
        Polls currentPoll = new Polls()
        {
            PollName = "",
            Yes = 0,
            No = 0

        };
        bool running = false;
        

        [Command("newpoll"), Description("Starts A Poll")]
        public async Task newpoll(IUserMessage msg, string pollName)
        {
            if (running == false)
            {
                running = true;
                string discordID = msg.Author.Username;
                await msg.Channel.SendMessageAsync($"{discordID} created a new poll: \r\n'{pollName}?' (yes / no)");
                currentPoll.PollName = pollName;
                currentPoll.Owner = discordID;
            }
            else
            {
                await msg.Channel.SendMessageAsync($"The current poll is '{currentPoll.PollName}?' \r\nThis poll was created by {currentPoll.Owner} \r\nPlease wait for them to end the current poll");
            }
            
        }

        [Command("vote"), Description("Vote on a poll")]
        public async Task vote(IUserMessage msg, string vote)
        {
            switch (vote)
            {
                case "yes":
                    currentPoll.Yes += 1;
                    break;
                case "no":
                    currentPoll.No += 1;
                    break;
                default:
                    await msg.Channel.SendMessageAsync("Please selecet 'yes' or 'no'");
                    break;
            }
            
        }

        [Command("endpoll"), Description("End the current poll")]
        public async Task endpoll(IUserMessage msg)
        {
            
            if (currentPoll.Owner == msg.Author.Username)
            {
                string winner;
                running = false;

                if (currentPoll.Yes > currentPoll.No)
                {
                    winner = "YES";
                }
                else
                {
                    winner = "No";
                }

                await msg.Channel.SendMessageAsync($"The Votes for '{currentPoll.PollName}?' are: \r\nYes: {currentPoll.Yes} \r\nNo: {currentPoll.No} \r\nThe winner is {winner}");

                currentPoll.PollName = "";
                currentPoll.Yes = 0;
                currentPoll.No = 0;
            }
            else
            {
                await msg.Channel.SendMessageAsync($"Only the current poll owner can end a poll \r\nThe current poll is '{currentPoll.PollName}?' \r\nThis poll was created by {currentPoll.Owner} \r\nPlease wait for them to end the current poll");
            }
        }
    }
}
