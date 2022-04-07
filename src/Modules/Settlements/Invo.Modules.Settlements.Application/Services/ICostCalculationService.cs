using System.Collections.Generic;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Models.Payments;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Application.Services
{
    public interface ICostCalculationService
    {
        IEnumerable<ICost> CalculateCostsIncludingLimitations(IEnumerable<CostInvoice> costs);
        decimal GetToSpentValue(MonthIncome monthIncome, MonthIncomeVat monthIncomeVat, TaxToPay taxToPay, VatToPay vatToPay);
    }
}