using Invo.Modules.Settlements.Domain.Repositories;
using Invo.Modules.Settlements.Infrastructure.EntityFramework;
using Invo.Modules.Settlements.Infrastructure.EntityFramework.Repositories;
using Invo.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Modules.Settlements.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
            => services
                .AddPostgres<SettlementsDbContext>()
                .AddScoped<IIncomeInvoiceRepository, PostgresIncomeInvoiceRepository>()
                .AddScoped<ICostInvoiceRepository, PostgresCostInvoiceRepository>();
    }
}