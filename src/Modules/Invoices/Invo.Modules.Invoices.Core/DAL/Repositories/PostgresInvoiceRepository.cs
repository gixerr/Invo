using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Invoices.Core.Entities;
using Invo.Modules.Invoices.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Invoices.Core.DAL.Repositories
{
    internal class PostgresInvoiceRepository : IInvoiceRepository
    {
        private readonly InvoicesDbContext _dbContext;
        private readonly DbSet<Invoice> _invoices;

        public PostgresInvoiceRepository(InvoicesDbContext dbContext)
        {
            _dbContext = dbContext;
            _invoices = _dbContext.Invoices;
        }

        public async Task<Invoice> GetAsync(Guid id)
            => await _invoices.Include(x => x.Items).SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<IReadOnlyList<Invoice>> BrowseAsync()
            => await _invoices.ToListAsync();

        public async Task<IReadOnlyList<Invoice>> BrowseBySellerAsync(Guid id)
            => await _invoices.Where(x => x.SellerId.Equals(id)).ToListAsync();

        public async Task AddAsync(Invoice invoice)
        {
            await _invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _invoices.Update(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Invoice invoice)
        {
            _invoices.Remove(invoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}