using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dagagino.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public required string ArName { get; set; }
        public required string EnName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required string Salt { get; set; }
        public required string Password { get; set; }
        public required string Address { get; set; }
        public string? Description { get; set; }
        public required string GovernorateState { get; set; }

    }
}