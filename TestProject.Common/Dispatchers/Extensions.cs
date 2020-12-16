using EcoShop.Common.Dispatchers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace EcoShop.Common.Dispatchers
{
    public static class Extensions
    {
        public static void AddDispatchers(this IServiceCollection services)
        {
            //services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<ICommandDispatcher,CommandDispatcher>();
            services.AddTransient<IDispatcher, Dispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
        }
    }
}