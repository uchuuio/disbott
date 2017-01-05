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
        public void Vote_Poll_Check()
        {
            File.Delete(Constants.pollPath);

            setUpPoll();

            string voted = PollController.VoteOnPoll("1", "yes");

            Assert.AreEqual(voted, "Thanks for the vote");

            string answer = PollController.ReurnCurrentPolls();

            Assert.AreEqual(answer, $"1, {question}?,Yes: 1, No: 0 \n");

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
    }
}
