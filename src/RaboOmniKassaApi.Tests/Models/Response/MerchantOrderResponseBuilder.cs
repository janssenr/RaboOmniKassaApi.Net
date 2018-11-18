using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    public static class MerchantOrderResponseBuilder
    {
        public static MerchantOrderResponse NewInstance()
        {
            return Net.Models.Response.Response.CreateInstance<MerchantOrderResponse>(GetData("http://localhost/redirect/url"), GetSigningKey());
        }
        public static MerchantOrderResponse InvalidSignatureInstance()
        {
            return Net.Models.Response.Response.CreateInstance<MerchantOrderResponse>(GetData("http://some.other.host/redirect/url"), GetSigningKey());
        }

        public static string NewInstanceAsJson()
        {
            return GetData("http://localhost/redirect/url");
        }

        private static string GetData(string redirectUrl)
        {
            var merchantOrderResponse = new MerchantOrderResponse
            {
                RedirectUrl = redirectUrl,
                Signature = "2b997c845b6f83d8cf90c5c7f0121e727729f9325d0b954ac7ad9e8a4f3cba26931293e96f973f19dffe86628d1312a2c4ccd6dbaae8fd78b30fb0122fbcf8ee"
            };
            return JsonHelper.Serialize(merchantOrderResponse);
        }

        public static SigningKey GetSigningKey()
        {
            return new SigningKey("secret");
        }
    }
}
