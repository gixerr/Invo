using System.Threading.Channels;
using Invo.Shared.Abstractions.Messaging;

namespace Invo.Shared.Infrastructure.Messaging.Dispatchers
{
    public class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessage> _messages = Channel.CreateUnbounded<IMessage>();

        public ChannelReader<IMessage> Reader => _messages.Reader;
        public ChannelWriter<IMessage> Writer => _messages.Writer;
    }
}