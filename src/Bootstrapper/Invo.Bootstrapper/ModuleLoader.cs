using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Invo.Shared.Abstractions.Modules;
using Microsoft.Extensions.Configuration;

namespace Invo.Bootstrapper
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string modulePart = "Invo.Modules.";
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();
            
            var disabledModules = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    continue;
                }

                var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
                var moduleEnabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!moduleEnabled)
                {
                    disabledModules.Add(file);
                }
            }
            
            disabledModules.ForEach(x => files.Remove(x));
            
            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
            => assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToList();

    }
}