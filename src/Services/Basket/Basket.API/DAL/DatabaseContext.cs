using AutoMapper.Configuration;
using Basket.API.DAL.Entities;
using Basket.API.DAL.Interfaces.Mongo;
using Basket.API.Startup.Settings;
using MongoDB.Driver;

namespace Basket.API.DAL
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public DatabaseContext(AppSettings appSettings)
        {
            var mongoClient = new MongoClient(appSettings.MongoDbSettings.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(appSettings.MongoDbSettings.DatabaseName);

            Orders = _mongoDatabase.GetCollection<Order>(appSettings.MongoDbSettings.OrdersCollectionName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mongoDatabase.GetCollection<T>(name);
        }

        public IMongoCollection<Order> Orders { get; private set; }
    }
}
