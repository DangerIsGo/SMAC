using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class Threads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(Session["SectionName"].ToString()))
            //    Response.Redirect("/Classes.aspx");

            var section = Request.Cookies["SmacCookie"]["SectionName"].ToString();
            var mClass = Request.Cookies["SmacCookie"]["ClassName"].ToString();
            var subject = Request.Cookies["SmacCookie"]["SubjectName"].ToString();
            var mPeriod = Request.Cookies["SmacCookie"]["MarkingPeriod"].ToString();
            var sYear = Request.Cookies["SmacCookie"]["SchoolYear"].ToString();
            var schoolId = int.Parse(Request.Cookies["SmacCookie"]["SchoolId"]);

            List<string> list = new List<string>() { "Id", "Title", "Author", "LastPostAt", "LastPostBy", "Replies" };

            DataTable table = new DataTable();
            foreach (var item in list)
                table.Columns.Add(item, typeof(string));

            var threads = ThreadEntity.GetSectionPosts(section, mClass, subject, schoolId);

            foreach (var thread in threads)
            {
                table.Rows.Add(thread.id, thread.title, thread.author, thread.lastPostAt.Value.ToString("M/d/yyyy h:mm tt"), thread.lastPostBy, thread.replies);
            }

            threadTableBody.DataSource = table;
            threadTableBody.DataBind();
        }
    }
}