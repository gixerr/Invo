using System;
using Invo.Shared.Abstractions.Calculations;

namespace Invo.Shared.Infrastructure.Services
{
    internal class GrossNetCalculationService : IGrossNetCalculationService
    {
        public decimal GetVatAmount(decimal netPrice, decimal vatRate)
            => netPrice * vatRate / 100;

        public decimal GetGrossPrice(decimal netPrice, decimal vatRate)
            => Math.Round(netPrice + GetVatAmount(netPrice, vatRate), 2);

        public decimal GetNetAmount(decimal netPrice, decimal amount)
            => Math.Round(netPrice * amount, 2);
        
        public decimal GetSummarisedVatAmount(decimal netPrice, decimal vatRate, decimal itemAmount)
            => Math.Round(GetVatAmount(netPrice, vatRate) * itemAmount, 2);
        
        public decimal GetGrossAmount(decimal netPrice, decimal vatRate, decimal itemAmount)
            => Math.Round(GetNetAmount(netPrice, itemAmount) + GetSummarisedVatAmount(netPrice, vatRate, itemAmount), 2);

    }
}