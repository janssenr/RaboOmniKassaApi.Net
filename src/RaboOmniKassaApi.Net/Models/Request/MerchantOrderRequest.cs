using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models.Request
{
    /// <summary>
    /// Summary description for MerchantOrderRequest
    /// </summary>
    [DataContract]
    public class MerchantOrderRequest : MerchantOrder
    {
        [DataMember(Name = "timestamp", EmitDefaultValue = false, IsRequired = true)]
        public string TimestampRaw { get; set; }
        public DateTime Timestamp
        {
            get => DateTime.ParseExact(TimestampRaw, "yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);
            set => TimestampRaw = value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        }

        private MerchantOrderRequest() { }
        public MerchantOrderRequest(MerchantOrder merchantOrder, SigningKey signingKey) : base(merchantOrder)
        {
            Timestamp = DateTime.Now;
            CalculateAndSetSignature(signingKey);
        }

        public override List<string> GetSignatureData()
        {
            var signatureData = new List<string>
            {
                TimestampRaw,
                MerchantOrderId
            };
            signatureData.AddRange(Amount.GetSignatureData());
            signatureData.Add(Language);
            signatureData.Add(Description);
            signatureData.Add(MerchantReturnUrl);
            if (OrderItems != null)
            {
                signatureData.AddRange(GetOrderItemSignatureData());
            }
            if (ShippingDetail != null)
            {
                signatureData.AddRange(ShippingDetail.GetSignatureData());
            }
            if (!string.IsNullOrWhiteSpace(PaymentBrandString))
            {
                signatureData.Add(PaymentBrandString);
            }
            if (!string.IsNullOrWhiteSpace(PaymentBrandForceString))
            {
                signatureData.Add(PaymentBrandForceString);
            }
            if (CustomerInformation != null)
            {
                signatureData.AddRange(CustomerInformation.GetSignatureData());
            }
            if (BillingDetail != null)
            {
                signatureData.AddRange(BillingDetail.GetSignatureData());
            }
            return signatureData;
        }

        private List<string> GetOrderItemSignatureData()
        {
            List<string> orderItemsSignatureData = new List<string>();
            foreach (var orderItem in OrderItems)
            {
                orderItemsSignatureData.AddRange(orderItem.GetSignatureData());
            }
            return orderItemsSignatureData;
        }
    }
}
