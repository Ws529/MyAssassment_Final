using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAssessment.Models;
using MyAssessment.Services;

namespace MyAssessment.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly MongoDbService _db;

        public AuthController(MongoDbService db)
        {
            _db = db;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                // Validasi input
                if (string.IsNullOrWhiteSpace(request.NamaLengkap) ||
                    string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Semua field harus diisi!"
                    });
                }

                // Validasi panjang username
                if (request.Username.Length < 3)
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Username minimal 3 karakter!"
                    });
                }

                // Validasi panjang password
                if (request.Password.Length < 6)
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Password minimal 6 karakter!"
                    });
                }

                // Cek apakah username sudah terdaftar
                var existingUser = await _db.Users
                    .Find(u => u.Username.ToLower() == request.Username.ToLower())
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Username sudah digunakan! Silakan pilih username lain."
                    });
                }

                // Hash password
                string passwordHash = PasswordService.HashPassword(request.Password);

                // Buat user baru
                var newUser = new User
                {
                    NamaLengkap = request.NamaLengkap.Trim(),
                    Username = request.Username.ToLower().Trim(),
                    PasswordHash = passwordHash,
                    Role = "Guru",
                    CreatedAt = DateTime.UtcNow
                };

                await _db.Users.InsertOneAsync(newUser);

                return Ok(new AuthResponse
                {
                    Success = true,
                    Message = "Registrasi berhasil! Silakan login.",
                    User = new UserInfo
                    {
                        Id = newUser.Id,
                        NamaLengkap = newUser.NamaLengkap,
                        Username = newUser.Username,
                        Role = newUser.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new AuthResponse
                {
                    Success = false,
                    Message = "Terjadi kesalahan: " + ex.Message
                });
            }
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                // Validasi input
                if (string.IsNullOrWhiteSpace(request.Username) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Username dan password harus diisi!"
                    });
                }

                // Cari user berdasarkan username
                var user = await _db.Users
                    .Find(u => u.Username.ToLower() == request.Username.ToLower().Trim())
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Username tidak terdaftar!"
                    });
                }

                // Verifikasi password
                if (!PasswordService.VerifyPassword(request.Password, user.PasswordHash))
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Password salah!"
                    });
                }

                // Update last login
                var update = Builders<User>.Update.Set(u => u.LastLogin, DateTime.UtcNow);
                await _db.Users.UpdateOneAsync(u => u.Id == user.Id, update);

                return Ok(new AuthResponse
                {
                    Success = true,
                    Message = "Login berhasil!",
                    User = new UserInfo
                    {
                        Id = user.Id,
                        NamaLengkap = user.NamaLengkap,
                        Username = user.Username,
                        Role = user.Role
                    }
                });
            }
            catch (Exception ex)
            {
                return Ok(new AuthResponse
                {
                    Success = false,
                    Message = "Terjadi kesalahan: " + ex.Message
                });
            }
        }

        // GET: api/auth/check
        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            return Ok(new { authenticated = false, message = "Session check endpoint" });
        }
    }
}
