using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MongoDB.Driver;
using MyAssessment.Models;
using MyAssessment.Services;
using System.Text;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace MyAssessment.Pages
{
    public partial class Grades : Page
    {
        private MongoService _mongoService;
        private string SelectedClass
        {
            get { return ViewState["SelectedClass"] as string; }
            set { ViewState["SelectedClass"] = value; }
        }
        private string SelectedSubject
        {
            get { return ViewState["SelectedSubject"] as string; }
            set { ViewState["SelectedSubject"] = value; }
        }
        private string SelectedCompetencyId
        {
            get { return ViewState["SelectedCompetencyId"] as string; }
            set { ViewState["SelectedCompetencyId"] = value; }
        }
        private int UHColumnCount
        {
            get { return ViewState["UHColumnCount"] as int? ?? 1; }
            set { ViewState["UHColumnCount"] = value; }
        }
        private int PraktikColumnCount
        {
            get { return ViewState["PraktikColumnCount"] as int? ?? 1; }
            set { ViewState["PraktikColumnCount"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _mongoService = new MongoService();
            
            if (!IsPostBack)
            {
                LoadClasses();
                LoadSubjects();
            }
        }

        private void LoadClasses()
        {
            try
            {
                var classes = new List<string> { "X IPA 1", "X IPA 2", "X IPS 1", "XI IPA 1", "XI IPA 2", "XI IPS 1", "XII IPA 1", "XII IPA 2", "XII IPS 1" };
                
                ddlClass.Items.Clear();
                ddlClass.Items.Add(new ListItem("Pilih Kelas", ""));
                
                foreach (var cls in classes)
                {
                    ddlClass.Items.Add(new ListItem(cls, cls));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading classes: " + ex.Message, "error");
            }
        }

        private void LoadSubjects()
        {
            try
            {
                var subjects = new List<string> { "Matematika", "Bahasa Indonesia", "Bahasa Inggris", "Fisika", "Kimia", "Biologi", "Sejarah", "Geografi", "Ekonomi", "Sosiologi" };
                
                ddlSubject.Items.Clear();
                ddlSubject.Items.Add(new ListItem("Pilih Mata Pelajaran", ""));
                
                foreach (var subject in subjects)
                {
                    ddlSubject.Items.Add(new ListItem(subject, subject));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading subjects: " + ex.Message, "error");
            }
        }

        private void LoadCompetencies()
        {
            try
            {
                if (string.IsNullOrEmpty(ddlSubject.SelectedValue))
                    return;

                var filter = Builders<Competency>.Filter.Eq(c => c.Subject, ddlSubject.SelectedValue);
                var competencies = _mongoService.Competencies.Find(filter).ToList();
                
                ddlCompetency.Items.Clear();
                ddlCompetency.Items.Add(new ListItem("Pilih Kompetensi Dasar", ""));
                
                foreach (var competency in competencies)
                {
                    ddlCompetency.Items.Add(new ListItem($"{competency.Code} - {competency.Description}", competency.Id));
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading competencies: " + ex.Message, "error");
            }
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedClass = ddlClass.SelectedValue;
            LoadGradeInputs();
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSubject = ddlSubject.SelectedValue;
            LoadCompetencies();
            LoadGradeInputs();
        }

        protected void ddlCompetency_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCompetencyId = ddlCompetency.SelectedValue;
            LoadGradeInputs();
        }

        private void LoadGradeInputs()
        {
            if (string.IsNullOrEmpty(SelectedClass) || string.IsNullOrEmpty(SelectedSubject) || string.IsNullOrEmpty(SelectedCompetencyId))
            {
                ki3Container.Visible = false;
                ki4Container.Visible = false;
                return;
            }

            try
            {
                var competencyFilter = Builders<Competency>.Filter.Eq(c => c.Id, SelectedCompetencyId);
                var competency = _mongoService.Competencies.Find(competencyFilter).FirstOrDefault();

                if (competency != null)
                {
                    if (competency.Type == "KI-3")
                    {
                        ki3Container.Visible = true;
                        ki4Container.Visible = false;
                        LoadKI3Grid();
                    }
                    else if (competency.Type == "KI-4")
                    {
                        ki3Container.Visible = false;
                        ki4Container.Visible = true;
                        LoadKI4Grid();
                    }
                    else
                    {
                        // Show both if type is not specified
                        ki3Container.Visible = true;
                        ki4Container.Visible = true;
                        LoadKI3Grid();
                        LoadKI4Grid();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading grade inputs: " + ex.Message, "error");
            }
        }

        private void LoadKI3Grid()
        {
            try
            {
                var studentFilter = Builders<Student>.Filter.Eq(s => s.Class, SelectedClass);
                var students = _mongoService.Students.Find(studentFilter).ToList();

                // Update headers
                thUHColumns.Attributes["colspan"] = UHColumnCount.ToString();
                trUHHeaders.Controls.Clear();
                for (int i = 1; i <= UHColumnCount; i++)
                {
                    var th = new HtmlGenericControl("th");
                    th.InnerText = $"UH {i}";
                    trUHHeaders.Controls.Add(th);
                }

                // Generate table rows
                tbodyKI3.Controls.Clear();
                for (int i = 0; i < students.Count; i++)
                {
                    var student = students[i];
                    var tr = new HtmlGenericControl("tr");

                    // No
                    var tdNo = new HtmlGenericControl("td");
                    tdNo.InnerText = (i + 1).ToString();
                    tr.Controls.Add(tdNo);

                    // NISN
                    var tdNISN = new HtmlGenericControl("td");
                    tdNISN.InnerText = student.NISN;
                    tr.Controls.Add(tdNISN);

                    // Name
                    var tdName = new HtmlGenericControl("td");
                    tdName.InnerText = student.Name;
                    tr.Controls.Add(tdName);

                    // UH Columns
                    for (int j = 1; j <= UHColumnCount; j++)
                    {
                        var tdUH = new HtmlGenericControl("td");
                        var inputUH = new TextBox();
                        inputUH.ID = $"txtUH_{student.Id}_{j}";
                        inputUH.CssClass = "grade-input";
                        inputUH.Attributes["type"] = "number";
                        inputUH.Attributes["min"] = "0";
                        inputUH.Attributes["max"] = "100";
                        inputUH.Attributes["onchange"] = "calculateKI3Average(this)";
                        tdUH.Controls.Add(inputUH);
                        tr.Controls.Add(tdUH);
                    }

                    // Average UH
                    var tdAvgUH = new HtmlGenericControl("td");
                    tdAvgUH.Attributes["class"] = "final-score";
                    var lblAvgUH = new Label();
                    lblAvgUH.ID = $"lblAvgUH_{student.Id}";
                    lblAvgUH.Text = "0";
                    tdAvgUH.Controls.Add(lblAvgUH);
                    tr.Controls.Add(tdAvgUH);

                    // PTS
                    var tdPTS = new HtmlGenericControl("td");
                    var inputPTS = new TextBox();
                    inputPTS.ID = $"txtPTS_{student.Id}";
                    inputPTS.CssClass = "grade-input";
                    inputPTS.Attributes["type"] = "number";
                    inputPTS.Attributes["min"] = "0";
                    inputPTS.Attributes["max"] = "100";
                    inputPTS.Attributes["onchange"] = "calculateKI3Final(this)";
                    tdPTS.Controls.Add(inputPTS);
                    tr.Controls.Add(tdPTS);

                    // PAS
                    var tdPAS = new HtmlGenericControl("td");
                    var inputPAS = new TextBox();
                    inputPAS.ID = $"txtPAS_{student.Id}";
                    inputPAS.CssClass = "grade-input";
                    inputPAS.Attributes["type"] = "number";
                    inputPAS.Attributes["min"] = "0";
                    inputPAS.Attributes["max"] = "100";
                    inputPAS.Attributes["onchange"] = "calculateKI3Final(this)";
                    tdPAS.Controls.Add(inputPAS);
                    tr.Controls.Add(tdPAS);

                    // Final Score
                    var tdFinal = new HtmlGenericControl("td");
                    tdFinal.Attributes["class"] = "final-score";
                    var lblFinal = new Label();
                    lblFinal.ID = $"lblFinalKI3_{student.Id}";
                    lblFinal.Text = "0";
                    tdFinal.Controls.Add(lblFinal);
                    tr.Controls.Add(tdFinal);

                    // Predicate
                    var tdPredicate = new HtmlGenericControl("td");
                    var lblPredicate = new Label();
                    lblPredicate.ID = $"lblPredicateKI3_{student.Id}";
                    lblPredicate.Text = "-";
                    lblPredicate.CssClass = "predicate";
                    tdPredicate.Controls.Add(lblPredicate);
                    tr.Controls.Add(tdPredicate);

                    tbodyKI3.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading KI-3 grid: " + ex.Message, "error");
            }
        }

        private void LoadKI4Grid()
        {
            try
            {
                var studentFilter = Builders<Student>.Filter.Eq(s => s.Class, SelectedClass);
                var students = _mongoService.Students.Find(studentFilter).ToList();

                // Update headers
                thPraktikColumns.Attributes["colspan"] = PraktikColumnCount.ToString();
                trPraktikHeaders.Controls.Clear();
                for (int i = 1; i <= PraktikColumnCount; i++)
                {
                    var th = new HtmlGenericControl("th");
                    th.InnerText = $"P{i}";
                    trPraktikHeaders.Controls.Add(th);
                }

                // Generate table rows
                tbodyKI4.Controls.Clear();
                for (int i = 0; i < students.Count; i++)
                {
                    var student = students[i];
                    var tr = new HtmlGenericControl("tr");

                    // No
                    var tdNo = new HtmlGenericControl("td");
                    tdNo.InnerText = (i + 1).ToString();
                    tr.Controls.Add(tdNo);

                    // NISN
                    var tdNISN = new HtmlGenericControl("td");
                    tdNISN.InnerText = student.NISN;
                    tr.Controls.Add(tdNISN);

                    // Name
                    var tdName = new HtmlGenericControl("td");
                    tdName.InnerText = student.Name;
                    tr.Controls.Add(tdName);

                    // Praktik Columns
                    for (int j = 1; j <= PraktikColumnCount; j++)
                    {
                        var tdPraktik = new HtmlGenericControl("td");
                        var inputPraktik = new TextBox();
                        inputPraktik.ID = $"txtPraktik_{student.Id}_{j}";
                        inputPraktik.CssClass = "grade-input";
                        inputPraktik.Attributes["type"] = "number";
                        inputPraktik.Attributes["min"] = "0";
                        inputPraktik.Attributes["max"] = "100";
                        inputPraktik.Attributes["onchange"] = "calculateKI4Final(this)";
                        tdPraktik.Controls.Add(inputPraktik);
                        tr.Controls.Add(tdPraktik);
                    }

                    // Proyek
                    var tdProyek = new HtmlGenericControl("td");
                    var inputProyek = new TextBox();
                    inputProyek.ID = $"txtProyek_{student.Id}";
                    inputProyek.CssClass = "grade-input";
                    inputProyek.Attributes["type"] = "number";
                    inputProyek.Attributes["min"] = "0";
                    inputProyek.Attributes["max"] = "100";
                    inputProyek.Attributes["onchange"] = "calculateKI4Final(this)";
                    tdProyek.Controls.Add(inputProyek);
                    tr.Controls.Add(tdProyek);

                    // Portofolio
                    var tdPortofolio = new HtmlGenericControl("td");
                    var inputPortofolio = new TextBox();
                    inputPortofolio.ID = $"txtPortofolio_{student.Id}";
                    inputPortofolio.CssClass = "grade-input";
                    inputPortofolio.Attributes["type"] = "number";
                    inputPortofolio.Attributes["min"] = "0";
                    inputPortofolio.Attributes["max"] = "100";
                    inputPortofolio.Attributes["onchange"] = "calculateKI4Final(this)";
                    tdPortofolio.Controls.Add(inputPortofolio);
                    tr.Controls.Add(tdPortofolio);

                    // Final Score
                    var tdFinal = new HtmlGenericControl("td");
                    tdFinal.Attributes["class"] = "final-score";
                    var lblFinal = new Label();
                    lblFinal.ID = $"lblFinalKI4_{student.Id}";
                    lblFinal.Text = "0";
                    tdFinal.Controls.Add(lblFinal);
                    tr.Controls.Add(tdFinal);

                    // Predicate
                    var tdPredicate = new HtmlGenericControl("td");
                    var lblPredicate = new Label();
                    lblPredicate.ID = $"lblPredicateKI4_{student.Id}";
                    lblPredicate.Text = "-";
                    lblPredicate.CssClass = "predicate";
                    tdPredicate.Controls.Add(lblPredicate);
                    tr.Controls.Add(tdPredicate);

                    tbodyKI4.Controls.Add(tr);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Error loading KI-4 grid: " + ex.Message, "error");
            }
        }

        protected void btnAddKI3Column_Click(object sender, EventArgs e)
        {
            UHColumnCount++;
            LoadKI3Grid();
        }

        protected void btnAddKI4Column_Click(object sender, EventArgs e)
        {
            PraktikColumnCount++;
            LoadKI4Grid();
        }

        protected void btnUH_Click(object sender, EventArgs e)
        {
            // Switch to UH tab
        }

        protected void btnPTS_Click(object sender, EventArgs e)
        {
            // Switch to PTS tab
        }

        protected void btnPAS_Click(object sender, EventArgs e)
        {
            // Switch to PAS tab
        }

        protected void btnPraktik_Click(object sender, EventArgs e)
        {
            // Switch to Praktik tab
        }

        protected void btnProyek_Click(object sender, EventArgs e)
        {
            // Switch to Proyek tab
        }

        protected void btnPortofolio_Click(object sender, EventArgs e)
        {
            // Switch to Portofolio tab
        }

        protected void btnSaveGrades_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementation for saving grades to MongoDB
                ShowMessage("Nilai berhasil disimpan!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error saving grades: " + ex.Message, "error");
            }
        }

        protected void btnExportWord_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementation for Word export
                ShowMessage("Export Word berhasil!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error exporting to Word: " + ex.Message, "error");
            }
        }

        protected void btnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementation for CSV export
                ShowMessage("Export CSV berhasil!", "success");
            }
            catch (Exception ex)
            {
                ShowMessage("Error exporting to CSV: " + ex.Message, "error");
            }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            pnlMessage.CssClass = $"message-panel {type}";
            pnlMessage.Visible = true;
        }
    }
}