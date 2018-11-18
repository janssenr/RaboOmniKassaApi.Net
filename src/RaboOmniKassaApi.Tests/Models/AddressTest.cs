using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Helpers;

namespace RaboOmniKassaApi.Tests.Models
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void TestConstruction()
        {
            var address = AddressBuilder.MakeFullAddress();
            Assert.AreEqual("Jan", address.FirstName);
            Assert.AreEqual("van", address.MiddleName);
            Assert.AreEqual("Veen", address.LastName);
            Assert.AreEqual("Voorbeeldstraat", address.Street);
            Assert.AreEqual("1234AB", address.PostalCode);
            Assert.AreEqual("Haarlem", address.City);
            Assert.AreEqual("NL", address.CountryCode);
            Assert.AreEqual("5", address.HouseNumber);
            Assert.AreEqual("a", address.HouseNumberAddition);
        }

        [TestMethod]
        public void TestSignatureData()
        {
            var expectedSignatureData = new List<string> {"Jan", "van", "Veen", "Voorbeeldstraat", "5", "a", "1234AB", "Haarlem", "NL"};
            var address = AddressBuilder.MakeFullAddress();
            var actualSignatureData = address.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignatureData, actualSignatureData);
        }

        [TestMethod]
        public void TestSignatureDataWithNullValues()
        {
            var expectedSignatureData = new List<string> { "Jan", null, "Veen", "Voorbeeldstraat", "1234AB", "Haarlem", "NL" };
            var address = AddressBuilder.MakeSmallAddress();
            var actualSignatureData = address.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignatureData, actualSignatureData);
        }

        [TestMethod]
        public void TestJsonSerialize()
        {
            var expectedJson = "{\"firstName\":\"Jan\",\"middleName\":\"van\",\"lastName\":\"Veen\",\"street\":\"Voorbeeldstraat\",\"houseNumber\":\"5\",\"houseNumberAddition\":\"a\",\"postalCode\":\"1234AB\",\"city\":\"Haarlem\",\"countryCode\":\"NL\"}";
            var address = AddressBuilder.MakeFullAddress();
            var actualJson = JsonHelper.Serialize(address);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void TestJsonSerializeWithNullValues()
        {
            var expectedJson = "{\"firstName\":\"Jan\",\"lastName\":\"Veen\",\"street\":\"Voorbeeldstraat\",\"postalCode\":\"1234AB\",\"city\":\"Haarlem\",\"countryCode\":\"NL\"}";
            var address = AddressBuilder.MakeSmallAddress();
            var actualJson = JsonHelper.Serialize(address);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
