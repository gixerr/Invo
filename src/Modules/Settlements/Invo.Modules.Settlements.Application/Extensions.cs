using Invo.Modules.Settlements.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Modules.Settlements.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddServices();
    }
}