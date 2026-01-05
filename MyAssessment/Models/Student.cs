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

        // Field NIS
        [BsonElement("NIS")]
        [JsonPropertyName("NIS")]
        public string NIS { get; set; } = string.Empty;

        [BsonElement("Nama")]
        [JsonPropertyName("Nama")]
        public string Nama { get; set; } = string.Empty;

        [BsonElement("Kelas")]
        [JsonPropertyName("Kelas")]
        public string Kelas { get; set; } = string.Empty;

        // Field Jenis Kelamin
        [BsonElement("JenisKelamin")]
        [JsonPropertyName("JenisKelamin")]
        public string JenisKelamin { get; set; } = string.Empty;

        [BsonElement("TanggalLahir")]
        [JsonPropertyName("TanggalLahir")]
        public string? TanggalLahir { get; set; }

        [BsonElement("Alamat")]
        [JsonPropertyName("Alamat")]
        public string? Alamat { get; set; }
    }
}