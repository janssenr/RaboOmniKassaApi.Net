using System;
using System.Text;

namespace RaboOmniKassaApi.Net.Models.Signing
{
    /// <summary>
    /// This class exists to prevent the signing key data to be printed or logged.
    /// </summary>
    public class SigningKey
    {
        private readonly byte[] _signingData;

        public SigningKey(byte[] signingData)
        {
            _signingData = signingData;
        }

        public SigningKey(string signingKey)
        {
            _signingData = Encoding.UTF8.GetBytes(signingKey);
        }

        public byte[] GetSigningData()
        {
            return _signingData;
        }
    }
}