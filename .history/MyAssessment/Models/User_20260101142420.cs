using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyAssessment.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("namaLengkap")]
        public string NamaLengkap { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("role")]
        public string Role { get; set; } = "Guru";

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("lastLogin")]
        public DateTime? LastLogin { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterRequest
    {
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public UserInfo User { get; set; }
    }

    public class UserInfo
    {
        public string Id { get; set; }
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
