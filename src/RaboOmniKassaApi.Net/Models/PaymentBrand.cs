using System;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// This class houses the different types of payment brands that can be included in the MerchantOrder to restrict
    /// the payment brands that the consumer can choose from.
    /// </summary>
    [DataContract]
    public enum PaymentBrand
    {
        [EnumMember(Value = "IDEAL")]
        Ideal,
        [EnumMember(Value = "AFTERPAY")]
        Afterpay,
        [EnumMember(Value = "PAYPAL")]
        Paypal,
        [EnumMember(Value = "MASTERCARD")]
        Mastercard,
        [EnumMember(Value = "VISA")]
        Visa,
        [EnumMember(Value = "BANCONTACT")]
        Bancontact,
        [EnumMember(Value = "MAESTRO")]
        Maestro,
        [EnumMember(Value = "V_PAY")]
        VPay,
        /// <summary>
        /// The CARDS type comprises MASTERCARD, VISA, BANCONTACT, MAESTRO and V_PAY.
        /// </summary>
        [EnumMember(Value = "CARDS")]
        Cards
    }

    internal static class PaymentBrandExtensions
    {
        public static string ToFriendlyString(this PaymentBrand me)
        {
            switch (me)
            {
                case PaymentBrand.Ideal:
                    return "IDEAL";
                case PaymentBrand.Afterpay:
                    return "AFTERPAY";
                case PaymentBrand.Paypal:
                    return "PAYPAL";
                case PaymentBrand.Mastercard:
                    return "MASTERCARD";
                case PaymentBrand.Visa:
                    return "VISA";
                case PaymentBrand.Bancontact:
                    return "BANCONTACT";
                case PaymentBrand.Maestro:
                    return "MAESTRO";
                case PaymentBrand.VPay:
                    return "V_PAY";
                case PaymentBrand.Cards:
                    return "CARDS";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}