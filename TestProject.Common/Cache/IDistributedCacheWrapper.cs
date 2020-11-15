using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoShop.Common.Cache
{
    public interface IDistributedCacheWrapper
    {
        IDistributedCache DistributedCache { get; }
        Task<T> GetCacheValueAsync<T>(string key, CancellationToken token = default) where T : class;
        Task<IEnumerable<T>> GetCacheValuesAsync<T>(CancellationToken token = default) where T : class;
        Task SetCacheValueAsync<T>(string key, T value, CancellationToken token = default) where T : class;
        Task<bool> ExistAsync(string key, CancellationToken token = default);       
    }
}
