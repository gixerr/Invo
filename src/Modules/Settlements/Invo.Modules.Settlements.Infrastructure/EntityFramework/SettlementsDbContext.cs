using Invo.Modules.Settlements.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework
{
    internal class SettlementsDbContext : DbContext
    {
        public DbSet<IncomeInvoice> IncomeInvoices { get; set; }
        public DbSet<CostInvoice> CostInvoices { get; set; }

        public SettlementsDbContext(DbContextOptions<SettlementsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("settlements");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}