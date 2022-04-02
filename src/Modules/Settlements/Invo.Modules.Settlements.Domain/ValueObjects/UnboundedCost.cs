using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.ValueObjects
{
    public class UnboundedCost : ICost
    {
        public decimal CostAmount { get; }
        public decimal VatAmount { get; set; }

        public UnboundedCost(decimal taxAmount, decimal vatAmount)
        {
            CostAmount = taxAmount;
            VatAmount = vatAmount;
        }
    }
}