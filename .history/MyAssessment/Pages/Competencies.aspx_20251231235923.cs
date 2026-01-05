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
    public partial class Competencies : Page
    {
        private MongoService _mongoService;
        private string EditingCompetencyId
        {
            get { return ViewState["EditingCompetencyId"] as string; }
            set { ViewState["EditingCompetencyId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _mongoService = new MongoService();
            
            if (!IsPostBack)
            {
                LoadFilterSubjects();
                LoadCompetencies();
            }
        }

        private void LoadFilterSubjects()
        {
            try
            {
                var subjects = new List<string> { "Matematika", "Bahasa Indonesia", "Bahasa Inggris", "Fisika", "Kimia", "Biologi", "Sejarah", "Geografi", "Ekonomi", "Sosiologi" };
                
                ddlFilterSubject.Items.Clear();
                ddlFilterSubject.Items.Add(new ListItem("Semua Mata Pelajaran", ""));
                
                foreach (var subject in subjects)
                {
                    ddlFilterSubject.Items.Add(new ListItem(subject, subject));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading filter subjects: " + ex.Message, "error");
            }
        }

        private void LoadCompetencies()
        {
            try
            {
                FilterBuilder<Competency> filterBuilder = Builders<Competency>.Filter;
                var filter = filterBuilder.Empty;

                // Apply subject filter if selected
                if (!string.IsNullOrEmpty(ddlFilterSubject.SelectedValue))
                {
                    filter = filterBuilder.Eq(c => c.Subject, ddlFilterSubject.SelectedValue);
                }

                // Apply search filter if provided
                var searchTerm = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var searchFilter = filterBuilder.Or(
                        filterBuilder.Regex(c => c.Code, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                        filterBuilder.Regex(c => c.Description, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i")),
                        filterBuilder.Regex(c => c.Subject, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"))
                    );

                    filter = filter == filterBuilder.Empty ? searchFilter : filterBuilder.And(filter, searchFilter);
                }

                var competencies = _mongoService.Competencies.Find(filter).ToList();
                gvCompetencies.DataSource = competencies;
                gvCompetencies.DataBind();
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading competencies: " + ex.Message, "error");
            }
        }

        protected void btnAddCompetency_Click(object sender, EventArgs e)
        {
            ClearForm();
            competencyForm.Visible = true;
            lblFormTitle.Text = "Tambah Kompetensi Dasar Baru";
            EditingCompetencyId = null;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    var competency = new Competency
                    {
                        Code = txtCode.Text.Trim(),
                        Description = txtDescription.Text.Trim(),
                        Subject = ddlSubject.SelectedValue,
                        Grade = ddlGrade.SelectedValue,
                        Type = ddlType.SelectedValue,
                        KKM = int.Parse(txtKKM.Text)
                    };

                    if (string.IsNullOrEmpty(EditingCompetencyId))
                    {
                        // Add new competency
                        _mongoService.Competencies.InsertOne(competency);
                        ShowMessage("Kompetensi Dasar berhasil ditambahkan!", "success");
                    }
                    else
                    {
                        // Update existing competency
                        competency.Id = EditingCompetencyId;
                        var filter = Builders<Competency>.Filter.Eq(c => c.Id, EditingCompetencyId);
                        _mongoService.Competencies.ReplaceOne(filter, competency);
                        ShowMessage("Kompetensi Dasar berhasil diperbarui!", "success");
                    }

                    ClearForm();
                    competencyForm.Visible = false;
                    LoadCompetencies();
                }
                catch (Exception ex)
                {
                    ShowMessage("Error saving competency: " + ex.Message, "error");
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            competencyForm.Visible = false;
            EditingCompetencyId = null;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCompetencies();
        }

        protected void ddlFilterSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCompetencies();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var competencies = _mongoService.Competencies.Find(_ => true).ToList();
                var csv = new StringBuilder();
                
                // Header
                csv.AppendLine("Kode KD,Mata Pelajaran,Kelas,Jenis KI,Deskripsi,KKM");
                
                // Data
                foreach (var competency in competencies)
                {
                    csv.AppendLine($"{competency.Code},{competency.Subject},{competency.Grade},{competency.Type},\"{competency.Description}\",{competency.KKM}");
                }

                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment; filename=KompetensiDasar.csv");
                Response.Write(csv.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ShowMessage("Error exporting data: " + ex.Message, "error");
            }
        }

        protected void gvCompetencies_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var competencyId = e.CommandArgument.ToString();

            try
            {
                if (e.CommandName == "EditCompetency")
                {
                    var filter = Builders<Competency>.Filter.Eq(c => c.Id, competencyId);
                    var competency = _mongoService.Competencies.Find(filter).FirstOrDefault();

                    if (competency != null)
                    {
                        txtCode.Text = competency.Code;
                        txtDescription.Text = competency.Description;
                        ddlSubject.SelectedValue = competency.Subject;
                        ddlGrade.SelectedValue = competency.Grade;
                        ddlType.SelectedValue = competency.Type;
                        txtKKM.Text = competency.KKM.ToString();

                        competencyForm.Visible = true;
                        lblFormTitle.Text = "Edit Kompetensi Dasar";
                        EditingCompetencyId = competencyId;
                    }
                }
                else if (e.CommandName == "DeleteCompetency")
                {
                    var filter = Builders<Competency>.Filter.Eq(c => c.Id, competencyId);
                    _mongoService.Competencies.DeleteOne(filter);
                    
                    ShowMessage("Kompetensi Dasar berhasil dihapus!", "success");
                    LoadCompetencies();
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, "error");
            }
        }

        private void ClearForm()
        {
            txtCode.Text = "";
            txtDescription.Text = "";
            ddlSubject.SelectedIndex = 0;
            ddlGrade.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            txtKKM.Text = "75";
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = $"message-panel {type}";
            pnlMessage.Visible = true;
        }
    }
}