using System.Collections.Generic;
using System.Linq;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Domain.Models.Payments
{
    public class MonthIncome : ICalculation
    {
        public decimal Value { get; }

        public MonthIncome(IEnumerable<IncomeInvoice> incomeInvoices)
        {
            Value = incomeInvoices.Sum(x => x.NetAmount);
        }
    }
}