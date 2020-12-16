using System.Threading.Tasks;
using EcoShop.Common.Messages;
using EcoShop.Common.Types;

namespace EcoShop.Common.Dispatchers
{
    public interface IDispatcher
    {
        Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}