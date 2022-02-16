using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;

namespace Invo.Modules.Invoices.Core.Repositories
{
    internal interface IInvoiceRepository
    {
        Task<Invoice> GetAsync(Guid id);
        Task<IReadOnlyList<Invoice>> BrowseAsync();
        Task<IReadOnlyList<Invoice>> BrowseBySellerAsync(Guid id);
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync(Invoice invoice);
    }
}