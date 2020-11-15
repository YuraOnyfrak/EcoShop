using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Common.Cache
{
    public class DistributedCacheWrapper : IDistributedCacheWrapper
    {
        private readonly IDistributedCache _cache;
        private readonly RedisCasheOptions _options;

        public IDistributedCache DistributedCache { get { return _cache; } }

        public DistributedCacheWrapper(IDistributedCache cache, IOptions<RedisCasheOptions> options)
        {
            _cache = cache;
            _options = options.Value;
        }

        public async Task<T> GetCacheValueAsync<T>(string key, CancellationToken token = default) where T : class
        {
            string value = await _cache.GetStringAsync(key, token);

            if (string.IsNullOrEmpty(value))
                return null;

            T desereliazedValue = JsonConvert.DeserializeObject<T>(value);

            return desereliazedValue;
        }

        public async Task SetCacheValueAsync<T>(string key, T value, CancellationToken token = default) where T : class
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            if (_options.CacheExpire)
            {
                // Remove item from cache after duration
                cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_options.ExpireMinute);
            }

            string result = JsonConvert.SerializeObject(value); 
            await _cache.SetStringAsync(key, result, token);
        }

        public async Task<bool> ExistAsync(string key, CancellationToken token = default)
        {
            string value = await _cache.GetStringAsync(key, token);

            return !string.IsNullOrEmpty(value);
        }

        public Task<IEnumerable<T>> GetCacheValuesAsync<T>(CancellationToken token = default) where T : class
        {
            
            //_cache.
            return null;
        }
    }
}
