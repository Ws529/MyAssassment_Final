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
                    string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Semua field harus diisi!"
                    });
                }

                // Validasi format email
                if (!IsValidEmail(request.Email))
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Format email tidak valid!"
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

                // Cek apakah email sudah terdaftar
                var existingUser = await _db.Users
                    .Find(u => u.Email.ToLower() == request.Email.ToLower())
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Email sudah terdaftar! Silakan gunakan email lain."
                    });
                }

                // Hash password
                string passwordHash = PasswordService.HashPassword(request.Password);

                // Buat user baru
                var newUser = new User
                {
                    NamaLengkap = request.NamaLengkap.Trim(),
                    Email = request.Email.ToLower().Trim(),
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
                        Email = newUser.Email,
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
                if (string.IsNullOrWhiteSpace(request.Email) ||
                    string.IsNullOrWhiteSpace(request.Password))
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Email dan password harus diisi!"
                    });
                }

                // Cari user berdasarkan email
                var user = await _db.Users
                    .Find(u => u.Email.ToLower() == request.Email.ToLower().Trim())
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return Ok(new AuthResponse
                    {
                        Success = false,
                        Message = "Email tidak terdaftar!"
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
                        Email = user.Email,
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
            // Untuk pengecekan session dari frontend
            return Ok(new { authenticated = false, message = "Session check endpoint" });
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
