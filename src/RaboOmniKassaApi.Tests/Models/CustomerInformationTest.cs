using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Helpers;

namespace RaboOmniKassaApi.Tests.Models
{
    [TestClass]
    public class CustomerInformationTest
    {
        [TestMethod]
        public void TestConstruction()
        {
            var customerInformation = CustomerInformationBuilder.MakeCompleteCustomerInformation();
            Assert.AreEqual("jan.van.veen@gmail.com", customerInformation.EmailAddress);
            Assert.AreEqual("20-03-1987", customerInformation.DateOfBirthRaw);
            Assert.AreEqual("M", customerInformation.Gender);
            Assert.AreEqual("J.M.", customerInformation.Initials);
            Assert.AreEqual("0204971111", customerInformation.TelephoneNumber);
        }

        [TestMethod]
        public void TestSignatureData()
        {
            var expectedSignatureData = new List<string> { "jan.van.veen@gmail.com", "20-03-1987", "M", "J.M.", "0204971111" };
            var customerInformation = CustomerInformationBuilder.MakeCompleteCustomerInformation();
            var actualSignatureData = customerInformation.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignatureData, actualSignatureData);
        }

        [TestMethod]
        public void TestSignatureDataWithNullValues()
        {
            var expectedSignatureData = new List<string> { null, null, null, null, null };
            var address = CustomerInformationBuilder.MakeCustomerInformationWithoutOptionals();
            var actualSignatureData = address.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignatureData, actualSignatureData);
        }

        [TestMethod]
        public void TestJsonSerialize()
        {
            var expectedJson = "{\"emailAddress\":\"jan.van.veen@gmail.com\",\"dateOfBirth\":\"20-03-1987\",\"gender\":\"M\",\"initials\":\"J.M.\",\"telephoneNumber\":\"0204971111\"}";
            var customerInformation = CustomerInformationBuilder.MakeCompleteCustomerInformation();
            var actualJson = JsonHelper.Serialize(customerInformation, "dd-MM-yyyy");

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void TestJsonSerializeWithNullValues()
        {
            var expectedJson = "{}";
            var customerInformation = CustomerInformationBuilder.MakeCustomerInformationWithoutOptionals();
            var actualJson = JsonHelper.Serialize(customerInformation, "dd-MM-yyyy");

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
