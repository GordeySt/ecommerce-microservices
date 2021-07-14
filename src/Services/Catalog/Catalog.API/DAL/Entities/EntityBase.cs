using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Catalog.API.DAL.Entities
{
    public class EntityBase
    {
        [BsonId]
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
