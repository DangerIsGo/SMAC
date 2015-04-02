using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SMAC
{
    public partial class Threads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = Request.Cookies["SmacCookie"]["UserId"];
            var secId = Request.QueryString["secId"];
            var thrdId = Request.QueryString["threadId"];

            // Verify user enrolled in section and verify section is valid Integer
            int sectionId = -1;
            int threadId = -1;

            if (int.TryParse(secId, out sectionId))
            {
                var enrolls = SectionEntity.GetSection(sectionId).Enrollments.ToList().Where(t => t.UserId == userId).ToList();

                if (enrolls.Count == 0)
                    Response.Redirect("/Classes.aspx");

                DataTable table = new DataTable();
                List<string> list = new List<string>();

                if (!string.IsNullOrWhiteSpace(thrdId))
                {
                    if (int.TryParse(thrdId, out threadId))
                    {
                        list = new List<string>() { "Date", "Author", "Content" };

                        foreach (var item in list)
                            table.Columns.Add(item, typeof(string));

                        var posts = ThreadEntity.GetAllPostsOfThread(threadId);

                        var op = posts.Where(t => t.RepliedTo == null).FirstOrDefault();

                        if (op != null)
                            threadTitle.Text = op.ThreadTitle;

                        foreach (var post in posts)
                        {
                            table.Rows.Add(post.DateTimePosted.ToString("M/d/yyyy h:mm tt"), post.UserId == userId ? "You" : post.User.FirstName + " " + post.User.LastName, post.Content);
                        }

                        convoListView.DataSource = table;
                        convoListView.DataBind();
                        return;
                    }
                    else
                        Response.Redirect("/Threads.aspx?secId=" + secId.ToString());
                }

                list = new List<string>() { "Id", "Title", "Author", "LastPostAt", "LastPostBy", "Replies", "SecId" };

                foreach (var item in list)
                    table.Columns.Add(item, typeof(string));

                var threads = ThreadEntity.GetSectionPosts(sectionId);

                foreach (var thread in threads)
                {
                    table.Rows.Add(thread.id, thread.title, thread.author, thread.lastPostAt.Value.ToString("M/d/yyyy h:mm tt"), thread.lastPostBy, thread.replies, sectionId.ToString());
                }

                threadTableBody.DataSource = table;
                threadTableBody.DataBind();
            }            
        }
    }
}