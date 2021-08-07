using Basket.API.DAL.Entities;
using MongoDB.Driver;

namespace Basket.API.DAL.Interfaces.Mongo
{
    public interface IDatabaseContext
    {
        IMongoCollection<Order> Orders { get; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
