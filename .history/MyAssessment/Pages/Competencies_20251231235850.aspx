<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Competencies.aspx.cs" Inherits="MyAssessment.Pages.Competencies" %>

<!DOCTYPE html>
<html lang="id">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Kompetensi Dasar - My Assessment</title>
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
                    <a href="#" class="nav-link active">Kompetensi</a>
                    <a href="Grades.aspx" class="nav-link">Penilaian</a>
                </div>
            </div>
        </nav>

        <!-- Page Header -->
        <section class="page-header">
            <div class="container">
                <h1>Kompetensi Dasar (KD)</h1>
                <p>Kelola Kompetensi Dasar untuk setiap mata pelajaran sesuai K13</p>
            </div>
        </section>

        <!-- Content -->
        <section class="page-content">
            <div class="container">
                <!-- Action Bar -->
                <div class="action-bar">
                    <div class="search-filter">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Cari KD..." CssClass="search-input"></asp:TextBox>
                        <asp:DropDownList ID="ddlFilterSubject" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterSubject_SelectedIndexChanged">
                            <asp:ListItem Value="">Semua Mata Pelajaran</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="btn-search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="action-buttons">
                        <asp:Button ID="btnAddCompetency" runat="server" Text="+ Tambah KD" CssClass="btn-primary" OnClick="btnAddCompetency_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="📊 Export CSV" CssClass="btn-secondary" OnClick="btnExport_Click" />
                    </div>
                </div>

                <!-- Competency Form -->
                <div id="competencyForm" class="form-container" runat="server" visible="false">
                    <div class="form-card">
                        <h3>
                            <asp:Label ID="lblFormTitle" runat="server" Text="Tambah Kompetensi Dasar Baru"></asp:Label>
                        </h3>
                        <div class="form-grid">
                            <div class="form-group">
                                <label>Kode KD</label>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="form-input" placeholder="Contoh: 3.1, 4.1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCode" 
                                    ErrorMessage="Kode KD wajib diisi" CssClass="error-message" ValidationGroup="CompetencyForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Mata Pelajaran</label>
                                <asp:DropDownList ID="ddlSubject" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Pilih Mata Pelajaran</asp:ListItem>
                                    <asp:ListItem Value="Matematika">Matematika</asp:ListItem>
                                    <asp:ListItem Value="Bahasa Indonesia">Bahasa Indonesia</asp:ListItem>
                                    <asp:ListItem Value="Bahasa Inggris">Bahasa Inggris</asp:ListItem>
                                    <asp:ListItem Value="Fisika">Fisika</asp:ListItem>
                                    <asp:ListItem Value="Kimia">Kimia</asp:ListItem>
                                    <asp:ListItem Value="Biologi">Biologi</asp:ListItem>
                                    <asp:ListItem Value="Sejarah">Sejarah</asp:ListItem>
                                    <asp:ListItem Value="Geografi">Geografi</asp:ListItem>
                                    <asp:ListItem Value="Ekonomi">Ekonomi</asp:ListItem>
                                    <asp:ListItem Value="Sosiologi">Sosiologi</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="ddlSubject" 
                                    ErrorMessage="Mata pelajaran wajib dipilih" CssClass="error-message" ValidationGroup="CompetencyForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Kelas</label>
                                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Pilih Kelas</asp:ListItem>
                                    <asp:ListItem Value="X">X</asp:ListItem>
                                    <asp:ListItem Value="XI">XI</asp:ListItem>
                                    <asp:ListItem Value="XII">XII</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Jenis KI</label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Pilih Jenis KI</asp:ListItem>
                                    <asp:ListItem Value="KI-3">KI-3 (Pengetahuan)</asp:ListItem>
                                    <asp:ListItem Value="KI-4">KI-4 (Keterampilan)</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" 
                                    ErrorMessage="Jenis KI wajib dipilih" CssClass="error-message" ValidationGroup="CompetencyForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>KKM</label>
                                <asp:TextBox ID="txtKKM" runat="server" CssClass="form-input" TextMode="Number" 
                                    placeholder="75" Text="75" min="0" max="100"></asp:TextBox>
                                <asp:RangeValidator ID="rvKKM" runat="server" ControlToValidate="txtKKM" 
                                    MinimumValue="0" MaximumValue="100" Type="Integer"
                                    ErrorMessage="KKM harus antara 0-100" CssClass="error-message" ValidationGroup="CompetencyForm"></asp:RangeValidator>
                            </div>
                            <div class="form-group full-width">
                                <label>Deskripsi Kompetensi Dasar</label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-input" TextMode="MultiLine" 
                                    Rows="4" placeholder="Masukkan deskripsi lengkap KD"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" 
                                    ErrorMessage="Deskripsi KD wajib diisi" CssClass="error-message" ValidationGroup="CompetencyForm"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn-primary" 
                                OnClick="btnSave_Click" ValidationGroup="CompetencyForm" />
                            <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn-secondary" 
                                OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <!-- Competencies Grid -->
                <div class="data-grid">
                    <asp:GridView ID="gvCompetencies" runat="server" AutoGenerateColumns="false" 
                        CssClass="grid-table" OnRowCommand="gvCompetencies_RowCommand" DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="Code" HeaderText="Kode KD" />
                            <asp:BoundField DataField="Subject" HeaderText="Mata Pelajaran" />
                            <asp:BoundField DataField="Grade" HeaderText="Kelas" />
                            <asp:BoundField DataField="Type" HeaderText="Jenis KI" />
                            <asp:BoundField DataField="Description" HeaderText="Deskripsi" />
                            <asp:BoundField DataField="KKM" HeaderText="KKM" />
                            <asp:TemplateField HeaderText="Aksi">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-edit" 
                                        CommandName="EditCompetency" CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="Hapus" CssClass="btn-delete" 
                                        CommandName="DeleteCompetency" CommandArgument='<%# Eval("Id") %>' 
                                        OnClientClick="return confirm('Yakin ingin menghapus KD ini?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="empty-state">
                                <h3>Belum ada data Kompetensi Dasar</h3>
                                <p>Klik tombol "Tambah KD" untuk menambah Kompetensi Dasar baru</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>

                <!-- Message Panel -->
                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="message-panel">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>

                <!-- Info Card -->
                <div class="instructions-card" style="margin-top: 40px;">
                    <h4>Informasi Kompetensi Dasar K13:</h4>
                    <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 20px; margin-top: 15px;">
                        <div>
                            <h5>KI-3 (Pengetahuan)</h5>
                            <ul>
                                <li>Kode dimulai dengan angka 3 (contoh: 3.1, 3.2)</li>
                                <li>Penilaian: Ulangan Harian, PTS, PAS</li>
                                <li>Fokus pada aspek kognitif dan pemahaman</li>
                            </ul>
                        </div>
                        <div>
                            <h5>KI-4 (Keterampilan)</h5>
                            <ul>
                                <li>Kode dimulai dengan angka 4 (contoh: 4.1, 4.2)</li>
                                <li>Penilaian: Praktik, Proyek, Portofolio</li>
                                <li>Fokus pada aspek psikomotor dan keterampilan</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </form>

    <script src="../assets/js/main.js"></script>
</body>
</html>