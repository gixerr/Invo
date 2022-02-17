namespace Invo.Shared.Infrastructure.Services
{
    internal class GrossNetCalculationService : IGrossNetCalculationService
    {
        public decimal GetVatAmount(decimal netPrice, decimal vatRate)
            => netPrice * vatRate / 100;

        public decimal GetGrossPrice(decimal netPrice, decimal vatRate)
            => netPrice + GetVatAmount(netPrice, vatRate);

        public decimal GetNetAmount(decimal netPrice, decimal amount)
            => netPrice * amount;
        
        public decimal GetSummarisedVatAmount(decimal netPrice, decimal vatRate, decimal itemAmount)
            => GetVatAmount(netPrice, vatRate) * itemAmount;
        
        public decimal GetGrossAmount(decimal netPrice, decimal vatRate, decimal itemAmount)
            => (GetNetAmount(netPrice, itemAmount) + GetSummarisedVatAmount(netPrice, vatRate, itemAmount));

    }
}