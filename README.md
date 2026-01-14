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

Semua halaman menggunakan **layout konsisten**: sidebar menu + header sistem.

---

## ⚙️ Cara Menjalankan Aplikasi

### Prasyarat
- .NET SDK terinstal  
- MongoDB aktif  
- IIS terkonfigurasi  


