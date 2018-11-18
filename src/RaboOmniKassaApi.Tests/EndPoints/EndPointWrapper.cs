using RaboOmniKassaApi.Net.Connectors;
using RaboOmniKassaApi.Net.EndPoints;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.EndPoints
{
    public class EndPointWrapper : EndPoint
    {
        public EndPointWrapper(IConnector connector, SigningKey signingKey) : base(connector, signingKey) { }
    }
}
