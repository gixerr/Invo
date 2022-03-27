using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Invo.Shared.Abstractions.Events;
using Invo.Shared.Abstractions.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Invo.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var moduleSettings = GetSettings("*").ToList();
                moduleSettings.ForEach(x => configuration.AddJsonFile(x));

                var environmentSpecificModuleSettings = GetSettings($"*.{context.HostingEnvironment.EnvironmentName}").ToList();
                environmentSpecificModuleSettings.ForEach(x => configuration.AddJsonFile(x));
                
                IEnumerable<string> GetSettings(string pattern) =>
                    Directory.EnumerateFiles(context.HostingEnvironment.ContentRootPath, $"module.{pattern}.json",
                        SearchOption.AllDirectories);
            });

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services,
            IList<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();

            return services;
        }
        
        private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var registry = new ModuleRegistry();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();

            var eventTypes = types
                .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                .ToArray();

            services.AddSingleton<IModuleRegistry>(sp =>
            {
                var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                var eventDispatcherType = eventDispatcher.GetType();
                
                foreach (var type in eventTypes)
                {
                    registry.AddBroadcastFunc(type, @event =>
                        (Task) eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                            ?.MakeGenericMethod(type)
                            .Invoke(eventDispatcher, new[] {@event}));
                }

                return registry;
            });
        }
        
    }
}