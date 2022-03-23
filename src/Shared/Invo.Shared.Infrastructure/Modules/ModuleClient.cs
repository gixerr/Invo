using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invo.Shared.Abstractions.Modules;

namespace Invo.Shared.Infrastructure.Modules
{
    public class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
        }
        
        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry.GetBroadcastRegistrations(key)
                .Where(x => x.ReceiverType != message.GetType());

            var tasks = new List<Task>();

            foreach (var registration in registrations)
            {
                var func = registration.Func;
                var receiverMessage = TranslateType(message, registration.ReceiverType);
                tasks.Add(func(receiverMessage));
            }

            await Task.WhenAll(tasks);
        }

        private object TranslateType(object value, Type registrationReceiverType)
            => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), registrationReceiverType);
    }
}