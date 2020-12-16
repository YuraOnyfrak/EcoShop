using EcoShop.Common.Handlers;
using EcoShop.Common.Types;
using Microsoft.AspNetCore.Http;
using Project.Common.Handlers;
using System.Threading.Tasks;

namespace EcoShop.Common.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueryDispatcher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _httpContextAccessor.HttpContext.RequestServices.GetService(handlerType);

            return await handler.HandleAsync((dynamic)query);
        }
    }
}