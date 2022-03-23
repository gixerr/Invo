using System;
using System.Threading.Tasks;

namespace Invo.Shared.Infrastructure.Modules
{
    public sealed class ModuleRequestRegistration
    {
        public Type RequestType { get; }
        public Type ResponseType { get; }
        public Func<object, Task<object>> Func { get; }

        public ModuleRequestRegistration(Type requestType, Type responseType, Func<object, Task<object>> func)
        {
            RequestType = requestType;
            ResponseType = responseType;
            Func = func;
        }
    }
}