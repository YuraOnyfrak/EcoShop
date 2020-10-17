using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Common.Fabio;

namespace DShop.Common.Fabio
{
    public static class Extensions
    {
        public static IServiceCollection AddFabio(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                services.AddOptions<FabioOptions>();
            }
            services.AddTransient<FabioMessageHandler>();
            services.AddHttpClient<IFabioHttpClient, FabioHttpClient>()
                .AddHttpMessageHandler<FabioMessageHandler>();

            return services;
        }
    }
}