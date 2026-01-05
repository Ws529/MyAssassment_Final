using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAssessment.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nisn")]
        public string NISN { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }

        [BsonElement("kelas")]
        public string Kelas { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("tanggalLahir")]
        public string TanggalLahir { get; set; }

        [BsonElement("alamat")]
        public string Alamat { get; set; }
    }
}