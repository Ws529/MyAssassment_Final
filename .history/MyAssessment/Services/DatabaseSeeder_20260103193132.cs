using MongoDB.Driver;
using MyAssessment.Models;

namespace MyAssessment.Services
{
    public class DatabaseSeeder
    {
        private readonly MongoDbService _db;

        public DatabaseSeeder(MongoDbService db)
        {
            _db = db;
        }

        public async Task CheckAndSeedAsync()
        {
            // Check if competencies collection is empty
            var count = await _db.Competencies.CountDocumentsAsync(_ => true);
            if (count == 0)
            {
                await SeedKompetensiDasarAsync();
                Console.WriteLine("✅ Database seeded with K13 Kompetensi Dasar data!");
            }
            else
            {
                Console.WriteLine($"ℹ️ Database already has {count} KD records. Skipping seed.");
            }

            // Seed Kelas if empty
            var kelasCount = await _db.KelasList.CountDocumentsAsync(_ => true);
            if (kelasCount == 0)
            {
                await SeedKelasAsync();
                Console.WriteLine("✅ Database seeded with Kelas data!");
            }

            // Seed Mapel if empty
            var mapelCount = await _db.MapelList.CountDocumentsAsync(_ => true);
            if (mapelCount == 0)
            {
                await SeedMapelAsync();
                Console.WriteLine("✅ Database seeded with Mapel data!");
            }

            // Seed Students if empty - My Hero Academia characters
            var studentCount = await _db.Students.CountDocumentsAsync(_ => true);
            if (studentCount == 0)
            {
                await SeedStudentsAsync();
                Console.WriteLine("✅ Database seeded with 20 My Hero Academia students!");
            }
            else
            {
                Console.WriteLine($"ℹ️ Database already has {studentCount} student records. Skipping seed.");
            }
        }

        private async Task SeedKelasAsync()
        {
            var kelasList = new List<Kelas>
            {
                new Kelas { Nama = "X IPA 1" },
                new Kelas { Nama = "X IPA 2" },
                new Kelas { Nama = "X IPS 1" },
                new Kelas { Nama = "X IPS 2" },
                new Kelas { Nama = "XI IPA 1" },
                new Kelas { Nama = "XI IPA 2" },
                new Kelas { Nama = "XI IPS 1" },
                new Kelas { Nama = "XI IPS 2" },
                new Kelas { Nama = "XII IPA 1" },
                new Kelas { Nama = "XII IPA 2" },
                new Kelas { Nama = "XII IPS 1" },
                new Kelas { Nama = "XII IPS 2" }
            };
            await _db.KelasList.InsertManyAsync(kelasList);
        }

        private async Task SeedMapelAsync()
        {
            var mapelList = new List<MapelItem>
            {
                new MapelItem { Nama = "Matematika", Kode = "MTK", Kelompok = "A", KKM = 75 },
                new MapelItem { Nama = "Fisika", Kode = "FIS", Kelompok = "C", KKM = 75 },
                new MapelItem { Nama = "Kimia", Kode = "KIM", Kelompok = "C", KKM = 75 },
                new MapelItem { Nama = "Biologi", Kode = "BIO", Kelompok = "C", KKM = 75 },
                new MapelItem { Nama = "Bahasa Indonesia", Kode = "BIN", Kelompok = "A", KKM = 78 },
                new MapelItem { Nama = "Bahasa Inggris", Kode = "BIG", Kelompok = "A", KKM = 75 },
                new MapelItem { Nama = "Bahasa Jepang", Kode = "BJP", Kelompok = "C", KKM = 72 }
            };
            await _db.MapelList.InsertManyAsync(mapelList);
        }

        private async Task SeedStudentsAsync()
        {
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
        }

