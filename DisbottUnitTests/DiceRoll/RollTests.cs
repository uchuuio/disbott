using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Disbott.Modules.Roll;
using Disbott.Modules;
using Discord;

namespace DisbottUnitTests
{
    [TestClass]
    class RollTests
    {
        private IUserMessage msg;
        Roll Testroll = new Roll();

        [TestMethod]
        public async void Can_Roll_Dice()
        {
            await Testroll.roll(msg, "1d20");
        }
    }
}
