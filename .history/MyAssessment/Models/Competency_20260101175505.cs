using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAssessment.Models
{
    public class Competency
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [BsonElement("kode")]
        [JsonPropertyName("kode")]
        public string Kode { get; set; }

        [BsonElement("mapel")]
        [JsonPropertyName("mapel")]
        public string Mapel { get; set; }

        [BsonElement("kelas")]
        [JsonPropertyName("kelas")]
        public string Kelas { get; set; }

        [BsonElement("jenis")]
        [JsonPropertyName("jenis")]
        public string Jenis { get; set; }

        [BsonElement("deskripsi")]
        [JsonPropertyName("deskripsi")]
        public string Deskripsi { get; set; }

        [BsonElement("kkm")]
        [JsonPropertyName("kkm")]
        public int KKM { get; set; } = 75;
    }
}
