using Invo.Modules.Invoices.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invo.Modules.Invoices.Core.DAL.Configurations
{
    internal class InvoiceConfiguration : IEntityTypeConfiguration<IncomeInvoice>
    {
        public void Configure(EntityTypeBuilder<IncomeInvoice> builder)
        {
            
        }
    }
}