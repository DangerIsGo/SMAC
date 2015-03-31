using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public static class Helpers
    {
        public enum Roles
        {
            None = 0,       // Default incase no role is assigned
            Student = 1,
            Teacher = 2,
            Staff = 3,
            Admin = 4
        }

        public static Roles GetUserRole(string userId, SmacEntities context)
        {
            using (context == null ? new SmacEntities() : context)
            {
                var user = (from a in context.Users where a.UserId == userId select a).FirstOrDefault();

                if (user.Admin != null)
                    return Roles.Admin;
                else if (user.Student != null)
                    return Roles.Student;
                else if (user.Staff != null)
                    return Roles.Staff;
                else if (user.Teacher != null)
                    return Roles.Teacher;
                else
                    return Roles.None;
            }
        }
    }
}
