## MyAssessment

| Keterangan     | Data                         |
|----------------|------------------------------|
| Nama           | Wawan Suwandi                |
| NIM            | 312310457                    |
| Kelas          | TI23A5                       |
| Mata Kuliah    | Pemrograman Visual Desktop   |

---


## 🧩 Deskripsi Proyek

**MyAssessment** adalah sistem informasi berbasis web untuk mengelola pencatatan nilai akademik siswa sesuai **Kurikulum 2013**.  
Aplikasi ini memusatkan pengelolaan data siswa, kompetensi dasar, mata pelajaran, serta proses input dan rekap nilai dalam satu platform terintegrasi.

---

## 🎯 Latar Belakang

Pencatatan nilai secara manual menimbulkan:

- Risiko kesalahan data  
- Duplikasi entri  
- Sulitnya penelusuran histori nilai  
- Inefisiensi penyusunan laporan  

Digitalisasi melalui **MyAssessment** mengimplementasikan:

- Desain UI terstruktur  
- Arsitektur client–server  
- Integrasi database NoSQL  
- Akses melalui web server lokal  

---

## 🏁 Tujuan Pengembangan

- **Otomatisasi Penilaian**  
  Mengganti proses manual menjadi alur digital terstandarisasi.  

- **Efisiensi Administrasi**  
  Mempercepat pengelolaan data dan penyusunan laporan guru.  

- **Akurasi Data**  
  Menjamin konsistensi melalui penyimpanan terpusat.  

- **Kemudahan Akses**  
  Informasi akademik tersedia real-time melalui dashboard.  

---

## 🚀 Fitur Utama

### 1. Manajemen Data Siswa
- Input, edit, hapus data siswa  
- Pencarian berdasarkan NIS atau nama  

### 2. Manajemen Kompetensi Dasar
- Pengelolaan KD KI-3 dan KI-4  
- Penetapan KKM  
- Deskripsi kompetensi  

### 3. Manajemen Mata Pelajaran
- Pengelompokan Mapel A & B  
- Penetapan KKM per mapel  
- Manajemen kode dan nama  

### 4. Input Penilaian
- **KI-3**: UH1–UH3, PTS, PAS  
- **KI-4**: Praktik, Proyek, Portofolio  
- Perhitungan rata-rata otomatis  

### 5. Rekapitulasi Nilai
- Rekap per kelas & mapel  
- Akumulasi KI-3 dan KI-4  
- Predikat otomatis  
- Export ke CSV  

### 6. Dashboard Ringkasan
- Statistik siswa, kelas, mapel, KD  
- Aktivitas terbaru  
- Distribusi KD  

### 7. Autentikasi Pengguna
- Login berbasis akun  
- Profil guru & data sekolah  

---

## 🛠 Teknologi

| Komponen         | Teknologi       |
|------------------|-----------------|
| Framework        | .NET            |
| Database         | MongoDB         |
| Web Server       | IIS             |


---

## 🏗 Arsitektur Sistem

**Model: Client – Server Berbasis Web**

### Input  
Pengguna mengakses sistem melalui browser lokal dan mengisi data melalui form terstruktur.

### Proses  
Server IIS memproses request HTTP, aplikasi .NET menjalankan:  
- Validasi data  
- Perhitungan nilai  
- Logika Kurikulum 2013  

### Penyimpanan  
MongoDB menyimpan data dalam format dokumen JSON, dengan koleksi terpisah untuk:  
- Siswa  
- Kompetensi Dasar  
- Nilai  
- Mata Pelajaran  

### Output  
- Dashboard real-time  
- Laporan rekap  
- File export CSV  

---

## 🖥 Desain Antarmuka

- **Login** : Form minimalis autentikasi  
- **Dashboard** :  
  - Statistik utama  
  - Aktivitas terbaru  
  - Grafik distribusi KD  
- **Data Siswa** :  
  - Tabel interaktif + search bar  
- **Kompetensi Dasar** :  
  - Filter per mata pelajaran  
- **Mata Pelajaran** :  
  - Statistik kelompok A & B  
- **Input Penilaian** :  
  - Dropdown kelas, mapel, KD  
  - Rata-rata & predikat otomatis  
- **Rekap Nilai** :  
  - Filter kelas & mapel  
  - Tombol export CSV  
- **Profil** :  
  - Data guru & sekolah  

## ⚙️ Cara Menjalankan Aplikasi

### Prasyarat
- .NET SDK terinstal  
- MongoDB aktif  
- IIS terkonfigurasi  

---

## 🖥 Tampilan Antarmuka (UI) MyAssessment

Bagian ini menjelaskan struktur dan fungsi setiap halaman utama pada sistem **MyAssessment** agar memudahkan pemahaman alur penggunaan aplikasi.

---

### 🔐 Halaman Login
- Form autentikasi dengan field **Username** dan **Password**  
- Desain minimalis dan fokus pada keamanan akses  
- Validasi input untuk mencegah login tidak sah  

