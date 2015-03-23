using SMAC.Database;
using System;
using System.Text;
using System.Web.UI;

namespace SMAC
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                var usr = UserCredentialEntity.Authenticate(Request.Form["username"].Trim(), Request.Form["password"]);

                StringBuilder sb = new StringBuilder();

                var genders = GenderEntity.GetGenders();

                genders.ForEach(t => sb.Append(t.GenderType.ToString() + ":"));
                sb.Remove(sb.Length-1, 1);

                Session["UserId"] = usr.UserId;
                Session["FirstName"] = usr.FirstName;
                Session["LastName"] = usr.LastName;
                Session["PhoneNumber"] = usr.PhoneNumber;
                Session["Email"] = usr.EmailAddress;
                Session["MiddleName"] = usr.MiddleName;
                Session["UserName"] = usr.UserCredential.UserName;
                Session["Gender"] = usr.Gender.GenderType;
                Session["Genders"] = sb.ToString();

                Response.Redirect("/Home.aspx");
            }
            catch (Exception ex)
            {
                loginStatus.Text = ex.Message;
                loginStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}