using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SMAC.Database
{
    public class EnrollmentEntity
    {
        //public static void CreateEnrollment(string studentId, int schoolId, string subjectId, int classId, 
        //    int sectionId, int markingPeriodId)
        //{
        //    try
        //    {
        //        using (SmacEntities context = new SmacEntities())
        //        {
        //            var enroll = new Enrollment()
        //            {
        //                Student = StudentEntity.GetStudent(studentId),
        //                MarkingPeriod = MarkingPeriodEntity.GetMarkingPeriod(schoolId, markingPeriodId),
        //                Section = SectionEntity.GetSection(schoolId, subjectId, classId, sectionId)
        //            };

        //            context.Enrollments.Add(enroll);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static Enrollment GetEnrollment(string sId, int schoolId, int subjectId, int classId, 
            int sectionId, int markingPeriodId)
        {
            using (SmacEntities context = new SmacEntities())
            {
                return (from a in context.Enrollments
                        where a.UserId == sId && a.SchoolId == schoolId
                            && a.ClassId == classId && a.SubjectId == subjectId && a.SectionId == sectionId
                            && a.MarkingPeriodId == markingPeriodId
                        select a).FirstOrDefault();
            }
        }

        public static void DeleteEnrollment(string studentId, int schoolId, int subjectId, int classId,
            int sectionId, int markingPeriodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(studentId, schoolId, subjectId, classId, sectionId, markingPeriodId);

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

        public static void SetGrade(string studentId, int schoolId, int subjectId, int classId,
            int sectionId, int markingPeriodId, string grade)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(studentId, schoolId, subjectId, classId, sectionId, markingPeriodId);

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

        public static void RemoveGrade(string studentId, int schoolId, int subjectId, int classId,
            int sectionId, int markingPeriodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(studentId, schoolId, subjectId, classId, sectionId, markingPeriodId);

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

        public static List<Enrollment> GetStudentEnrollments(string studentId, int schoolId, int markingPeriodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Enrollments
                            where a.MarkingPeriodId == markingPeriodId
                                && a.SchoolId == schoolId && a.UserId == studentId
                            select a).Include(t=>t.Section.Class.Subject).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Student> GetSectionRoster(int schoolId, int subjectId, int classId,
            int sectionId, int markingPeriodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Enrollments
                            where a.MarkingPeriodId == markingPeriodId
                                && a.SchoolId == schoolId && a.SubjectId == subjectId && a.ClassId == classId
                                && a.SectionId == sectionId
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
