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
        /// Validate that coin returns either 1 or 2
        /// </summary>
        [Test]
        public void Coin_Is_Either_Heads_Or_Tails()
        {
            var coinflip = CoinFlipController.Flip();
            var possibleOutcomes = new[] {1, 2};
            Assert.Contains(coinflip, possibleOutcomes);
        }

        /// <summary>
        /// Validate a coin can be Tails
        /// </summary>
        [Test]
        public void Coin_Can_Be_Heads()
        {
            var result = CoinFlipController.GetString(1);
            Assert.That(result, Is.EqualTo("Heads"));
        }

        /// <summary>
        /// Validate a coin can be Tails
        /// </summary>
        [Test]
        public void Coin_Can_Be_Tails()
        {
            var result = CoinFlipController.GetString(2);
            Assert.That(result, Is.EqualTo("Tails"));
        }
    }
}
