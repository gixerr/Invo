using System;
using Invo.Shared.Abstractions.Events;

namespace Invo.Modules.Settlements.Application.Events.External
{
    internal record IncomeInvoiceDeleted(Guid Id) : IEvent;
}