using System.Runtime.CompilerServices;
using Invo.Modules.Invoices.Core.DAL;
using Invo.Modules.Invoices.Core.DAL.Repositories;
using Invo.Modules.Invoices.Core.Repositories;
using Invo.Modules.Invoices.Core.Services;
using Invo.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Invo.Modules.Invoices.Api")]
namespace Invo.Modules.Invoices.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<InvoicesDbContext>();
            services.AddScoped<IIncomeInvoiceRepository, PostgresIncomeInvoiceRepository>();
            services.AddScoped<ICostInvoiceRepository, PostgresCostInvoiceRepository>();
            services.AddScoped<IIncomeInvoiceService, IncomeInvoiceService>();
            services.AddScoped<ICostInvoiceService, CostInvoiceService>();
            services.AddScoped<IInvoiceItemsService, InvoiceItemsService>();
            return services;
        }
    }
}