using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Modules.Settlements.Domain.Entities;
using Invo.Modules.Settlements.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Repositories
{
    internal class PostgresIncomeInvoiceRepository : IIncomeInvoiceRepository
    {
        private readonly SettlementsDbContext _dbContext;
        private readonly DbSet<IncomeInvoice> _incomeInvoices;

        public PostgresIncomeInvoiceRepository(SettlementsDbContext dbContext)
        {
            _dbContext = dbContext;
            _incomeInvoices = _dbContext.IncomeInvoices;
        }

        public async Task<IncomeInvoice> GetAsync(Guid id)
            => _incomeInvoices.SingleOrDefault(x => x.Id.Equals(id));

        public async Task<IReadOnlyList<IncomeInvoice>> BrowseAsync(Guid sellerId)
            => await _incomeInvoices.Where(x => x.SellerId.Equals(sellerId)).ToListAsync();

        public async Task<IReadOnlyList<IncomeInvoice>> BrowseAsync(Guid sellerId, int month, int year)
            => await _incomeInvoices
                .Where(x =>
                    x.SellerId.Equals(sellerId) && x.DateOfIssue.Month.Equals(month) && x.DateOfIssue.Year.Equals(year))
                .ToListAsync();

        public async Task AddAsync(IncomeInvoice incomeInvoice)
        {
            await _incomeInvoices.AddAsync(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(IncomeInvoice incomeInvoice)
        {
            _incomeInvoices.Update(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(IncomeInvoice incomeInvoice)
        {
            _incomeInvoices.Remove(incomeInvoice);
            await _dbContext.SaveChangesAsync();
        }
    }
}