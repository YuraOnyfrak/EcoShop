using EcoShop.Common.Messages;
using System.Threading.Tasks;

namespace EcoShop.Common.Dispatchers
{
    public interface ICommandDispatcher
    {
         Task SendAsync<T>(T command) where T : ICommand;
    }
}