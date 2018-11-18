using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for OrderItem
    /// </summary>
    [DataContract]
    public class OrderItem : ISignatureDataProvider
    {
        [DataMember(Name = "id", EmitDefaultValue = false, IsRequired = false, Order = 1)]
        public string Id { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public string Name { get; set; }

        [DataMember(Name = "description", EmitDefaultValue = false, IsRequired = false, Order = 3)]
        public string Description { get; set; }

        [DataMember(Name = "quantity", EmitDefaultValue = false, IsRequired = true, Order = 4)]
        public int Quantity { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false, IsRequired = true, Order = 5)]
        public Money Amount { get; set; }

        [DataMember(Name = "tax", EmitDefaultValue = false, IsRequired = false, Order = 6)]
        public Money Tax { get; set; }

        public ProductType? Category { get; set; }
        [DataMember(Name = "category", EmitDefaultValue = false, IsRequired = true, Order = 7)]
        public string CategoryString
        {
            get { return Category.HasValue ? Category.Value.ToFriendlyString() : null; }
            set { Category = value != null ? (ProductType)Enum.Parse(typeof(ProductType), value) : (ProductType?)null; }
        }

        public VatCategory? VatCategory { get; set; }
        [DataMember(Name = "vatCategory", EmitDefaultValue = false, IsRequired = false, Order = 8)]
        public string VatCategoryString
        {
            get { return VatCategory.HasValue ? VatCategory.Value.ToFriendlyString() : null; }
            set { VatCategory = value != null ? (VatCategory)Enum.Parse(typeof(VatCategory), value) : (VatCategory?)null; }
        }

        public List<string> GetSignatureData()
        {
            var data = new List<string>();
            if (Id != null)
            {
                data.Add(Id);
            }
            data.Add(Name);
            data.Add(Description);
            data.Add(Quantity.ToString());
            data.AddRange(Amount.GetSignatureData());
            data.AddRange(Tax != null ? Tax.GetSignatureData() : new List<string> { null });
            data.Add(!string.IsNullOrWhiteSpace(CategoryString) ? CategoryString : null);
            if (!string.IsNullOrWhiteSpace(VatCategoryString))
            {
                data.Add(VatCategoryString);
            }
            return data;
        }
    }
}