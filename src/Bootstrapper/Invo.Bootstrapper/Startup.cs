using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Invo.Shared.Abstractions.Modules;
using Invo.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Invo.Bootstrapper
{
    public class Startup
    {
        private readonly IList<Assembly> _assemblies;
        private readonly IList<IModule> _modules;

        public Startup()
        {
            _assemblies = ModuleLoader.LoadAssemblies();
            _modules = ModuleLoader.LoadModules(_assemblies);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();
            services.AddModules(_modules);
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            app.UseInfrastructure();
            app.UseModules(_modules);
            
            logger.LogInformation($"Loaded modules: {string.Join(", ", _modules.Select(x => x.Name))}");
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Invo API"));
            });
            
            _assemblies.Clear();
            _modules.Clear();
        }
    }
}