using Microsoft.Extensions.DependencyInjection;

namespace Invo.Modules.Settlements.Application.Factories
{
    public static class Extensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services)
            => services.AddScoped<IMonthSettlementFactory, MonthSettlementFactory>();
    }
}