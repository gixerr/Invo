using Invo.Shared.Abstractions.Exceptions;

namespace Invo.Modules.Settlements.Application.Exceptions
{
    public class InvoiceAlreadyExistsException : InvoException
    {
        public InvoiceAlreadyExistsException(string invoiceNumber) : base($"Invoice with number '{invoiceNumber}' was already added")
        {
        }
    }
}