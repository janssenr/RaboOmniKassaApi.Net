using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// Summary description for MerchantOrderStatusResponse
    /// </summary>
    [DataContract]
    public class MerchantOrderStatusResponse : Response
    {
        [DataMember(Name = "moreOrderResultsAvailable", EmitDefaultValue = true, IsRequired = true)]
        public bool MoreOrderResultsAvailable { get; set; }

        [DataMember(Name = "orderResults", EmitDefaultValue = false, IsRequired = true)]
        public MerchantOrderResult[] OrderResults { get; set; }

        public override List<string> GetSignatureData()
        {
            var data = new List<string>
            {
                MoreOrderResultsAvailable ? "true" : "false",
            };
            data.AddRange(GetOrderResultsSignatureData());
            return data;
        }

        private List<string> GetOrderResultsSignatureData()
        {
            var signatureData = new List<string>();
            foreach (var orderResult in OrderResults)
            {
                signatureData.AddRange(orderResult.GetSignatureData());
            }
            return signatureData;
        }

        public override bool Equals(object obj)
        {
            var merchantOrderStatusResponse = obj as MerchantOrderStatusResponse;

            if (merchantOrderStatusResponse == null) return false;
            if (!merchantOrderStatusResponse.MoreOrderResultsAvailable.Equals(MoreOrderResultsAvailable)) return false;
            if (!merchantOrderStatusResponse.OrderResults.SequenceEqual(OrderResults)) return false;
            return true;
        }

        protected bool Equals(MerchantOrderStatusResponse other)
        {
            return MoreOrderResultsAvailable == other.MoreOrderResultsAvailable && Equals(OrderResults, other.OrderResults);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (MoreOrderResultsAvailable.GetHashCode() * 397) ^ (OrderResults != null ? OrderResults.GetHashCode() : 0);
            }
        }
    }
}