using System;
using System.Collections.Generic;
using System.Text;

namespace EcoShop.Common.Cache
{
    public class RedisCasheOptions
    {
        public string RedisServerUrl { get; set; }
        public bool CacheExpire { get; set; }
        public int ExpireMinute { get; set; }
    }
}
