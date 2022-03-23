using System.Collections.Generic;
using System.Reflection;
using Invo.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Shared.Infrastructure.Events
{
    public static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.Scan(x => x.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
        
    }
}