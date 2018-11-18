using System;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for ProductType
    /// </summary>
    [DataContract]
    public enum ProductType
    {
        [EnumMember(Value = "PHYSICAL")]
        Physical,
        [EnumMember(Value = "DIGITAL")]
        Digital
    }

    internal static class ProductTypeExtensions
    {
        public static string ToFriendlyString(this ProductType me)
        {
            switch (me)
            {
                case ProductType.Physical:
                    return "PHYSICAL";
                case ProductType.Digital:
                    return "DIGITAL";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}