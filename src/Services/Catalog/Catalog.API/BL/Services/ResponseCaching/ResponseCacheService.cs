using Catalog.API.BL.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Services.ResponseCaching
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IRedisCacheClient _redisCache;

        public ResponseCacheService(IRedisCacheClient redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _redisCache.GetDbFromConfiguration().AddAsync(cacheKey, serializedResponse);
            await _redisCache.GetDbFromConfiguration().UpdateExpiryAsync(cacheKey, timeToLive);
        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var cachedResponse = await _redisCache.GetDbFromConfiguration().GetAsync<string>(cacheKey);

            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }

        public async Task FlushCachedResponsesAsync()
        {
            await _redisCache.GetDbFromConfiguration().FlushDbAsync();
        }
    }
}
