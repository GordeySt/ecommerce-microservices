using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Threading.Tasks;

namespace Basket.API.DAL.Repositories.Redis
{
    public class AsyncBaseRepository<T> : IAsyncBaseRepository<T> where T
        : EntityBase
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public AsyncBaseRepository(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task<T> AddAsync(T item) 
        {
            await _redisCacheClient.GetDbFromConfiguration()
                .AddAsync(item.Id.ToString(), JsonConvert.SerializeObject(item));

            return item;
        }

        public async Task DeleteAsync(string cacheKey) => await _redisCacheClient.GetDbFromConfiguration()
                .RemoveAsync(cacheKey);

        public async Task<T> GetAsync(string cacheKey)
        {
            var cachedItem = await _redisCacheClient.GetDbFromConfiguration()
                .GetAsync<string>(cacheKey);

            return cachedItem is null ? null : JsonConvert.DeserializeObject<T>(cachedItem);
        }
    }
}
