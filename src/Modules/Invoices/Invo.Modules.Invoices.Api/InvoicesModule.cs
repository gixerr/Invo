using Invo.Modules.Invoices.Core;
using Invo.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Modules.Invoices.Api
{
    internal class InvoicesModule : IModule
    {
        public const string BasePath = "invoices-module";
        public string Name { get; } = "Invoices";
        public string Path => BasePath;
        
        public void Add(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}