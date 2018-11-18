using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.Connectors.Http;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Request;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.EndPoints
{
    public class EndPoint
    {
        private readonly IConnector _connector;
        private readonly SigningKey _signingKey;

        protected EndPoint(IConnector connector, SigningKey signingKey)
        {
            _connector = connector;
            _signingKey = signingKey;
        }

        public static EndPoint CreateInstance(string baseUrl, SigningKey signingKey, TokenProvider tokenProvider)
        {
            return new EndPoint(new ApiConnector(new HttpClientRestTemplate(baseUrl), tokenProvider), signingKey);
        }

        public string AnnounceMerchantOrder(MerchantOrder merchantOrder)
        {
            var request = new MerchantOrderRequest(merchantOrder, _signingKey);
            var responseAsJson = _connector.AnnounceMerchantOrder(request);
            var response = Response.CreateInstance<MerchantOrderResponse>(responseAsJson, _signingKey);
            return response.RedirectUrl;
        }

        public MerchantOrderStatusResponse RetrieveAnnouncement(AnnouncementResponse announcementResponse)
        {
            var announcementDataAsJson = _connector.GetAnnouncementData(announcementResponse);
            var merchantOrderStatusResponse = JsonHelper.Deserialize<MerchantOrderStatusResponse>(announcementDataAsJson);
            merchantOrderStatusResponse.ValidateSignature(_signingKey);
            return merchantOrderStatusResponse;
        }
    }
}
