using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dagagino.Models
{


    public class Governorate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string ArName { get; set; }
        public required string EnName { get; set; }
        public required List<GovernorateState> States { get; set; }
    }

    public class GovernorateState
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string ArName { get; set; }
        public required string EnName { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Governorate { get; set; }
    }
}