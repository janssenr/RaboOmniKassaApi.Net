using RaboOmniKassaApi.Net.Models;

namespace RaboOmniKassaApi.Tests.Models
{
    public static class AddressBuilder
    {
        public static Address MakeFullAddress()
        {
            return new Address
            {
                FirstName = "Jan",
                MiddleName = "van",
                LastName = "Veen",
                Street = "Voorbeeldstraat",
                PostalCode = "1234AB",
                City = "Haarlem",
                CountryCode = "NL",
                HouseNumber = "5",
                HouseNumberAddition = "a"
            };
        }

        public static Address MakeSmallAddress()
        {
            return new Address
            {
                FirstName = "Jan",
                LastName = "Veen",
                Street = "Voorbeeldstraat",
                PostalCode = "1234AB",
                City = "Haarlem",
                CountryCode = "NL",
            };
        }
    }
}
