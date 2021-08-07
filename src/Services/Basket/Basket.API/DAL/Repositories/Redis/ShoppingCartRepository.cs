using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Basket.API.DAL.Repositories.Redis
{
    public class ShoppingCartRepository : AsyncBaseRepository<ShoppingCart>,
        IShoppingCartRepository
    {
        public ShoppingCartRepository(IRedisCacheClient redisCacheClient) : base(redisCacheClient)
        {
        }
    }
}
