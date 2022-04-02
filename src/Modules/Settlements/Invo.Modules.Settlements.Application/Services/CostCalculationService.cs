using System;
using System.Collections.Generic;
using System.Linq;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.ValueObjects;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Modules.Settlements.Application.Services
{
    internal sealed class CostCalculationService : ICostCalculationService
    {
        public IEnumerable<ICost> ProcessCosts(IEnumerable<CostInvoice> costs)
            => costs.Select(cost => (ICost)(cost.IsCarInvoice
                ? new MixedCycleCarCost(cost.NetAmount, cost.VatAmount)
                : new UnboundedCost(cost.NetAmount, cost.VatAmount)));

        public decimal GetToSpentValue(decimal totalMonthIncome, decimal totalIncomeVat, decimal taxToPay, decimal vatToPay)
            => Math.Round(totalMonthIncome + totalIncomeVat - taxToPay - vatToPay, 2);
    }
}