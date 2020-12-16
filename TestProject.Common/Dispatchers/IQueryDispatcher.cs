using EcoShop.Common.Types;
using System.Threading.Tasks;

namespace EcoShop.Common.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}