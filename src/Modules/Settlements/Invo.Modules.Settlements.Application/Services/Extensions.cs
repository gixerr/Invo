using Microsoft.Extensions.DependencyInjection;

namespace Invo.Modules.Settlements.Application.Services
{
    public static class Extensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services.AddScoped<ICostCalculationService, CostCalculationService>();
    }
}