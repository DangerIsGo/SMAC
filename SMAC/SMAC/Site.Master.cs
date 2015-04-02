using SMAC.Database;
using System;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace SMAC
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            this.userRole.Value = Database.Helpers.GetUserRole(Request.Cookies["SmacCookie"]["UserId"]).ToString();

            if (Session["UpdateCookie"] != null && Session["UpdateCookie"].ToString() == "yes")
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
                ck.Values.Add("SchoolName", Session["SchoolName"].ToString());
                ck.Values.Add("SchoolId", Session["SchoolId"].ToString());
                ck.Values.Add("Genders", sb.ToString());

                Response.Cookies.Add(ck);
                Session["UpdateCookie"] = "no";
            }
            else
            {
                Session["UserId"] = Request.Cookies["SmacCookie"]["UserId"];
                Session["FirstName"] = Request.Cookies["SmacCookie"]["FirstName"];
                Session["MiddleName"] = Request.Cookies["SmacCookie"]["MiddleName"];
                Session["LastName"] = Request.Cookies["SmacCookie"]["LastName"];
                Session["PhoneNumber"] = Request.Cookies["SmacCookie"]["PhoneNumber"];
                Session["Email"] = Request.Cookies["SmacCookie"]["Email"];
                Session["UserName"] = Request.Cookies["SmacCookie"]["UserName"];
                Session["Gender"] = Request.Cookies["SmacCookie"]["Gender"];
                Session["SchoolName"] = Request.Cookies["SmacCookie"]["SchoolName"];
                Session["SchoolId"] = Request.Cookies["SmacCookie"]["SchoolId"];
                Session["Genders"] = Request.Cookies["SmacCookie"]["Genders"];

                if (Session["SchoolName"] != null)
                {
                    schoolBanner.Visible = true;
                    schoolBanner.InnerHtml = Request.Cookies["SmacCookie"]["SchoolName"];
                }
            }
        }
    }
}