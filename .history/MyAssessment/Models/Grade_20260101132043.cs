using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MyAssessment.Models
{
    public class Grade
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("studentId")]
        public string StudentId { get; set; }

        [BsonElement("competencyId")]
        public string CompetencyId { get; set; }

        [BsonElement("kelas")]
        public string Kelas { get; set; }

        [BsonElement("mapel")]
        public string Mapel { get; set; }

        [BsonElement("nilaiUH")]
        public List<double> NilaiUH { get; set; } = new List<double>();

        [BsonElement("nilaiPTS")]
        public double NilaiPTS { get; set; }

        [BsonElement("nilaiPAS")]
        public double NilaiPAS { get; set; }

        [BsonElement("nilaiPraktik")]
        public List<double> NilaiPraktik { get; set; } = new List<double>();

        [BsonElement("nilaiProyek")]
        public double NilaiProyek { get; set; }

        [BsonElement("nilaiPortofolio")]
        public double NilaiPortofolio { get; set; }

        [BsonElement("nilaiAkhirKI3")]
        public double NilaiAkhirKI3 { get; set; }

        [BsonElement("nilaiAkhirKI4")]
        public double NilaiAkhirKI4 { get; set; }

        [BsonElement("predikatKI3")]
        public string PredikatKI3 { get; set; }

        [BsonElement("predikatKI4")]
        public string PredikatKI4 { get; set; }

        [BsonElement("semester")]
        public int Semester { get; set; }

        [BsonElement("tahunAjaran")]
        public string TahunAjaran { get; set; }
    }
}