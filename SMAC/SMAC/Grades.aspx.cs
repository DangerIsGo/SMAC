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

            if (!IsPostBack)
            {
                var years = SchoolYearEntity.GetSchoolYears(schoolId);

                this.yearList.Items.Add(new ListItem("-----------------------------", "-"));

                foreach (var year in years)
                {
                    this.yearList.Items.Add(new ListItem(year.Year, year.SchoolYearId.ToString()));
                }
            }
        }
    }
}