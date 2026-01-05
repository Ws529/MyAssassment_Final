<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyAssessment.Default" %>

<!DOCTYPE html>
<html lang="id">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>My Assessment - Sistem Penilaian K13</title>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    <link href="assets/css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation -->
        <nav class="navbar">
            <div class="nav-container">
                <div class="nav-brand">
                    <i class="icon-assessment"></i>
                    <span>My Assessment</span>
                </div>
                <div class="nav-menu">
                    <a href="#" class="nav-link">Dashboard</a>
                    <a href="#" class="nav-link">Data</a>
                    <a href="#" class="nav-link">Penilaian</a>
                    <a href="#" class="nav-link">FAQ</a>
                    <a href="#" class="nav-link">Kontak</a>
                    <button class="btn-primary">Admin Panel</button>
                </div>
            </div>
        </nav>

        <!-- Hero Section -->
        <section class="hero">
            <div class="hero-container">
                <div class="hero-content">
                    <h1>Sistem Penilaian Kurikulum Merdeka: Mudah & Otomatis</h1>
                    <p>Kelola Sistem Pembelajaran lebih Sistematis dan Rapi! Fitur Otomatis untuk penilaian berbasis Kompetensi Dasar (KD).</p>
                    <div class="search-box">
                        <input type="text" placeholder="Cari siswa, guru, atau mata pelajaran..." />
                        <button type="button">Cari</button>
                    </div>
                </div>
                <div class="hero-features">
                    <div class="feature-card">
                        <div class="feature-icon">📊</div>
                        <h3>Penilaian Akademik</h3>
                        <p>Sistem penilaian otomatis berdasarkan KI-3 dan KI-4 sesuai standar K13</p>
                    </div>
                    <div class="feature-card">
                        <div class="feature-icon">👥</div>
                        <h3>Data Siswa</h3>
                        <p>Kelola data siswa lengkap dengan riwayat penilaian dan rapor digital</p>
                    </div>
                    <div class="feature-card">
                        <div class="feature-icon">📋</div>
                        <h3>Kompetensi Dasar</h3>
                        <p>Manajemen KD yang terintegrasi dengan sistem penilaian otomatis</p>
                    </div>
                    <div class="feature-card">
                        <div class="feature-icon">📈</div>
                        <h3>Laporan Digital</h3>
                        <p>Ekspor laporan dalam format Word dan CSV untuk kebutuhan administrasi</p>
                    </div>
                </div>
            </div>
        </section>

        <!-- Browse Modules -->
        <section class="modules">
            <div class="container">
                <h2>Browse Modules</h2>
                <div class="module-tabs">
                    <button class="tab-btn active">Data Master</button>
                    <button class="tab-btn">Penilaian</button>
                    <button class="tab-btn">Laporan</button>
                </div>
                <div class="module-grid">
                    <div class="module-card">
                        <div class="module-icon">👥</div>
                        <h3>Data Siswa</h3>
                        <p>Kelola data lengkap siswa termasuk NISN, kelas, dan informasi akademik lainnya.</p>
                        <asp:Button ID="btnDataSiswa" runat="server" Text="Kelola Siswa" CssClass="btn-module" OnClick="btnDataSiswa_Click" />
                    </div>
                    <div class="module-card">
                        <div class="module-icon">👨‍🏫</div>
                        <h3>Data Guru</h3>
                        <p>Manajemen data guru meliputi mata pelajaran yang diampu dan informasi kontak.</p>
                        <asp:Button ID="btnDataGuru" runat="server" Text="Kelola Guru" CssClass="btn-module" OnClick="btnDataGuru_Click" />
                    </div>
                    <div class="module-card">
                        <div class="module-icon">📚</div>
                        <h3>Data Kelas</h3>
                        <p>Atur pembagian kelas, wali kelas, dan mata pelajaran untuk setiap tingkatan.</p>
                        <asp:Button ID="btnDataKelas" runat="server" Text="Kelola Kelas" CssClass="btn-module" OnClick="btnDataKelas_Click" />
                    </div>
                </div>
            </div>
        </section>

        <!-- How It Works -->
        <section class="how-it-works">
            <div class="container">
                <h2>Bagaimana Cara Kerjanya?</h2>
                <div class="steps">
                    <div class="step">
                        <div class="step-number">1</div>
                        <h3>Tentukan Tujuan Pembelajaran</h3>
                        <p>Setiap guru dapat menentukan tujuan pembelajaran berdasarkan Kompetensi Dasar untuk setiap mata pelajaran.</p>
                    </div>
                    <div class="step">
                        <div class="step-number">2</div>
                        <h3>Input Nilai Assessment</h3>
                        <p>Masukkan nilai penilaian harian, PTS, dan PAS untuk KI-3, serta nilai praktik untuk KI-4.</p>
                    </div>
                    <div class="step">
                        <div class="step-number">3</div>
                        <h3>Unduh Rapor Otomatis</h3>
                        <p>Sistem akan menghitung predikat otomatis dan menghasilkan rapor dalam format Word dan CSV.</p>
                    </div>
                </div>
            </div>
        </section>

        <!-- FAQ Section -->
        <section class="faq">
            <div class="container">
                <h2>Frequently Asked Questions</h2>
                <div class="faq-list">
                    <div class="faq-item">
                        <div class="faq-question">
                            <span>Apa perbedaan TP dengan KD pada kurikulum merdeka?</span>
                            <span class="faq-toggle">+</span>
                        </div>
                        <div class="faq-answer">
                            <p>KD (Kompetensi Dasar) adalah standar kompetensi yang harus dicapai siswa, sedangkan TP (Tujuan Pembelajaran) adalah penjabaran lebih spesifik dari KD untuk setiap pertemuan pembelajaran.</p>
                        </div>
                    </div>
                    <div class="faq-item">
                        <div class="faq-question">
                            <span>Bagaimana sistem menentukan predikat otomatis?</span>
                            <span class="faq-toggle">+</span>
                        </div>
                        <div class="faq-answer">
                            <p>Sistem menghitung berdasarkan KKM yang telah ditetapkan. Nilai ≥90 = A, 80-89 = B, sesuai KKM = C, di bawah KKM = D.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <!-- Footer -->
        <footer class="footer">
            <div class="container">
                <div class="footer-content">
                    <div class="footer-section">
                        <h3>My Assessment</h3>
                        <p>Sistem penilaian Kurikulum Merdeka yang mudah dan otomatis untuk pendidikan Indonesia.</p>
                    </div>
                    <div class="footer-section">
                        <h4>Fitur Utama</h4>
                        <ul>
                            <li>Kelola Pembelajaran</li>
                            <li>Penilaian Otomatis</li>
                            <li>Rapor Digital</li>
                            <li>Ekspor Otomatis</li>
                        </ul>
                    </div>
                    <div class="footer-section">
                        <h4>Dukungan</h4>
                        <ul>
                            <li>Panduan Pengguna</li>
                            <li>Video Tutorial</li>
                            <li>FAQ</li>
                            <li>Technical Support</li>
                        </ul>
                    </div>
                    <div class="footer-section">
                        <h4>Kontak</h4>
                        <ul>
                            <li>📧 support@myassessment.id</li>
                            <li>📞 +62 21 1234 5678</li>
                            <li>🌐 www.myassessment.id</li>
                        </ul>
                    </div>
                </div>
            </div>
        </footer>
    </form>

    <script src="assets/js/main.js"></script>
</body>
</html>