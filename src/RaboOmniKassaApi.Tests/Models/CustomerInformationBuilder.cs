using RaboOmniKassaApi.Net.Models;

namespace RaboOmniKassaApi.Tests.Models
{
    public static class CustomerInformationBuilder
    {
        public static CustomerInformation MakeCompleteCustomerInformation()
        {
            return new CustomerInformation
            {
                EmailAddress = "jan.van.veen@gmail.com",
                DateOfBirthRaw = "20-03-1987",
                Gender = "M",
                Initials = "J.M.",
                TelephoneNumber = "0204971111"
            };
        }

        public static CustomerInformation MakeCustomerInformationWithoutOptionals()
        {
            return new CustomerInformation();
        }
    }
}
