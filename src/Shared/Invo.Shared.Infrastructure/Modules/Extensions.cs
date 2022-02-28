using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
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
    }
}