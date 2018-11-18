using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models;

namespace RaboOmniKassaApi.Tests.Models
{
    [TestClass]
    public class MoneyTest
    {
        [TestMethod]
        public void TestFromCents()
        {
            var money = Money.FromCents("EUR", 100);

            Assert.AreEqual("EUR", money.Currency);
            Assert.AreEqual(100, money.Amount);
        }

        [TestMethod]
        public void TestFromDecimal()
        {
            var money = Money.FromDecimal("EUR", 5.00);

            Assert.AreEqual("EUR", money.Currency);
            Assert.AreEqual(500, money.Amount);
        }

        [TestMethod]
        public void TestFromDecimalCorrectRounding()
        {
            var noRoundingNeed = Money.FromDecimal("EUR", 9.99);
            Assert.AreEqual(999, noRoundingNeed.Amount, 0, "Amount is incorrect for scenario: no rounding needed");

            var roundCeilingNeeded = Money.FromDecimal("EUR", 9.999);
            Assert.AreEqual(1000, roundCeilingNeeded.Amount, 0, "Amount is incorrect for scenario: round ceiling required");

            var roundFloorNeeded = Money.FromDecimal("EUR", 9.991);
            Assert.AreEqual(999, roundFloorNeeded.Amount, 0, "Amount is incorrect for scenario: round floor required");

            var edgeCase = Money.FromDecimal("EUR", 9.995);
            Assert.AreEqual(1000, edgeCase.Amount, 0, "Amount is incorrect for scenario: edge case (0.5)");
        }

        [TestMethod]
        public void TestSignature()
        {
            var expectedSignatureData = new List<string>
            {
                "EUR",
                "100"
            };
            var money = Money.FromCents("EUR", 100);
            var actualSignatureData = money.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignatureData, actualSignatureData);
        }

        [TestMethod]
        public void TestJsonSerialize()
        {
            var expectedJson = "{\"currency\":\"EUR\",\"amount\":100}";
            var money = Money.FromCents("EUR", 100);
            var actualJson = JsonHelper.Serialize(money);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
