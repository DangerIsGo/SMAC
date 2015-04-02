using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class NewThread : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var secId = int.Parse(Request.QueryString["secId"]);

            var section = SectionEntity.GetSection(secId);
            var mClass = ClassEntity.GetClass(section.ClassId);

            this.threadHeader.Text = mClass.ClassName + " - " + section.SectionName;
        }

        protected void submitNewThread_Click(object sender, EventArgs e)
        {
            var secId = int.Parse(Request.QueryString["secId"]);
            var schoolId = int.Parse(Request.Cookies["SmacCookie"]["SchoolId"]);
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            ThreadEntity.CreatePost(userId, this.threadTitle.Text, secId, this.threadInput.Text, null);

            Response.Redirect("/Threads.aspx?secId=" + secId.ToString());
        }
    }
}