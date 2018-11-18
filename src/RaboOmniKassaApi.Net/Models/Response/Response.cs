using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Helpers;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// Summary description for Response
    /// </summary>
    [DataContract]
    public class Response : SignedResponse
    {
        public static T CreateInstance<T>(string json, SigningKey signingKey) where T : Response
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            var response = JsonHelper.Deserialize<T>(json);

            response.ValidateSignature(signingKey);
            return response;
        }
    }
}