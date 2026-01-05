<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="MyAssessment.Pages.Grades" %>

<!DOCTYPE html>
<html lang="id">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Input Penilaian - My Assessment</title>
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600;700&display=swap" rel="stylesheet">
    <link href="../assets/css/style.css" rel="stylesheet" />
    <link href="../assets/css/pages.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation -->
        <nav class="navbar">
            <div class="nav-container">
                <div class="nav-brand">
                    <i class="icon-assessment"></i>
                    <a href="../Default.aspx" style="text-decoration: none; color: inherit;">My Assessment</a>
                </div>
                <div class="nav-menu">
                    <a href="../Default.aspx" class="nav-link">Dashboard</a>
                    <a href="Students.aspx" class="nav-link">Data Siswa</a>
                    <a href="Teachers.aspx" class="nav-link">Data Guru</a>
                    <a href="Competencies.aspx" class="nav-link">Kompetensi</a>
                    <a href="#" class="nav-link active">Penilaian</a>
                </div>
            </div>
        </nav>

        <!-- Page Header -->
        <section class="page-header">
            <div class="container">
                <h1>Input Penilaian K13</h1>
                <p>Kelola penilaian berdasarkan Kompetensi Dasar (KD) untuk KI-3 dan KI-4</p>
            </div>
        </section>

        <!-- Content -->
        <section class="page-content">
            <div class="container">
                <!-- Filter Section -->
                <div class="action-bar">
                    <div class="search-filter">
                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                            <asp:ListItem Value="">Pilih Kelas</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                            <asp:ListItem Value="">Pilih Mata Pelajaran</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlCompetency" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCompetency_SelectedIndexChanged">
                            <asp:ListItem Value="">Pilih Kompetensi Dasar</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="action-buttons">
                        <asp:Button ID="btnExportWord" runat="server" Text="📄 Export Word" CssClass="btn-primary" OnClick="btnExportWord_Click" />
                        <asp:Button ID="btnExportCSV" runat="server" Text="📊 Export CSV" CssClass="btn-secondary" OnClick="btnExportCSV_Click" />
                    </div>
                </div>

                <!-- KI-3 (Pengetahuan) Section -->
                <div class="grade-input-container" id="ki3Container" runat="server" visible="false">
                    <div class="grade-header">
                        <h3>KI-3 (Pengetahuan)</h3>
                        <asp:Button ID="btnAddKI3Column" runat="server" Text="+ Tambah Kolom UH" CssClass="add-column-btn" OnClick="btnAddKI3Column_Click" />
                    </div>
                    
                    <div class="assessment-tabs">
                        <asp:Button ID="btnUH" runat="server" Text="Ulangan Harian" CssClass="tab-btn active" OnClick="btnUH_Click" />
                        <asp:Button ID="btnPTS" runat="server" Text="PTS" CssClass="tab-btn" OnClick="btnPTS_Click" />
                        <asp:Button ID="btnPAS" runat="server" Text="PAS" CssClass="tab-btn" OnClick="btnPAS_Click" />
                    </div>

                    <asp:Panel ID="pnlKI3Grid" runat="server">
                        <table class="grade-table">
                            <thead>
                                <tr>
                                    <th rowspan="2">No</th>
                                    <th rowspan="2">NISN</th>
                                    <th rowspan="2">Nama Siswa</th>
                                    <th id="thUHColumns" runat="server" colspan="1">Ulangan Harian</th>
                                    <th rowspan="2">Rata-rata UH</th>
                                    <th rowspan="2">PTS</th>
                                    <th rowspan="2">PAS</th>
                                    <th rowspan="2">Nilai Akhir</th>
                                    <th rowspan="2">Predikat</th>
                                </tr>
                                <tr id="trUHHeaders" runat="server">
                                    <th>UH 1</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyKI3" runat="server">
                                <!-- Dynamic content will be generated here -->
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>

                <!-- KI-4 (Keterampilan) Section -->
                <div class="grade-input-container" id="ki4Container" runat="server" visible="false">
                    <div class="grade-header">
                        <h3>KI-4 (Keterampilan)</h3>
                        <asp:Button ID="btnAddKI4Column" runat="server" Text="+ Tambah Kolom Praktik" CssClass="add-column-btn" OnClick="btnAddKI4Column_Click" />
                    </div>
                    
                    <div class="assessment-tabs">
                        <asp:Button ID="btnPraktik" runat="server" Text="Praktik" CssClass="tab-btn active" OnClick="btnPraktik_Click" />
                        <asp:Button ID="btnProyek" runat="server" Text="Proyek" CssClass="tab-btn" OnClick="btnProyek_Click" />
                        <asp:Button ID="btnPortofolio" runat="server" Text="Portofolio" CssClass="tab-btn" OnClick="btnPortofolio_Click" />
                    </div>

                    <asp:Panel ID="pnlKI4Grid" runat="server">
                        <table class="grade-table">
                            <thead>
                                <tr>
                                    <th rowspan="2">No</th>
                                    <th rowspan="2">NISN</th>
                                    <th rowspan="2">Nama Siswa</th>
                                    <th id="thPraktikColumns" runat="server" colspan="1">Praktik</th>
                                    <th rowspan="2">Proyek</th>
                                    <th rowspan="2">Portofolio</th>
                                    <th rowspan="2">Nilai Akhir</th>
                                    <th rowspan="2">Predikat</th>
                                </tr>
                                <tr id="trPraktikHeaders" runat="server">
                                    <th>P1</th>
                                </tr>
                            </thead>
                            <tbody id="tbodyKI4" runat="server">
                                <!-- Dynamic content will be generated here -->
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>

                <!-- Save Button -->
                <div class="form-actions" style="justify-content: center; margin-top: 30px;">
                    <asp:Button ID="btnSaveGrades" runat="server" Text="💾 Simpan Semua Nilai" CssClass="btn-primary" 
                        OnClick="btnSaveGrades_Click" style="padding: 15px 40px; font-size: 16px;" />
                </div>

                <!-- Message Panel -->
                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="message-panel">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>

                <!-- Instructions -->
                <div class="instructions-card" style="margin-top: 40px;">
                    <h4>Petunjuk Penggunaan:</h4>
                    <ul>
                        <li>Pilih Kelas, Mata Pelajaran, dan Kompetensi Dasar terlebih dahulu</li>
                        <li>Gunakan tombol "Tambah Kolom" untuk menambah kolom penilaian sesuai kebutuhan</li>
                        <li>Nilai diisi dalam rentang 0-100</li>
                        <li>Sistem akan menghitung nilai akhir dan predikat secara otomatis</li>
                        <li>Klik "Simpan Semua Nilai" untuk menyimpan perubahan</li>
                        <li>Gunakan tombol Export untuk mengunduh laporan dalam format Word atau CSV</li>
                    </ul>
                </div>
            </div>
        </section>
    </form>

    <script src="../assets/js/main.js"></script>
    <script src="../assets/js/grades.js"></script>
</body>
</html>