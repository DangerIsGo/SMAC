using SMAC.Database;
using System;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class SchoolSelection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var userId = Request.Cookies["SmacCookie"]["UserId"];

                var schoolList = SchoolEntity.GetUsersSchools(userId);

                ddList_SchoolSelect.Items.Clear();

                ddList_SchoolSelect.Items.Add(new ListItem("------------------"));

                foreach (var school in schoolList)
                {
                    ddList_SchoolSelect.Items.Add(new ListItem(school.SchoolName, school.SchoolId.ToString()));
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            if (SchoolEntity.GetUsersSchools(userId).Count == 1)
            {
                Response.Redirect("/Home.aspx");
            }
        }

        protected void SchoolSelectSubmit_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;
            tkt = new FormsAuthenticationTicket(1, Session["UserName"].ToString(), DateTime.Now, DateTime.Now.AddMinutes(60), false, "");
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie("SmacCookie", cookiestr);


            StringBuilder sb = new StringBuilder();

            var genders = GenderEntity.GetGenders();

            genders.ForEach(t => sb.Append(t.GenderType.ToString() + ":"));
            sb.Remove(sb.Length - 1, 1);

            ck.Values.Add("UserId", Session["UserId"].ToString());
            ck.Values.Add("FirstName", Session["FirstName"].ToString());
            ck.Values.Add("MiddleName", Session["MiddleName"].ToString());
            ck.Values.Add("LastName", Session["LastName"].ToString());
            ck.Values.Add("PhoneNumber", Session["PhoneNumber"].ToString());
            ck.Values.Add("Email", Session["Email"].ToString());
            ck.Values.Add("UserName", Session["UserName"].ToString());
            ck.Values.Add("Gender", Session["Gender"].ToString());
            ck.Values.Add("SchoolName", ddList_SchoolSelect.SelectedItem.Text);
            ck.Values.Add("SchoolId", ddList_SchoolSelect.SelectedItem.Value);
            ck.Values.Add("Genders", sb.ToString());

            Response.Cookies.Add(ck);
            Session["UpdateCookie"] = "no";

            Response.Redirect("/Home.aspx");
        }
    }
}