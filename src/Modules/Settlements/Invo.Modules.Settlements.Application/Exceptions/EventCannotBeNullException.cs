using Invo.Shared.Abstractions.Exceptions;

namespace Invo.Modules.Settlements.Application.Exceptions
{
    public class EventCannotBeNullException : InvoException
    {
        public EventCannotBeNullException(string parameterName) : base($"{nameof(parameterName)} can't be null.")
        {
        }
    }
}