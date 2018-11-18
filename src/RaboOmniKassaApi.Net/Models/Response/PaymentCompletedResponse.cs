using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// Summary description for PaymentCompletedResponse
    /// </summary>
    [DataContract]
    public class PaymentCompletedResponse : SignedResponse
    {
        [DataMember(Name = "orderID", EmitDefaultValue = false, IsRequired = true)]
        public string OrderId { get; set; }

        [DataMember(Name = "status", EmitDefaultValue = false, IsRequired = true)]
        public string Status { get; set; }

        private PaymentCompletedResponse() { }

        /// <summary>
        /// Creates a new PaymentCompletedResponse instance.
        /// It sanitizes the input and validates the signature resulting in a valid instance or the value FALSE.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <param name="signature"></param>
        /// <param name="signingKey"></param>
        /// <returns>Returns null if the signature is invalid, otherwise the instance is returned.</returns>
        public static PaymentCompletedResponse CreateInstance(string orderId, string status, string signature, SigningKey signingKey)
        {
            orderId = Regex.Replace(orderId, "/[^0-9A-Za-z]/", "");
            status = Regex.Replace(status, "/[^A-Z_]/", "");
            signature = Regex.Replace(signature, "/[^0-9a-f]/", "");

            var instance = new PaymentCompletedResponse
            {
                OrderId = orderId,
                Status = status,
                Signature = signature
            };
            try
            {
                instance.ValidateSignature(signingKey);
            }
            catch (InvalidSignatureException)
            {
                return null;
            }
            return instance;
        }

        public override List<string> GetSignatureData()
        {
            return new List<string> { OrderId, Status };
        }
    }
}