using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAssessment.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [BsonElement("nisn")]
        [JsonPropertyName("nisn")]
        public string Nisn { get; set; } = string.Empty;

        [BsonElement("nama")]
        [JsonPropertyName("nama")]
        public string Nama { get; set; } = string.Empty;

        [BsonElement("kelas")]
        [JsonPropertyName("kelas")]
        public string Kelas { get; set; } = string.Empty;

        [BsonElement("gender")]
        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [BsonElement("tanggalLahir")]
        [JsonPropertyName("tanggalLahir")]
        public string? TanggalLahir { get; set; }

        [BsonElement("alamat")]
        [JsonPropertyName("alamat")]
        public string? Alamat { get; set; }
    }
}