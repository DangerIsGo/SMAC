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
                var periods = MarkingPeriodEntity.GetMarkingPeriods(schoolId);

                this.periodList.Items.Add("-----------------------------");

                foreach (var mp in periods)
                {
                    this.periodList.Items.Add(new ListItem(mp.FullYear ? "All Year" : mp.Period, mp.MarkingPeriodId.ToString()));
                }


                this.yearList.Items.Add("-----------------------------");

                foreach (var year in years)
                {
                    this.yearList.Items.Add(new ListItem(year.Year, year.Year));
                }
            }
        }
    }
}