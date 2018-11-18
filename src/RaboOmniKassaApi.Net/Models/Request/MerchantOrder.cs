using System;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models.Request
{
    /// <summary>
    /// Summary description for MerchantOrder
    /// </summary>
    [DataContract]
    public class MerchantOrder : SignedRequest
    {
        [DataMember(Name = "merchantOrderId", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public string MerchantOrderId { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false, IsRequired = false, Order = 2)]
        public string Description { get; set; }

        [DataMember(Name = "orderItems", EmitDefaultValue = false, IsRequired = false, Order = 3)]
        public OrderItem[] OrderItems { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public Money Amount { get; set; }

        [DataMember(Name = "shippingDetail", EmitDefaultValue = false, IsRequired = false, Order = 5)]
        public Address ShippingDetail { get; set; }

        [DataMember(Name = "billingDetail", EmitDefaultValue = false, IsRequired = false, Order = 6)]
        public Address BillingDetail { get; set; }

        [DataMember(Name = "customerInformation", EmitDefaultValue = false, IsRequired = false, Order = 7)]
        public CustomerInformation CustomerInformation { get; set; }

        [DataMember(Name = "language", EmitDefaultValue = false, IsRequired = false, Order = 8)]
        public string Language { get; set; }

        [DataMember(Name = "merchantReturnURL", EmitDefaultValue = false, IsRequired = true, Order = 9)]
        public string MerchantReturnUrl { get; set; }

        public PaymentBrand? PaymentBrand { get; set; }
        [DataMember(Name = "paymentBrand", EmitDefaultValue = false, IsRequired = false, Order = 10)]
        public string PaymentBrandString
        {
            get { return PaymentBrand.HasValue ? PaymentBrand.Value.ToFriendlyString() : null; }
            set { PaymentBrand = value != null ? (PaymentBrand)Enum.Parse(typeof(PaymentBrand), value) : (PaymentBrand?)null; }
        }

        public PaymentBrandForce? PaymentBrandForce { get; set; }
        [DataMember(Name = "paymentBrandForce", EmitDefaultValue = false, IsRequired = false, Order = 11)]
        public string PaymentBrandForceString
        {
            get { return PaymentBrandForce.HasValue ? PaymentBrandForce.Value.ToFriendlyString() : null; }
            set { PaymentBrandForce = value != null ? (PaymentBrandForce)Enum.Parse(typeof(PaymentBrandForce), value) : (PaymentBrandForce?)null; }
        }

        public MerchantOrder() { }

        public MerchantOrder(MerchantOrder merchantOrder)
        {
            MerchantOrderId = merchantOrder.MerchantOrderId;
            Description = merchantOrder.Description;
            OrderItems = merchantOrder.OrderItems;
            Amount = merchantOrder.Amount;
            ShippingDetail = merchantOrder.ShippingDetail;
            BillingDetail = merchantOrder.BillingDetail;
            CustomerInformation = merchantOrder.CustomerInformation;
            Language = merchantOrder.Language;
            MerchantReturnUrl = merchantOrder.MerchantReturnUrl;
            PaymentBrand = merchantOrder.PaymentBrand;
            PaymentBrandForce = merchantOrder.PaymentBrandForce;
        }
    }
}