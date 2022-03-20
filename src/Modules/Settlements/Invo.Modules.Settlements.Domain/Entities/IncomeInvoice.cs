using System;

namespace Invo.Modules.Settlements.Domain.Entities
{
    public class IncomeInvoice : Invoice
    {
        public Guid SellerId { get; set; }
    }
}