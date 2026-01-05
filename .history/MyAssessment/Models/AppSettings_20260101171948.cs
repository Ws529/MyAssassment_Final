using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAssessment.Models
{
    public class AppSettings
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        // Profil Pengampu
        [BsonElement("teacherName")]
        [JsonPropertyName("teacherName")]
        public string TeacherName { get; set; } = "";

        [BsonElement("teacherNIP")]
        [JsonPropertyName("teacherNIP")]
        public string TeacherNIP { get; set; } = "";

        [BsonElement("teacherMapel")]
        [JsonPropertyName("teacherMapel")]
        public string TeacherMapel { get; set; } = "";

        [BsonElement("teacherGelar")]
        [JsonPropertyName("teacherGelar")]
        public string TeacherGelar { get; set; } = "";

        // Identitas Sekolah
        [BsonElement("namaSekolah")]
        [JsonPropertyName("namaSekolah")]
        public string NamaSekolah { get; set; } = "";

        [BsonElement("npsn")]
        [JsonPropertyName("npsn")]
        public string NPSN { get; set; } = "";

        [BsonElement("alamatSekolah")]
        [JsonPropertyName("alamatSekolah")]
        public string AlamatSekolah { get; set; } = "";

        [BsonElement("kepalaSekolah")]
        [JsonPropertyName("kepalaSekolah")]
        public string KepalaSekolah { get; set; } = "";

        [BsonElement("nipKepalaSekolah")]
        [JsonPropertyName("nipKepalaSekolah")]
        public string NIPKepalaSekolah { get; set; } = "";

        [BsonElement("kotaSekolah")]
        [JsonPropertyName("kotaSekolah")]
        public string KotaSekolah { get; set; } = "";

        [BsonElement("provinsiSekolah")]
        [JsonPropertyName("provinsiSekolah")]
        public string ProvinsiSekolah { get; set; } = "";

        // Tahun Ajaran & Semester
        [BsonElement("tahunAjaran")]
        [JsonPropertyName("tahunAjaran")]
        public string TahunAjaran { get; set; } = "2025/2026";

        [BsonElement("semester")]
        [BsonRepresentation(BsonType.String)]
        [JsonPropertyName("semester")]
        public string Semester { get; set; } = "1";

        // KKM Default
        [BsonElement("kkmDefault")]
        [JsonPropertyName("kkmDefault")]
        public int KKMDefault { get; set; } = 75;

        // Backup Info
        [BsonElement("lastBackup")]
        [JsonPropertyName("lastBackup")]
        public DateTime? LastBackup { get; set; }

        // Legacy fields for compatibility
        [BsonElement("schoolName")]
        [JsonPropertyName("schoolName")]
        public string SchoolName { get; set; } = "";

        [BsonElement("schoolAddress")]
        [JsonPropertyName("schoolAddress")]
        public string SchoolAddress { get; set; } = "";

        [BsonElement("principalName")]
        [JsonPropertyName("principalName")]
        public string PrincipalName { get; set; } = "";

        [BsonElement("principalNIP")]
        [JsonPropertyName("principalNIP")]
        public string PrincipalNIP { get; set; } = "";

        [BsonElement("academicYear")]
        [JsonPropertyName("academicYear")]
        public string AcademicYear { get; set; } = "2025/2026";
    }

    public class Kelas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [BsonElement("nama")]
        [JsonPropertyName("nama")]
        public string Nama { get; set; }
    }

    public class MapelItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [BsonElement("nama")]
        [JsonPropertyName("nama")]
        public string Nama { get; set; }
    }
}
