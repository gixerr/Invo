using System;
using System.Threading.Tasks;

namespace Invo.Shared.Infrastructure.Modules
{
    public class ModuleBroadcastRegistration
    {
        public Type ReceiverType { get; }
        public Func<object, Task> Func { get; set; }
        public string Key => ReceiverType.Name;

        public ModuleBroadcastRegistration(Type receiverType, Func<object, Task> func)
        {
            ReceiverType = receiverType;
            Func = func;
        }
    }
}