using SMAC.Database;
using System;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class Grades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Session.Keys.Count == 0)
                return;
            var userId = Session["UserId"].ToString();

            var schools = SchoolEntity.GetUsersSchools(userId);

            this.schoolList.Items.Add(new ListItem("-----------------"));

            foreach (var school in schools)
            {
                this.schoolList.Items.Add(new ListItem(school.SchoolName, school.SchoolId.ToString()));
            }
        }
    }
}