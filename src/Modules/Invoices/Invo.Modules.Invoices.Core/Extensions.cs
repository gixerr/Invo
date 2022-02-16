using System.Runtime.CompilerServices;
using Invo.Modules.Invoices.Core.Repositories;
using Invo.Modules.Invoices.Core.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Invo.Modules.Invoices.Api")]
namespace Invo.Modules.Invoices.Core
{
    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IInvoiceRepository, InMemoryInvoiceRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            return services;
        }
    }
}