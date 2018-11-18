using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Signing
{
    [TestClass]
    public class SignableTest
    {
        [TestMethod]
        public void TestCalculateSignatureNullIsEmptyString()
        {
            var signingKey = new SigningKey("testKey");

            var left = new SignableSpy(new List<string> { null, "foo", null, "bar", null});
            var right = new SignableSpy(new List<string> { "", "foo", "", "bar", "" });

            Assert.AreEqual(left.GetCalculatedSignature(signingKey), right.GetCalculatedSignature(signingKey));
        }

        [TestMethod]
        public void TestCalculateBase64EncodedKey()
        {
            var signingKey = new SigningKey(Convert.FromBase64String("AHwD9V0BWrG8I39BnWmcQQ=="));

            var left = new SignableSpy(new List<string> { "", "foo", "", "bar", "" });

            Assert.AreEqual(left.GetCalculatedSignature(signingKey), "de414646b65ac54f045716e5e79e3b5ab2d78db1e5e5bd24e9c2c8e87ad21c2795814d7a0a551b7fc8c3cb75cbcc62ca556078a10f4591ba48025fe9096786ee");
        }

        [TestMethod]
        public void TestCalculateBase64EncodedVsTextKey()
        {
            var leftKey = new SigningKey(Convert.FromBase64String("c2VjcmV0"));
            var left = new SignableSpy(new List<string> { "", "foo", "", "bar", "" });

            var rightKey = new SigningKey("secret");
            var right = new SignableSpy(new List<string> { "", "foo", "", "bar", "" });

            Assert.AreEqual(left.GetCalculatedSignature(leftKey), right.GetCalculatedSignature(rightKey));
        }

        [TestMethod]
        public void TestCalculateSignatureDifferentData()
        {
            var signingKey = new SigningKey("testKey");

            var left = new SignableSpy(new List<string> { "Foo", "Bar" });
            var right = new SignableSpy(new List<string> { "foo", "bar" });

            Assert.AreNotEqual(left.GetCalculatedSignature(signingKey), right.GetCalculatedSignature(signingKey));

            left = new SignableSpy(new List<string> { "foo ", "bar " });
            right = new SignableSpy(new List<string> { "foo", "bar" });

            Assert.AreNotEqual(left.GetCalculatedSignature(signingKey), right.GetCalculatedSignature(signingKey));

            left = new SignableSpy(new List<string> { " foo", " bar" });
            right = new SignableSpy(new List<string> { "foo", "bar" });

            Assert.AreNotEqual(left.GetCalculatedSignature(signingKey), right.GetCalculatedSignature(signingKey));

            left = new SignableSpy(new List<string> { "bar", "foo" });
            right = new SignableSpy(new List<string> { "foo", "bar" });

            Assert.AreNotEqual(left.GetCalculatedSignature(signingKey), right.GetCalculatedSignature(signingKey));
        }

        [TestMethod]
        public void TestCalculateSignatureDifferentKey()
        {
            var left = new SignableSpy(new List<string> { "foo", "bar" });
            var leftKey = new SigningKey("testKey1");

            var right = new SignableSpy(new List<string> { "foo", "bar" });
            var rightKey = new SigningKey("testKey2");

            Assert.AreNotEqual(left.GetCalculatedSignature(leftKey), right.GetCalculatedSignature(rightKey));
        }
    }
}
