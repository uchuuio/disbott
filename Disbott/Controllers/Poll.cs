using Disbott.Models.Objects;
using Discord;
using LiteDB;
using System;
using System.Linq;

namespace Disbott.Controllers
{
    public class PollController
    {
        public static bool AddNewPoll(string userName, string question, DateTime time)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var Polls = db.GetCollection<PollSchema>("poll");

                var newPoll = new PollSchema
                {
                    Question = question,
                    Time = time,
                    Owner = userName,
                    IsRunning = true
                };

                Polls.Insert(newPoll);
            }

            return true;
        }

        public static PollSchema GetPollResults(string question)
        {
            PollSchema pollResults = new PollSchema();

            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindOne(x => x.Question.Equals(question));

                var poll = result;

                pollResults.Id = poll.Id;
                pollResults.Yes = poll.Yes;
                pollResults.No = poll.No;
                pollResults.Question = poll.Question;

                return pollResults;
            }
        }

        public static string ReurnCurrentPolls()
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                string currentPolls = "";
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindAll();

                var allPolls = result as PollSchema[] ?? result.ToArray();

                foreach (var poll in allPolls)
                {
                    if (poll.IsRunning == true)
                    {
                        currentPolls += $"{poll.Id}, {poll.Question}?,Yes: {poll.Yes}, No: {poll.No}";
                    }
                    else
                    {

                    }
                }

                return currentPolls;
            }
        }

        public static string VoteOnPoll(string number, string voteyn)
        {
            int id = Convert.ToInt32(number);

            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindOne(x => x.Id.Equals(id));

                var poll = result;

                if (voteyn == "yes")
                {
                    poll.Yes += 1;
                    polls.Update(id ,poll);
                }
                else if (voteyn == "no")
                {
                    poll.No += 1;
                    polls.Update(id, poll);
                }
                else
                {
                    return "Did not update";
                }
                return "Thanks for the vote";
            }
        }

        public static bool DeletePoll(int number, string userID = "Admin")
        {
            int id = Convert.ToInt32(number);

            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                if (userID == "Admin")
                {
                    polls.Delete(x => x.Id.Equals(id));
                    return true;
                }
                else
                {
                    var result = polls.Find(x => x.Id.Equals(id));
                    var allpolls = result as PollSchema[] ?? result.ToArray();

                    if (allpolls[0].Owner == userID)
                    {
                        polls.Delete(x => x.Id.Equals(id));
                        return true;
                    }
                    //Delete the item with passed in ID
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool DeletePollEnd(string question)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                polls.Delete(x => x.Question.Equals(question));

                return true;
            }
        }

        public static bool FindPoll(string question)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindAll();

                var allPolls = result as PollSchema[] ?? result.ToArray();

                try
                {
                    string answer = allPolls[0].Question;
                    return true;
                }
                catch (IndexOutOfRangeException e)
                {
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool stopPollRunning(string question)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindOne(x => x.Question.Equals(question));

                var poll = result;

                poll.IsRunning = false;
                int userId = poll.Id;

                polls.Update(userId, poll);

                return true;
            }
        }

        //PollSchema currentPoll = new PollSchema()
        //{
        //    Yes = 0,
        //    No = 0,
        //    PollName = ""
        //};
        //bool running = false;

        //public string CreateNewPoll(IUserMessage msg, string pollName)
        //{
        //    if (running == false)
        //    {
        //        running = true;
        //        string discordID = msg.Author.Username;
        //        currentPoll.PollName = pollName;
        //        currentPoll.Owner = discordID;
        //        return $"{discordID} created a new poll: \r\n'{pollName}?' (yes / no)";
        //    }
        //    else
        //    {
        //        return $"The current poll is '{currentPoll.PollName}?' \r\nThis poll was created by {currentPoll.Owner} \r\nPlease wait for them to end the current poll \r\nAn officer can do this using the 'officerendpoll' command";
        //    }

        //}

        //public string VoteOnPoll(string vote)
        //{
        //    if (running == false)
        //    {
        //        return "There is currently no poll running \r\nType newpoll to start a new poll";
        //    }
        //    else
        //    {
        //        if (vote == "yes")
        //        {
        //            currentPoll.Yes += 1;
        //        }
        //        else if (vote == "no")
        //        {
        //            currentPoll.No += 1;
        //        }
        //        return null;
        //    }
        //}

        //public string EndPoll(IUserMessage msg)
        //{
        //    if (running == false)
        //    {
        //        return "There is currently no poll running \r\nType newpoll to start a new poll";
        //    }
        //    else
        //    {
        //        if (currentPoll.Owner == msg.Author.Username)
        //        {
        //            string winner;
        //            running = false;

        //            if (currentPoll.Yes > currentPoll.No)
        //            {
        //                winner = "Yes";
        //            }
        //            else if (currentPoll.Yes == currentPoll.No)
        //            {
        //                winner = "Nobody, Nobody wins...";
        //            }
        //            else
        //            {
        //                winner = "No";
        //            }

        //            string finalPollname = currentPoll.PollName;
        //            int finalYes = currentPoll.Yes;
        //            int finalNo = currentPoll.No;

        //            currentPoll.PollName = "";
        //            currentPoll.Yes = 0;
        //            currentPoll.No = 0;

        //            return $"The Votes for '{finalPollname}?' are: \r\nYes: {finalYes} \r\nNo: {finalNo} \r\nThe winner is {winner}";
        //        }
        //        else
        //        {
        //            return $"Only the current poll owner can end a poll \r\nThe current poll is '{currentPoll.PollName}?' \r\nThis poll was created by {currentPoll.Owner} \r\nPlease wait for them to end the current poll \r\nAn officer can do this using the 'officerendpoll' command";
        //        }
        //    }
        //}

        //public string OfficerEndPoll()
        //{
        //    string winner;
        //    running = false;

        //    if (currentPoll.Yes > currentPoll.No)
        //    {
        //        winner = "Yes";
        //    }
        //    if (currentPoll.Yes == currentPoll.No)
        //    {
        //        winner = "Nobody, Nobody wins...";
        //    }
        //    else
        //    {
        //        winner = "No";
        //    }

        //    string finalPollname = currentPoll.PollName;
        //    int finalYes = currentPoll.Yes;
        //    int finalNo = currentPoll.No;

        //    currentPoll.PollName = "";
        //    currentPoll.Yes = 0;
        //    currentPoll.No = 0;

        //    return $"The Votes for '{finalPollname}?' are: \r\nYes: {finalYes} \r\nNo: {finalNo} \r\nThe winner is {winner}";
        //}
    }
}
