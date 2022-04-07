namespace Invo.Shared.Abstractions.Calculations
{
    public interface IGrossNetCalculationService
    {
        public decimal GetVatAmount(decimal netAmount, decimal vatRate);
        public decimal GetGrossPrice(decimal netPrice, decimal vatRate);
        public decimal GetNetAmount(decimal netPrice, decimal amount);
        decimal GetSummarisedVatAmount(decimal netPrice, decimal vatRate, decimal itemAmount);
        decimal GetGrossAmount(decimal netAmount, decimal vatRate, decimal itemAmount);
    }
}