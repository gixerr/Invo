using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;

namespace Invo.Modules.Invoices.Core.Repositories
{
    internal interface ICostInvoiceRepository
    {
        Task<CostInvoice> GetAsync(Guid id);
        Task<IReadOnlyList<CostInvoice>> BrowseAsync();
        Task<IReadOnlyList<CostInvoice>> BrowseBySellerAsync(Guid id);
        Task AddAsync(CostInvoice costInvoice);
        Task UpdateAsync(CostInvoice incomeInvoice);
        Task DeleteAsync(CostInvoice incomeInvoice);
    }
}