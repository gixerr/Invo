using Invo.Shared.Abstractions.Exceptions;

namespace Invo.Shared.Infrastructure.Exceptions
{
    internal class InvalidCurrencyException : InvoException
    {
        public InvalidCurrencyException() : base("Selected currency is unavailable.")
        {
        }
    }
}