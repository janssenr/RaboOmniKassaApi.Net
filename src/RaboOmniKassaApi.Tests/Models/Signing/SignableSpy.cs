using System.Collections.Generic;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Signing
{
    public class SignableSpy : Signable
    {
        private readonly List<string> _signatureData;

        public SignableSpy(List<string> signatureData)
        {
            _signatureData = signatureData;
        }

        public string GetCalculatedSignature(SigningKey signingKey)
        {
            return CalculateSignature(signingKey);
        }

        public override List<string> GetSignatureData()
        {
            return _signatureData;
        }
    }
}
