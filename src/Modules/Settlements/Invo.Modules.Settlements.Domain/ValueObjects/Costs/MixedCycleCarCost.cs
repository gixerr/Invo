using System;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.ValueObjects.Costs
{
    public class MixedCycleCarCost : ICost
    {
        public decimal CostAmount { get; }
        public decimal VatAmount { get; set; }

        public MixedCycleCarCost(decimal taxAmount, decimal vatAmount)
        {
            VatAmount = Math.Round(vatAmount * (decimal)0.5, 2);
            CostAmount =  Math.Round(taxAmount * (decimal)0.75, 2);
        }
    }
}