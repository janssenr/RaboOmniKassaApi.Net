using System.Collections.Generic;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    [DataContract]
    public class Address : ISignatureDataProvider
    {
        [DataMember(Name = "firstName", EmitDefaultValue = false, IsRequired = false, Order = 1)]
        public string FirstName { get; set; }

        [DataMember(Name = "middleName", EmitDefaultValue = false, IsRequired = false, Order = 2)]
        public string MiddleName { get; set; }

        [DataMember(Name = "lastName", EmitDefaultValue = false, IsRequired = true, Order = 3)]
        public string LastName { get; set; }

        [DataMember(Name = "street", EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public string Street { get; set; }

        [DataMember(Name = "houseNumber", EmitDefaultValue = false, IsRequired = false, Order = 5)]
        public string HouseNumber { get; set; }

        [DataMember(Name = "houseNumberAddition", EmitDefaultValue = false, IsRequired = false, Order = 6)]
        public string HouseNumberAddition { get; set; }

        [DataMember(Name = "postalCode", EmitDefaultValue = false, IsRequired = true, Order = 7)]
        public string PostalCode { get; set; }

        [DataMember(Name = "city", EmitDefaultValue = false, IsRequired = true, Order = 8)]
        public string City { get; set; }

        [DataMember(Name = "countryCode", EmitDefaultValue = false, IsRequired = true, Order = 9)]
        public string CountryCode { get; set; }

        public List<string> GetSignatureData()
        {
            var data = new List<string>
            {
                FirstName,
                MiddleName,
                LastName,
                Street
            };
            if (HouseNumber != null)
            {
                data.Add(HouseNumber);
            }
            if (HouseNumberAddition != null)
            {
                data.Add(HouseNumberAddition);
            }
            data.Add(PostalCode);
            data.Add(City);
            data.Add(CountryCode);
            return data;
        }
    }
}