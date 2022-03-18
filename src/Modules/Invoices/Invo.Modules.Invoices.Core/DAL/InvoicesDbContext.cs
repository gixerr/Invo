using Invo.Modules.Invoices.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invo.Modules.Invoices.Core.DAL
{
    internal class InvoicesDbContext : DbContext
    {
        public DbSet<IncomeInvoice> IncomeInvoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("invoices");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}