using System;
using Invo.Shared.Abstractions.Events;

namespace Invo.Modules.Invoices.Core.Events
{
    public record IncomeInvoiceAdded(Guid Id, string Type, string Number, DateTime DateOfIssue,
        decimal VatAmount, decimal NetAmount, Guid SellerId) : IEvent;
}