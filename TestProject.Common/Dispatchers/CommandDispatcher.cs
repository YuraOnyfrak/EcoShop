using EcoShop.Common.Common;
using EcoShop.Common.Messages;
using Microsoft.AspNetCore.Http;
using Project.Common.Handlers;
using Project.Common.Messages;
using System.Threading.Tasks;

namespace EcoShop.Common.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommandDispatcher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SendAsync<T>(T command) where T : ICommand
        {
            var type = typeof(ICommandHandler<T>);
            var serviceHandler = _httpContextAccessor.HttpContext.RequestServices.GetService(type) as ICommandHandler<T>;
            await serviceHandler.HandleAsync(command, CorrelationContext.Empty);
        }
    }
}