using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Domain.Entities;

namespace Invo.Modules.Settlements.Domain.Repositories
{
    public interface ICostInvoiceRepository
    {
        Task<IReadOnlyList<CostInvoice>> Get(Guid buyerId, int month, int year);
        Task AddAsync(CostInvoice costInvoice);
        Task UpdateAsync(CostInvoice costInvoice);
        Task DeleteAsync(CostInvoice costInvoice);
    }
}