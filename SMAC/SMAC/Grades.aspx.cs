using SMAC.Database;
using System;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class Grades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var schoolId = int.Parse(Request.Cookies["SmacCookie"]["SchoolId"]);
            var userId = int.Parse(Request.Cookies["SmacCookie"]["UserId"]);

            if (!IsPostBack)
            {
                var years = SchoolYearEntity.GetSchoolYears(schoolId);

                this.userRole.Value = Database.Helpers.GetUserRole(userId.ToString()).ToString().ToLower();

                this.yearList.Items.Add(new ListItem("-----------------------------", "-"));

                foreach (var year in years)
                {
                    this.yearList.Items.Add(new ListItem(year.Year, year.SchoolYearId.ToString()));
                }
            }
        }
    }
}