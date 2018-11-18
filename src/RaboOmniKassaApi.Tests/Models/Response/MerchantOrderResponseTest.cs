using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class MerchantOrderResponseTest
    {
        [TestMethod]
        public void TestThatObjectIsCorrectlyConstructed()
        {
            var response = MerchantOrderResponseBuilder.NewInstance();
            
            Assert.AreEqual("http://localhost/redirect/url", response.RedirectUrl);
        }

        [TestMethod]
        public void TestThatInvalidSignatureExceptionIsThrownWhenTheSignaturesDoNotMatch()
        {
            Assert.ThrowsException<InvalidSignatureException>(MerchantOrderResponseBuilder.InvalidSignatureInstance);
        }
    }
}
