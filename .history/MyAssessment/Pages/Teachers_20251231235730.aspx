<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Teachers.aspx.cs" Inherits="MyAssessment.Pages.Teachers" %>

<!DOCTYPE html>
<html lang="id">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Data Guru - My Assessment</title>
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
                    <a href="#" class="nav-link active">Data Guru</a>
                    <a href="Competencies.aspx" class="nav-link">Kompetensi</a>
                    <a href="Grades.aspx" class="nav-link">Penilaian</a>
                </div>
            </div>
        </nav>

        <!-- Page Header -->
        <section class="page-header">
            <div class="container">
                <h1>Data Guru</h1>
                <p>Kelola data guru dan mata pelajaran yang diampu</p>
            </div>
        </section>

        <!-- Content -->
        <section class="page-content">
            <div class="container">
                <!-- Action Bar -->
                <div class="action-bar">
                    <div class="search-filter">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Cari guru..." CssClass="search-input"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="btn-search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="action-buttons">
                        <asp:Button ID="btnAddTeacher" runat="server" Text="+ Tambah Guru" CssClass="btn-primary" OnClick="btnAddTeacher_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="📊 Export CSV" CssClass="btn-secondary" OnClick="btnExport_Click" />
                    </div>
                </div>

                <!-- Teacher Form -->
                <div id="teacherForm" class="form-container" runat="server" visible="false">
                    <div class="form-card">
                        <h3>
                            <asp:Label ID="lblFormTitle" runat="server" Text="Tambah Guru Baru"></asp:Label>
                        </h3>
                        <div class="form-grid">
                            <div class="form-group">
                                <label>NIP</label>
                                <asp:TextBox ID="txtNIP" runat="server" CssClass="form-input" placeholder="Masukkan NIP"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNIP" runat="server" ControlToValidate="txtNIP" 
                                    ErrorMessage="NIP wajib diisi" CssClass="error-message" ValidationGroup="TeacherForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Nama Lengkap</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-input" placeholder="Masukkan nama lengkap"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" 
                                    ErrorMessage="Nama wajib diisi" CssClass="error-message" ValidationGroup="TeacherForm"></asp:RequiredFieldValidator>
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
                                    <asp:ListItem Value="PKN">PKN</asp:ListItem>
                                    <asp:ListItem Value="Seni Budaya">Seni Budaya</asp:ListItem>
                                    <asp:ListItem Value="Penjaskes">Penjaskes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-input" TextMode="Email" placeholder="Masukkan email"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>No. Telepon</label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-input" placeholder="Masukkan nomor telepon"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn-primary" 
                                OnClick="btnSave_Click" ValidationGroup="TeacherForm" />
                            <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn-secondary" 
                                OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <!-- Teachers Grid -->
                <div class="data-grid">
                    <asp:GridView ID="gvTeachers" runat="server" AutoGenerateColumns="false" 
                        CssClass="grid-table" OnRowCommand="gvTeachers_RowCommand" DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="NIP" HeaderText="NIP" />
                            <asp:BoundField DataField="Name" HeaderText="Nama Lengkap" />
                            <asp:BoundField DataField="Subject" HeaderText="Mata Pelajaran" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Phone" HeaderText="No. Telepon" />
                            <asp:TemplateField HeaderText="Aksi">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-edit" 
                                        CommandName="EditTeacher" CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="Hapus" CssClass="btn-delete" 
                                        CommandName="DeleteTeacher" CommandArgument='<%# Eval("Id") %>' 
                                        OnClientClick="return confirm('Yakin ingin menghapus data guru ini?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="empty-state">
                                <h3>Belum ada data guru</h3>
                                <p>Klik tombol "Tambah Guru" untuk menambah data guru baru</p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>

                <!-- Message Panel -->
                <asp:Panel ID="pnlMessage" runat="server" Visible="false" CssClass="message-panel">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </section>
    </form>

    <script src="../assets/js/main.js"></script>
</body>
</html>