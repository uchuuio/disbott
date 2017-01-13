using Disbott.Models.Objects;
using Discord;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Disbott.Controllers
{
    public class PollController
    {
        /// <summary>
        /// Starts a new poll, sets up db and passes all information in
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="question"></param>
        /// <param name="time"></param>
        /// <returns>Object with user info</returns>
        public static PollSchema AddNewPoll(string userName, string question, DateTime time)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var Polls = db.GetCollection<PollSchema>("poll");

                List<string> l = new List<string>();

                var newPoll = new PollSchema
                {
                    Question = question,
                    Time = time,
                    Owner = userName,
                    IsRunning = true,
                    UsersVoted = l
                };

                Polls.Insert(newPoll);

                PollSchema initialUserInfo = GetPollResults(question);

                return initialUserInfo;
            }
        }

        /// <summary>
        /// Gets the results of the poll. (Method cant be called by user, its auto) 
        /// </summary>
        /// <param name="question"></param>
        /// <returns>Object containing poll results</returns>
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

        /// <summary>
        /// Gets a list of current live polls.
        /// </summary>
        /// <returns>String of poll details</returns>
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
                        currentPolls += $"{poll.Id}, {poll.Question}?,Yes: {poll.Yes}, No: {poll.No} \n";
                    }
                    else
                    {

                    }
                }

                return currentPolls;
            }
        }

        /// <summary>
        /// Adds a vote to a poll, adds the vote to the db
        /// </summary>
        /// <param name="number"></param>
        /// <param name="voteyn"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string VoteOnPoll(string number, string voteyn, string userID)
        {
            int id = Convert.ToInt32(number);

            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindOne(x => x.Id.Equals(id));

                var poll = result;

                bool matched = poll.UsersVoted.Contains(userID);

                if (matched) 
                {
                    return "You have already Voted!";
                }
                else
                {
                    if (voteyn == "yes")
                    {
                        poll.Yes += 1;
                        polls.Update(id, poll);
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
                    poll.UsersVoted.Add(userID);
                    polls.Update(id, poll);
                    return "Thanks for the vote";
                }
            }
        }

        /// <summary>
        /// Deletes a Poll from the db (Own user only)
        /// </summary>
        /// <param name="number"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DeletePoll(int number, string userID = "Admin2")
        {
            int id = Convert.ToInt32(number);

            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                if (userID == "Admin2")
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

        /// <summary>
        /// Automatic method to clean up when a poll ends
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool DeletePollEnd(string question)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                polls.Delete(x => x.Question.Equals(question));

                return true;
            }
        }

        /// <summary>
        /// Automatic method to find a poll in the list (Cant be called by user)
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static bool FindPoll(string question)
        {
            using (var db = new LiteDatabase(Constants.pollPath))
            {
                var polls = db.GetCollection<PollSchema>("poll");

                var result = polls.FindOne(x => x.Question.Equals(question));

                var poll = result;

                string answer = result.Question;
                return true;
            }
        }

        /// <summary>
        /// Automatic method to stop a poll runnning (Cant be called by user)
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
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
    }
}
