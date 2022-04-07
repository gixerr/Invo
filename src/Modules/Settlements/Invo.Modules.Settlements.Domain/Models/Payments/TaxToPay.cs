using System;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.Models.Payments
{
    public class TaxToPay : ICalculation
    {
        public decimal TaxRate => (decimal)0.19;
        public decimal Value { get; }

        public TaxToPay(MonthIncome monthIncome, MonthCosts monthCosts)
        {
            Value = Math.Round((monthIncome.Value - monthCosts.Value) * TaxRate, 2);
        }
    }
}