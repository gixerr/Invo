using System.Threading.Channels;
using Invo.Shared.Abstractions.Messaging;

namespace Invo.Shared.Infrastructure.Messaging.Dispatchers
{
    public interface IMessageChannel
    {
        ChannelReader<IMessage> Reader { get; }
        ChannelWriter<IMessage> Writer { get; }
    }
}