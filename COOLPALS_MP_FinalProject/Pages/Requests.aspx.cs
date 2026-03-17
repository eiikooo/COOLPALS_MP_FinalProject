using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace COOLPALS_MP_FinalProject
{
    public partial class Request : Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["PairEdDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 1. Check if user is logged in
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/Pages/Login.aspx");
                    return;
                }

                // 2. Check if TutorID and TutorName exist in URL
                if (Request.QueryString["TutorID"] == null ||
                    Request.QueryString["TutorName"] == null ||
                    Request.QueryString["SkillID"] == null)
                {
                    lblMessage.Text = "Please select a tutor first.";
                    btnSubmit.Enabled = false;
                    return;
                }

                // 3. Validate TutorID is actually a number
                int tutorId, skillId;
                if (!int.TryParse(Request.QueryString["TutorID"], out tutorId) ||
                    !int.TryParse(Request.QueryString["SkillID"], out skillId))
                {
                    lblMessage.Text = "Invalid tutor or skill information.";
                    btnSubmit.Enabled = false;
                    return;
                }

                // 4. Set tutor name in textbox
                txtTutor.Text = Request.QueryString["TutorName"];

                // 5. Load skills dropdown
                LoadSkills(skillId);
            }
        }

        private void LoadSkills(int selectedSkillId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT SkillID, SkillName FROM Skills ORDER BY SkillName";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlSkill.DataSource = reader;
                ddlSkill.DataTextField = "SkillName";
                ddlSkill.DataValueField = "SkillID";
                ddlSkill.DataBind();

                reader.Close();

                if (ddlSkill.Items.FindByValue(selectedSkillId.ToString()) != null)
                {
                    ddlSkill.SelectedValue = selectedSkillId.ToString();
                }
            }
        }

        protected void SubmitRequest(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            int learnerId = Convert.ToInt32(Session["UserID"]);

            int tutorId;
            if (!int.TryParse(Request.QueryString["TutorID"], out tutorId))
            {
                lblMessage.Text = "Invalid tutor selected.";
                return;
            }

            if (learnerId == tutorId)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "You cannot send a request to yourself.";
                return;
            }

            int skillId = Convert.ToInt32(ddlSkill.SelectedValue);

            string availability = "";
            if (!string.IsNullOrWhiteSpace(txtDate.Text) && !string.IsNullOrWhiteSpace(txtTime.Text))
            {
                availability = txtDate.Text + " " + txtTime.Text;
            }

            string notes = txtNotes.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    INSERT INTO LearningRequests
                    (LearnerID, TutorID, SkillID, Message, Availability, Status, RequestDate)
                    VALUES
                    (@learner, @tutor, @skill, @message, @availability, @status, @requestDate)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@learner", learnerId);
                cmd.Parameters.AddWithValue("@tutor", tutorId);
                cmd.Parameters.AddWithValue("@skill", skillId);
                cmd.Parameters.AddWithValue("@message", notes);
                cmd.Parameters.AddWithValue("@availability", availability);
                cmd.Parameters.AddWithValue("@status", "Pending");
                cmd.Parameters.AddWithValue("@requestDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            Response.Redirect("~/Pages/SessionHistory.aspx");
        }
    }
}
