using Microsoft.AspNetCore.Mvc;
using MyAssessment.Services;

namespace MyAssessment.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoDbService _db;

        public HomeController(MongoDbService db)
        {
            _db = db;
        }

        // Dashboard - /Home/Dashboard atau /
        public IActionResult Dashboard()
        {
            return View();
        }

        // Alias untuk index
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        // Data Siswa - /Home/DataSiswa
        public IActionResult DataSiswa()
        {
            return View();
        }

        // Kompetensi Dasar - /Home/KompetensiDasar
        public IActionResult KompetensiDasar()
        {
            return View();
        }

        // Input Penilaian - /Home/InputPenilaian
        public IActionResult InputPenilaian()
        {
            return View();
        }

        // Rekap Nilai - /Home/RekapNilai
        public IActionResult RekapNilai()
        {
            return View();
        }

        // Cetak Rapor - /Home/CetakRapor
        public IActionResult CetakRapor()
        {
            return View();
        }

        // Pengaturan - /Home/Pengaturan
        public IActionResult Pengaturan()
        {
            return View();
        }

        // Login - /Home/Login
        public IActionResult Login()
        {
            return View();
        }

        // Register - /Home/Register
        public IActionResult Register()
        {
            return View();
        }
    }
}
