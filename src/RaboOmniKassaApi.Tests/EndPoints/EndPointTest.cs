using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.EndPoints;
using RaboOmniKassaApi.Net.Models.Request;
using RaboOmniKassaApi.Net.Models.Signing;
using RaboOmniKassaApi.Tests.Models.Request;
using RaboOmniKassaApi.Tests.Models.Response;

namespace RaboOmniKassaApi.Tests.EndPoints
{
    [TestClass]
    public class EndPointTest
    {
        private EndPoint _client;
        private Mock<IConnector> _connector;
        private SigningKey _signingKey;

        [TestInitialize]
        public void Setup()
        {
            _signingKey = new SigningKey("secret");
            _connector = new Mock<IConnector>();
            _client = new EndPointWrapper(_connector.Object, _signingKey);
        }

        [TestMethod]
        public void TestAnnounceMerchantOrder()
        {
            var merchantOrder = MerchantOrderBuilder.MakeCompleteOrder();
            //var merchantOrderRequest = MerchantOrderRequestBuilder.MakeCompleteRequest();

            //_connector.Setup(s => s.AnnounceMerchantOrder(merchantOrderRequest)).Returns(MerchantOrderResponseBuilder.NewInstanceAsJson);
            _connector.Setup(s => s.AnnounceMerchantOrder(It.IsAny<MerchantOrderRequest>())).Returns(MerchantOrderResponseBuilder.NewInstanceAsJson);

            var result = _client.AnnounceMerchantOrder(merchantOrder);

            Assert.AreEqual("http://localhost/redirect/url", result);
        }

        [TestMethod]
        public void TestRetrieveAnnouncement()
        {
            var announcementResponse = AnnouncementResponseBuilder.NewInstance();
            var merchantOrderStatusResponse = MerchantOrderStatusResponseBuilder.NewInstance();
            var merchantOrderStatusResponseAsJson = MerchantOrderStatusResponseBuilder.NewInstanceAsJson();

            _connector.Setup(s => s.GetAnnouncementData(announcementResponse)).Returns(merchantOrderStatusResponseAsJson);

            var result = _client.RetrieveAnnouncement(announcementResponse);

            Assert.AreEqual(merchantOrderStatusResponse, result);
        }
    }
}
