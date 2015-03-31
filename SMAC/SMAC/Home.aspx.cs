using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Data;

namespace SMAC
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var schoolId = int.Parse(Request.Cookies["SmacCookie"]["SchoolId"]);

            if (IsPostBack)
            {
                var school = SchoolEntity.GetSchool(schoolId);
                var latestNews = LatestNewsEntity.GetLatestNews(schoolId, null);

                this.SchoolNameDiv.InnerText = school.SchoolName;
                this.SchoolAddrDiv.InnerText = school.StreetAddress;
                this.SchoolCityDiv.InnerText = school.City;
                this.SchoolStateDiv.InnerText = school.State;
                this.SchoolZipDiv.InnerText = school.ZipCode;
                this.SchoolPhoneDiv.InnerText = school.PhoneNumber;

                var list = new List<string> { "News", "Author" };

                var table = new DataTable();

                foreach (var item in list)
                    table.Columns.Add(item, typeof(string));

                //Now add some rows(which will be repeated in the ItemTemplate)
                foreach (var news in latestNews)
                {
                    table.Rows.Add(news.Content, string.Concat("Posted by ", news.User.FirstName, " ", news.User.LastName, " at ", news.PostedAt.ToString()));
                }

                latestNewsListView.DataSource = table;
                latestNewsListView.DataBind();
            }
        }
    }
}