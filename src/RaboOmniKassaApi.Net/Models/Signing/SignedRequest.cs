using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models.Signing
{
    /// <summary>
    /// This class is responsible for calculating and setting the signature for the request it represents.
    /// </summary>
    [DataContract]
    public abstract class SignedRequest : Signable
    {
        public void CalculateAndSetSignature(SigningKey signingKey)
        {
            Signature = CalculateSignature(signingKey);
        }
    }
}