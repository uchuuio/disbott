using Disbott;
using Disbott.Controllers;

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisbottUnitTests.MessageCountOperations
{
    [TestFixture]
    public class MessageCountOperations
    {
        [Test]
        public void Can_Log_Message()
        {
            File.Delete(Constants.MessageCountPath);
            bool success = MessageCountController.MessageRecord(1);

            Assert.That(success, Is.EqualTo(true));
            File.Delete(Constants.MessageCountPath);
        }

        [Test]
        public void Can_Get_MessageCount_Check()
        {
            File.Delete(Constants.MessageCountPath);
            MessageCountController.MessageRecord(1);
            string count = MessageCountController.GetMessages(1);
            Assert.AreEqual($"<@!1> has posted 1 messages", count);
            File.Delete(Constants.MessageCountPath);
        }

        [Test]
        public void Can_Log_Second_Message()
        {
            File.Delete(Constants.MessageCountPath);
            MessageCountController.MessageRecord(1);
            bool success = MessageCountController.MessageRecord(1);

            Assert.That(success, Is.EqualTo(true));
            File.Delete(Constants.MessageCountPath);
        }
    }
}
