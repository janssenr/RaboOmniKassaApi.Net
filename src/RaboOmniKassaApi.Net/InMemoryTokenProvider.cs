using System.Collections.Generic;
using RaboOmniKassaApi.Net.Connectors;

namespace RaboOmniKassaApi.Net
{
    /// <summary>
    /// Summary description for InMemoryTokenProvider
    /// </summary>
    public sealed class InMemoryTokenProvider : TokenProvider
    {
        private readonly Dictionary<string, string> _map = new Dictionary<string, string>();

        public InMemoryTokenProvider(string refreshToken)
        {
            SetValue(RefreshToken, refreshToken);
        }

        public override string GetValue(string key)
        {
            return _map[key];
        }

        public override void SetValue(string key, string value)
        {
            _map[key] = value;
        }

        public override void Flush()
        {
            //throw new NotImplementedException();
        }
    }
}