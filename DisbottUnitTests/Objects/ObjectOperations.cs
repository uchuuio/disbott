using System;
using Disbott.Controllers;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;
using System.IO;
using Disbott;
using Disbott.Models.Objects;

namespace DisbottUnitTests.Objects
{
    [TestFixture]
    public class ObjectOperations
    {
        [Test]
        public void Can_Create_MessageSchema()
        {
            ulong messageCount = 10000;

            MessageCountSchema messages = new MessageCountSchema();
            messages.Messages = messageCount;

            Type type = messages.Messages.GetType();

            Assert.AreEqual(type, typeof(ulong));
        }

        [Test]
        public void Can_Create_LolSummoner()
        {
            ulong DiscordID = 1000000;

            LoLSummoner summoner = new LoLSummoner();

            Type typeDID = summoner.DiscordID.GetType();
            Type typeID = summoner.Id.GetType();
            Type typeSID = summoner.SummonerID.GetType();

            Assert.AreEqual(typeDID, typeof(ulong));
            Assert.AreEqual(typeID, typeof(ulong));
            Assert.AreEqual(typeSID, typeof(int));
        }
    }
}
