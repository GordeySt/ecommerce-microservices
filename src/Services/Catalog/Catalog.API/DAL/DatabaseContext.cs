using Catalog.API.DAL.Entities;
using Catalog.API.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Catalog.API.DAL
{
    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public DatabaseContext(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration
                .GetValue<string>("DatabaseSettings:ConnectionString"));
            _mongoDatabase = mongoClient.GetDatabase(configuration
                .GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = _mongoDatabase.GetCollection<Product>(configuration
                .GetValue<string>("DatabaseSettings:CollectionName"));

            DatabaseContextSeed.SeedData(Products);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mongoDatabase.GetCollection<T>(name);
        }

        public IMongoCollection<Product> Products { get; private set; }
    }
}
