using System;
using Disbott.Controllers;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using System.IO;
using Disbott;

namespace DisbottUnitTests.Poll
{
    [TestFixture]
    public class PollOperations
    {
        private string userID = "Admin";
        private string question = "Did this test work";
        private TimeSpan timeToWait = new TimeSpan(0, 0, 1);

        private DateTime setUpPoll()
        {
            DateTime time = DateTime.Now.Add(timeToWait);
            PollController.AddNewPoll(userID, question, time);
            return time;
        }

        [Test]
        public void Add_Poll_Check()
        {
            setUpPoll();

            Assert.That(PollController.FindPoll(question), Is.EqualTo(true));

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Get_Polls_Check()
        {
            setUpPoll();

            string answer = PollController.ReurnCurrentPolls();

            Assert.AreEqual(answer, $"1, {question}?,Yes: 0, No: 0 \n");

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Vote_Poll_Yes_Check()
        {
            File.Delete(Constants.pollPath);

            setUpPoll();

            string voted = PollController.VoteOnPoll("1", "yes", "Admin");

            Assert.AreEqual(voted, "Thanks for the vote");

            string answer = PollController.ReurnCurrentPolls();

            Assert.AreEqual(answer, $"1, {question}?,Yes: 1, No: 0 \n");

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Vote_Poll_Already_Voted_Check()
        {
            File.Delete(Constants.pollPath);

            setUpPoll();

            PollController.VoteOnPoll("1", "yes", "Admin");
            string voted = PollController.VoteOnPoll("1", "yes", "Admin");

            Assert.AreEqual(voted, "You have already Voted!");

            string answer = PollController.ReurnCurrentPolls();

            Assert.AreEqual(answer, $"1, {question}?,Yes: 1, No: 0 \n");

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Vote_Poll_No_Check()
        {
            File.Delete(Constants.pollPath);

            setUpPoll();

            string voted = PollController.VoteOnPoll("1", "no", "Admin");

            Assert.AreEqual(voted, "Thanks for the vote");

            string answer = PollController.ReurnCurrentPolls();

            Assert.AreEqual(answer, $"1, {question}?,Yes: 0, No: 1 \n");

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Vote_Poll_Failed_Check()
        {
            File.Delete(Constants.pollPath);

            setUpPoll();

            string voted = PollController.VoteOnPoll("1", "cake", "Admin");

            Assert.AreEqual(voted, "Did not update");

            string answer = PollController.ReurnCurrentPolls();

            Assert.AreEqual(answer, $"1, {question}?,Yes: 0, No: 0 \n");

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Delete_Poll_Check()
        {
            setUpPoll();
            
            Assert.That(PollController.DeletePoll(1, userID), Is.EqualTo(true));

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Delete_Poll_Admin_Check()
        {
            setUpPoll();

            Assert.That(PollController.DeletePoll(1, "Admin2"), Is.EqualTo(true));

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Delete_Poll_Failed_Check()
        {
            setUpPoll();

            Assert.That(PollController.DeletePoll(1, "test"), Is.EqualTo(false));

            File.Delete(Constants.pollPath);
        }


        [Test]
        public void Delete_Auto_Poll_Check()
        {
            setUpPoll();

            Assert.That(PollController.DeletePollEnd(question));

            File.Delete(Constants.pollPath);
        }
        

        [Test]
        public void Find_Poll_Check()
        {
            setUpPoll();

            Assert.That(PollController.FindPoll(question), Is.EqualTo(true));

            File.Delete(Constants.pollPath);
        }

        [Test]
        public void Poll_Stop_Running_Check()
        {
            setUpPoll();

            Assert.That(PollController.stopPollRunning(question), Is.EqualTo(true));

            File.Delete(Constants.pollPath);
        }
    }
}
