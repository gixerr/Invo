using System;
using System.Collections.Generic;
using Invo.Modules.Settlements.Domain.Entities;

namespace Invo.Modules.Settlements.Application.Factories
{
    public interface IMonthSettlementFactory
    {
        MonthSettlement Create(IEnumerable<IncomeInvoice> incomes, IEnumerable<CostInvoice> costs, Guid CompanyId,
            int month, int year);
    }
}