using System.Threading.Tasks;
using Invo.Shared.Abstractions.Messaging;

namespace Invo.Shared.Infrastructure.Messaging.Dispatchers
{
    public interface IAsyncMessageDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}