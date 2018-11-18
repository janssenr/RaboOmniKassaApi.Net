using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace RaboOmniKassaApi.Net.Models.Response
{
    /// <summary>
    /// With an instance of this class you can retrieve the order statuses at the Rabobank OmniKassa.
    /// </summary>
    [DataContract]
    public class AnnouncementResponse : Response
    {
        [DataMember(Name = "poiId", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public int PoiId { get; set; }

        [DataMember(Name = "authentication", EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public string Authentication { get; set; }

        [DataMember(Name = "expiry", EmitDefaultValue = false, IsRequired = true, Order = 3)]
        public string ExpiryRaw { get; set; }
        public DateTime Expiry
        {
            get => DateTime.ParseExact(ExpiryRaw, "yyyy-MM-ddTHH:mm:ss.fffzzz", CultureInfo.InvariantCulture);
            set => ExpiryRaw = value.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");
        }

        [DataMember(Name = "eventName", EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public string EventName { get; set; }

        public override List<string> GetSignatureData()
        {
            return new List<string> { Authentication, ExpiryRaw, EventName, PoiId.ToString() };
        }
    }
}