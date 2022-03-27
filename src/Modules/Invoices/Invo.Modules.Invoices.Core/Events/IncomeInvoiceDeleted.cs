using System;
using Invo.Shared.Abstractions.Events;

namespace Invo.Modules.Invoices.Core.Events
{
    internal record IncomeInvoiceDeleted(Guid Id) : IEvent;
}