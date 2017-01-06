using System;
using Disbott.Controllers;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using System.IO;
using Disbott;

namespace DisbottUnitTests
{
    [TestFixture]
    public class RemindMeOperations
    {
        private TimeSpan timeToWait = new TimeSpan(0, 0, 1);
        private string note = "This test was sucssessful";
        private string userId = "Admin";

        private DateTime setUpUser()
        {
            DateTime time = DateTime.Now.Add(timeToWait);
            RemindMeController.AddRemindMeHistory(time, note, userId);

            return time;
        } 

        [Test]
        public void Add_Reminder_Check()
        {
            setUpUser();
            Assert.That(RemindMeController.FindReminder(note), Is.EqualTo(true));

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Get_Reminder_Check()
        {
            File.Delete(Constants.remindMePath);

            var time = setUpUser();

            string answer = RemindMeController.GetReminders();

            Assert.AreEqual(answer, $"1, {userId}, {time}, {note} \r\n");

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Get_My_Reminders_Check()
        {
            File.Delete(Constants.remindMePath);

            var time = setUpUser();

            string answer2 = RemindMeController.GetMyReminders(userId);

            Assert.AreEqual($"1, {userId}, {time}, {note} \r\n", answer2 );

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Delete_Reminder_Check()
        {
            setUpUser();

            Assert.That(RemindMeController.DeleteReminder(1,userId), Is.EqualTo(true));

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Delete_Reminder_Admin_Check()
        {
            setUpUser();

            Assert.That(RemindMeController.DeleteReminder(1, "Admin2"), Is.EqualTo(true));

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Delete_Reminder_Fails()
        {
            setUpUser();
            Assert.That(RemindMeController.DeleteReminder(1, "Test"), Is.EqualTo(false));
            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Delete_Reminder_End_Check()
        {
            setUpUser();

            Assert.That(RemindMeController.DeleteReminderEnd(note), Is.EqualTo(true));

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Find_Reminder_Check()
        {
            setUpUser();

            Assert.That(RemindMeController.FindReminder(note), Is.EqualTo(true));

            File.Delete(Constants.remindMePath);
        }

        [Test]
        public void Find_Reminder_Failed()
        {
            File.Delete(Constants.remindMePath);
            Assert.That(RemindMeController.FindReminder(note), Is.EqualTo(false));
        }
    }
}
