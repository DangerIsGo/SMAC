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
            var userId = Request.Cookies["SmacCookie"]["UserId"];
            var secId = Request.QueryString["secId"];

            // Verify user enrolled in section and verify section is valid Integer
            int sectionId = -1;

            if (int.TryParse(secId, out sectionId))
            {
                var enrolls = SectionEntity.GetSection(sectionId).Enrollments.ToList().Where(t => t.UserId == userId).ToList();

                if (enrolls.Count == 0)
                    Response.Redirect("/Classes.aspx");

                List<string> list = new List<string>() { "Id", "Title", "Author", "LastPostAt", "LastPostBy", "Replies" };

                DataTable table = new DataTable();
                foreach (var item in list)
                    table.Columns.Add(item, typeof(string));

                var threads = ThreadEntity.GetSectionPosts(sectionId);

                foreach (var thread in threads)
                {
                    table.Rows.Add(thread.id, thread.title, thread.author, thread.lastPostAt.Value.ToString("M/d/yyyy h:mm tt"), thread.lastPostBy, thread.replies);
                }

                threadTableBody.DataSource = table;
                threadTableBody.DataBind();
            }
        }
    }
}