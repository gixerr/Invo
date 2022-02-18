using System;
using Invo.Shared.Abstractions.Calculations;
using Invo.Shared.Abstractions.Currency;

namespace Invo.Shared.Infrastructure.Services
{
    class CurrencyService : ICurrencyService
    {
        private Currency UserCurrency => Currency.Pln;
        public decimal ConvertToUserCurrency(decimal amount, Currency itemCurrency, decimal exchangeRate)
        {
            if (itemCurrency.Equals(UserCurrency)) return amount;

            return amount * exchangeRate;
        }
    }
}