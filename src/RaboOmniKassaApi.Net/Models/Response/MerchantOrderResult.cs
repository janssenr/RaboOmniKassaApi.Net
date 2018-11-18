using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// This object contains information, like status and paid amount, of an order.
    /// </summary>
    [DataContract]
    public class MerchantOrderResult : ISignatureDataProvider
    {
        [DataMember(Name = "poiId", EmitDefaultValue = false, IsRequired = true)]
        public int PoiId { get; set; }

        [DataMember(Name = "merchantOrderId", EmitDefaultValue = false, IsRequired = true)]
        public string MerchantOrderId { get; set; }

        [DataMember(Name = "omnikassaOrderId", EmitDefaultValue = false, IsRequired = true)]
        public string OmnikassaOrderId { get; set; }

        [DataMember(Name = "orderStatus", EmitDefaultValue = false, IsRequired = true)]
        public string OrderStatus { get; set; }

        [DataMember(Name = "orderStatusDateTime", EmitDefaultValue = false, IsRequired = true)]
        public string OrderStatusDateTimeRaw { get; set; }
        public DateTime OrderStatusDateTime
        {
            get => DateTime.ParseExact(OrderStatusDateTimeRaw, "yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);
            set => OrderStatusDateTimeRaw = value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        }

        [DataMember(Name = "errorCode", EmitDefaultValue = false, IsRequired = true)]
        public string ErrorCode { get; set; }

        [DataMember(Name = "paidAmount", EmitDefaultValue = false, IsRequired = true)]
        public Money PaidAmount { get; set; }

        [DataMember(Name = "totalAmount", EmitDefaultValue = false, IsRequired = true)]
        public Money TotalAmount { get; set; }

        public List<string> GetSignatureData()
        {
            var data = new List<string>
            {
                MerchantOrderId,
                OmnikassaOrderId,
                PoiId.ToString(),
                OrderStatus,
                OrderStatusDateTimeRaw,
                ErrorCode,
            };
            data.AddRange(PaidAmount.GetSignatureData());
            data.AddRange(TotalAmount.GetSignatureData());
            return data;
        }

        public override bool Equals(object obj)
        {
            var merchantOrderResult = obj as MerchantOrderResult;

            if (merchantOrderResult == null) return false;
            if (!merchantOrderResult.PoiId.Equals(PoiId)) return false;
            if (!merchantOrderResult.MerchantOrderId.Equals(MerchantOrderId)) return false;
            if (!merchantOrderResult.OmnikassaOrderId.Equals(OmnikassaOrderId)) return false;
            if (!merchantOrderResult.OrderStatus.Equals(OrderStatus)) return false;
            if (!merchantOrderResult.OrderStatusDateTimeRaw.Equals(OrderStatusDateTimeRaw)) return false;
            if (!merchantOrderResult.ErrorCode.Equals(ErrorCode)) return false;
            if (!merchantOrderResult.PaidAmount.Equals(PaidAmount)) return false;
            if (!merchantOrderResult.TotalAmount.Equals(TotalAmount)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PoiId;
                hashCode = (hashCode * 397) ^ (MerchantOrderId != null ? MerchantOrderId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OmnikassaOrderId != null ? OmnikassaOrderId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OrderStatus != null ? OrderStatus.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OrderStatusDateTimeRaw != null ? OrderStatusDateTimeRaw.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ErrorCode != null ? ErrorCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PaidAmount != null ? PaidAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TotalAmount != null ? TotalAmount.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}