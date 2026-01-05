using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyAssessment.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nisn")]
        public string NISN { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("class")]
        public string Class { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("birthDate")]
        public DateTime BirthDate { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}