using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class StaffEntity
    {
        public static void CreateUserAsStaff(string Id, string fName, string mName, string lName, string email, string phone,
                                        string gender, bool isActive, DateTime startDate, DateTime? endDate)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (UserEntity.GetUser(Id) != null)
                    {
                        throw new Exception("Staff not created.  User already exists.");
                    }
                    else
                    {
                        Staff Staff = new Staff
                        {
                            User = new User()
                            {
                                UserId = Id,
                                FirstName = fName,
                                MiddleName = mName,
                                LastName = lName,
                                EmailAddress = email,
                                PhoneNumber = phone,
                                GenderType = gender,
                                StartDate = startDate,
                                EndDate = endDate,
                                IsActive = isActive
                            },
                            UserId = Id
                        };

                        context.Staffs.Add(Staff);

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PromoteStaff(string Id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (UserEntity.GetUser(Id) == null)
                    {
                        throw new Exception("User not set as Staff.  User ID doesn't exist.");
                    }
                    else
                    {
                        if (IsStaff(Id))
                        {
                            throw new Exception("User not set as Staff.  User already set as Staff.");
                        }
                        else
                        {
                            Staff Staff = new Staff();
                            Staff.UserId = Id;
                            Staff.User = UserEntity.GetUser(Id);

                            context.Staffs.Add(Staff);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsStaff(string Id)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Staffs where a.UserId == Id select a).FirstOrDefault() != null;
            }
        }

        public static void RevokeStaff(string Id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (UserEntity.GetUser(Id) == null)
                    {
                        throw new Exception("User not removed as Staff.  User doesn't exist.");
                    }
                    else if (!IsStaff(Id))
                    {
                        throw new Exception("User not removed as Staff.  User not an Staff.");
                    }
                    else
                    {
                        Staff Staff = new Staff();
                        Staff.UserId = Id;
                        Staff.User = UserEntity.GetUser(Id);

                        context.Staffs.Add(Staff);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
