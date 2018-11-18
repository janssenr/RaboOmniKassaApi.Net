using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Net.Models.Request;

namespace RaboOmniKassaApi.Tests.Models.Request
{
    public static class MerchantOrderBuilder
    {
        public static MerchantOrder MakeMinimalOrder()
        {
            return new MerchantOrder
            {
                MerchantOrderId = "100",
                Amount = Money.FromDecimal("EUR", 99.99),
                MerchantReturnUrl = "http://localhost/"
            };
        }

        public static MerchantOrder MakeWithOrderItemsWithoutOptionalFields()
        {
            return new MerchantOrder
            {
                MerchantOrderId = "100",
                OrderItems = new[] { OrderItemBuilder.MakeOrderItemWithoutOptionals() },
                Amount = Money.FromDecimal("EUR", 99.99),
                MerchantReturnUrl = "http://localhost/"
            };
        }

        public static MerchantOrder MakeWithShippingDetailsWithoutOptionalFields()
        {
            return new MerchantOrder
            {
                MerchantOrderId = "100",
                ShippingDetail = AddressBuilder.MakeFullAddress(),
                Amount = Money.FromDecimal("EUR", 99.99),
                MerchantReturnUrl = "http://localhost/"
            };
        }

        public static MerchantOrder MakeWithPaymentBrandRestrictionButWithoutOtherOptionalFields(PaymentBrand paymentBrand, PaymentBrandForce paymentBrandForce)
        {
            return new MerchantOrder
            {
                MerchantOrderId = "100",
                Amount = Money.FromDecimal("EUR", 99.99),
                MerchantReturnUrl = "http://localhost/",
                PaymentBrand = paymentBrand,
                PaymentBrandForce = paymentBrandForce
            };
        }

        public static MerchantOrder MakeCompleteOrder()
        {
            var shippingDetail = AddressBuilder.MakeFullAddress();

            var billingDetail = new Address
            {
                FirstName = "Piet",
                MiddleName = "van der",
                LastName = "Stoel",
                Street = "Dorpsstraat",
                PostalCode = "4321YZ",
                City = "Bennebroek",
                CountryCode = "NL",
                HouseNumber = "9",
                HouseNumberAddition = "rood"
            };

            var customerInformation = CustomerInformationBuilder.MakeCompleteCustomerInformation();

            return new MerchantOrder
            {
                MerchantOrderId = "100",
                Description = "Order ID: " + "100",
                OrderItems = new[] { OrderItemBuilder.MakeCompleteOrderItem() },
                Amount = Money.FromDecimal("EUR", 99.99),
                ShippingDetail = shippingDetail,
                BillingDetail = billingDetail,
                CustomerInformation = customerInformation,
                Language = "NL",
                MerchantReturnUrl = "http://localhost/",
                PaymentBrand = PaymentBrand.Ideal,
                PaymentBrandForce = PaymentBrandForce.ForceOnce
            };
        }
    }
}
