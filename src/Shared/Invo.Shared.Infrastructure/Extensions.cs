using System.Runtime.CompilerServices;
using Invo.Shared.Abstractions.Calculations;
using Invo.Shared.Infrastructure.Api;
using Invo.Shared.Infrastructure.Exceptions;
using Invo.Shared.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Invo.Bootstrapper")]
namespace Invo.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddErrorHandling();
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            services.AddSingleton<IGrossNetCalculationService, GrossNetCalculationService>();
            services.AddSingleton<ICurrencyService, CurrencyService>();
            
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandling();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Invo API"));
            });
            
            return app;
        }
    }
}