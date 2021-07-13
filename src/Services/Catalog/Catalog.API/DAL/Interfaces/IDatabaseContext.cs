using Catalog.API.DAL.Entities;
using MongoDB.Driver;

namespace Catalog.API.DAL.Interfaces
{
    public interface IDatabaseContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
