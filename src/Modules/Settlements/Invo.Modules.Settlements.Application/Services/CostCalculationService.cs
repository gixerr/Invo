using System;
using System.Collections.Generic;
using System.Linq;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Models.Costs;
using Invo.Modules.Settlements.Domain.Models.Payments;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Application.Services
{
    internal sealed class CostCalculationService : ICostCalculationService
    {
        public IEnumerable<ICost> CalculateCostsIncludingLimitations(IEnumerable<CostInvoice> costs)
            => costs.Select(cost => (ICost)(cost.IsCarInvoice
                ? new MixedCycleCarCost(cost.NetAmount, cost.VatAmount)
                : new UnboundedCost(cost.NetAmount, cost.VatAmount)));

        public decimal GetToSpentValue(MonthIncome monthIncome, MonthIncomeVat monthIncomeVat, TaxToPay taxToPay,
            VatToPay vatToPay)
            => Math.Round(monthIncome.Value + monthIncomeVat.Value - taxToPay.Value - vatToPay.Value, 2);
    }
}