using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;
using MyAssessment.Models;
using MyAssessment.Services;
using System.Text;

namespace MyAssessment.Pages
{
    public partial class Teachers : Page
    {
        private MongoService _mongoService;
        private string EditingTeacherId
        {
            get { return ViewState["EditingTeacherId"] as string; }
            set { ViewState["EditingTeacherId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _mongoService = new MongoService();
            
            if (!IsPostBack)
            {
                LoadTeachers();
            }
        }

        private void LoadTeachers()
        {
            try
            {
                var teachers = _mongoService.Teachers.Find(_ => true).ToList();
                gvTeachers.DataSource = teachers;
                gvTeachers.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading teachers: " + ex.Message, "error");
            }
        }

        protected void btnAddTeacher_Click(object sender, EventArgs e)
        {
            ClearForm();
            teacherForm.Visible = true;
            lblFormTitle.Text = "Tambah Guru Baru";
            EditingTeacherId = null;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var teacher = new Teacher
                    {
                        NIP = txtNIP.Text.Trim(),
                        Name = txtName.Text.Trim(),
                        Subject = ddlSubject.SelectedValue,
                        Email = txtEmail.Text.Trim(),
                        Phone = txtPhone.Text.Trim()
                    };

                    if (string.IsNullOrEmpty(EditingTeacherId))
                    {
                        // Add new teacher
                        _mongoService.Teachers.InsertOne(teacher);
                        ShowMessage("Data guru berhasil ditambahkan!", "success");
                    }
                    else
                    {
                        // Update existing teacher
                        teacher.Id = EditingTeacherId;
                        var filter = Builders<Teacher>.Filter.Eq(t => t.Id, EditingTeacherId);
                        _mongoService.Teachers.ReplaceOne(filter, teacher);
                        ShowMessage("Data guru berhasil diperbarui!", "success");
                    }

                    ClearForm();
                    teacherForm.Visible = false;
                    LoadTeachers();
                }
                catch (Exception ex)
                {
                    ShowMessage("Error saving teacher: " + ex.Message, "error");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            teacherForm.Visible = false;
            EditingTeacherId = null;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var searchTerm = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchTerm))
                {
                    LoadTeachers();
                    return;
                }

                var filter = Builders<Teacher>.Filter.Or(
                    Builders<Teacher>.Filter.Regex(t => t.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                    Builders<Teacher>.Filter.Regex(t => t.NIP, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                    Builders<Teacher>.Filter.Regex(t => t.Subject, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
                );

                var teachers = _mongoService.Teachers.Find(filter).ToList();
                gvTeachers.DataSource = teachers;
                gvTeachers.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error searching teachers: " + ex.Message, "error");
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var teachers = _mongoService.Teachers.Find(_ => true).ToList();
                var csv = new StringBuilder();
                
                // Header
                csv.AppendLine("NIP,Nama,Mata Pelajaran,Email,No. Telepon");
                
                // Data
                foreach (var teacher in teachers)
                {
                    csv.AppendLine($"{teacher.NIP},{teacher.Name},{teacher.Subject},{teacher.Email},{teacher.Phone}");
                }

                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=DataGuru.csv");
                Response.Write(csv.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ShowMessage("Error exporting data: " + ex.Message, "error");
            }
        }

        protected void gvTeachers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var teacherId = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "EditTeacher")
                {
                    var filter = Builders<Teacher>.Filter.Eq(t => t.Id, teacherId);
                    var teacher = _mongoService.Teachers.Find(filter).FirstOrDefault();

                    if (teacher != null)
                    {
                        txtNIP.Text = teacher.NIP;
                        txtName.Text = teacher.Name;
                        ddlSubject.SelectedValue = teacher.Subject;
                        txtEmail.Text = teacher.Email;
                        txtPhone.Text = teacher.Phone;

                        teacherForm.Visible = true;
                        lblFormTitle.Text = "Edit Data Guru";
                        EditingTeacherId = teacherId;
                    }
                }
                else if (e.CommandName == "DeleteTeacher")
                {
                    var filter = Builders<Teacher>.Filter.Eq(t => t.Id, teacherId);
                    _mongoService.Teachers.DeleteOne(filter);
                    
                    ShowMessage("Data guru berhasil dihapus!", "success");
                    LoadTeachers();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, "error");
            }
        }

        private void ClearForm()
        {
            txtNIP.Text = "";
            txtName.Text = "";
            ddlSubject.SelectedIndex = 0;
            txtEmail.Text = "";
            txtPhone.Text = "";
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = $"message-panel {type}";
            pnlMessage.Visible = true;
        }
    }
}