namespace Invo.Shared.Abstractions.Calculations
{
    public interface ICost
    {
        public decimal CostAmount { get; }
        public decimal VatAmount { get; }
    }
}