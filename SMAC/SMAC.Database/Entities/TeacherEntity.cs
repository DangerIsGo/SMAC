using System;
using System.Linq;

namespace SMAC.Database
{
    public class TeacherEntity
    {
        private static SmacEntities context;

        public static void CreateUserAsTeacher(string Id, string fName, string mName, string lName, string email, string phone,
                                        string gender, bool isActive, DateTime startDate, DateTime? endDate)
        {
            try
            {
                context = new SmacEntities();

                if (UserEntity.GetUser(Id) != null)
                {
                    throw new Exception("Teacher not created.  User already exists.");
                }
                else
                {
                    Teacher Teacher = new Teacher
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

                    context.Teachers.Add(Teacher);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Teacher GetTeacher(string Id)
        {
            try
            {
                context = new SmacEntities();
                return (from a in context.Teachers where a.UserId == Id select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PromoteTeacher(string uId)
        {
            try
            {
                context = new SmacEntities();

                if (UserEntity.GetUser(uId) == null)
                {
                    throw new Exception("User not set as Teacher.  User ID doesn't exist.");
                }
                else
                {
                    if (IsTeacher(uId))
                    {
                        throw new Exception("User not set as Teacher.  User already set as Teacher.");
                    }
                    else
                    {
                        Teacher Teacher = new Teacher();
                        Teacher.UserId = uId;
                        Teacher.User = UserEntity.GetUser(uId);

                        context.Teachers.Add(Teacher);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsTeacher(string uId)
        {
            context = new SmacEntities();

            return (from a in context.Teachers where a.UserId == uId select a).FirstOrDefault() != null;
        }

        public static void RevokeTeacher(string uId)
        {
            try
            {
                context = new SmacEntities();

                if (UserEntity.GetUser(uId) == null)
                {
                    throw new Exception("User not removed as Teacher.  User doesn't exist.");
                }
                else if (!IsTeacher(uId))
                {
                    throw new Exception("User not removed as Teacher.  User is not a Teacher.");
                }
                else
                {
                    Teacher Teacher = new Teacher();
                    Teacher.UserId = uId;
                    Teacher.User = UserEntity.GetUser(uId);

                    context.Teachers.Add(Teacher);
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
