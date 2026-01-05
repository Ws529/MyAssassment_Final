using System;
using System.Web.UI;

namespace MyAssessment
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize page
            }
        }

        protected void btnDataSiswa_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pages/Students.aspx");
        }

        protected void btnDataGuru_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pages/Teachers.aspx");
        }

        protected void btnDataKelas_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pages/Competencies.aspx");
        }
    }
}