using System;
using NUnit.Framework;

using Disbott.Controllers;

namespace DisbottUnitTests
{
    /// <summary>
    /// Tests to confirm coinflips work as intended
    /// </summary>
    [TestFixture]
    public class CoinflipOperations
    {
        /// <summary>
        /// Validate that coin returns either Heads or Tails
        /// </summary>
        [Test]
        public void Coin_Is_Either_Heads_Or_Tails()
        {
            var coinflip = CoinFlipController.Flip();
            string[] possibleOutcomes = new string[] {"Heads", "Tails"};
            Assert.Contains(coinflip, possibleOutcomes);
        }
    }
}
