# RaboOmniKassaApi.Net

The RaboOmniKassaApi.Net library is a C# API that can be used to communicate with the [OmniKassa](https://www.rabobank.nl/bedrijven/betalen/geld-ontvangen/rabo-omnikassa/) from the [Rabobank](https://www.rabobank.nl).

[![GitHub license](https://img.shields.io/badge/license-MIT-green.svg)](https://raw.githubusercontent.com/janssenr/RaboOmniKassaApi.Net/master/LICENSE)
[![Twitter URL](https://img.shields.io/badge/twitter-follow-1da1f2.svg)](https://twitter.com/janssenr)
[![Donate](https://img.shields.io/badge/%24-donate-ff00ff.svg)](https://www.paypal.me/janssenr)

[![Build status](https://ci.appveyor.com/api/projects/status/h3v0kq65hrexbc26?svg=true)](https://ci.appveyor.com/project/janssenr/raboomnikassaapi-net/branch/master)
[![Build Status](https://travis-ci.org/janssenr/RaboOmniKassaApi.Net.svg?branch=master)](https://travis-ci.org/janssenr/RaboOmniKassaApi.Net)
[![codecov](https://codecov.io/gh/janssenr/RaboOmniKassaApi.Net/branch/master/graph/badge.svg)](https://codecov.io/gh/janssenr/RaboOmniKassaApi.Net)

## Supported Platforms
- .NET Framework (4.5.1 and higher)

## Installation

The package for this library is available on [NuGet](https://www.nuget.org/packages/RaboOmniKassaApi).

## API 

### Steps in processing Payments
The processing of payment consists of three calls from the web shop to the Rabo
OmniKassa, and one call from the Rabo OmniKassa to the web store. The API helps to
simplify these steps. The payment steps are elaborated below. For a more
high level explanation of the steps of the payment process: see the Manual Rabo
OmniKassa.

#### Preparing order
In order to make a payment request, an order must first be placed. The order contains all
the information about the payment request that the Rabo OmniKassa requires to guide
the consumer through the payment steps. In the following code blocks there are
examples to create an order. Requirements are set for the order and if this does not meet
these requirements, it will in principle be rejected.
```C#
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
```

#### Creating endpoint
In addition to an Order, an instance of an Endpoint is also required. With this Endpoint it is
possible to perform all calls to the Rabo OmniKassa. An Endpoint is instantiated with three
parameters: a refresh token, a signing key, and a testMode parameter.

The refresh token can be found in the Rabo
OmniDashboard and is a unique token, with long shelf life, used to request an access
token at the Rabo OmniKassa.

The signing key is a secret key that can be found in the Rabo OmniDashboard. This
signing key is Base64 encoded. The signing key is used to encrypt all messages of the
calls from the Rabo OmniKassa. This is an extra layer of security to show that messages
do indeed come from the web shop and not from any hackers.

The testMode parameter determines whether the Production environment or the Sandbox environment is
linked.

```C#
        var refreshToken = WebConfigurationManager.AppSettings["RefreshToken"];
        var signingKey = WebConfigurationManager.AppSettings["SigningKey"];
        var testMode = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);
        var client = new OmniKassaApiClient(refreshToken, signingKey, testMode);
```

#### Sending Order
With this Endpoint we can announce the order and the URL of the Rabo OmniKassa,
where the user can pay the order, is sent back.
```C#
        var redirectUrl = client.AnnounceMerchantOrder(order);
        //Redirect user to Rabo OmniKassa
        Response.Redirect(redirectUrl, true);
```

### Consumer pays the order
The consumer can pay the order in the Rabo OmniKassa. When this is done, the
consumer returns to the webshop via the merchantReturnUrl specified during the
preparation of the order.

To check whether the signature is authentic, the following code can be used. It is also
advisable to use the PaymentCompletedResponse class so that the order_id, status, and
signature are cleaned if they contain invalid / unsafe values.
```C#
        var orderId = Request.QueryString["order_id"];
        var status = Request.QueryString["status"];
        var signature = Request.QueryString["signature"];
        var signingKey = new SigningKey(Convert.FromBase64String(WebConfigurationManager.AppSettings["SigningKey"]));
        var paymentCompletedResponse = PaymentCompletedResponse.CreateInstance(orderId, status, signature, signingKey);
        if (paymentCompletedResponse == null)
        {
            throw new Exception("The payment completed response was invalid.");
        }

        // Use these variables instead of using the URL parameters ($orderId and $status). Input validation has been performed on these values.
        var validatedMerchantOrderId = paymentCompletedResponse.OrderId;
        var validatedStatus = paymentCompletedResponse.Status;
        // ... complete payment
```
If the answer is invalid, it is advisable to refer the user to an error page but to consider
the order as 'open'. At a later stage the actual status of the order can be retrieved by
means of notifications.

### Receive updates about orders
All status transitions of an order are kept by the Rabo OmniKassa so that they can be
offered to the webshop with the help of notifications. The Rabo OmniKassa does this by
sending a notification to the webhookUrl via a POST request with the notification as
JSON included. The webhookUrl can be configured in the Rabo OmniDashboard.

With this JSON it is possible to build up an AnnouncementResponse with which the
actual information of the orders can be retrieved.
```C#
        string jsonData = new StreamReader(Request.InputStream).ReadToEnd();
        if (string.IsNullOrWhiteSpace(jsonData))
        {
            throw new Exception("Invalid notification call.");
        }

        var refreshToken = WebConfigurationManager.AppSettings["RefreshToken"];
        var signingKey = WebConfigurationManager.AppSettings["SigningKey"];
        var testMode = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);
        var client = new OmniKassaApiClient(refreshToken, signingKey, testMode);

        var announcementResponse = RaboOmniKassaApi.Net.Models.Response.Response.CreateInstance<AnnouncementResponse>(jsonData, new SigningKey(Convert.FromBase64String(signingKey)));

        bool moreResultsAvailable;
        do
        {
            var response = client.RetrieveAnnouncement(announcementResponse);

            //... Update the order statuses	

            moreResultsAvailable = response.MoreOrderResultsAvailable;

        } while (moreResultsAvailable);
```
At this step the signature of the notification is also checked. If this goes wrong, it could
be that, for example, a new signing key has been generated in the Rabo OmniDashboard
and it has not yet been processed by all systems. The Rabo OmniKassa then tries again
at a later time.