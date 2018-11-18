using System;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// This class provides constants for the paymentBrandForce field of a MerchantOrder. This field is used in
    /// combination with the paymentBrand field.
    /// When paymentBrandForce is set to FORCE_ONCE then the supplied paymentBrand is only enforced in the customer's first
    /// payment attempt. If the payment was not successful then the consumer is allowed to select an alternative
    /// payment brand in the Hosted Payment Pages.
    /// When paymentBrandForce is set to FORCE_ALWAYS then the consumer is not allowed to select an alternative
    /// payment brand, the customer is restricted to use the provided paymentBrand for all payment attempts.
    /// </summary>
    [DataContract]
    public enum PaymentBrandForce
    {
        [EnumMember(Value = "FORCE_ONCE")]
        ForceOnce,
        [EnumMember(Value = "FORCE_ALWAYS")]
        ForceAlways
    }

    internal static class PaymentBrandForceExtensions
    {
        public static string ToFriendlyString(this PaymentBrandForce me)
        {
            switch (me)
            {
                case PaymentBrandForce.ForceOnce:
                    return "FORCE_ONCE";
                case PaymentBrandForce.ForceAlways:
                    return "FORCE_ALWAYS";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}