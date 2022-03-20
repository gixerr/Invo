using System;

namespace Invo.Modules.Settlements.Domain.Entities
{
    public class CostInvoice : Invoice
    {
        public Guid BuyerId { get; set; }
        public bool IsCarInvoice { get; set; }
    }
}