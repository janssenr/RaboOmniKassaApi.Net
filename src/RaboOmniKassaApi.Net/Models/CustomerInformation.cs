using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for CustomerInformation
    /// </summary>
    [DataContract]
    public class CustomerInformation : ISignatureDataProvider
    {
        [DataMember(Name = "emailAddress", EmitDefaultValue = false, IsRequired = false, Order = 1)]
        public string EmailAddress { get; set; }

        [DataMember(Name = "dateOfBirth", EmitDefaultValue = false, IsRequired = false, Order = 2)]
        public string DateOfBirthRaw { get; set; }
        public DateTime? DateOfBirth
        {
            get => !string.IsNullOrWhiteSpace(DateOfBirthRaw) ? DateTime.ParseExact(DateOfBirthRaw, "dd-MM-yyyy", CultureInfo.InvariantCulture) : (DateTime?)null;
            set => DateOfBirthRaw = value?.ToString("dd-MM-yyyy");
        }

        [DataMember(Name = "gender", EmitDefaultValue = false, IsRequired = false, Order = 3)]
        public string Gender { get; set; }

        [DataMember(Name = "initials", EmitDefaultValue = false, IsRequired = false, Order = 4)]
        public string Initials { get; set; }

        [DataMember(Name = "telephoneNumber", EmitDefaultValue = false, IsRequired = false, Order = 5)]
        public string TelephoneNumber { get; set; }

        public List<string> GetSignatureData()
        {
            return new List<string>
            {
                EmailAddress,
                DateOfBirthRaw,
                Gender,
                Initials,
                TelephoneNumber
            };
        }
    }
}