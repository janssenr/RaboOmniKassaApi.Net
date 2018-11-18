using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class SignedResponseTest
    {
        [TestMethod]
        public void TestThatInvalidSignatureDoesNotLogSignatureKey()
        {
            try
            {
                var json = MerchantOrderResponseBuilder.NewInstanceAsJson();
                Net.Models.Response.Response.CreateInstance<MerchantOrderResponse>(json, new SigningKey("invalid_signature"));
            }
            catch (InvalidSignatureException invalidSignatureException)
            {
                var stackTrace = invalidSignatureException.StackTrace;
                Assert.IsFalse(stackTrace.Contains("invalid_signature"));
            }
        }
    }
}
