using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models;

namespace RaboOmniKassaApi.Tests.Models
{
    [TestClass]
    public class OrderItemTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            var orderItem = OrderItemBuilder.MakeCompleteOrderItem();

            Assert.AreEqual("15", orderItem.Id);
            Assert.AreEqual("Name", orderItem.Name);
            Assert.AreEqual("Description", orderItem.Description);
            Assert.AreEqual(1, orderItem.Quantity);
            Assert.AreEqual(Money.FromCents("EUR", 100), orderItem.Amount);
            Assert.AreEqual(Money.FromCents("EUR", 50), orderItem.Tax);
            Assert.AreEqual(ProductType.Digital, orderItem.Category);
            Assert.AreEqual(VatCategory.Low, orderItem.VatCategory);
        }

        [TestMethod]
        public void TestSignature()
        {
            var expectedSignature = new List<string>
            {
                "15",
                "Name",
                "Description",
                "1",
                "EUR",
                "100",
                "EUR",
                "50",
                "DIGITAL",
                "2"
            };
            var orderItem = OrderItemBuilder.MakeCompleteOrderItem();
            var actualSignature = orderItem.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignature, actualSignature);
        }

        [TestMethod]
        public void TestSignatureWithoutOptionalFields()
        {
            var expectedSignature = new List<string>
            {
                "Name",
                "Description",
                "1",
                "EUR",
                "100",
                null,
                "DIGITAL"
            };
            var orderItem = OrderItemBuilder.MakeOrderItemWithoutOptionals();
            var actualSignature = orderItem.GetSignatureData();

            CollectionAssert.AreEqual(expectedSignature, actualSignature);
        }

        [TestMethod]
        public void TestJsonSerialize()
        {
            var expectedJson = "{\"id\":\"15\",\"name\":\"Name\",\"description\":\"Description\",\"quantity\":1,\"amount\":{\"currency\":\"EUR\",\"amount\":100},\"tax\":{\"currency\":\"EUR\",\"amount\":50},\"category\":\"DIGITAL\",\"vatCategory\":\"2\"}";
            var orderItem = OrderItemBuilder.MakeCompleteOrderItem();
            var actualJson = JsonHelper.Serialize(orderItem);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void TestJsonSerializeWithoutOptionalFields()
        {
            var expectedJson = "{\"name\":\"Name\",\"description\":\"Description\",\"quantity\":1,\"amount\":{\"currency\":\"EUR\",\"amount\":100},\"category\":\"DIGITAL\"}";
            var orderItem = OrderItemBuilder.MakeOrderItemWithoutOptionals();
            var actualJson = JsonHelper.Serialize(orderItem);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
