using RaboOmniKassaApi.Net.Models;

namespace RaboOmniKassaApi.Tests.Models
{
    public static class OrderItemBuilder
    {
        public static OrderItem MakeCompleteOrderItem()
        {
            return new OrderItem
            {
                Id = "15",
                Name = "Name",
                Description = "Description",
                Quantity = 1,
                Amount = Money.FromCents("EUR", 100),
                Tax = Money.FromCents("EUR", 50),
                Category = ProductType.Digital,
                VatCategory = VatCategory.Low
            };
        }

        public static OrderItem MakeOrderItemWithoutOptionals()
        {
            return new OrderItem
            {
                Name = "Name",
                Description = "Description",
                Quantity = 1,
                Amount = Money.FromCents("EUR", 100),
                Category = ProductType.Digital,
            };
        }
    }
}
