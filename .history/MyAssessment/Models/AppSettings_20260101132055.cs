using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAssessment.Models
{
    public class AppSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("teacherName")]
        public string TeacherName { get; set; } = "Nama Guru";

        [BsonElement("teacherNIP")]
        public string TeacherNIP { get; set; } = "000000000000";

        [BsonElement("schoolName")]
        public string SchoolName { get; set; } = "SMA Negeri 1";

        [BsonElement("schoolAddress")]
        public string SchoolAddress { get; set; } = "Alamat Sekolah";

        [BsonElement("principalName")]
        public string PrincipalName { get; set; } = "Nama Kepala Sekolah";

        [BsonElement("principalNIP")]
        public string PrincipalNIP { get; set; } = "000000000000";

        [BsonElement("academicYear")]
        public string AcademicYear { get; set; } = "2025/2026";

        [BsonElement("semester")]
        public int Semester { get; set; } = 1;
    }

    public class Kelas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }
    }

    public class MapelItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }
    }
}