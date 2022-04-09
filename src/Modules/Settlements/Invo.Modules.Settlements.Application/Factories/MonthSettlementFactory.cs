using System;
using System.Collections.Generic;
using Invo.Modules.Settlements.Application.Services;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Models.Payments;

namespace Invo.Modules.Settlements.Application.Factories
{
    public class MonthSettlementFactory : IMonthSettlementFactory
    {
        private readonly ICostCalculationService _costCalculationService;

        public MonthSettlementFactory(ICostCalculationService costCalculationService)
        {
            _costCalculationService = costCalculationService;
        }

        public MonthSettlement Create(IEnumerable<IncomeInvoice> incomes, IEnumerable<CostInvoice> costs,
            Guid companyId, int month, int year)
        {
            MonthIncome monthIncome = new(incomes);
            MonthIncomeVat monthIncomeVat = new(incomes);
            var calculatedCosts = _costCalculationService.CalculateCostsIncludingLimitations(costs);
            MonthCosts monthCosts = new(calculatedCosts);
            MonthCostsVat monthCostsVat = new(calculatedCosts);
            TaxToPay taxToPay = new(monthIncome, monthCosts);
            VatToPay vatToPay = new(monthIncomeVat, monthCostsVat);
            SocialSecurity socialSecurity = new(monthIncome, taxToPay);
            ToSpent toSpent = new(monthIncome, monthIncomeVat, taxToPay, vatToPay, socialSecurity);

            return new MonthSettlement(monthIncome, monthIncomeVat, monthCosts, monthCostsVat, taxToPay, vatToPay, socialSecurity,
                toSpent, month, year, companyId);
        }
    }
}