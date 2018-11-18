using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models.Signing
{
    /// <summary>
    /// This class is responsible for validating the signatures.
    /// </summary>
    [DataContract]
    public abstract class SignedResponse : Signable
    {
        /// <summary>
        /// Validate the signature of this response.
        /// </summary>
        /// <param name="signingKey">the merchant signing key</param>
        public void ValidateSignature(SigningKey signingKey)
        {
            if (Signature != CalculateSignature(signingKey))
            {
                throw new InvalidSignatureException();
            }
        }
    }
}