using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMAC
{
    public partial class NewMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var schoolId = Request.Cookies["SmacCookie"]["SchoolId"];
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            var usrList = UserEntity.GetAllUsersInSchool(int.Parse(schoolId));

            usrList = usrList.Where(t=>t.UserId != userId).ToList();

            ddl_AllUsers.Items.Add(new ListItem("----------------------------------------------------------"));

            foreach(var user in usrList)
            {
                ddl_AllUsers.Items.Add(new ListItem(user.LastName + "," + user.FirstName, user.UserId));
            }
        }
    }
}