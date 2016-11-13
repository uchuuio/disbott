using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disbott.Views;
using Disbott.Models.Objects;
using Discord;

namespace Disbott.Controllers
{
    public class Poll
    {
        Polls currentPoll = new Polls()
        {
            Yes = 0,
            No = 0,
            PollName = ""
        };
        bool running = false;

        public string CreateNewPoll(IUserMessage msg, string pollName)
        {
            if (running == false)
            {
                running = true;
                string discordID = msg.Author.Username;
                currentPoll.PollName = pollName;
                currentPoll.Owner = discordID;
                return $"{discordID} created a new poll: \r\n'{pollName}?' (yes / no)";
            }
            else
            {
                return $"The current poll is '{currentPoll.PollName}?' \r\nThis poll was created by {currentPoll.Owner} \r\nPlease wait for them to end the current poll";
            }

        }

        public string VoteOnPoll(string vote)
        {
            if (running == false)
            {
                return "There is currently no poll running \r\nType newpoll to start a new poll";
            }
            else
            {
                if (vote == "yes")
                {
                    currentPoll.Yes += 1;
                }
                else if (vote == "no")
                {
                    currentPoll.No += 1;
                }
                return null;
            }
        }

        public string EndPoll(IUserMessage msg)
        {
            if (running == false)
            {
                return "There is currently no poll running \r\nType newpoll to start a new poll";
            }
            else
            {
                if (currentPoll.Owner == msg.Author.Username)
                {
                    string winner;
                    running = false;

                    if (currentPoll.Yes > currentPoll.No)
                    {
                        winner = "Yes";
                    }
                    else if (currentPoll.Yes == currentPoll.No)
                    {
                        winner = "Nobody, Nobody wins...";
                    }
                    else
                    {
                        winner = "No";
                    }

                    string finalPollname = currentPoll.PollName;
                    int finalYes = currentPoll.Yes;
                    int finalNo = currentPoll.No;

                    currentPoll.PollName = "";
                    currentPoll.Yes = 0;
                    currentPoll.No = 0;

                    return $"The Votes for '{finalPollname}?' are: \r\nYes: {finalYes} \r\nNo: {finalNo} \r\nThe winner is {winner}";
                }
                else
                {
                    return $"Only the current poll owner can end a poll \r\nThe current poll is '{currentPoll.PollName}?' \r\nThis poll was created by {currentPoll.Owner} \r\nPlease wait for them to end the current poll";
                }
            }
        }

        public string OfficerEndPoll()
        {
            string winner;
            running = false;

            if (currentPoll.Yes > currentPoll.No)
            {
                winner = "Yes";
            }
            if (currentPoll.Yes == currentPoll.No)
            {
                winner = "Nobody, Nobody wins...";
            }
            else
            {
                winner = "No";
            }

            string finalPollname = currentPoll.PollName;
            int finalYes = currentPoll.Yes;
            int finalNo = currentPoll.No;

            currentPoll.PollName = "";
            currentPoll.Yes = 0;
            currentPoll.No = 0;

            return $"The Votes for '{finalPollname}?' are: \r\nYes: {finalYes} \r\nNo: {finalNo} \r\nThe winner is {winner}";
        }
    }
}
