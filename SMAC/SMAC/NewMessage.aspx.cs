﻿using SMAC.Database;
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
    }
}