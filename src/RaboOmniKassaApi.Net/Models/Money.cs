using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using RaboOmniKassaApi.Net.Models.Signing;

namespace RaboOmniKassaApi.Net.Models
{
    /// <summary>
    /// Summary description for Money
    /// </summary>
    [DataContract]
    public class Money : ISignatureDataProvider
    {
        [DataMember(Name = "currency", EmitDefaultValue = false, IsRequired = true, Order = 1)]
        public string Currency { get; set; }

        [DataMember(Name = "amount", EmitDefaultValue = false, IsRequired = true, Order = 2)]
        public int Amount { get; set; }

        public List<string> GetSignatureData()
        {
            return new List<string> { Currency, Amount.ToString() };
        }

        public static Money FromCents(string currency, int amount)
        {
            var money = new Money
            {
                Currency = currency,
                Amount = amount
            };
            return money;
        }

        public static Money FromDecimal(string currency, double amount)
        {
            var roundedAmount = Math.Round(Convert.ToDecimal(amount), 2);
            var money = new Money
            {
                Currency = currency,
                Amount = Convert.ToInt32(roundedAmount * 100)
            };
            return money;
        }

        public override bool Equals(object obj)
        {
            var money = obj as Money;

            if (money == null) return false;
            if (!money.Currency.Equals(Currency)) return false;
            if (!money.Amount.Equals(Amount)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Currency != null ? Currency.GetHashCode() : 0) * 397) ^ Amount;
            }
        }

        public override string ToString()
        {
            return Amount / 100 + " " + Currency;
        }

    }
}