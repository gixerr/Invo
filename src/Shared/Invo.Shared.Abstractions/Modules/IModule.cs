using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invo.Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }
        string Path { get; }
        void Add(IServiceCollection services);
        void Use(IApplicationBuilder app);

    }
}