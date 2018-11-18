using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// Once an order is announced, an instance of this object will be returned.
    /// You can use this object to retrieve the redirect URL to which the customer should be redirected.
    /// </summary>
    [DataContract]
    public class MerchantOrderResponse : Response
    {
        [DataMember(Name = "redirectUrl", EmitDefaultValue = false, IsRequired = true)]
        public string RedirectUrl { get; set; }

        public override List<string> GetSignatureData()
        {
            return new List<string> { RedirectUrl };
        }
    }
}