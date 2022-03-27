using Invo.Shared.Abstractions.Exceptions;

namespace Invo.Shared.Infrastructure.Exceptions
{
    internal class InvalidExchangeRateException : InvoException
    {
        public InvalidExchangeRateException() : base("Exchange rate can't be null.")
        {
        }
    }
}