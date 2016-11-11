using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Disbott;
using Disbott.Modules;
using Disbott.Modules.Roll;
using System.Linq;

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

            var validated_cake = RollMethod.ValidateDiceRoll("Cake");
            Assert.IsFalse(validated_cake.Success);

            var validated_null = RollMethod.ValidateDiceRoll("");
            Assert.IsFalse(validated_null.Success);
        }

        [TestMethod]
        public void Can_Get_Number_Of_Dice()
        {
            var numberOfDice_below10 = RollMethod.GetNumberOfDice("1d20");
            Assert.IsTrue(Convert.ToInt32(numberOfDice_below10.Value) == 1);

            var numberOfDice_above10 = RollMethod.GetNumberOfDice("20d20");
            Assert.IsTrue(Convert.ToInt32(numberOfDice_above10.Value) == 20);
        }

        [TestMethod]
        public void Can_Get_Number_Of_Sides()
        {
            var numberOfSides_below10 = RollMethod.GetNumberOfSides("1d2");
            Assert.IsTrue(Convert.ToInt32(numberOfSides_below10.Value) == 2);

            var numberOfSides_above10 = RollMethod.GetNumberOfSides("10d20");
            Assert.IsTrue(Convert.ToInt32(numberOfSides_above10.Value) == 20);
        }

        [TestMethod]
        public void Can_Create_Number_Array()
        {
            var array = RollMethod.Rolling(1, 20);
            Assert.IsTrue(array.Sum() >= 1 && array.Sum() <= 20);

            var array2 = RollMethod.Rolling(20, 10);
            Assert.IsTrue(array2.Sum() > 20 && array2.Sum() < 200);
        }
    }
}
