using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invo.Shared.Infrastructure.Modules
{
    public interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        ModuleRequestRegistration GetRequestedRegistration(string path);
        void AddBroadcastFunc(Type requestType, Func<object, Task> func);
        void AddRequestFunc(string path, Type requestedType, Type responseType, Func<object, Task<object>> func);
    }
}