using System;
using NUnit.Framework;

using Disbott.Controllers;

namespace DisbottUnitTests
{
    /// <summary>
    /// Tests to confirm DiceRolls work as intended
    /// </summary>
    [TestFixture]
    public class DiceRollOperations
    {
        /// <summary>
        /// Validate that dices can roll
        /// </summary>
        [Test]
        public void Can_Validate_Dice_Roll_Passes()
        {
            var validated = RollController.ValidateDiceRoll("1d20");
            Assert.IsTrue(validated.Success);

            var validated_cake = RollController.ValidateDiceRoll("Cake");
            Assert.IsFalse(validated_cake.Success);

            var validated_null = RollController.ValidateDiceRoll("");
            Assert.IsFalse(validated_null.Success);
        }

        /// <summary>
        /// Validate we can get the number of dice
        /// </summary>
        [Test]
        public void Can_Get_Number_Of_Dice()
        {
            var numberOfDice_below10 = RollController.GetNumberOfDice("1d20");
            Assert.IsTrue(Convert.ToInt32(numberOfDice_below10.Value) == 1);

            var numberOfDice_above10 = RollController.GetNumberOfDice("20d20");
            Assert.IsTrue(Convert.ToInt32(numberOfDice_above10.Value) == 20);
        }

        /// <summary>
        /// Validate we can get the number of sides
        /// </summary>
        [Test]
        public void Can_Get_Number_Of_Sides()
        {
            var numberOfSides_below10 = RollController.GetNumberOfSides("1d2");
            Assert.IsTrue(Convert.ToInt32(numberOfSides_below10.Value) == 2);

            var numberOfSides_above10 = RollController.GetNumberOfSides("10d20");
            Assert.IsTrue(Convert.ToInt32(numberOfSides_above10.Value) == 20);
        }
    }
}
