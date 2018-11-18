using RaboOmniKassaApi.Net.Models;
using RaboOmniKassaApi.Net.Models.Request;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Tests.Models.Request
{
    public static class MerchantOrderRequestBuilder
    {
        public static MerchantOrderRequest MakeCompleteRequest()
        {
            return new MerchantOrderRequest(MerchantOrderBuilder.MakeCompleteOrder(), GetSigningKey());
        }

        public static MerchantOrderRequest MakeWithOrderItemsWithoutOptionalFieldsRequest()
        {
            return new MerchantOrderRequest(MerchantOrderBuilder.MakeWithOrderItemsWithoutOptionalFields(), GetSigningKey());
        }

        public static MerchantOrderRequest MakeWithShippingDetailsWithoutOptionalFieldsRequest()
        {
            return new MerchantOrderRequest(MerchantOrderBuilder.MakeWithShippingDetailsWithoutOptionalFields(), GetSigningKey());
        }

        public static MerchantOrderRequest MakeWithPaymentBrandButWithoutOtherOptionalFields()
        {
            return new MerchantOrderRequest(MerchantOrderBuilder.MakeWithPaymentBrandRestrictionButWithoutOtherOptionalFields(PaymentBrand.Ideal, PaymentBrandForce.ForceOnce), GetSigningKey());
        }

        public static MerchantOrderRequest MakeMinimalRequest()
        {
            return new MerchantOrderRequest(MerchantOrderBuilder.MakeMinimalOrder(), GetSigningKey());
        }

        public static SigningKey GetSigningKey()
        {
            return new SigningKey("secret");
        }
    }
}
