using System;
using System.Linq;

namespace SMAC.Database
{
    public class StudentEntity
    {
        public static void CreateUserAsStudent(string Id, string fName, string mName, string lName, string email, string phone,
                                        string gender, bool isActive, DateTime startDate, DateTime? endDate)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (UserEntity.GetUser(Id) != null)
                    {
                        throw new Exception("Student not created.  User already exists.");
                    }
                    else
                    {
                        Student Student = new Student
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

                        context.Students.Add(Student);

                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Student GetStudent(string Id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Students where a.UserId == Id select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void PromoteStudent(string uId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (UserEntity.GetUser(uId) == null)
                    {
                        throw new Exception("User not set as Student.  User ID doesn't exist.");
                    }
                    else
                    {
                        if (IsStudent(uId))
                        {
                            throw new Exception("User not set as Student.  User already set as Student.");
                        }
                        else
                        {
                            Student Student = new Student();
                            Student.UserId = uId;
                            Student.User = UserEntity.GetUser(uId);

                            context.Students.Add(Student);
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

        public static bool IsStudent(string uId)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Students where a.UserId == uId select a).FirstOrDefault() != null;
            }
        }

        public static void RevokeStudent(string uId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (UserEntity.GetUser(uId) == null)
                    {
                        throw new Exception("User not removed as Student.  User doesn't exist.");
                    }
                    else if (!IsStudent(uId))
                    {
                        throw new Exception("User not removed as Student.  User not an Student.");
                    }
                    else
                    {
                        Student Student = new Student();
                        Student.UserId = uId;
                        Student.User = UserEntity.GetUser(uId);

                        context.Students.Add(Student);
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
