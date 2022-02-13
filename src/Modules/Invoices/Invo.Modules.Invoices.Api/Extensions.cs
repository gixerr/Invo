using System.Runtime.CompilerServices;
using Invo.Modules.Invoices.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Invo.Bootstrapper")]
namespace Invo.Modules.Invoices.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddInvoices(this IServiceCollection services)
        {
            services.AddCore();
            return services;
        }
    }
}