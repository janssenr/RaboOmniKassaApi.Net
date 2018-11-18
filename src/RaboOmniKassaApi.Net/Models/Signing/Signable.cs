using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Helpers;

namespace RaboOmniKassaApi.Net.Models.Signing
{
    /// <summary>
    /// Summary description for Signable
    /// </summary>
    [DataContract]
    public abstract class Signable : ISignatureDataProvider
    {
        [DataMember(Name = "signature", EmitDefaultValue = false, IsRequired = true)]
        public string Signature { get; set; }

        protected string CalculateSignature(SigningKey signingKey)
        {
            var signatureData = GetSignatureData();
            var preparedSignatureData = Join(signatureData);
            return HashHelper.GetHash(HashHelper.HashType.HmacSha512, preparedSignatureData, signingKey.GetSigningData());
        }

        public virtual List<string> GetSignatureData()
        {
            throw new NotImplementedException();
        }

        private string Join(List<string> input)
        {
            return string.Join(",", input);
        }
    }
}