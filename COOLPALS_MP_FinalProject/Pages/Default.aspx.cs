using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace COOLPALS_MP_FinalProject
{
    public partial class Default : Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["PairEdDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowGuestView();

                if (Session["UserID"] != null)
                {
                    int userId;
                    if (int.TryParse(Session["UserID"].ToString(), out userId))
                    {
                        string firstName = GetName(userId);

                        if (IsNewUser(userId))
                        {
                            ShowNewUserView(firstName);
                        }
                        else
                        {
                            ShowReturningUserView(firstName);
                        }
                    }
                }
            }
        }

        private void ShowGuestView()
        {
            lblMessage.Text = "Welcome to PairEd! Please register or login to continue.";
            btnRegister.Visible = true;
            btnLogin.Visible = true;

            btnAddSkills.Visible = false;
            btnProfile.Visible = false;
            btnTutors.Visible = false;
            btnRequests.Visible = false;
        }

        private void ShowNewUserView(string firstName)
        {
            lblMessage.Text = $"Welcome to PairEd, {firstName}! Let's set up your profile by adding your first skill.";
            btnAddSkills.Visible = true;

            btnRegister.Visible = false;
            btnLogin.Visible = false;
        }

        private void ShowReturningUserView(string firstName)
        {
            lblMessage.Text = $"Welcome back, {firstName}! Choose an option below.";
            btnProfile.Visible = true;
            btnTutors.Visible = true;
            btnRequests.Visible = true;
            btnAddSkills.Visible = true;


            btnRegister.Visible = false;
            btnLogin.Visible = false;
        }

        protected void RegisterUser(object sender, EventArgs e)
        {
            Response.Redirect("Register.aspx");
        }

        protected void LoginUser(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        // HELPER FUNCTIONS
        private string GetName(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT FirstName FROM Users WHERE UserID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", userId);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "User";
                }
            }
            catch
            {
                return "User";
            }
        }

        private bool IsNewUser(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT COUNT(*) FROM UserSkills WHERE UserID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", userId);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count == 0; // New user if no skills yet
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
