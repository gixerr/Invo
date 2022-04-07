using System.Collections.Generic;
using System.Linq;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.Models.Payments
{
    public class MonthCostsVat : ICalculation
    {
        public decimal Value { get; }

        public MonthCostsVat(IEnumerable<ICost> costs)
        {
            Value = costs.Sum(x => x.VatAmount);
            
        }
    }
}