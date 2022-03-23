using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Invo.Shared.Abstractions.Modules;

namespace Invo.Shared.Infrastructure.Modules
{
    internal sealed class ModuleRegistry : IModuleRegistry
    {
        private readonly List<ModuleBroadcastRegistration> _broadcastRegistrations = new();
        private readonly Dictionary<string, ModuleRequestRegistration> _requestRegistrations = new();

        public IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key)
            => _broadcastRegistrations.Where(x => x.Key.Equals(key));

        public ModuleRequestRegistration GetRequestedRegistration(string path)
            => _requestRegistrations.TryGetValue(path, out var registration) ? registration : null;

        public void AddBroadcastFunc(Type requestType, Func<object, Task> func)
        {
            if (string.IsNullOrWhiteSpace(requestType.Namespace))
            {
                throw new InvalidOperationException("Missing namespace.");
            }

            var registration = new ModuleBroadcastRegistration(requestType, func);
            _broadcastRegistrations.Add(registration);
        }

        public void AddRequestFunc(string path, Type requestedType, Type responseType, Func<object, Task<object>> func)
        {
            if (path is null)
            {
                throw new InvalidOperationException("Request path can not be null");
            }

            var registration = new ModuleRequestRegistration(requestedType, responseType, func);
            _requestRegistrations.Add(path,registration);
        }
    }
}