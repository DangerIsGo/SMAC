using SMAC.Database;
using System;
using System.Text;
using System.Web;
using System.Web.Security;
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

                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                tkt = new FormsAuthenticationTicket(1, Request.Form["username"].Trim(), DateTime.Now, DateTime.Now.AddMinutes(60), false, "");
                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie("SmacCookie", cookiestr);
                

                StringBuilder sb = new StringBuilder();

                var genders = GenderEntity.GetGenders();

                genders.ForEach(t => sb.Append(t.GenderType.ToString() + ":"));
                sb.Remove(sb.Length-1, 1);

                ck.Values.Add("UserId", usr.UserId);
                ck.Values.Add("FirstName", usr.FirstName);
                ck.Values.Add("LastName", usr.LastName);
                ck.Values.Add("PhoneNumber", usr.PhoneNumber);
                ck.Values.Add("Email", usr.EmailAddress);
                ck.Values.Add("MiddleName", usr.MiddleName);
                ck.Values.Add("UserName", usr.UserCredential.UserName);
                ck.Values.Add("Gender", usr.GenderType);
                ck.Values.Add("Genders", sb.ToString());

                Response.Cookies.Add(ck);
                
                FormsAuthentication.RedirectFromLoginPage(Request.Form["username"].Trim(), false);
            }
            catch (Exception ex)
            {
                loginStatus.Text = ex.Message;
                loginStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}