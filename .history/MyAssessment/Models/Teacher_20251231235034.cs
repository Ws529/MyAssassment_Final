using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyAssessment.Models
{
    public class Teacher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nip")]
        public string NIP { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}