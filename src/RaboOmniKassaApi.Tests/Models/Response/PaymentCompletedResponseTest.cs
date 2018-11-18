using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class PaymentCompletedResponseTest
    {
        [TestMethod]
        public void TestThatIsValidReturnsTrueForAValidSignature()
        {
            var signingKey = new SigningKey("secret");
            var paymentCompletedResponse = PaymentCompletedResponse.CreateInstance("1", "COMPLETED", "b890b2f3c6f102bb853ed448dd58d2c13cc695541f5eecca713470e68ced6f2c1a5f5ddd529a732ff51a019126ffefa8bd1d0193b596b393339ffcbf6f335241", signingKey);
            Assert.IsNotNull(paymentCompletedResponse);
            Assert.AreEqual("1", paymentCompletedResponse.OrderId);
            Assert.AreEqual("COMPLETED", paymentCompletedResponse.Status);
        }

        [TestMethod]
        public void TestThatIsValidReturnsFalseForInvalidSignatures()
        {
            var signingKey = new SigningKey("secret");
            var paymentCompletedResponse = PaymentCompletedResponse.CreateInstance("1", "CANCELLED", "ffb94fef027526bab3f98eaa432974daea4e743f09de86ab732208497805bb12", signingKey);
            Assert.IsNull(paymentCompletedResponse, "The given payment complete response was valid, but should be invalid");
        }

        [TestMethod]
        public void TestThatIsValidReturnsTrueForUnderscoreInStatus()
        {
            var signingKey = new SigningKey("secret");
            var paymentCompletedResponse = PaymentCompletedResponse.CreateInstance("1", "IN_PROGRESS", "1a551027bc3cc041a56b9efa252640c76b2e5815f816dd123fa1b32b4683729e904b5fa711870b956f1d9b16c714168d129068a48f875c2f91185d6c18eccf61", signingKey);
            Assert.IsNotNull(paymentCompletedResponse);
            Assert.AreEqual("1", paymentCompletedResponse.OrderId);
            Assert.AreEqual("IN_PROGRESS", paymentCompletedResponse.Status);
        }

        [TestMethod]
        public void TestThatLettersinOrderIdIsValid()
        {
            var signingKey = new SigningKey("secret");
            var paymentCompletedResponse = PaymentCompletedResponse.CreateInstance("Test1234", "COMPLETED", "bf4f5b787d954296b9c2e15028c2311df5e31a3d94c540e361faf1d0951b7858041089d430e17730f1efd3a308881c094355f55e09b993ca53f2063859d1eb4b", signingKey);
            Assert.IsNotNull(paymentCompletedResponse);
            Assert.AreEqual("Test1234", paymentCompletedResponse.OrderId);
            Assert.AreEqual("COMPLETED", paymentCompletedResponse.Status);
        }
    }
}
