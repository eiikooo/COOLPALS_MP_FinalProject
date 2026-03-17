using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COOLPALS_MP_FinalProject
{
    public partial class Tutor : Page
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
                
                LoadTutors();
            }
        }

        private void LoadTutors(string skillFilter = "")
        {
            int currentUserId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"SELECT u.UserID, u.FirstName, u.LastName, u.Program, u.YearLevel,
                                        s.SkillID, s.SkillName, sc.CategoryName, 
                                        us.ProficiencyLevel, us.YearsExperience
                                 FROM UserSkills us
                                 INNER JOIN Users u ON us.UserID = u.UserID
                                 INNER JOIN Skills s ON us.SkillID = s.SkillID
                                 INNER JOIN SkillCategories sc ON s.CategoryID = sc.CategoryID
                                 WHERE us.CanTutor = 1 
                                    AND u.IsActive = 1
                                    AND u.UserID <> @CurrentUserID";

                if (!string.IsNullOrWhiteSpace(skillFilter))
                {
                    query += " AND s.SkillName LIKE @skill";
                }

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@CurrentUserID", currentUserId);

                if (!string.IsNullOrWhiteSpace(skillFilter))
                {
                    da.SelectCommand.Parameters.AddWithValue("@skill", "%" + skillFilter + "%");
                }

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvTutors.DataSource = dt;
                gvTutors.DataBind();
            }
        }

        protected void SearchTutors(object sender, EventArgs e)
        {
            LoadTutors(txtSearch.Text.Trim());
        }

        protected void gvTutors_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RequestTutor")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                int tutorId = Convert.ToInt32(gvTutors.DataKeys[rowIndex]["UserID"]);
                int skillId = Convert.ToInt32(gvTutors.DataKeys[rowIndex]["SkillID"]);

                string firstName = gvTutors.Rows[rowIndex].Cells[0].Text;
                string lastName = gvTutors.Rows[rowIndex].Cells[1].Text;
                string tutorName = firstName + " " + lastName;

                Response.Redirect("~/Pages/Requests.aspx?TutorID=" + tutorId +
                                  "&TutorName=" + Server.UrlEncode(tutorName) +
                                  "&SkillID=" + skillId);
            }
        }
    }
}
