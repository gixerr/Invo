using System;
using Invo.Shared.Abstractions.Exceptions;

namespace Invo.Modules.Settlements.Application.Exceptions
{
    public class InvoiceNotFoundException : InvoException
    {
        public InvoiceNotFoundException(Guid id) : base($"Invoice with ID: '{id}' was not found")
        {
        }
    }
}