        private async Task SeedKompetensiDasarAsync()
        {
            var kdList = new List<Competency>();

            // ========== MATEMATIKA (KKM 75) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Matematika", Kode = "3.1", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan dan menentukan penyelesaian sistem persamaan linear dua variabel" },
                new Competency { Mapel = "Matematika", Kode = "3.2", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan dan menentukan penyelesaian sistem persamaan linear tiga variabel" },
                new Competency { Mapel = "Matematika", Kode = "3.3", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan dan menentukan penyelesaian pertidaksamaan linear satu variabel" },
                new Competency { Mapel = "Matematika", Kode = "3.4", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan dan menentukan penyelesaian pertidaksamaan nilai mutlak" },
                new Competency { Mapel = "Matematika", Kode = "3.5", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan fungsi (terutama fungsi linear, fungsi kuadrat, dan fungsi rasional)" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Matematika", Kode = "4.1", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyelesaikan masalah kontekstual yang berkaitan dengan sistem persamaan linear dua variabel" },
                new Competency { Mapel = "Matematika", Kode = "4.2", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyelesaikan masalah kontekstual yang berkaitan dengan sistem persamaan linear tiga variabel" },
                new Competency { Mapel = "Matematika", Kode = "4.3", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyelesaikan masalah kontekstual yang berkaitan dengan pertidaksamaan linear satu variabel" },
                new Competency { Mapel = "Matematika", Kode = "4.4", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyelesaikan masalah kontekstual yang berkaitan dengan pertidaksamaan nilai mutlak" },
                new Competency { Mapel = "Matematika", Kode = "4.5", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan dan menyelesaikan masalah yang berkaitan dengan fungsi" }
            });

            // ========== FISIKA (KKM 75) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Fisika", Kode = "3.1", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan hakikat ilmu Fisika dan perannya dalam kehidupan, metode ilmiah, dan keselamatan kerja di laboratorium" },
                new Competency { Mapel = "Fisika", Kode = "3.2", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menerapkan prinsip-prinsip pengukuran besaran fisis, ketepatan, ketelitian, dan angka penting" },
                new Competency { Mapel = "Fisika", Kode = "3.3", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menerapkan prinsip penjumlahan vektor sebidang (misalnya perpindahan)" },
                new Competency { Mapel = "Fisika", Kode = "3.4", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menganalisis besaran-besaran fisis pada gerak lurus dengan kecepatan konstan dan gerak lurus dengan percepatan konstan" },
                new Competency { Mapel = "Fisika", Kode = "3.5", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menganalisis gerak parabola dengan menggunakan vektor, berikut makna fisisnya" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Fisika", Kode = "4.1", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Membuat prosedur kerja ilmiah dan keselamatan kerja misalnya pada pengukuran kalor" },
                new Competency { Mapel = "Fisika", Kode = "4.2", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan hasil pengukuran besaran fisis berikut ketelitiannya dengan menggunakan peralatan dan teknik yang tepat" },
                new Competency { Mapel = "Fisika", Kode = "4.3", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Merancang percobaan untuk menentukan resultan vektor sebidang beserta presentasi hasil dan makna fisisnya" },
                new Competency { Mapel = "Fisika", Kode = "4.4", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan data dan grafik hasil percobaan gerak benda untuk menyelidiki karakteristik gerak lurus" },
                new Competency { Mapel = "Fisika", Kode = "4.5", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Mempresentasikan data hasil percobaan gerak parabola dan makna fisisnya" }
            });

            // ========== KIMIA (KKM 75) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Kimia", Kode = "3.1", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan metode ilmiah, hakikat ilmu Kimia, keselamatan dan keamanan di laboratorium, serta peran kimia dalam kehidupan" },
                new Competency { Mapel = "Kimia", Kode = "3.2", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menganalisis perkembangan model atom Dalton, Thomson, Rutherford, Bohr, dan mekanika gelombang" },
                new Competency { Mapel = "Kimia", Kode = "3.3", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan konfigurasi elektron dan pola konfigurasi elektron terluar untuk setiap golongan dalam tabel periodik" },
                new Competency { Mapel = "Kimia", Kode = "3.4", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menganalisis kemiripan sifat unsur dalam golongan dan keperiodikannya" },
                new Competency { Mapel = "Kimia", Kode = "3.5", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Membandingkan ikatan ion, ikatan kovalen, ikatan kovalen koordinasi, dan ikatan logam serta kaitannya dengan sifat zat" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Kimia", Kode = "4.1", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan hasil rancangan dan hasil percobaan ilmiah" },
                new Competency { Mapel = "Kimia", Kode = "4.2", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan fenomena alam atau hasil percobaan menggunakan model atom" },
                new Competency { Mapel = "Kimia", Kode = "4.3", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menentukan letak suatu unsur dalam tabel periodik berdasarkan konfigurasi elektron" },
                new Competency { Mapel = "Kimia", Kode = "4.4", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan hasil analisis data-data unsur dalam kaitannya dengan kemiripan dan keperiodikan sifat unsur" },
                new Competency { Mapel = "Kimia", Kode = "4.5", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Merancang dan melakukan percobaan untuk menunjukkan karakteristik senyawa ion atau senyawa kovalen" }
            });

            // ========== BIOLOGI (KKM 75) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Biologi", Kode = "3.1", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan ruang lingkup biologi (permasalahan pada berbagai objek biologi dan tingkat organisasi kehidupan)" },
                new Competency { Mapel = "Biologi", Kode = "3.2", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menganalisis berbagai tingkat keanekaragaman hayati di Indonesia beserta ancaman dan pelestariannya" },
                new Competency { Mapel = "Biologi", Kode = "3.3", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menjelaskan prinsip-prinsip klasifikasi makhluk hidup dalam lima kingdom" },
                new Competency { Mapel = "Biologi", Kode = "3.4", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menganalisis struktur, replikasi dan peran virus dalam kehidupan" },
                new Competency { Mapel = "Biologi", Kode = "3.5", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Mengidentifikasi struktur, cara hidup, reproduksi dan peran bakteri dalam kehidupan" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Biologi", Kode = "4.1", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan data hasil penerapan metode ilmiah tentang permasalahan pada berbagai objek biologi" },
                new Competency { Mapel = "Biologi", Kode = "4.2", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan hasil observasi berbagai tingkat keanekaragaman hayati di Indonesia dan usulan upaya pelestariannya" },
                new Competency { Mapel = "Biologi", Kode = "4.3", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyusun kladogram berdasarkan prinsip-prinsip klasifikasi makhluk hidup" },
                new Competency { Mapel = "Biologi", Kode = "4.4", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Melakukan kampanye tentang bahaya virus dalam kehidupan terutama bahaya AIDS" },
                new Competency { Mapel = "Biologi", Kode = "4.5", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyajikan data tentang ciri-ciri dan peran bakteri dalam kehidupan" }
            });

            // ========== BAHASA INDONESIA (KKM 78) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Bahasa Indonesia", Kode = "3.1", Jenis = "KI-3", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengidentifikasi laporan hasil observasi yang dipresentasikan dengan lisan dan tulis" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "3.2", Jenis = "KI-3", KKM = 78, Kelas = "X",
                    Deskripsi = "Menganalisis isi dan aspek kebahasaan dari minimal dua teks laporan hasil observasi" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "3.3", Jenis = "KI-3", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengidentifikasi (permasalahan, argumentasi, pengetahuan, dan rekomendasi) teks eksposisi" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "3.4", Jenis = "KI-3", KKM = 78, Kelas = "X",
                    Deskripsi = "Menganalisis struktur dan kebahasaan teks eksposisi" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "3.5", Jenis = "KI-3", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengevaluasi teks anekdot dari aspek makna tersirat" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Bahasa Indonesia", Kode = "4.1", Jenis = "KI-4", KKM = 78, Kelas = "X",
                    Deskripsi = "Menginterpretasi isi teks laporan hasil observasi berdasarkan interpretasi baik secara lisan maupun tulis" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "4.2", Jenis = "KI-4", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengonstruksi teks laporan hasil observasi dengan memerhatikan isi dan aspek kebahasaan" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "4.3", Jenis = "KI-4", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengembangkan isi (permasalahan, argumen, pengetahuan, dan rekomendasi) teks eksposisi secara lisan dan tulis" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "4.4", Jenis = "KI-4", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengonstruksikan teks eksposisi dengan memerhatikan isi (permasalahan, argumen, pengetahuan, rekomendasi), struktur dan kebahasaan" },
                new Competency { Mapel = "Bahasa Indonesia", Kode = "4.5", Jenis = "KI-4", KKM = 78, Kelas = "X",
                    Deskripsi = "Mengonstruksi makna tersirat dalam sebuah teks anekdot baik lisan maupun tulis" }
            });

            // ========== BAHASA INGGRIS (KKM 75) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Bahasa Inggris", Kode = "3.1", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menerapkan fungsi sosial, struktur teks, dan unsur kebahasaan teks interaksi transaksional lisan dan tulis yang melibatkan tindakan memberi dan meminta informasi terkait jati diri dan hubungan keluarga" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "3.2", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menerapkan fungsi sosial, struktur teks, dan unsur kebahasaan teks interaksi interpersonal lisan dan tulis yang melibatkan tindakan memberikan ucapan selamat dan memuji bersayap (extended)" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "3.3", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Menerapkan fungsi sosial, struktur teks, dan unsur kebahasaan teks interaksi transaksional lisan dan tulis yang melibatkan tindakan memberi dan meminta informasi terkait niat melakukan suatu tindakan/kegiatan" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "3.4", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Membedakan fungsi sosial, struktur teks, dan unsur kebahasaan beberapa teks deskriptif lisan dan tulis dengan memberi dan meminta informasi terkait tempat wisata dan bangunan bersejarah terkenal" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "3.5", Jenis = "KI-3", KKM = 75, Kelas = "X",
                    Deskripsi = "Membedakan fungsi sosial, struktur teks, dan unsur kebahasaan beberapa teks khusus dalam bentuk pemberitahuan (announcement)" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Bahasa Inggris", Kode = "4.1", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyusun teks interaksi transaksional lisan dan tulis pendek dan sederhana yang melibatkan tindakan memberi dan meminta informasi terkait jati diri" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "4.2", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyusun teks interaksi interpersonal lisan dan tulis sederhana yang melibatkan tindakan memberikan ucapan selamat dan memuji bersayap (extended)" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "4.3", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyusun teks interaksi transaksional lisan dan tulis pendek dan sederhana yang melibatkan tindakan memberi dan meminta informasi terkait niat melakukan suatu tindakan/kegiatan" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "4.4", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyusun teks deskriptif lisan dan tulis, pendek dan sederhana, terkait tempat wisata dan bangunan bersejarah terkenal" },
                new Competency { Mapel = "Bahasa Inggris", Kode = "4.5", Jenis = "KI-4", KKM = 75, Kelas = "X",
                    Deskripsi = "Menyusun teks khusus dalam bentuk pemberitahuan (announcement), lisan dan tulis, pendek dan sederhana" }
            });

            // ========== BAHASA JEPANG (KKM 72) ==========
            kdList.AddRange(new[]
            {
                // KI-3 Pengetahuan
                new Competency { Mapel = "Bahasa Jepang", Kode = "3.1", Jenis = "KI-3", KKM = 72, Kelas = "X",
                    Deskripsi = "Mendemonstrasikan tindak tutur untuk memperkenalkan diri (自己紹介/Jikoshoukai) dengan memperhatikan fungsi sosial, struktur teks, dan unsur kebahasaan" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "3.2", Jenis = "KI-3", KKM = 72, Kelas = "X",
                    Deskripsi = "Mendemonstrasikan tindak tutur untuk menyatakan dan menanyakan keberadaan orang dan benda (います/あります)" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "3.3", Jenis = "KI-3", KKM = 72, Kelas = "X",
                    Deskripsi = "Mendemonstrasikan tindak tutur untuk menyatakan dan menanyakan jumlah benda dengan memperhatikan fungsi sosial dan unsur kebahasaan" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "3.4", Jenis = "KI-3", KKM = 72, Kelas = "X",
                    Deskripsi = "Mendemonstrasikan tindak tutur untuk menyatakan dan menanyakan waktu (jam, hari, tanggal, bulan, tahun)" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "3.5", Jenis = "KI-3", KKM = 72, Kelas = "X",
                    Deskripsi = "Mendemonstrasikan tindak tutur untuk menyatakan dan menanyakan kegiatan sehari-hari (毎日の活動)" },
                // KI-4 Keterampilan
                new Competency { Mapel = "Bahasa Jepang", Kode = "4.1", Jenis = "KI-4", KKM = 72, Kelas = "X",
                    Deskripsi = "Memproduksi teks lisan dan tulis sederhana untuk memperkenalkan diri dengan memperhatikan fungsi sosial, struktur teks, dan unsur kebahasaan" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "4.2", Jenis = "KI-4", KKM = 72, Kelas = "X",
                    Deskripsi = "Memproduksi teks lisan dan tulis untuk menyatakan dan menanyakan keberadaan orang dan benda" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "4.3", Jenis = "KI-4", KKM = 72, Kelas = "X",
                    Deskripsi = "Memproduksi teks lisan dan tulis untuk menyatakan dan menanyakan jumlah benda" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "4.4", Jenis = "KI-4", KKM = 72, Kelas = "X",
                    Deskripsi = "Memproduksi teks lisan dan tulis untuk menyatakan dan menanyakan waktu" },
                new Competency { Mapel = "Bahasa Jepang", Kode = "4.5", Jenis = "KI-4", KKM = 72, Kelas = "X",
                    Deskripsi = "Memproduksi teks lisan dan tulis untuk menyatakan dan menanyakan kegiatan sehari-hari" }
            });

            await _db.Competencies.InsertManyAsync(kdList);
        }
    }
}
