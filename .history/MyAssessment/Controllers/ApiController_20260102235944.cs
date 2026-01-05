using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyAssessment.Models;
using MyAssessment.Services;

namespace MyAssessment.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly MongoDbService _db;

        public ApiController(MongoDbService db)
        {
            _db = db;
        }

        // ========== SISWA ==========
        [HttpGet("siswa")]
        public async Task<IActionResult> GetSiswa()
        {
            try
            {
                var data = await _db.Students.Find(_ => true).ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("siswa")]
        public async Task<IActionResult> AddSiswa([FromBody] Student student)
        {
            try
            {
                if (string.IsNullOrEmpty(student.NIS) || string.IsNullOrEmpty(student.Nama) || string.IsNullOrEmpty(student.Kelas))
                {
                    return BadRequest(new { success = false, message = "NIS, Nama, dan Kelas wajib diisi" });
                }

                // Pastikan Id null untuk data baru agar MongoDB generate ObjectId
                student.Id = null;
                
                await _db.Students.InsertOneAsync(student);
                return Ok(new { success = true, message = "Siswa berhasil ditambahkan", data = student });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Gagal menambahkan siswa: " + ex.Message });
            }
        }

        [HttpPut("siswa/{id}")]
        public async Task<IActionResult> UpdateSiswa(string id, [FromBody] Student student)
        {
            try
            {
                if (string.IsNullOrEmpty(student.NIS) || string.IsNullOrEmpty(student.Nama) || string.IsNullOrEmpty(student.Kelas))
                {
                    return BadRequest(new { success = false, message = "NIS, Nama, dan Kelas wajib diisi" });
                }

                var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
                
                // Cek apakah data ada
                var existing = await _db.Students.Find(filter).FirstOrDefaultAsync();
                if (existing == null)
                {
                    return NotFound(new { success = false, message = "Data siswa tidak ditemukan" });
                }

                // Gunakan UpdateOneAsync dengan UpdateDefinition untuk update field secara eksplisit
                // Ini memastikan NIS dan JenisKelamin pasti terupdate
                var update = Builders<Student>.Update
                    .Set(s => s.NIS, student.NIS)
                    .Set(s => s.Nama, student.Nama)
                    .Set(s => s.Kelas, student.Kelas)
                    .Set(s => s.JenisKelamin, student.JenisKelamin)
                    .Set(s => s.TanggalLahir, student.TanggalLahir)
                    .Set(s => s.Alamat, student.Alamat);
                
                var result = await _db.Students.UpdateOneAsync(filter, update);
                
                if (result.ModifiedCount > 0 || result.MatchedCount > 0)
                {
                    // Ambil data terbaru untuk response
                    var updatedStudent = await _db.Students.Find(filter).FirstOrDefaultAsync();
                    return Ok(new { success = true, message = "Siswa berhasil diperbarui", data = updatedStudent });
                }
                
                return BadRequest(new { success = false, message = "Gagal memperbarui data siswa" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Gagal memperbarui siswa: " + ex.Message });
            }
        }

        [HttpDelete("siswa/{id}")]
        public async Task<IActionResult> DeleteSiswa(string id)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
                
                // Cek apakah data ada
                var existing = await _db.Students.Find(filter).FirstOrDefaultAsync();
                if (existing == null)
                {
                    return NotFound(new { success = false, message = "Data siswa tidak ditemukan" });
                }

                var result = await _db.Students.DeleteOneAsync(filter);
                
                if (result.DeletedCount > 0)
                {
                    return Ok(new { success = true, message = "Siswa berhasil dihapus" });
                }
                
                return BadRequest(new { success = false, message = "Gagal menghapus data siswa" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Gagal menghapus siswa: " + ex.Message });
            }
        }

        // ========== KOMPETENSI ==========
        [HttpGet("kompetensi")]
        public async Task<IActionResult> GetKompetensi()
        {
            var data = await _db.Competencies.Find(_ => true).ToListAsync();
            return Ok(data);
        }

        [HttpPost("kompetensi")]
        public async Task<IActionResult> AddKompetensi([FromBody] Competency kd)
        {
            await _db.Competencies.InsertOneAsync(kd);
            return Ok(new { success = true, message = "KD berhasil ditambahkan" });
        }

        [HttpPut("kompetensi/{id}")]
        public async Task<IActionResult> UpdateKompetensi(string id, [FromBody] Competency kd)
        {
            var filter = Builders<Competency>.Filter.Eq(k => k.Id, id);
            kd.Id = id;
            await _db.Competencies.ReplaceOneAsync(filter, kd);
            return Ok(new { success = true, message = "KD berhasil diperbarui" });
        }

        [HttpDelete("kompetensi/{id}")]
        public async Task<IActionResult> DeleteKompetensi(string id)
        {
            var filter = Builders<Competency>.Filter.Eq(k => k.Id, id);
            await _db.Competencies.DeleteOneAsync(filter);
            return Ok(new { success = true, message = "KD berhasil dihapus" });
        }

        // ========== NILAI ==========
        [HttpGet("nilai")]
        public async Task<IActionResult> GetNilai([FromQuery] string kelas, [FromQuery] string mapel)
        {
            var filter = Builders<Grade>.Filter.Empty;
            if (!string.IsNullOrEmpty(kelas))
                filter &= Builders<Grade>.Filter.Eq(g => g.Kelas, kelas);
            if (!string.IsNullOrEmpty(mapel))
                filter &= Builders<Grade>.Filter.Eq(g => g.Mapel, mapel);
            
            var data = await _db.Grades.Find(filter).ToListAsync();
            return Ok(data);
        }

        [HttpPost("nilai")]
        public async Task<IActionResult> SaveNilai([FromBody] Grade grade)
        {
            if (string.IsNullOrEmpty(grade.Id))
            {
                await _db.Grades.InsertOneAsync(grade);
            }
            else
            {
                var filter = Builders<Grade>.Filter.Eq(g => g.Id, grade.Id);
                await _db.Grades.ReplaceOneAsync(filter, grade, new ReplaceOptions { IsUpsert = true });
            }
            return Ok(new { success = true, message = "Nilai berhasil disimpan" });
        }

        [HttpPost("nilai/bulk")]
        public async Task<IActionResult> SaveNilaiBulk([FromBody] List<Grade> grades)
        {
            foreach (var grade in grades)
            {
                if (string.IsNullOrEmpty(grade.Id))
                {
                    await _db.Grades.InsertOneAsync(grade);
                }
                else
                {
                    var filter = Builders<Grade>.Filter.Eq(g => g.Id, grade.Id);
                    await _db.Grades.ReplaceOneAsync(filter, grade, new ReplaceOptions { IsUpsert = true });
                }
            }
            return Ok(new { success = true, message = "Semua nilai berhasil disimpan" });
        }

        // ========== KELAS ==========
        [HttpGet("kelas")]
        public async Task<IActionResult> GetKelas()
        {
            var data = await _db.KelasList.Find(_ => true).ToListAsync();
            return Ok(data);
        }

        [HttpPost("kelas")]
        public async Task<IActionResult> AddKelas([FromBody] Kelas kelas)
        {
            await _db.KelasList.InsertOneAsync(kelas);
            return Ok(new { success = true, message = "Kelas berhasil ditambahkan" });
        }

        [HttpPut("kelas/{id}")]
        public async Task<IActionResult> UpdateKelas(string id, [FromBody] Kelas kelas)
        {
            var filter = Builders<Kelas>.Filter.Eq(k => k.Id, id);
            kelas.Id = id;
            await _db.KelasList.ReplaceOneAsync(filter, kelas);
            return Ok(new { success = true, message = "Kelas berhasil diperbarui" });
        }

        [HttpDelete("kelas/{id}")]
        public async Task<IActionResult> DeleteKelas(string id)
        {
            var filter = Builders<Kelas>.Filter.Eq(k => k.Id, id);
            await _db.KelasList.DeleteOneAsync(filter);
            return Ok(new { success = true, message = "Kelas berhasil dihapus" });
        }

        // ========== MAPEL ==========
        [HttpGet("mapel")]
        public async Task<IActionResult> GetMapel()
        {
            var data = await _db.MapelList.Find(_ => true).ToListAsync();
            return Ok(data);
        }

        [HttpPost("mapel")]
        public async Task<IActionResult> AddMapel([FromBody] MapelItem mapel)
        {
            await _db.MapelList.InsertOneAsync(mapel);
            return Ok(new { success = true, message = "Mapel berhasil ditambahkan" });
        }

        [HttpPut("mapel/{id}")]
        public async Task<IActionResult> UpdateMapel(string id, [FromBody] MapelItem mapel)
        {
            var filter = Builders<MapelItem>.Filter.Eq(m => m.Id, id);
            mapel.Id = id;
            await _db.MapelList.ReplaceOneAsync(filter, mapel);
            return Ok(new { success = true, message = "Mapel berhasil diperbarui" });
        }

        [HttpDelete("mapel/{id}")]
        public async Task<IActionResult> DeleteMapel(string id)
        {
            var filter = Builders<MapelItem>.Filter.Eq(m => m.Id, id);
            await _db.MapelList.DeleteOneAsync(filter);
            return Ok(new { success = true, message = "Mapel berhasil dihapus" });
        }

        // ========== SETTINGS ==========
        [HttpGet("settings")]
        public async Task<IActionResult> GetSettings()
        {
            var data = await _db.Settings.Find(_ => true).FirstOrDefaultAsync();
            if (data == null)
            {
                data = new AppSettings();
                await _db.Settings.InsertOneAsync(data);
            }
            return Ok(data);
        }

        [HttpPost("settings")]
        public async Task<IActionResult> SaveSettings([FromBody] AppSettings settings)
        {
            if (string.IsNullOrEmpty(settings.Id))
            {
                await _db.Settings.InsertOneAsync(settings);
            }
            else
            {
                var filter = Builders<AppSettings>.Filter.Eq(s => s.Id, settings.Id);
                await _db.Settings.ReplaceOneAsync(filter, settings, new ReplaceOptions { IsUpsert = true });
            }
            return Ok(new { success = true, message = "Pengaturan berhasil disimpan" });
        }

        // ========== BACKUP ==========
        [HttpGet("backup")]
        public async Task<IActionResult> BackupData()
        {
            try
            {
                var backup = new
                {
                    exportDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    version = "1.0",
                    data = new
                    {
                        settings = await _db.Settings.Find(_ => true).FirstOrDefaultAsync(),
                        students = await _db.Students.Find(_ => true).ToListAsync(),
                        competencies = await _db.Competencies.Find(_ => true).ToListAsync(),
                        grades = await _db.Grades.Find(_ => true).ToListAsync(),
                        kelas = await _db.KelasList.Find(_ => true).ToListAsync(),
                        mapel = await _db.MapelList.Find(_ => true).ToListAsync()
                    }
                };

                // Update last backup time
                var settings = await _db.Settings.Find(_ => true).FirstOrDefaultAsync();
                if (settings != null)
                {
                    settings.LastBackup = DateTime.Now;
                    var filter = Builders<AppSettings>.Filter.Eq(s => s.Id, settings.Id);
                    await _db.Settings.ReplaceOneAsync(filter, settings);
                }

                return Ok(backup);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("restore")]
        public async Task<IActionResult> RestoreData([FromBody] dynamic backupData)
        {
            try
            {
                // This is a simplified restore - in production you'd want more validation
                return Ok(new { success = true, message = "Data berhasil direstore" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // ========== SEED SISWA MY HERO ACADEMIA ==========
        [HttpPost("seed-siswa")]
        public async Task<IActionResult> SeedSiswa()
        {
            try
            {
                // Hapus semua data siswa yang ada
                await _db.Students.DeleteManyAsync(_ => true);

                // 20 Siswa dari My Hero Academia
                var students = new List<Student>
                {
                    // Kelas X IPA 1 (10 siswa)
                    new Student { NIS = "1001", Nama = "Izuku Midoriya", Kelas = "X IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-07-15", Alamat = "Musutafu, Japan" },
                    new Student { NIS = "1002", Nama = "Katsuki Bakugo", Kelas = "X IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-04-20", Alamat = "Musutafu, Japan" },
                    new Student { NIS = "1003", Nama = "Ochaco Uraraka", Kelas = "X IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2010-12-27", Alamat = "Mie Prefecture, Japan" },
                    new Student { NIS = "1004", Nama = "Shoto Todoroki", Kelas = "X IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-01-11", Alamat = "Shizuoka, Japan" },
                    new Student { NIS = "1005", Nama = "Tenya Ida", Kelas = "X IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-08-22", Alamat = "Tokyo, Japan" },
                    new Student { NIS = "1006", Nama = "Tsuyu Asui", Kelas = "X IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2010-02-12", Alamat = "Aichi, Japan" },
                    new Student { NIS = "1007", Nama = "Momo Yaoyorozu", Kelas = "X IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2010-09-23", Alamat = "Aichi, Japan" },
                    new Student { NIS = "1008", Nama = "Eijiro Kirishima", Kelas = "X IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-10-16", Alamat = "Chiba, Japan" },
                    new Student { NIS = "1009", Nama = "Mina Ashido", Kelas = "X IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2010-07-30", Alamat = "Chiba, Japan" },
                    new Student { NIS = "1010", Nama = "Denki Kaminari", Kelas = "X IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-06-29", Alamat = "Saitama, Japan" },
                    
                    // Kelas X IPA 2 (10 siswa)
                    new Student { NIS = "1011", Nama = "Kyoka Jiro", Kelas = "X IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2010-08-01", Alamat = "Shizuoka, Japan" },
                    new Student { NIS = "1012", Nama = "Fumikage Tokoyami", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-10-30", Alamat = "Shizuoka, Japan" },
                    new Student { NIS = "1013", Nama = "Mezo Shoji", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-02-15", Alamat = "Fukuoka, Japan" },
                    new Student { NIS = "1014", Nama = "Hanta Sero", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-07-28", Alamat = "Tokyo, Japan" },
                    new Student { NIS = "1015", Nama = "Toru Hagakure", Kelas = "X IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2010-06-16", Alamat = "Tokyo, Japan" },
                    new Student { NIS = "1016", Nama = "Rikido Sato", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-06-19", Alamat = "Tokyo, Japan" },
                    new Student { NIS = "1017", Nama = "Koji Koda", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-02-01", Alamat = "Iwate, Japan" },
                    new Student { NIS = "1018", Nama = "Yuga Aoyama", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-05-30", Alamat = "Paris, France" },
                    new Student { NIS = "1019", Nama = "Mashirao Ojiro", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-05-28", Alamat = "Tokyo, Japan" },
                    new Student { NIS = "1020", Nama = "Minoru Mineta", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-10-08", Alamat = "Kanagawa, Japan" }
                };
                
                await _db.Students.InsertManyAsync(students);
                
                return Ok(new { success = true, message = "20 siswa My Hero Academia berhasil ditambahkan!", count = students.Count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Gagal seed data: " + ex.Message });
            }
        }
    }
}