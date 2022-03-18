using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;

namespace Invo.Modules.Invoices.Core.Repositories
{
    internal class InMemoryIncomeInvoiceRepository : IIncomeInvoiceRepository
    {
        private readonly List<IncomeInvoice> _invoices = new();

        public Task<IncomeInvoice> GetAsync(Guid id)
            => Task.FromResult(_invoices.SingleOrDefault(x => x.Id.Equals(id)));

        public async Task<IReadOnlyList<IncomeInvoice>> BrowseAsync()
        {
            await Task.CompletedTask;

            return _invoices;
        }

        public async Task<IReadOnlyList<IncomeInvoice>> BrowseBySellerAsync(Guid id)
        {
            await Task.CompletedTask;

            return _invoices.Where(x => x.SellerId.Equals(id)).ToList();
        }

        public Task AddAsync(IncomeInvoice incomeInvoice)
        {
            _invoices.Add(incomeInvoice);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(IncomeInvoice incomeInvoice)
            => Task.CompletedTask;

        public Task DeleteAsync(IncomeInvoice incomeInvoice)
        {
            _invoices.Remove(incomeInvoice);

            return Task.CompletedTask;
        }
    }
}