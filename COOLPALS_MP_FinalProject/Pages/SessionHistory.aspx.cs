using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace COOLPALS_MP_FinalProject
{
    public partial class SessionHistory : Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["PairEdDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                    return;
                }

                LoadRequests();
            }
        }
        private void LoadRequests()
        {
            int learnerId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT 
                        lr.RequestID,
                        u.FirstName + ' ' + u.LastName AS TutorName,
                        s.SkillName,
                        lr.Message,
                        lr.Availability,
                        lr.Status,
                        lr.RequestDate,
                        lr.ResponseDate,
                        lr.CompletedDate,
                        lr.CancelledDate
                    FROM LearningRequests lr
                    INNER JOIN Users u ON lr.TutorID = u.UserID
                    INNER JOIN Skills s ON lr.SkillID = s.SkillID
                    WHERE lr.LearnerID = @LearnerID
                    ORDER BY lr.RequestDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@LearnerID", learnerId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvHistory.DataSource = dt;
                gvHistory.DataBind();
            }
        }
    }
}