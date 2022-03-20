using Invo.Modules.Settlements.Application;
using Invo.Modules.Settlements.Domain;
using Invo.Modules.Settlements.Infrastructure;
using Invo.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Modules.Settlements.Api
{
    public class SettlementsModule : IModule
    {
        public const string BasePath = "settlements-module";
        public string Name { get; } = "Settlements";
        public string Path => BasePath;
        public void Register(IServiceCollection services)
        {
            services
                .AddDomain()
                .AddApplication()
                .AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}