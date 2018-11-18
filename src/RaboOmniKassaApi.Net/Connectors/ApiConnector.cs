using System;
using RaboOmniKassaApi.Net.Connectors.Http;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Net.Models.Request;
using RaboOmniKassaApi.Net.Models.Response;

namespace RaboOmniKassaApi.Net.Connectors
{
    /// <summary>
    /// Summary description for ApiConnector
    /// </summary>
    public class ApiConnector : IConnector
    {
        private readonly IRestTemplate _restTemplate;
        private readonly TokenProvider _tokenProvider;
        private AccessToken _accessToken;

        public ApiConnector(IRestTemplate restTemplate, TokenProvider tokenProvider)
        {
            _restTemplate = restTemplate;
            _tokenProvider = tokenProvider;
        }

        public string AnnounceMerchantOrder(MerchantOrderRequest order)
        {
            ValidateToken();
            _restTemplate.SetToken(_accessToken.Token);
            return _restTemplate.Post("order/server/api/order", JsonHelper.Serialize(order));
        }

        public string GetAnnouncementData(AnnouncementResponse announcement)
        {
            //ValidateToken();
            _restTemplate.SetToken(announcement.Authentication);
            return _restTemplate.Get("order/server/api/events/results/" + announcement.EventName);
        }

        private void ValidateToken()
        {
            try
            {
                if (_accessToken == null)
                {
                    _accessToken = _tokenProvider.GetAccessToken();
                }

                if (_accessToken == null || IsExpired(_accessToken))
                {
                    UpdateToken();
                }
            }
            catch
            {
                UpdateToken();
            }
        }

        private bool IsExpired(AccessToken token)
        {
            var validUntil = token.ValidUntil;
            var currentDate = DateTime.Now;
            //Difference in seconds
            var difference = validUntil.Subtract(currentDate).TotalMilliseconds;
            return (difference / token.DurationInMilliseconds) < 5;
        }

        private void UpdateToken()
        {
            _accessToken = RetrieveNewToken();
            _tokenProvider.SetAccessToken(_accessToken);
        }

        private AccessToken RetrieveNewToken()
        {
            var refreshToken = _tokenProvider.GetRefreshToken();
            _restTemplate.SetToken(refreshToken);
            var accessTokenJson = _restTemplate.Get("gatekeeper/refresh");
            return JsonHelper.Deserialize<AccessToken>(accessTokenJson);
        }
    }
}