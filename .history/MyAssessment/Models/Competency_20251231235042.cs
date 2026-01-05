using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyAssessment.Models
{
    public class Competency
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("grade")]
        public string Grade { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } // KI-3 or KI-4

        [BsonElement("kkm")]
        public int KKM { get; set; } = 75;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}