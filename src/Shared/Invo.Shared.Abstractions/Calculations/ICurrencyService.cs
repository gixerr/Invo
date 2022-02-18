namespace Invo.Shared.Abstractions.Calculations
{
    public interface ICurrencyService
    {
        decimal ConvertToUserCurrency(decimal amount, Currency.Currency itemCurrency, decimal exchangeRate);
    }
}