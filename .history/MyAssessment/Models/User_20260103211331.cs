using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MyAssessment.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [BsonElement("namaLengkap")]
        [JsonPropertyName("namaLengkap")]
        public string NamaLengkap { get; set; } = "";

        [BsonElement("username")]
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";

        [BsonElement("passwordHash")]
        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; } = "";

        [BsonElement("role")]
        [JsonPropertyName("role")]
        public string Role { get; set; } = "Guru";

        [BsonElement("createdAt")]
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("lastLogin")]
        [JsonPropertyName("lastLogin")]
        public DateTime? LastLogin { get; set; }
    }

    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
        
        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }

    public class RegisterRequest
    {
        [JsonPropertyName("namaLengkap")]
        public string NamaLengkap { get; set; } = "";
        
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
        
        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }

    public class AuthResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
        
        [JsonPropertyName("user")]
        public UserInfo? User { get; set; }
    }

    public class UserInfo
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        
        [JsonPropertyName("namaLengkap")]
        public string NamaLengkap { get; set; } = "";
        
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";
        
        [JsonPropertyName("role")]
        public string Role { get; set; } = "";
    }
}
