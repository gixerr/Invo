using System.Collections.Generic;
using System.Reflection;
using Invo.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Shared.Infrastructure.Commands
{
    public static class Extensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.Scan(x => x.FromAssemblies(assemblies)
                .AddClasses(x => x.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces().WithScopedLifetime());

            return services;
        }
    }
}