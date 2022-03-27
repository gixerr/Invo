using System;
using Invo.Shared.Abstractions.Events;

namespace Invo.Modules.Settlements.Application.Events.External
{
    internal record CostInvoiceAdded(Guid Id, Guid BuyerId, bool IsCarInvoice, string Type, DateTime DateOfIssue,
        decimal VatAmount, decimal NetAmount, string Number) : IEvent;
}