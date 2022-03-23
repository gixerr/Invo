using System.Threading.Tasks;

namespace Invo.Shared.Abstractions.Modules
{
    public interface IModuleClient
    {
        Task PublishAsync(object message);
    }
}