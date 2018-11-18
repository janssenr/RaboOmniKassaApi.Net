using System;
using System.Web.Configuration;
using RaboOmniKassaApi.Net;
using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Net.Models.Request;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var orderItems = new[]
        {
            new OrderItem
            {
                Id = "1",
                Name = "Test product",
                Description = "Description",
                Quantity = 1,
                Amount = Money.FromDecimal("EUR", 99.99),
                Tax = Money.FromDecimal("EUR", 20.99),
                Category = ProductType.Digital,
                VatCategory = VatCategory.High
            }
        };

        var shippingDetail = new Address
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

        var billingDetail = new Address
        {
            FirstName = "Jan",
            MiddleName = "van",
            LastName = "Veen",
            Street = "Factuurstraat",
            PostalCode = "2314AB",
            City = "Haarlem",
            CountryCode = "NL",
            HouseNumber = "15",
        };

        var customerInformation = new CustomerInformation
        {
            EmailAddress = "jan.van.veen@gmail.com",
            DateOfBirth = new DateTime(1987, 3, 20),
            Gender = "M",
            Initials = "J.M.",
            TelephoneNumber = "0204971111"
        };

        var order = new MerchantOrder
        {
            MerchantOrderId = "100",
            Description = "Order ID: 100",
            OrderItems = orderItems,
            Amount = Money.FromDecimal("EUR", 99.99),
            ShippingDetail = shippingDetail,
            BillingDetail = billingDetail,
            CustomerInformation = customerInformation,
            Language = "NL",
            MerchantReturnUrl = "http://localhost/",
            PaymentBrand = PaymentBrand.Ideal,
            PaymentBrandForce = PaymentBrandForce.ForceOnce
        };

        var refreshToken = WebConfigurationManager.AppSettings["RefreshToken"];
        var signingKey = WebConfigurationManager.AppSettings["SigningKey"];
        var testMode = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);
        var client = new OmniKassaApiClient(refreshToken, signingKey, testMode);
        var redirectUrl = client.AnnounceMerchantOrder(order);
        //Redirect user to Rabo OmniKassa
        Response.Redirect(redirectUrl, true);
    }
}