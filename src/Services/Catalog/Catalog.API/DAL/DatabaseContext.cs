using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Catalog.API.Startup.Settings;
using MongoDB.Driver;

namespace Catalog.API.DAL
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public DatabaseContext(AppSettings appSettings)
        {
            var mongoClient = new MongoClient(appSettings.DbSettings.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(appSettings.DbSettings.DatabaseName);

            Products = _mongoDatabase.GetCollection<Product>(appSettings.DbSettings.CollectionName);

            DatabaseContextSeed.SeedData(Products);
        }

        public IMongoCollection<T> GetCollection<T>(string name) => 
            _mongoDatabase.GetCollection<T>(name);

        public IMongoCollection<Product> Products { get; private set; }
    }
}
