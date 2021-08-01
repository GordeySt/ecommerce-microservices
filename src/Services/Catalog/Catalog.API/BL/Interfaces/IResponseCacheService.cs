using System;
using System.Threading.Tasks;

namespace Catalog.API.BL.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string> GetCachedResponseAsync(string cachedKey);

        Task RemoveCachedResponseAsync(string cacheKey);
    }
}
