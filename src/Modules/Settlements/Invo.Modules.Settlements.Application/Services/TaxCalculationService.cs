using System;

namespace Invo.Modules.Settlements.Application.Services
{
    public class TaxCalculationService : ITaxCalculationService
    {
        public decimal TaxRate => (decimal)0.19;

        public decimal GetTaxToPay(decimal income, decimal costs)
            => Math.Round((income - costs) * TaxRate, 2);

        public decimal GetVatToPay(decimal incomeVat, decimal costVat)
            => Math.Round(incomeVat - costVat, 2);
    }
}