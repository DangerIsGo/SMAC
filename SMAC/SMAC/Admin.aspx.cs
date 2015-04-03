using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            if (Database.Helpers.GetUserRole(userId) != Database.Helpers.Roles.Admin)
            {
                // Get outta here ya punk!
                Response.Redirect("Home.aspx");
            }


        }
    }
}