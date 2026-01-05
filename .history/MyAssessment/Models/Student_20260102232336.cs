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

        // Field NIS - support both "nis" and "nisn" from MongoDB
        [BsonElement("nis")]
        [JsonPropertyName("nis")]
        public string Nis { get; set; } = string.Empty;

        [BsonElement("nama")]
        [JsonPropertyName("nama")]
        public string Nama { get; set; } = string.Empty;

        [BsonElement("kelas")]
        [JsonPropertyName("kelas")]
        public string Kelas { get; set; } = string.Empty;

        // Field Jenis Kelamin - support "jenisKelamin" 
        [BsonElement("jenisKelamin")]
        [JsonPropertyName("jenisKelamin")]
        public string JenisKelamin { get; set; } = string.Empty;

        [BsonElement("tanggalLahir")]
        [JsonPropertyName("tanggalLahir")]
        public string? TanggalLahir { get; set; }

        [BsonElement("alamat")]
        [JsonPropertyName("alamat")]
        public string? Alamat { get; set; }
    }
}