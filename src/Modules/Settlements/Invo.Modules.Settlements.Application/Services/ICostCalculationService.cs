using System.Collections.Generic;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Application.Services
{
    public interface ICostCalculationService
    {
        IEnumerable<ICost> ProcessCosts(IEnumerable<CostInvoice> costs);
        decimal GetToSpentValue(decimal totalMonthIncome, decimal totalIncomeVat, decimal taxToPay, decimal vatToPay);
    }
}