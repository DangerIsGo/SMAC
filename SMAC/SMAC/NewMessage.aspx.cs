using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Data;

namespace SMAC
{
    public partial class NewMessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var schoolId = Request.Cookies["SmacCookie"]["SchoolId"];
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            if (!IsPostBack)
            {
                var usrList = UserEntity.GetAllUsersInSchool(int.Parse(schoolId), userId);

                List<string> items = new List<string>() { "Id", "Role", "Name" };

                DataTable table = new DataTable();
                foreach (var item in items)
                    table.Columns.Add(item, typeof(string));

                table.Rows.Add("-", "-", "----------------------------------------------------------");

                foreach(var user in usrList)
                {
                    table.Rows.Add(
                        user.UserId,
                        DetermineRole(user),
                        user.LastName + ", " + user.FirstName + (string.IsNullOrWhiteSpace(user.MiddleName) ? string.Empty : user.MiddleName)
                        );
                }

                receiverListView.DataSource = table;
                receiverListView.DataBind();
            }
        }

        private string DetermineRole(usp_GetUsersInSchool_Result user)
        {
            if (user.Student.HasValue && user.Student.Value)
                return "student";

            if (user.Teacher.HasValue && user.Teacher.Value)
                return "teacher";

            if (user.Admin.HasValue && user.Admin.Value)
                return "admin";

            if (user.Staff.HasValue && user.Staff.Value)
                return "staff";

            return string.Empty;
        }

        protected void textSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //var userId = Request.Cookies["SmacCookie"]["UserId"];
                //var toUserId = this.ddl_AllUsers.SelectedValue;
                //var content = this.textInput.Text.Trim();

                //PrivateMessageEntity.SendPrivateMessage(toUserId, userId, content);

                //this.textInput.Text = string.Empty;
                //this.ddl_AllUsers.SelectedIndex = 0;
                //this.textSubmit.Enabled = false;

                //this.sendStatus.Text = "Message was sent successfully!";
                //this.sendStatus.ForeColor = System.Drawing.Color.Green;
            }
            catch
            {
                this.sendStatus.Text = "An internal error has occurred.  Please notify your administrator.";
                this.sendStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}