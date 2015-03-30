using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class UserEntity
    {
        public static void CreateUser(string Id, string fName, string mName, string lName, string email, string phone, 
                                        string gender, bool? isActive, DateTime? startDate, DateTime? endDate)
        {
            CreateEditUser("ADD", Id, fName, mName, lName, email, phone, gender, isActive, startDate, endDate);
        }

        public static void UpdateUser(string Id, string fName, string mName, string lName, string email, string phone, 
                                        string gender, bool? isActive, DateTime? startDate, DateTime? endDate)
        {
            CreateEditUser("EDIT", Id, fName, mName, lName, email, phone, gender, isActive, startDate, endDate);
        }

        private static void CreateEditUser(string op, string Id, string fName, string mName, string lName, string email, string phone,
                                        string gender, bool? isActive, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetUser(Id) != null)
                        {
                            throw new Exception("User was not created.  User ID already exists.");
                        }
                        else
                        {
                            var usr = new User
                            {
                                UserId = Id,
                                FirstName = fName,
                                MiddleName = !string.IsNullOrWhiteSpace(mName) ? mName : null,
                                LastName = lName,
                                EmailAddress = !string.IsNullOrWhiteSpace(email) ? email : null,
                                PhoneNumber = !string.IsNullOrWhiteSpace(phone) ? phone : null,
                                GenderType = gender,
                                StartDate = startDate.HasValue ? startDate.Value : DateTime.Now,
                                EndDate = endDate,
                                IsActive = isActive.HasValue ? isActive.Value : true
                            };

                            context.Users.Add(usr);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetUser(Id) == null)
                        {
                            throw new Exception("User was not updated.  User ID not found.");
                        }
                        else
                        {
                            var user = GetUser(Id);

                            user.UserId = Id;
                            user.FirstName = fName;
                            user.MiddleName = !string.IsNullOrWhiteSpace(mName) ? mName : null;
                            user.LastName = lName;
                            user.EmailAddress = !string.IsNullOrWhiteSpace(email) ? email : null;
                            user.PhoneNumber = !string.IsNullOrWhiteSpace(phone) ? phone : null;
                            user.GenderType = gender;
                            user.StartDate = startDate.HasValue ? startDate.Value : user.StartDate;
                            user.EndDate = endDate;
                            user.IsActive = isActive.HasValue ? isActive.Value : user.IsActive;
                            context.Entry(user).State = System.Data.Entity.EntityState.Modified;
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

        public static User GetUser(string Id)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Users where a.UserId == Id select a).FirstOrDefault();
            }
        }

        public static User GetUser(string Id, SmacEntities context)
        {
            return (from a in context.Users where a.UserId == Id select a).FirstOrDefault();
        }

        public static List<User> GetAllUsersInSchool(int schoolId)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Schools where a.SchoolId == schoolId select a).First().Users.OrderBy(t=>t.LastName).ThenBy(t=>t.FirstName).ThenBy(t=>t.MiddleName).ToList();
            }
        }

        public static DateTime? GetLastLoggedIn(string Id)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Users where a.UserId == Id select a).FirstOrDefault().LastLoggedIn;
            }
        }

        public static DateTime? GetLastLoggedOut(string Id)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Users where a.UserId == Id select a).FirstOrDefault().LastLoggedOut;
            }
        }

        public static void SetLastLoggedIn(string Id, DateTime? newVal)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var user = (from a in context.Users where a.UserId == Id select a).FirstOrDefault();

                    if (user != null)
                    {
                        user.LastLoggedIn = newVal;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetLastLoggedOut(string Id, DateTime? newVal)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var user = (from a in context.Users where a.UserId == Id select a).FirstOrDefault();

                    if (user != null)
                    {
                        user.LastLoggedOut = newVal;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void AddEndDate(string Id, DateTime endDate)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (GetUser(Id) == null)
                    {
                        throw new Exception("User was not edited.  User ID not found.");
                    }
                    else
                    {
                        var user = GetUser(Id);

                        user.EndDate = endDate;
                        context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteUser(string Id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (GetUser(Id) == null)
                    {
                        throw new Exception("User was not deleted.  User ID not found.");
                    }
                    else
                    {
                        var user = GetUser(Id);

                        context.Users.Remove(user);

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
