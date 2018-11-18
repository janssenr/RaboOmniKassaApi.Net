using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    [TestClass]
    public class AnnouncementResponseTest
    {
        [TestMethod]
        public void TestThatObjectIsCorrectlyConstructed()
        {
            var response = AnnouncementResponseBuilder.NewInstance();

            Assert.AreEqual(1000, response.PoiId);
            Assert.AreEqual("MyJwt", response.Authentication);
            Assert.AreEqual("1970-01-01T00:00:00.000+02:00", response.ExpiryRaw);
            Assert.AreEqual("merchant.order.status.changed", response.EventName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidSignatureException))]
        public void TestThatInvalidSignatureExceptionIsThrownWhenTheSignaturesDoNotMatch()
        {
            var response = AnnouncementResponseBuilder.InvalidSignatureInstance();
            //response.ValidateSignature(AnnouncementResponseBuilder.GetSigningKey());
        }
    }
}
