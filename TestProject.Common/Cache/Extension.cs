using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.Cache
{
    public static class Extension
    {
        public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options => 
                options.Configuration = configuration.GetConnectionString(nameof(RedisCasheOptions.RedisServerUrl)));
            services.AddSingleton<IDistributedCacheWrapper, DistributedCacheWrapper>();

            return services;
        }
             
    }
}
