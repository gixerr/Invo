using System;
using System.Linq;
using Invo.Shared.Abstractions.Calculations;
using Invo.Shared.Abstractions.Currency;
using Invo.Shared.Infrastructure.Exceptions;

namespace Invo.Shared.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private Currency UserCurrency => Currency.Pln;
        public decimal ConvertToUserCurrency(decimal amount, Currency itemCurrency, decimal? exchangeRate)
        {
            var availableCurrencies = Enum.GetValues<Currency>();
            if (!availableCurrencies.Contains(itemCurrency))
            {
                throw new InvalidCurrencyException();
            }

            if (itemCurrency.Equals(UserCurrency)) return amount;
            
            if (exchangeRate is null)
            {
                throw new InvalidExchangeRateException();
            }
            
            return amount * decimal.Parse(exchangeRate.ToString());
        }
    }
}