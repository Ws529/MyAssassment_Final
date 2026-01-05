using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAssessment.Models
{
    public class Competency
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("kode")]
        public string Kode { get; set; }

        [BsonElement("mapel")]
        public string Mapel { get; set; }

        [BsonElement("kelas")]
        public string Kelas { get; set; }

        [BsonElement("jenis")]
        public string Jenis { get; set; }

        [BsonElement("deskripsi")]
        public string Deskripsi { get; set; }

        [BsonElement("kkm")]
        public int KKM { get; set; } = 75;
    }
}