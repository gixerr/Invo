using System;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.Models.Payments
{
    public class VatToPay : ICalculation
    {
        public decimal Value { get; }

        public VatToPay(MonthIncomeVat monthIncomeVat, MonthCostsVat monthCostsVat)
        {
            Value = Math.Round(monthIncomeVat.Value - monthCostsVat.Value, 2);
        }
    }
}