using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.Connectors.Http;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Tests.Models.Request;
using RaboOmniKassaApi.Tests.Models.Response;

namespace RaboOmniKassaApi.Tests.Connectors
{
    [TestClass]
    public class ApiConnectorTest
    {
        private AccessToken _accessToken;
        private AccessToken _expiredAccessToken;
        private AccessToken _secondAccessToken;
        private ApiConnector _connector;
        private Mock<IRestTemplate> _restTemplate;
        private Mock<TokenProvider> _tokenProvider;

        [TestInitialize]
        public void Setup()
        {
            _restTemplate = new Mock<IRestTemplate>();
            _tokenProvider = new Mock<TokenProvider>();
            _connector = new ApiConnector(_restTemplate.Object, _tokenProvider.Object);

            _accessToken = new AccessToken { Token = "accessToken1", ValidUntil = DateTime.Now.AddDays(1), DurationInMilliseconds = 1000 };
            _expiredAccessToken = new AccessToken { Token = "expiredAccessToken", ValidUntil = DateTime.Now.AddDays(-1), DurationInMilliseconds = 1000 };
            _secondAccessToken = new AccessToken { Token = "accessToken2", ValidUntil = DateTime.Now.AddDays(30), DurationInMilliseconds = 1000 };
        }

        [TestMethod]
        public void TestAnnounceOrder()
        {
            var order = MerchantOrderRequestBuilder.MakeCompleteRequest();
            var orderAsJson = JsonHelper.Serialize(order);
            var expectedResponse = MerchantOrderResponseBuilder.NewInstanceAsJson();

            PrepareTokenProviderWithAccessToken(_accessToken);
            _restTemplate.Setup(s => s.Post("order/server/api/order", orderAsJson)).Returns(expectedResponse);

            var actualResponse = _connector.AnnounceMerchantOrder(order);

            _restTemplate.Verify(v => v.SetToken(_accessToken.Token));
            _restTemplate.Verify(v => v.Post("order/server/api/order", orderAsJson));

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestMethod]
        public void TestGetAnnouncementData()
        {
            var announcement = AnnouncementResponseBuilder.NewInstance();
            var expectedResponse = MakeAnnouncementResponse(announcement.EventName);

            PrepareTokenProviderWithAccessToken(_accessToken);
            _restTemplate.Setup(s => s.Get("order/server/api/events/results/" + announcement.EventName, null)).Returns(expectedResponse);

            var actualResponse = _connector.GetAnnouncementData(announcement);

            _restTemplate.Verify(v => v.SetToken("MyJwt"));
            _restTemplate.Verify(v => v.Get("order/server/api/events/results/" + announcement.EventName, null));

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestMethod]
        public void TestExpiredTokenResultsInARetryAttemptWithADifferentToken()
        {
            var order = MerchantOrderRequestBuilder.MakeCompleteRequest();

            PrepareTokenProviderWithAccessToken(_expiredAccessToken);
            _restTemplate.Setup(s => s.Get("gatekeeper/refresh", null)).Returns(JsonHelper.Serialize(_secondAccessToken));

            _connector.AnnounceMerchantOrder(order);

            //Verify that a new access token is retrieved
            _restTemplate.Verify(v => v.Get("gatekeeper/refresh", null));
            
            //Verify that the correct token is used to call the API
            _restTemplate.Verify(v => v.SetToken(_expiredAccessToken.Token), Times.Never);
            _restTemplate.Verify(v => v.SetToken(_secondAccessToken.Token));

            //Verify that the new access token is stored in the token provider
            _tokenProvider.Verify(v => v.SetValue(TokenProvider.AccessToken, _secondAccessToken.Token));
            _tokenProvider.Verify(v => v.SetValue(TokenProvider.AccessTokenValidUntil, _secondAccessToken.ValidUntil.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")));
            _tokenProvider.Verify(v => v.SetValue(TokenProvider.AccessTokenDuration, _secondAccessToken.DurationInMilliseconds.ToString()));
        }

        [TestMethod]
        public void TestNoAccessTokenProvided()
        {
            var order = MerchantOrderRequestBuilder.MakeCompleteRequest();
            PrepareTokenProviderWithoutAccessToken();
            _restTemplate.Setup(s => s.Get("gatekeeper/refresh", null)).Returns(JsonHelper.Serialize(_secondAccessToken));

            _connector.AnnounceMerchantOrder(order);

            //Verify that a new access token is retrieved
            _restTemplate.Verify(v => v.Get("gatekeeper/refresh", null));

            //Verify that the correct token is used to call the API
            _restTemplate.Verify(v => v.SetToken(_secondAccessToken.Token));

            //Verify that the new access token is stored in the token provider
            _tokenProvider.Verify(v => v.SetValue(TokenProvider.AccessToken, _secondAccessToken.Token));
            _tokenProvider.Verify(v => v.SetValue(TokenProvider.AccessTokenValidUntil, _secondAccessToken.ValidUntil.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")));
            _tokenProvider.Verify(v => v.SetValue(TokenProvider.AccessTokenDuration, _secondAccessToken.DurationInMilliseconds.ToString()));
        }

        private void PrepareTokenProviderWithoutAccessToken()
        {
            PrepareTokenProviderWithAccessToken(null);
        }

        private void PrepareTokenProviderWithAccessToken(AccessToken accessToken)
        {
            string token = null;
            DateTime validUntil = DateTime.MinValue;
            int durationInMilliseconds = int.MaxValue;

            if (accessToken != null)
            {
                token = accessToken.Token;
                validUntil = accessToken.ValidUntil;
                durationInMilliseconds = accessToken.DurationInMilliseconds;
            }

            _tokenProvider.Setup(s => s.GetValue(TokenProvider.AccessToken)).Returns(token);
            _tokenProvider.Setup(s => s.GetValue(TokenProvider.AccessTokenValidUntil)).Returns(validUntil.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"));
            _tokenProvider.Setup(s => s.GetValue(TokenProvider.AccessTokenDuration)).Returns(durationInMilliseconds.ToString);
        }

        private string MakeAnnouncementResponse(string eventName)
        {
            if (eventName == "merchant.order.status.changed")
            {
                return MerchantOrderResponseBuilder.NewInstanceAsJson();
            }
            throw new Exception("Unknown announcement type");
        }
    }
}
