<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="MyAssessment.Pages.Students" %>

<!DOCTYPE html>
<html lang="id">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Data Siswa - My Assessment</title>
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
                    <a href="#" class="nav-link active">Data Siswa</a>
                    <a href="Teachers.aspx" class="nav-link">Data Guru</a>
                    <a href="Competencies.aspx" class="nav-link">Kompetensi</a>
                    <a href="Grades.aspx" class="nav-link">Penilaian</a>
                </div>
            </div>
        </nav>

        <!-- Page Header -->
        <section class="page-header">
            <div class="container">
                <h1>Data Siswa</h1>
                <p>Kelola data siswa lengkap dengan informasi akademik</p>
            </div>
        </section>

        <!-- Content -->
        <section class="page-content">
            <div class="container">
                <!-- Action Bar -->
                <div class="action-bar">
                    <div class="search-filter">
                        <asp:TextBox ID="txtSearch" runat="server" placeholder="Cari siswa..." CssClass="search-input"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="btn-search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="action-buttons">
                        <asp:Button ID="btnAddStudent" runat="server" Text="+ Tambah Siswa" CssClass="btn-primary" OnClick="btnAddStudent_Click" />
                        <asp:Button ID="btnExport" runat="server" Text="📊 Export CSV" CssClass="btn-secondary" OnClick="btnExport_Click" />
                    </div>
                </div>

                <!-- Student Form (Hidden by default) -->
                <div id="studentForm" class="form-container" runat="server" visible="false">
                    <div class="form-card">
                        <h3>
                            <asp:Label ID="lblFormTitle" runat="server" Text="Tambah Siswa Baru"></asp:Label>
                        </h3>
                        <div class="form-grid">
                            <div class="form-group">
                                <label>NISN</label>
                                <asp:TextBox ID="txtNISN" runat="server" CssClass="form-input" placeholder="Masukkan NISN"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNISN" runat="server" ControlToValidate="txtNISN" 
                                    ErrorMessage="NISN wajib diisi" CssClass="error-message" ValidationGroup="StudentForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Nama Lengkap</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-input" placeholder="Masukkan nama lengkap"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" 
                                    ErrorMessage="Nama wajib diisi" CssClass="error-message" ValidationGroup="StudentForm"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Kelas</label>
                                <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Pilih Kelas</asp:ListItem>
                                    <asp:ListItem Value="X IPA 1">X IPA 1</asp:ListItem>
                                    <asp:ListItem Value="X IPA 2">X IPA 2</asp:ListItem>
                                    <asp:ListItem Value="X IPS 1">X IPS 1</asp:ListItem>
                                    <asp:ListItem Value="XI IPA 1">XI IPA 1</asp:ListItem>
                                    <asp:ListItem Value="XI IPA 2">XI IPA 2</asp:ListItem>
                                    <asp:ListItem Value="XI IPS 1">XI IPS 1</asp:ListItem>
                                    <asp:ListItem Value="XII IPA 1">XII IPA 1</asp:ListItem>
                                    <asp:ListItem Value="XII IPA 2">XII IPA 2</asp:ListItem>
                                    <asp:ListItem Value="XII IPS 1">XII IPS 1</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Jenis Kelamin</label>
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="">Pilih Jenis Kelamin</asp:ListItem>
                                    <asp:ListItem Value="Laki-laki">Laki-laki</asp:ListItem>
                                    <asp:ListItem Value="Perempuan">Perempuan</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Tanggal Lahir</label>
                                <asp:TextBox ID="txtBirthDate" runat="server" CssClass="form-input" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="form-group full-width">
                                <label>Alamat</label>
                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-input" TextMode="MultiLine" 
                                    Rows="3" placeholder="Masukkan alamat lengkap"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn-primary" 
                                OnClick="btnSave_Click" ValidationGroup="StudentForm" />
                            <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="btn-secondary" 
                                OnClick="btnCancel_Click" CausesValidation="false" />
                        </div>
                    </div>
                </div>

                <!-- Students Grid -->
                <div class="data-grid">
                    <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="false" 
                        CssClass="grid-table" OnRowCommand="gvStudents_RowCommand" DataKeyNames="Id">
                        <Columns>
                            <asp:BoundField DataField="NISN" HeaderText="NISN" />
                            <asp:BoundField DataField="Name" HeaderText="Nama Lengkap" />
                            <asp:BoundField DataField="Class" HeaderText="Kelas" />
                            <asp:BoundField DataField="Gender" HeaderText="Jenis Kelamin" />
                            <asp:BoundField DataField="BirthDate" HeaderText="Tanggal Lahir" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:TemplateField HeaderText="Aksi">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn-edit" 
                                        CommandName="EditStudent" CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="Hapus" CssClass="btn-delete" 
                                        CommandName="DeleteStudent" CommandArgument='<%# Eval("Id") %>' 
                                        OnClientClick="return confirm('Yakin ingin menghapus data siswa ini?');" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="empty-state">
                                <h3>Belum ada data siswa</h3>
                                <p>Klik tombol "Tambah Siswa" untuk menambah data siswa baru</p>
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