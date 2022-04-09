using System;
using Invo.Modules.Settlements.Domain.Models.Payments;

namespace Invo.Modules.Settlements.Domain.Entities
{
    public class MonthSettlement
    {
        public Guid Id { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }
        public Guid CompanyId { get; private set; }
        public decimal Income { get; private set; }
        public decimal IncomeVat { get; private set; }
        public decimal Costs { get; private set; }
        public decimal CostsVat { get; private set; }
        public decimal TaxToPay { get; private set; }
        public decimal VatToPay { get; private set; }
        public decimal SocialSecurity { get; private set; }
        public decimal ToSpent { get; private set; }

        public MonthSettlement(MonthIncome monthIncome, MonthIncomeVat monthIncomeVat, MonthCosts monthCosts,
            MonthCostsVat monthCostsVat, TaxToPay taxToPay, VatToPay vatToPay, SocialSecurity socialSecurity, ToSpent toSpent, int month, int year, Guid companyId)
        {
            Id = Guid.NewGuid();
            Month = month;
            Year = year;
            CompanyId = companyId;
            Income = monthIncome.Value;
            IncomeVat = monthIncomeVat.Value;
            Costs = monthCosts.Value;
            CostsVat = monthCostsVat.Value;
            TaxToPay = taxToPay.Value;
            VatToPay = vatToPay.Value;
            ToSpent = toSpent.Value;
            SocialSecurity = socialSecurity.Value;
        }

        private MonthSettlement()
        {
        }
    }
}