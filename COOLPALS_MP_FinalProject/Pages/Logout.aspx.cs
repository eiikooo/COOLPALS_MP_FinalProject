using System;
using System.Web.UI;

namespace COOLPALS_MP_FinalProject
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Stops session and redirects to login page
            Session.Clear();
            Session.Abandon();
        }
    }
}
