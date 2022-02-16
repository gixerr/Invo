using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;

namespace Invo.Modules.Invoices.Core.Repositories
{
    internal class InMemoryInvoiceRepository : IInvoiceRepository
    {
        private readonly List<Invoice> _invoices = new();

        public Task<Invoice> GetAsync(Guid id)
            => Task.FromResult(_invoices.SingleOrDefault(x => x.Id.Equals(id)));

        public async Task<IReadOnlyList<Invoice>> BrowseAsync()
        {
            await Task.CompletedTask;

            return _invoices;
        }

        public async Task<IReadOnlyList<Invoice>> BrowseBySellerAsync(Guid id)
        {
            await Task.CompletedTask;

            return _invoices.Where(x => x.SellerId.Equals(id)).ToList();
        }

        public Task AddAsync(Invoice invoice)
        {
            _invoices.Add(invoice);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Invoice invoice)
            => Task.CompletedTask;

        public Task DeleteAsync(Invoice invoice)
        {
            _invoices.Remove(invoice);

            return Task.CompletedTask;
        }
    }
}