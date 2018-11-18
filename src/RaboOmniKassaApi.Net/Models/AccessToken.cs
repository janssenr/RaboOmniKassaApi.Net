using System;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for AccessToken
    /// </summary>
    [DataContract]
    public class AccessToken
    {
        [DataMember(Name = "token", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public string Token { get; set; }

        [DataMember(Name = "validUntil", EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public DateTime ValidUntil { get; set; }

        [DataMember(Name = "durationInMillis", EmitDefaultValue = false, IsRequired = true, Order = 3)]
        public int DurationInMilliseconds { get; set; }
    }
}