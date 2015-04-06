using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace SMAC.Database
{
    public class UserEntity
    {
        public static void CreateUser(string Id, string fName, string mName, string lName, string email, string phone, string gender, bool isActive, string userName, string password, DateTime startDate, DateTime? endDate, string role, int schoolId)
        {
            CreateEditUser("ADD", Id, fName, mName, lName, email, phone, gender, isActive, userName, password, startDate, endDate, role, schoolId);
        }

        public static void UpdateUser(string Id, string fName, string mName, string lName, string email, string phone, string gender, bool? isActive, string userName, string password, DateTime? startDate, DateTime? endDate, string role)
        {
            CreateEditUser("EDIT", Id, fName, mName, lName, email, phone, gender, isActive, userName, password, startDate, endDate, role, null);
        }

        private static void CreateEditUser(string op, string Id, string fName, string mName, string lName, string email, string phone, string gender, bool? isActive, string userName, string password, DateTime? startDate, DateTime? endDate, string role, int? schoolId)
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
                                Gender = (from a in context.Genders where a.GenderType == gender select a).FirstOrDefault(),
                                StartDate = startDate.Value,
                                EndDate = endDate,
                                IsActive = isActive.Value,
                                UserCredential = new UserCredential()
                                {
                                    Password = UserCredentialEntity.GetSHA256Hash(password),
                                    UserName = userName
                                }
                            };

                            if (schoolId.HasValue)
                            {
                                usr.Schools.Add((from a in context.Schools where a.SchoolId == schoolId.Value select a).FirstOrDefault());
                            }

                            context.Users.Add(usr);
                            context.SaveChanges();

                            if (role == "admin")
                            {
                                Admin newRole = new Admin();
                                newRole.User = usr;
                                context.Admins.Add(newRole);
                                context.SaveChanges();
                            }
                            else if (role == "student")
                            {
                                Student newRole = new Student();
                                newRole.User = usr;
                                context.Students.Add(newRole);
                                context.SaveChanges();

                            }
                            else if (role == "staff")
                            {
                                Staff newRole = new Staff();
                                newRole.User = usr;
                                context.Staffs.Add(newRole);
                                context.SaveChanges();
                            }
                            else if (role == "teacher")
                            {
                                Teacher newRole = new Teacher();
                                newRole.User = usr;
                                context.Teachers.Add(newRole);
                                context.SaveChanges();
                            }
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
                return (from a in context.Users where a.UserId == Id select a)
                    .Include(t=>t.Schools)
                    .Include(t=>t.Admin)
                    .Include(t=>t.Student)
                    .Include(t=>t.UserCredential)
                    .Include(t=>t.Staff)
                    .Include(t=>t.Teacher)
                    .FirstOrDefault();
            }
        }

        public static User GetUser(string Id, SmacEntities context)
        {
            return (from a in context.Users where a.UserId == Id select a).FirstOrDefault();
        }

        public static List<usp_GetUsersInSchool_Result> GetAllUsersInSchool(int schoolId, string userIdToFilter)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return context.usp_GetUsersInSchool(schoolId, userIdToFilter).ToList();
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
