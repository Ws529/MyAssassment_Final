using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAssessment.Models
{
    /// <summary>
    /// Dokumen tunggal untuk menyimpan pengaturan aplikasi
    /// termasuk identitas guru untuk keperluan ekspor laporan
    /// </summary>
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
}