using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Disbott;
using Disbott.Modules;
using Disbott.Modules.Roll;

namespace DisbottUnitTests
{
    [TestClass]
    public class DiceRollOperations
    {
        [TestMethod]
        public void Can_Validate_Dice_Roll_Passes()
        {
            var validated = RollMethod.ValidateDiceRoll("1d20");
            Assert.IsTrue(validated.Success);
        }

        [TestMethod]
        public void Can_Validate_Dice_Roll_Fails()
        {
            var validated_cake = RollMethod.ValidateDiceRoll("Cake");
            Assert.IsFalse(validated_cake.Success);

            var validated_null = RollMethod.ValidateDiceRoll("");
            Assert.IsFalse(validated_null.Success);
            
        }
    }
}
