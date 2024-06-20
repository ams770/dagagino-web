using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dagagino.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        [BsonElement("Name")]
        public required string Name { get; set; }

        public decimal Price { get; set; }

        public required string Category { get; set; }
    }
}