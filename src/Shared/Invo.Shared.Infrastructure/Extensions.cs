using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Invo.Shared.Abstractions.Calculations;
using Invo.Shared.Abstractions.Modules;
using Invo.Shared.Infrastructure.Api;
using Invo.Shared.Infrastructure.Database;
using Invo.Shared.Infrastructure.Exceptions;
using Invo.Shared.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Invo.Bootstrapper")]
namespace Invo.Shared.Infrastructure
{
    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IList<Assembly> assemblies,
            IList<IModule> modules)
        {
            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key,value) in configuration.AsEnumerable())
                {
                    if (!key.Contains(":module:enabled"))
                    {
                        continue;
                    }

                    if (!bool.Parse(value))
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }
            
            services.AddErrorHandling();
            services.AddHostedService<DatabaseInitializer>();
            services.AddControllers()
                .ConfigureApplicationPartManager(manager =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                            StringComparison.OrdinalIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    foreach (var removedPart in removedParts)
                    {
                        manager.ApplicationParts.Remove(removedPart);
                    }
                    
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

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}