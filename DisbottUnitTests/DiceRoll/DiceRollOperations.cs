using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Disbott;
using Disbott.Views;
using System.Linq;
using Disbott.Controllers;

namespace DisbottUnitTests
{
    [TestClass]
    public class DiceRollOperations
    {
        [TestMethod]
        public void Can_Validate_Dice_Roll_Passes()
        {
            var validated = RollController.ValidateDiceRoll("1d20");
            Assert.IsTrue(validated.Success);

            var validated_cake = RollController.ValidateDiceRoll("Cake");
            Assert.IsFalse(validated_cake.Success);

            var validated_null = RollController.ValidateDiceRoll("");
            Assert.IsFalse(validated_null.Success);
        }

        [TestMethod]
        public void Can_Get_Number_Of_Dice()
        {
            var numberOfDice_below10 = RollController.GetNumberOfDice("1d20");
            Assert.IsTrue(Convert.ToInt32(numberOfDice_below10.Value) == 1);

            var numberOfDice_above10 = RollController.GetNumberOfDice("20d20");
            Assert.IsTrue(Convert.ToInt32(numberOfDice_above10.Value) == 20);
        }

        [TestMethod]
        public void Can_Get_Number_Of_Sides()
        {
            var numberOfSides_below10 = RollController.GetNumberOfSides("1d2");
            Assert.IsTrue(Convert.ToInt32(numberOfSides_below10.Value) == 2);

            var numberOfSides_above10 = RollController.GetNumberOfSides("10d20");
            Assert.IsTrue(Convert.ToInt32(numberOfSides_above10.Value) == 20);
        }

        //[TestMethod]
        //public void Can_Create_Number_Array()
        //{
        //    var array = Roll.Rolling(1, 20);
        //    Assert.IsTrue(array.Sum() >= 1 && array.Sum() <= 20);

        //    var array2 = Roll.Rolling(20, 10);
        //    Assert.IsTrue(array2.Sum() > 20 && array2.Sum() < 200);
        //}
    }
}
