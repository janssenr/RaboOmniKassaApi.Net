using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class MerchantOrderStatusResponseTest
    {
        [TestMethod]
        public void TestThatObjectIsCorrectlyConstructed()
        {
            var merchantOrderResult = new MerchantOrderResult
            {
                PoiId = 1000,
                MerchantOrderId = "10",
                OmnikassaOrderId = "1",
                OrderStatus = "CANCELLED",
                ErrorCode = "666",
                OrderStatusDateTimeRaw = "1970-01-01T00:00:00.000+02:00",
                PaidAmount = Money.FromDecimal("EUR", 1),
                TotalAmount = Money.FromDecimal("EUR", 1)
            };
            var expectedOrderResults = new[]
            {
                merchantOrderResult
            };

            var response = MerchantOrderStatusResponseBuilder.NewInstance();

            CollectionAssert.AreEqual(expectedOrderResults, response.OrderResults);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSignatureException))]
        public void TestThatInvalidSignatureExceptionIsThrownWhenTheSignaturesDoNotMatch()
        {
            var response = MerchantOrderStatusResponseBuilder.InvalidSignatureInstance();
            //response.ValidateSignature(MerchantOrderStatusResponseBuilder.GetSigningKey());
        }
    }
}