---

### 📊 Dashboard Utama
Menjadi pusat kontrol sistem dengan informasi ringkas dan real-time:

- **Statistik Ringkas**
  - Total siswa  
  - Jumlah kelas aktif  
  - Total mata pelajaran  
  - Jumlah kompetensi dasar  

- **Panel Aktivitas**
  - Riwayat input nilai terbaru  
  - Perubahan data siswa  
  - Update kompetensi dasar  

- **Grafik Visual**
  - Distribusi KD per mata pelajaran  
  - Monitoring ketercapaian kompetensi  

---

### 👨‍🎓 Halaman Data Siswa
- Tabel interaktif dengan kolom:  
  **No | NIS | Nama Lengkap | Kelas | Jenis Kelamin | Aksi**  
- Fitur:
  - Tambah data siswa  
  - Edit data  
  - Hapus data  
  - Pencarian cepat (NIS/Nama)  

---

### 📘 Halaman Kompetensi Dasar
- Tabel KD dengan kolom:  
  **Kode KD | Mata Pelajaran | Jenis (KI-3/KI-4) | Deskripsi | KKM | Aksi**  
- Fitur:
  - Filter berdasarkan mata pelajaran  
  - Pengelolaan standar ketuntasan  
  - Pengaturan deskripsi kompetensi  

---

### 📚 Halaman Mata Pelajaran
- Tabel data mapel dengan kolom:  
  **Kode | Nama Mata Pelajaran | Kelompok (A/B) | KKM | Aksi**  
- Informasi tambahan:
  - Statistik jumlah mapel per kelompok  
  - Konsistensi data kurikulum  

---

### 📝 Halaman Input Penilaian
- Form seleksi:
  - **Kelas**  
  - **Mata Pelajaran**  
  - **Kompetensi Dasar**  

- Tabel nilai siswa:
  - Kolom **UH1–UH3, PTS, PAS** untuk KI-3  
  - Kolom **Praktik, Proyek, Portofolio** untuk KI-4  
  - Kolom **Rata-rata** dan **Predikat** terisi otomatis  

- Validasi otomatis:
  - Rentang nilai  
  - Kelengkapan input  

---

### 📑 Halaman Rekapitulasi Nilai
- Filter:
  - Berdasarkan **Kelas**  
  - Berdasarkan **Mata Pelajaran**  

- Tabel rekap:
  **NIS | Nama | KI-3 | KI-4 | Rata-rata | Predikat**  

- Fitur tambahan:
  - Export data ke **CSV**  
  - Tampilan ringkasan performa kelas  

---

### 👤 Halaman Profil
- Konfigurasi data:
  - Profil guru  
  - Informasi sekolah (nama, NPSN, alamat)  
- Manajemen akun pengguna  

---

## 🎨 Prinsip Desain UI

Seluruh tampilan MyAssessment dirancang dengan prinsip:

- **Konsistensi Layout**  
  Sidebar menu di kiri, header sistem di atas untuk seluruh halaman.

- **Kejelasan Navigasi**  
  Ikon dan label menu mudah dipahami oleh pengguna non-teknis.

- **Efisiensi Interaksi**  
  Minim klik untuk proses utama seperti input nilai dan pencarian data.

- **Keterbacaan Tinggi**  
  Penggunaan tabel, heading, dan spacing yang rapi untuk mengurangi beban visual.

- **Fokus pada Fungsionalitas**  
  Setiap elemen UI memiliki peran langsung terhadap alur kerja guru dan admin.


---

<img width="1347" height="642" alt="Halaman Rekap Nilai " src="https://github.com/user-attachments/assets/8ec809a4-f0d3-4f3f-b3b1-afddfb5b88a4" />
<img width="630" height="618" alt="Halaman Register   Login" src="https://github.com/user-attachments/assets/8783535a-7931-4797-8723-344a56142093" />
<img width="471" height="579" alt="Halaman Profil" src="https://github.com/user-attachments/assets/fc0ef744-b41d-41c3-90a6-d9ff93571498" />
<img width="1343" height="644" alt="Halaman Mata Pelajaran" src="https://github.com/user-attachments/assets/6083f190-ad0e-4d29-9138-a1cc1aa7a9b5" />
<img width="1342" height="640" alt="Halaman Kompetensi Dasar (KD)" src="https://github.com/user-attachments/assets/2bcdce87-fea9-45dd-a8cf-cc93aabfa2a2" />
<img width="1344" height="642" alt="Halaman Input Nilai" src="https://github.com/user-attachments/assets/3b826b3a-b67e-4715-a1d6-839b81d8ae00" />
<img width="1345" height="639" alt="Halaman Data Siswa" src="https://github.com/user-attachments/assets/765470dc-c6cc-42df-b251-9395d9b5f3e2" />
<img width="1352" height="641" alt="Halaman Dashboard" src="https://github.com/user-attachments/assets/4519b9d1-d978-41e5-8221-5d2ef056751e" />

---


