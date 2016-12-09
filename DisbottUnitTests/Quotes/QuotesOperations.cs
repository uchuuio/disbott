using System;
using Disbott.Controllers;
using NUnit.Framework;
using NUnit.Framework.Internal.Commands;

namespace DisbottUnitTests
{
    /// <summary>
    /// Tests to confirm Quotes are working as intended
    /// </summary>
    [TestFixture]
    public class QuotesOperations
    {
        /// <summary>
        /// Confirms Adding A Quote works
        /// </summary>
        [Test]
        public void Add_Quote_Check()
        {
            var result = QuotesController.AddQuoteMethod("Dan", "I'm in the army");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Confirm we can get a quote
        /// </summary>
        [Test]
        public void Get_Quote_Check()
        {
            QuotesController.AddQuoteMethod("disbott", "this is a test");
            var quoteTuple = QuotesController.GetQuoteMethod("disbott");

            Assert.That(quoteTuple.Item1, Is.EqualTo("disbott"));
            Assert.That(quoteTuple.Item2, Is.EqualTo("this is a test"));
        }

        /// <summary>
        /// Confirms if there are no quotes we show a fail
        /// </summary>
        [Test]
        public void Get_Quote_Fail()
        {
            var quoteTuple = QuotesController.GetQuoteMethod("tomo");
            Assert.IsNull(quoteTuple.Item2);
        }
        
        /// <summary>
        /// Confirms Deleting A Quote works
        /// </summary>
        [Test]
        public void Delete_Quote_Check()
        {
            var result = QuotesController.DeleteQuoteMethod("I'm in the army");
            Assert.IsTrue(result);
        }
    }
}
