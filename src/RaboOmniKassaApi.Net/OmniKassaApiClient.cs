using System;
using System.Net;
using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.Connectors.Http;
using RaboOmniKassaApi.Net.Models.Signing;
using Environment = RaboOmniKassaApi.Net.Models.Environment;
using EndPoint = RaboOmniKassaApi.Net.EndPoints.EndPoint;

namespace RaboOmniKassaApi.Net
{
    /// <summary>
    /// Summary description for OmniKassaApiClient
    /// </summary>
    public class OmniKassaApiClient : EndPoint
    {
        public OmniKassaApiClient(string refreshToken, string signingKey, bool testMode = false) : base(
            new ApiConnector(new HttpClientRestTemplate(testMode ? Environment.Sandbox : Environment.Production),
                new InMemoryTokenProvider(refreshToken)), new SigningKey(Convert.FromBase64String(signingKey)))
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
    }
}