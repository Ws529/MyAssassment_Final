using System.Security.Cryptography;
using System.Text;

namespace MyAssessment.Services
{
    public class PasswordService
    {
        // Hash password menggunakan SHA256 dengan salt
        public static string HashPassword(string password)
        {
            // Generate salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash password dengan salt menggunakan PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                
                // Gabungkan salt + hash
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Verifikasi password
        public static bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedHash);
                
                // Ekstrak salt (16 bytes pertama)
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                
                // Hash password input dengan salt yang sama
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    
                    // Bandingkan hash
                    for (int i = 0; i < 32; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                            return false;
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
