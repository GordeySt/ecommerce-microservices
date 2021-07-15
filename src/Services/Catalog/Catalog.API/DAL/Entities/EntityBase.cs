using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.API.DAL.Entities
{
    public class EntityBase
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
