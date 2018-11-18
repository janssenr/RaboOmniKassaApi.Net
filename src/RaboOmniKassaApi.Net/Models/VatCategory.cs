using System;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// This class houses the different types of VAT categories that can be assigned to a given order item.
    /// </summary>
    [DataContract]
    public enum VatCategory
    {
        [EnumMember(Value = "1")]
        High,
        [EnumMember(Value = "2")]
        Low,
        [EnumMember(Value = "3")]
        Zero,
        [EnumMember(Value = "4")]
        None
    }

    internal static class VatCategoryExtensions
    {
        public static string ToFriendlyString(this VatCategory me)
        {
            switch (me)
            {
                case VatCategory.High:
                    return "1";
                case VatCategory.Low:
                    return "2";
                case VatCategory.Zero:
                    return "3";
                case VatCategory.None:
                    return "4";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}