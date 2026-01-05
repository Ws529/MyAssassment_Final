using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAssessment.Models
{
    public class AppSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Profil Pengampu
        [BsonElement("teacherName")]
        public string TeacherName { get; set; } = "";

        [BsonElement("teacherNIP")]
        public string TeacherNIP { get; set; } = "";

        [BsonElement("teacherMapel")]
        public string TeacherMapel { get; set; } = "";

        [BsonElement("teacherGelar")]
        public string TeacherGelar { get; set; } = "";

        // Identitas Sekolah
        [BsonElement("namaSekolah")]
        public string NamaSekolah { get; set; } = "";

        [BsonElement("npsn")]
        public string NPSN { get; set; } = "";

        [BsonElement("alamatSekolah")]
        public string AlamatSekolah { get; set; } = "";

        [BsonElement("kepalaSekolah")]
        public string KepalaSekolah { get; set; } = "";

        [BsonElement("nipKepalaSekolah")]
        public string NIPKepalaSekolah { get; set; } = "";

        [BsonElement("kotaSekolah")]
        public string KotaSekolah { get; set; } = "";

        [BsonElement("provinsiSekolah")]
        public string ProvinsiSekolah { get; set; } = "";

        // Tahun Ajaran & Semester
        [BsonElement("tahunAjaran")]
        public string TahunAjaran { get; set; } = "2025/2026";

        [BsonElement("semester")]
        public string Semester { get; set; } = "1";

        // KKM Default
        [BsonElement("kkmDefault")]
        public int KKMDefault { get; set; } = 75;

        // Backup Info
        [BsonElement("lastBackup")]
        public DateTime? LastBackup { get; set; }

        // Legacy fields for compatibility
        [BsonElement("schoolName")]
        public string SchoolName { get; set; } = "";

        [BsonElement("schoolAddress")]
        public string SchoolAddress { get; set; } = "";

        [BsonElement("principalName")]
        public string PrincipalName { get; set; } = "";

        [BsonElement("principalNIP")]
        public string PrincipalNIP { get; set; } = "";

        [BsonElement("academicYear")]
        public string AcademicYear { get; set; } = "2025/2026";
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