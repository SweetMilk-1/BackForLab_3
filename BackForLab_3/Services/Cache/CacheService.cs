using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BackForLab_3.Services.Cache
{
    public class CacheService:ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache) {
            _distributedCache = distributedCache;
        }

        public async Task<T?> Get<T>(string key)
        {
            var music = await _distributedCache.GetStringAsync(key);
            if (music != null)
                return JsonSerializer.Deserialize<T>(music);
            return default;
        }

        public async Task<T> Set<T>(string key, T obj)
        {
            var resultJson = JsonSerializer.Serialize(obj);
            await _distributedCache.SetStringAsync(key, resultJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            });
            return obj;
        }
    }
}
