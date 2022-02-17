using Invo.Shared.Abstractions.Exceptions;

namespace Invo.Modules.Invoices.Core.Exceptions
{
    public class InvoiceAlreadyExistException : InvoException
    {
        public InvoiceAlreadyExistException(string invoiceNumber) : base($"Invoice with number '{invoiceNumber}' was already added")
        {
        }
    }
}