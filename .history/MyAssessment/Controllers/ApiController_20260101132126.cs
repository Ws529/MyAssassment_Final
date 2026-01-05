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
            var data = await _db.Students.Find(_ => true).ToListAsync();
            return Ok(data);
        }

        [HttpPost("siswa")]
        public async Task<IActionResult> AddSiswa([FromBody] Student student)
        {
            await _db.Students.InsertOneAsync(student);
            return Ok(new { success = true, message = "Siswa berhasil ditambahkan" });
        }

        [HttpPut("siswa/{id}")]
        public async Task<IActionResult> UpdateSiswa(string id, [FromBody] Student student)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
            student.Id = id;
            await _db.Students.ReplaceOneAsync(filter, student);
            return Ok(new { success = true, message = "Siswa berhasil diperbarui" });
        }

        [HttpDelete("siswa/{id}")]
        public async Task<IActionResult> DeleteSiswa(string id)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
            await _db.Students.DeleteOneAsync(filter);
            return Ok(new { success = true, message = "Siswa berhasil dihapus" });
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
    }
}