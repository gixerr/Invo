namespace Invo.Modules.Settlements.Application.Services
{
    public interface ITaxCalculationService
    {
        decimal TaxRate { get; }
        decimal GetTaxToPay(decimal income, decimal costs);
        decimal GetVatToPay(decimal incomeVat, decimal costVat);
    }
}