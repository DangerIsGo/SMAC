using System;
using System.Linq;

namespace SMAC.Database
{
    public class AdminEntity
    {
        private static SmacEntities context;

        public static void CreateUserAsAdmin(string Id, string fName, string mName, string lName, string email, string phone, 
                                        string gender, bool isActive, DateTime startDate, DateTime? endDate)
        {
            try
            {
                context = new SmacEntities();

                if (UserEntity.GetUser(Id) != null)
                {
                    throw new Exception("Admin not created.  User already exists.");
                }
                else
                {
                    Admin admin = new Admin
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

                    context.Admins.Add(admin);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PromoteAdmin(string Id)
        {
            try
            {
                context = new SmacEntities();

                if (UserEntity.GetUser(Id) == null)
                {
                    throw new Exception("User not set as Admin.  User ID doesn't exist.");
                }
                else
                {
                    if (IsAdmin(Id))
                    {
                        throw new Exception("User not set as Admin.  User already set as Admin.");
                    }
                    else
                    {
                        Admin admin = new Admin();
                        admin.UserId = Id;
                        admin.User = UserEntity.GetUser(Id);

                        context.Admins.Add(admin);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsAdmin(string uId)
        {
            context = new SmacEntities();

            return (from a in context.Admins where a.UserId == uId select a).FirstOrDefault() != null;
        }

        public static void RevokeAdmin(string Id)
        {
            try
            {
                context = new SmacEntities();

                if (UserEntity.GetUser(Id) == null)
                {
                    throw new Exception("User not removed as Admin.  User doesn't exist.");
                }
                else if (!IsAdmin(Id))
                {
                    throw new Exception("User not removed as Admin.  User not an Admin.");
                }
                else
                {
                    Admin admin = new Admin();
                    admin.UserId = Id;
                    admin.User = UserEntity.GetUser(Id);

                    context.Admins.Add(admin);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
