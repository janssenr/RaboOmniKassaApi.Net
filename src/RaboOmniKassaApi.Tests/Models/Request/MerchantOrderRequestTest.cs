using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Request;

namespace RaboOmniKassaApi.Tests.Models.Request
{
    [TestClass]
    public class MerchantOrderRequestTest
    {
        [TestMethod]
        public void testJsonEncoding_withoutOptionalFields()
        {
            var merchantOrderRequest = MerchantOrderRequestBuilder.MakeMinimalRequest();
            merchantOrderRequest.TimestampRaw = CreateTimeStamp();
            SetSignature(merchantOrderRequest);
            var expectedJson = "{\"signature\":\"36d5f1890af8407e39eddc9bddfcadce8b3e38707a6a416d52aef3d8c2ab76b3a1155457321ee3370ca0b2339f1894cd94e88c6d6fa013599a22be638c9fda4c\",\"merchantOrderId\":\"100\",\"amount\":{\"currency\":\"EUR\",\"amount\":9999},\"merchantReturnURL\":\"http:\\/\\/localhost\\/\",\"timestamp\":\"2016-12-21T14:13:56+01:00\"}";
            var actualJson = JsonHelper.Serialize(merchantOrderRequest);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void testJsonEncoding_allFields()
        {
            var merchantOrderRequest = MerchantOrderRequestBuilder.MakeCompleteRequest();
            merchantOrderRequest.TimestampRaw = CreateTimeStamp();
            SetSignature(merchantOrderRequest);
            var expectedJson = "{\"signature\":\"0712c403c89ae92f7531aea31280eb517e274c93c2196e905415407c0dc4bd71e7f0048720ae70d00303da8551485c6444c0f5ddf215ebeb7af4e34580a1af61\",\"merchantOrderId\":\"100\",\"description\":\"Order ID: 100\",\"orderItems\":[{\"id\":\"15\",\"name\":\"Name\",\"description\":\"Description\",\"quantity\":1,\"amount\":{\"currency\":\"EUR\",\"amount\":100},\"tax\":{\"currency\":\"EUR\",\"amount\":50},\"category\":\"DIGITAL\",\"vatCategory\":\"2\"}],\"amount\":{\"currency\":\"EUR\",\"amount\":9999},\"shippingDetail\":{\"firstName\":\"Jan\",\"middleName\":\"van\",\"lastName\":\"Veen\",\"street\":\"Voorbeeldstraat\",\"houseNumber\":\"5\",\"houseNumberAddition\":\"a\",\"postalCode\":\"1234AB\",\"city\":\"Haarlem\",\"countryCode\":\"NL\"},\"billingDetail\":{\"firstName\":\"Piet\",\"middleName\":\"van der\",\"lastName\":\"Stoel\",\"street\":\"Dorpsstraat\",\"houseNumber\":\"9\",\"houseNumberAddition\":\"rood\",\"postalCode\":\"4321YZ\",\"city\":\"Bennebroek\",\"countryCode\":\"NL\"},\"customerInformation\":{\"emailAddress\":\"jan.van.veen@gmail.com\",\"dateOfBirth\":\"20-03-1987\",\"gender\":\"M\",\"initials\":\"J.M.\",\"telephoneNumber\":\"0204971111\"},\"language\":\"NL\",\"merchantReturnURL\":\"http:\\/\\/localhost\\/\",\"paymentBrand\":\"IDEAL\",\"paymentBrandForce\":\"FORCE_ONCE\",\"timestamp\":\"2016-12-21T14:13:56+01:00\"}";
            var actualJson = JsonHelper.Serialize(merchantOrderRequest);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void testJsonEncoding_withOrderItemsWithoutOptionalFields()
        {
            var merchantOrderRequest = MerchantOrderRequestBuilder.MakeWithOrderItemsWithoutOptionalFieldsRequest();
            merchantOrderRequest.TimestampRaw = CreateTimeStamp();
            SetSignature(merchantOrderRequest);
            var expectedJson = "{\"signature\":\"765b966304f7e6d4f4c1dd5b684175a7f193eb282c27462d6e1c2c170d7d0db84c0136d06082bf4d62f12bbabb8a54abc64c2106016f041ec1c0ef2e599ac659\",\"merchantOrderId\":\"100\",\"orderItems\":[{\"name\":\"Name\",\"description\":\"Description\",\"quantity\":1,\"amount\":{\"currency\":\"EUR\",\"amount\":100},\"category\":\"DIGITAL\"}],\"amount\":{\"currency\":\"EUR\",\"amount\":9999},\"merchantReturnURL\":\"http:\\/\\/localhost\\/\",\"timestamp\":\"2016-12-21T14:13:56+01:00\"}";
            var actualJson = JsonHelper.Serialize(merchantOrderRequest);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void testJsonEncoding_withShippingDetailsWithoutOptionalFields()
        {
            var merchantOrderRequest = MerchantOrderRequestBuilder.MakeWithShippingDetailsWithoutOptionalFieldsRequest();
            merchantOrderRequest.TimestampRaw = CreateTimeStamp();
            SetSignature(merchantOrderRequest);
            var expectedJson = "{\"signature\":\"7a20d5896b7a379168da08bbce2fd9d02dde7fdc7c9963728c6f83d79d36477c797df89e14544210f746c666d14587f9724fe09df6cd8ff228d1d2c8e2480e8f\",\"merchantOrderId\":\"100\",\"amount\":{\"currency\":\"EUR\",\"amount\":9999},\"shippingDetail\":{\"firstName\":\"Jan\",\"middleName\":\"van\",\"lastName\":\"Veen\",\"street\":\"Voorbeeldstraat\",\"houseNumber\":\"5\",\"houseNumberAddition\":\"a\",\"postalCode\":\"1234AB\",\"city\":\"Haarlem\",\"countryCode\":\"NL\"},\"merchantReturnURL\":\"http:\\/\\/localhost\\/\",\"timestamp\":\"2016-12-21T14:13:56+01:00\"}";
            var actualJson = JsonHelper.Serialize(merchantOrderRequest);

            Assert.AreEqual(expectedJson, actualJson);
        }

        [TestMethod]
        public void testJsonEncoding_withPaymentBrandButWithoutOtherOptionalFields()
        {
            var merchantOrderRequest = MerchantOrderRequestBuilder.MakeWithPaymentBrandButWithoutOtherOptionalFields();
            merchantOrderRequest.TimestampRaw = CreateTimeStamp();
            SetSignature(merchantOrderRequest);
            var expectedJson = "{\"signature\":\"7b847bba5c597d27028463f771109305c9c546f8702463f9a253b7be1b8edae98223bf272d1beba21e7a35015b93bf3dd9b205cd8b73d6a024a096945ecc4f34\",\"merchantOrderId\":\"100\",\"amount\":{\"currency\":\"EUR\",\"amount\":9999},\"merchantReturnURL\":\"http:\\/\\/localhost\\/\",\"paymentBrand\":\"IDEAL\",\"paymentBrandForce\":\"FORCE_ONCE\",\"timestamp\":\"2016-12-21T14:13:56+01:00\"}";
            var actualJson = JsonHelper.Serialize(merchantOrderRequest);

            Assert.AreEqual(expectedJson, actualJson);
        }

        private void SetSignature(MerchantOrderRequest merchantOrderRequest)
        {
            merchantOrderRequest.CalculateAndSetSignature(MerchantOrderRequestBuilder.GetSigningKey());
        }

        private string CreateTimeStamp()
        {
            return "2016-12-21T14:13:56+01:00";
        }

    }
}
