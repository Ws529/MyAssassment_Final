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
            // 69+ Siswa dari berbagai anime
            var students = new List<Student>
            {
                // ========== Kelas X IPA 1 - My Hero Academia (10 siswa) ==========
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
                
                // ========== Kelas X IPA 2 - My Hero Academia (6 siswa) ==========
                new Student { NIS = "1011", Nama = "Kyoka Jiro", Kelas = "X IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2010-08-01", Alamat = "Shizuoka, Japan" },
                new Student { NIS = "1012", Nama = "Fumikage Tokoyami", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-10-30", Alamat = "Shizuoka, Japan" },
                new Student { NIS = "1013", Nama = "Mezo Shoji", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-02-15", Alamat = "Fukuoka, Japan" },
                new Student { NIS = "1014", Nama = "Hanta Sero", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-07-28", Alamat = "Tokyo, Japan" },
                new Student { NIS = "1015", Nama = "Toru Hagakure", Kelas = "X IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2010-06-16", Alamat = "Tokyo, Japan" },
                new Student { NIS = "1016", Nama = "Yuga Aoyama", Kelas = "X IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-05-30", Alamat = "Paris, France" },

                // ========== Kelas X IPS 1 - Jujutsu Kaisen (6 siswa) ==========
                new Student { NIS = "1017", Nama = "Yuji Itadori", Kelas = "X IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-03-20", Alamat = "Sendai, Japan" },
                new Student { NIS = "1018", Nama = "Megumi Fushiguro", Kelas = "X IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-12-22", Alamat = "Tokyo, Japan" },
                new Student { NIS = "1019", Nama = "Nobara Kugisaki", Kelas = "X IPS 1", JenisKelamin = "Perempuan", TanggalLahir = "2010-08-07", Alamat = "Morioka, Japan" },
                new Student { NIS = "1020", Nama = "Maki Zenin", Kelas = "X IPS 1", JenisKelamin = "Perempuan", TanggalLahir = "2010-01-20", Alamat = "Kyoto, Japan" },
                new Student { NIS = "1021", Nama = "Toge Inumaki", Kelas = "X IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-10-23", Alamat = "Tokyo, Japan" },
                new Student { NIS = "1022", Nama = "Panda", Kelas = "X IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2010-03-05", Alamat = "Tokyo, Japan" },

                // ========== Kelas X IPS 2 - Spy x Family (5 siswa) ==========
                new Student { NIS = "1023", Nama = "Anya Forger", Kelas = "X IPS 2", JenisKelamin = "Perempuan", TanggalLahir = "2010-10-02", Alamat = "Berlint" },
                new Student { NIS = "1024", Nama = "Damian Desmond", Kelas = "X IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-10-01", Alamat = "Berlint" },
                new Student { NIS = "1025", Nama = "Becky Blackbell", Kelas = "X IPS 2", JenisKelamin = "Perempuan", TanggalLahir = "2010-08-08", Alamat = "Berlint" },
                new Student { NIS = "1026", Nama = "Emile Elman", Kelas = "X IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-05-15", Alamat = "Berlint" },
                new Student { NIS = "1027", Nama = "Ewen Egeburg", Kelas = "X IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2010-06-20", Alamat = "Berlint" },

                // ========== Kelas XI IPA 1 - Naruto (6 siswa) ==========
                new Student { NIS = "2001", Nama = "Naruto Uzumaki", Kelas = "XI IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-10-10", Alamat = "Konoha" },
                new Student { NIS = "2002", Nama = "Sasuke Uchiha", Kelas = "XI IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-07-23", Alamat = "Konoha" },
                new Student { NIS = "2003", Nama = "Sakura Haruno", Kelas = "XI IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2009-03-28", Alamat = "Konoha" },
                new Student { NIS = "2004", Nama = "Hinata Hyuga", Kelas = "XI IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2009-12-27", Alamat = "Konoha" },
                new Student { NIS = "2005", Nama = "Shikamaru Nara", Kelas = "XI IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-09-22", Alamat = "Konoha" },
                new Student { NIS = "2006", Nama = "Ino Yamanaka", Kelas = "XI IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2009-09-23", Alamat = "Konoha" },

                // ========== Kelas XI IPA 2 - One Piece (6 siswa) ==========
                new Student { NIS = "2007", Nama = "Monkey D. Luffy", Kelas = "XI IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-05-05", Alamat = "East Blue" },
                new Student { NIS = "2008", Nama = "Roronoa Zoro", Kelas = "XI IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-11-11", Alamat = "East Blue" },
                new Student { NIS = "2009", Nama = "Nami", Kelas = "XI IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2009-07-03", Alamat = "East Blue" },
                new Student { NIS = "2010", Nama = "Nico Robin", Kelas = "XI IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2009-02-06", Alamat = "Ohara" },
                new Student { NIS = "2011", Nama = "Vinsmoke Sanji", Kelas = "XI IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-03-02", Alamat = "North Blue" },
                new Student { NIS = "2012", Nama = "Tony Tony Chopper", Kelas = "XI IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-12-24", Alamat = "Drum Island" },

                // ========== Kelas XI IPS 1 - Attack on Titan (6 siswa) ==========
                new Student { NIS = "2013", Nama = "Eren Yeager", Kelas = "XI IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-03-30", Alamat = "Shiganshina" },
                new Student { NIS = "2014", Nama = "Mikasa Ackerman", Kelas = "XI IPS 1", JenisKelamin = "Perempuan", TanggalLahir = "2009-02-10", Alamat = "Shiganshina" },
                new Student { NIS = "2015", Nama = "Armin Arlert", Kelas = "XI IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-11-03", Alamat = "Shiganshina" },
                new Student { NIS = "2016", Nama = "Levi Ackerman", Kelas = "XI IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-12-25", Alamat = "Underground" },
                new Student { NIS = "2017", Nama = "Historia Reiss", Kelas = "XI IPS 1", JenisKelamin = "Perempuan", TanggalLahir = "2009-01-15", Alamat = "Wall Rose" },
                new Student { NIS = "2018", Nama = "Jean Kirstein", Kelas = "XI IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2009-04-07", Alamat = "Trost" },

                // ========== Kelas XI IPS 2 - Haikyuu!! (6 siswa) ==========
                new Student { NIS = "2019", Nama = "Shoyo Hinata", Kelas = "XI IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-06-21", Alamat = "Miyagi" },
                new Student { NIS = "2020", Nama = "Tobio Kageyama", Kelas = "XI IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-12-22", Alamat = "Miyagi" },
                new Student { NIS = "2021", Nama = "Kei Tsukishima", Kelas = "XI IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-09-27", Alamat = "Miyagi" },
                new Student { NIS = "2022", Nama = "Tadashi Yamaguchi", Kelas = "XI IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2009-11-10", Alamat = "Miyagi" },
                new Student { NIS = "2023", Nama = "Hitoka Yachi", Kelas = "XI IPS 2", JenisKelamin = "Perempuan", TanggalLahir = "2009-09-04", Alamat = "Miyagi" },
                new Student { NIS = "2024", Nama = "Kiyoko Shimizu", Kelas = "XI IPS 2", JenisKelamin = "Perempuan", TanggalLahir = "2009-01-06", Alamat = "Miyagi" },

                // ========== Kelas XII IPA 1 - Demon Slayer (6 siswa) ==========
                new Student { NIS = "3001", Nama = "Tanjiro Kamado", Kelas = "XII IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-07-14", Alamat = "Mt. Kumotori" },
                new Student { NIS = "3002", Nama = "Nezuko Kamado", Kelas = "XII IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2008-12-28", Alamat = "Mt. Kumotori" },
                new Student { NIS = "3003", Nama = "Zenitsu Agatsuma", Kelas = "XII IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-09-03", Alamat = "Tokyo" },
                new Student { NIS = "3004", Nama = "Inosuke Hashibira", Kelas = "XII IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-04-22", Alamat = "Mountain" },
                new Student { NIS = "3005", Nama = "Kanao Tsuyuri", Kelas = "XII IPA 1", JenisKelamin = "Perempuan", TanggalLahir = "2008-05-19", Alamat = "Butterfly Mansion" },
                new Student { NIS = "3006", Nama = "Genya Shinazugawa", Kelas = "XII IPA 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-01-07", Alamat = "Tokyo" },

                // ========== Kelas XII IPA 2 - Tokyo Revengers (6 siswa) ==========
                new Student { NIS = "3007", Nama = "Takemichi Hanagaki", Kelas = "XII IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-06-25", Alamat = "Tokyo" },
                new Student { NIS = "3008", Nama = "Hinata Tachibana", Kelas = "XII IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2008-05-21", Alamat = "Tokyo" },
                new Student { NIS = "3009", Nama = "Manjiro Sano", Kelas = "XII IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-08-20", Alamat = "Tokyo" },
                new Student { NIS = "3010", Nama = "Ken Ryuguji", Kelas = "XII IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-10-27", Alamat = "Tokyo" },
                new Student { NIS = "3011", Nama = "Emma Sano", Kelas = "XII IPA 2", JenisKelamin = "Perempuan", TanggalLahir = "2008-11-25", Alamat = "Tokyo" },
                new Student { NIS = "3012", Nama = "Chifuyu Matsuno", Kelas = "XII IPA 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-12-19", Alamat = "Tokyo" },

                // ========== Kelas XII IPS 1 - Blue Lock (6 siswa) ==========
                new Student { NIS = "3013", Nama = "Yoichi Isagi", Kelas = "XII IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-04-01", Alamat = "Saitama" },
                new Student { NIS = "3014", Nama = "Meguru Bachira", Kelas = "XII IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-08-08", Alamat = "Tokyo" },
                new Student { NIS = "3015", Nama = "Rensuke Kunigami", Kelas = "XII IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-01-03", Alamat = "Ehime" },
                new Student { NIS = "3016", Nama = "Hyoma Chigiri", Kelas = "XII IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-05-15", Alamat = "Kanagawa" },
                new Student { NIS = "3017", Nama = "Gin Gagamaru", Kelas = "XII IPS 1", JenisKelamin = "Laki-laki", TanggalLahir = "2008-02-28", Alamat = "Osaka" },
                new Student { NIS = "3018", Nama = "Anri Teieri", Kelas = "XII IPS 1", JenisKelamin = "Perempuan", TanggalLahir = "2008-07-07", Alamat = "Tokyo" },

                // ========== Kelas XII IPS 2 - Solo Leveling (6 siswa) ==========
                new Student { NIS = "3019", Nama = "Sung Jin-woo", Kelas = "XII IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-03-22", Alamat = "Seoul" },
                new Student { NIS = "3020", Nama = "Cha Hae-in", Kelas = "XII IPS 2", JenisKelamin = "Perempuan", TanggalLahir = "2008-06-18", Alamat = "Seoul" },
                new Student { NIS = "3021", Nama = "Yoo Jin-ho", Kelas = "XII IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-09-12", Alamat = "Seoul" },
                new Student { NIS = "3022", Nama = "Sung Jin-ah", Kelas = "XII IPS 2", JenisKelamin = "Perempuan", TanggalLahir = "2008-11-05", Alamat = "Seoul" },
                new Student { NIS = "3023", Nama = "Choi Jong-in", Kelas = "XII IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-11-11", Alamat = "Seoul" },
                new Student { NIS = "3024", Nama = "Baek Yoon-ho", Kelas = "XII IPS 2", JenisKelamin = "Laki-laki", TanggalLahir = "2008-09-09", Alamat = "Seoul" }
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
