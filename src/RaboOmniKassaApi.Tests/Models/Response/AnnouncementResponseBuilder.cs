using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Response;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Response
{
    public static class AnnouncementResponseBuilder
    {
        public static AnnouncementResponse NewInstance()
        {
            return Net.Models.Response.Response.CreateInstance<AnnouncementResponse>(GetTestData(1000), GetSigningKey());
        }

        public static AnnouncementResponse InvalidSignatureInstance()
        {
            return Net.Models.Response.Response.CreateInstance<AnnouncementResponse>(GetTestData(100), GetSigningKey());
        }

        private static string GetTestData(int poiId)
        {
            var announcementResponse = new AnnouncementResponse
            {
                PoiId = poiId,
                Authentication = "MyJwt",
                ExpiryRaw = "1970-01-01T00:00:00.000+02:00",
                EventName = "merchant.order.status.changed",
                Signature = "ec0f64d23b91debd1249ee56b1b67540bf1720760cb23a9a286acc222724a8d15c7e33f193710e7e0322ced44d066f8cc6a2fdbd9398f36fb1f0a277431034aa"
            };
            return JsonHelper.Serialize(announcementResponse);
        }

        public static SigningKey GetSigningKey()
        {
            return new SigningKey("secret");
        }
    }
}
