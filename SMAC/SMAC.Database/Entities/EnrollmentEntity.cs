using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class EnrollmentEntity
    {
        public static void CreateEnrollment(string studentId, int schoolId, string subjName, string className, 
            string secName, int mpId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = new Enrollment()
                    {
                        Student = StudentEntity.GetStudent(studentId),
                        MarkingPeriod = MarkingPeriodEntity.GetMarkingPeriod(schoolId, mpId),
                        SchoolYear = SchoolYearEntity.GetSchoolYear(schoolId, year),
                        Section = SectionEntity.GetSection(schoolId, subjName, className, secName)
                    };

                    context.Enrollments.Add(enroll);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Enrollment GetEnrollment(string sId, int schoolId, string subjName, string className, 
            string secName, int mpId, string year)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Enrollments
                        where a.UserId == sId && a.SchoolId == schoolId
                            && a.ClassName == className && a.SubjectName == subjName && a.SectionName == secName
                            && a.MarkingPeriodId == mpId && a.Year == year
                        select a).FirstOrDefault();
            }
        }

        public static void DeleteEnrollment(string studentId, int schoolId, string subjName, string className,
            string secName, int mpId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(studentId, schoolId, subjName, className, secName, mpId, year);

                    if (enroll != null)
                    {
                        context.Enrollments.Remove(enroll);
                        context.SaveChanges();
                    }
                    else
                        throw new Exception("Enrollment not found.  Grade not set.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetGrade(string studentId, int schoolId, string subjName, string className,
            string secName, int mpId, string year, string grade)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(studentId, schoolId, subjName, className, secName, mpId, year);

                    if (enroll != null)
                    {
                        enroll.GradeValue = grade;
                        context.Entry(enroll).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                        throw new Exception("Enrollment not found.  Grade not set.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveGrade(string studentId, int schoolId, string subjName, string className,
            string secName, int mpId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(studentId, schoolId, subjName, className, secName, mpId, year);

                    if (enroll != null)
                    {
                        enroll.GradeValue = null;
                        context.Entry(enroll).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                    else
                        throw new Exception("Enrollment not found.  Grade not set.");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Enrollment> GetStudentEnrollments(string studentId, int schoolId, int mpId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Enrollments
                            where a.MarkingPeriodId == mpId && a.Year == year
                                && a.SchoolId == schoolId && a.UserId == studentId
                            select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Student> GetSectionRoster(int schoolId, string subjName, string className,
            string secName, int mpId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Enrollments
                            where a.MarkingPeriodId == mpId && a.Year == year
                                && a.SchoolId == schoolId && a.SubjectName == subjName && a.ClassName == className
                                && a.SectionName == secName
                            select a.Student).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
