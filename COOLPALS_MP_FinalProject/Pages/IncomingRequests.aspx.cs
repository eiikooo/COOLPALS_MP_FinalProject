using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COOLPALS_MP_FinalProject.Pages
{
    public partial class IncomingRequests : Page
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

                LoadIncomingRequests();
            }
        }

        private void LoadIncomingRequests()
        {
            int tutorId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT 
                        lr.RequestID,
                        u.FirstName + ' ' + u.LastName AS LearnerName,
                        s.SkillName,
                        lr.Message,
                        lr.Availability,
                        lr.Status,
                        lr.RequestDate
                    FROM LearningRequests lr
                    INNER JOIN Users u ON lr.LearnerID = u.UserID
                    INNER JOIN Skills s ON lr.SkillID = s.SkillID
                    WHERE lr.TutorID = @TutorID
                    ORDER BY lr.RequestDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TutorID", tutorId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvIncomingRequests.DataSource = dt;
                gvIncomingRequests.DataBind();
            }
        }

        protected void gvIncomingRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int requestId = Convert.ToInt32(gvIncomingRequests.DataKeys[rowIndex].Value);

            if (e.CommandName == "AcceptRequest")
            {
                UpdateRequestStatus(requestId, "Accepted");
            }
            else if (e.CommandName == "DeclineRequest")
            {
                UpdateRequestStatus(requestId, "Declined");
            }
            else if (e.CommandName == "CompleteRequest")
            {
                UpdateRequestStatus(requestId, "Completed");
            }

            LoadIncomingRequests();
        }

        private void UpdateRequestStatus(int requestId, string status)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    UPDATE LearningRequests
                    SET Status = @Status,
                        ResponseDate = CASE 
                            WHEN @Status IN ('Accepted','Declined') AND ResponseDate IS NULL THEN GETDATE()
                            ELSE ResponseDate
                        END,
                        CompletedDate = CASE
                            WHEN @Status = 'Completed' AND CompletedDate IS NULL THEN GETDATE()
                            ELSE CompletedDate
                        END,
                        CancelledDate = CASE
                            WHEN @Status = 'Cancelled' AND CancelledDate IS NULL THEN GETDATE()
                            ELSE CancelledDate
                        END
                    WHERE RequestID = @RequestID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RequestID", requestId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Request updated successfully.";
        }
    }
}