using System;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.Models.Payments
{
    public class ToSpent : ICalculation
    {
        public decimal Value { get; }

        public ToSpent(MonthIncome monthIncome, MonthIncomeVat monthIncomeVat, TaxToPay taxToPay, VatToPay vatToPay)
        {
            Value = Math.Round(monthIncome.Value + monthIncomeVat.Value - taxToPay.Value - vatToPay.Value, 2);
        }
    }
}