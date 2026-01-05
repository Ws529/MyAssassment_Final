using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MyAssessment.Models;
using MyAssessment.Services;
using System.Text;
using System.Web;

namespace MyAssessment.Pages
{
    public partial class Students : Page
    {
        private MongoService _mongoService;
        private string EditingStudentId
        {
            get { return ViewState["EditingStudentId"] as string; }
            set { ViewState["EditingStudentId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _mongoService = new MongoService();
            
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        private void LoadStudents()
        {
            try
            {
                var students = _mongoService.Students.Find(_ => true).ToList();
                gvStudents.DataSource = students;
                gvStudents.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading students: " + ex.Message, "error");
            }
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            ClearForm();
            studentForm.Visible = true;
            lblFormTitle.Text = "Tambah Siswa Baru";
            EditingStudentId = null;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var student = new Student
                    {
                        NISN = txtNISN.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Class = ddlClass.SelectedValue,
                        Gender = ddlGender.SelectedValue,
                        BirthDate = DateTime.Parse(txtBirthDate.Text),
                        Address = txtAddress.Text.Trim()
                    };

                    if (string.IsNullOrEmpty(EditingStudentId))
                    {
                        // Add new student
                        _mongoService.Students.InsertOne(student);
                        ShowMessage("Data siswa berhasil ditambahkan!", "success");
                    }
                    else
                    {
                        // Update existing student
                        student.Id = EditingStudentId;
                        var filter = Builders<Student>.Filter.Eq(s => s.Id, EditingStudentId);
                        _mongoService.Students.ReplaceOne(filter, student);
                        ShowMessage("Data siswa berhasil diperbarui!", "success");
                    }

                    ClearForm();
                    studentForm.Visible = false;
                    LoadStudents();
                }
                catch (Exception ex)
                {
                    ShowMessage("Error saving student: " + ex.Message, "error");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            studentForm.Visible = false;
            EditingStudentId = null;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadStudents();
                    return;
                }

                var filter = Builders<Student>.Filter.Or(
                    Builders<Student>.Filter.Regex(s => s.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                    Builders<Student>.Filter.Regex(s => s.NISN, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                    Builders<Student>.Filter.Regex(s => s.Class, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
                );

                var students = _mongoService.Students.Find(filter).ToList();
                gvStudents.DataSource = students;
                gvStudents.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error searching students: " + ex.Message, "error");
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var students = _mongoService.Students.Find(_ => true).ToList();
                var csv = new StringBuilder();
                
                // Header
                csv.AppendLine("NISN,Nama,Kelas,Jenis Kelamin,Tanggal Lahir,Alamat");
                
                // Data
                foreach (var student in students)
                {
                    csv.AppendLine($"{student.NISN},{student.Name},{student.Class},{student.Gender},{student.BirthDate:dd/MM/yyyy},{student.Address}");
                }

                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=DataSiswa.csv");
                Response.Write(csv.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ShowMessage("Error exporting data: " + ex.Message, "error");
            }
        }

        protected void gvStudents_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var studentId = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "EditStudent")
                {
                    var filter = Builders<Student>.Filter.Eq(s => s.Id, studentId);
                    var student = _mongoService.Students.Find(filter).FirstOrDefault();

                    if (student != null)
                    {
                        txtNISN.Text = student.NISN;
                        txtName.Text = student.Name;
                        ddlClass.SelectedValue = student.Class;
                        ddlGender.SelectedValue = student.Gender;
                        txtBirthDate.Text = student.BirthDate.ToString("yyyy-MM-dd");
                        txtAddress.Text = student.Address;

                        studentForm.Visible = true;
                        lblFormTitle.Text = "Edit Data Siswa";
                        EditingStudentId = studentId;
                    }
                }
                else if (e.CommandName == "DeleteStudent")
                {
                    var filter = Builders<Student>.Filter.Eq(s => s.Id, studentId);
                    _mongoService.Students.DeleteOne(filter);
                    
                    ShowMessage("Data siswa berhasil dihapus!", "success");
                    LoadStudents();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, "error");
            }
        }

        private void ClearForm()
        {
            txtNISN.Text = "";
            txtName.Text = "";
            ddlClass.SelectedIndex = 0;
            ddlGender.SelectedIndex = 0;
            txtBirthDate.Text = "";
            txtAddress.Text = "";
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = $"message-panel {type}";
            pnlMessage.Visible = true;
        }
    }
}