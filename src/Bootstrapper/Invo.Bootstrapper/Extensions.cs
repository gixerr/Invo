using System.Collections.Generic;
using System.Linq;
using Invo.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Bootstrapper
{
    internal static class Extensions
    {
        public static void AddModules(this IServiceCollection services, IList<IModule> modules)
            => modules.ToList().ForEach(x => x.Add(services));

        public static void UseModules(this IApplicationBuilder app, IList<IModule> modules)
            => modules.ToList().ForEach(x => x.Use(app));
    }
}