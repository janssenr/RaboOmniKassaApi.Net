using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    public static class MerchantOrderStatusResponseBuilder
    {
        public static MerchantOrderStatusResponse NewInstance()
        {
            return Net.Models.Response.Response.CreateInstance<MerchantOrderStatusResponse>(GetData(true), GetSigningKey());
        }
        public static MerchantOrderStatusResponse InvalidSignatureInstance()
        {
            return Net.Models.Response.Response.CreateInstance<MerchantOrderStatusResponse>(GetData(false), GetSigningKey());
        }

        public static string NewInstanceAsJson()
        {
            return GetData(true);
        }

        private static string GetData(bool moreOrderResultsAvailable)
        {
            var merchantOrderResponse = new MerchantOrderStatusResponse
            {
                MoreOrderResultsAvailable = moreOrderResultsAvailable,
                OrderResults = new[]
                {
                    new MerchantOrderResult
                    {
                        PoiId = 1000,
                        MerchantOrderId = "10",
                        OmnikassaOrderId = "1",
                        OrderStatus = "CANCELLED",
                        OrderStatusDateTimeRaw = "1970-01-01T00:00:00.000+02:00",
                        ErrorCode = "666",
                        PaidAmount = new Money
                        {
                            Currency = "EUR",
                            Amount = 100
                        },
                        TotalAmount = new Money
                        {
                            Currency = "EUR",
                            Amount = 100
                        }
                    }
                },
                Signature = "ddad03e536719f988a46f7edaf5808446838109b1644a13cef9e0e0f74825a70df618d325f8ce6eb09d629a70b6a0728f99fb8e85f249ca76636d7c13d54b841"
            };
            return JsonHelper.Serialize(merchantOrderResponse);
        }

        public static SigningKey GetSigningKey()
        {
            return new SigningKey("secret");
        }
    }
}
