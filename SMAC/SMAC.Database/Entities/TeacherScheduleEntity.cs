using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class TeacherScheduleEntity
    {
        public static void CreateTeacherSchedule(string teacherId, int sectionId, int schoolId, int markingPeriodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    TeacherSchedule sch = new TeacherSchedule()
                    {
                        Section = SectionEntity.GetSection(sectionId),
                        Teacher = TeacherEntity.GetTeacher(teacherId),
                        MarkingPeriodId = markingPeriodId
                    };

                    context.TeacherSchedules.Add(sch);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TeacherSchedule> GetTeacherSchedule(string teacherId, int periodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.TeacherSchedules where a.UserId == teacherId && a.MarkingPeriodId == periodId select a).Include(t=>t.Section.Class).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTeacherSchedule(string teacherId, int sectionId, int schoolId, int markingPeriodId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = (from a in context.TeacherSchedules
                               where a.UserId == teacherId && a.SectionId == sectionId
                               && a.MarkingPeriodId == markingPeriodId
                               select a).FirstOrDefault();

                    if (sch != null)
                    {
                        context.TeacherSchedules.Remove(sch);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    //    public static List<TeacherSchedule> GetTeacherSchedule(string teacherId, int mpId, string year)
    //    {
    //        try
    //        {
    //            using (SmacEntities context = new SmacEntities())
    //            {
    //                return (from a in context.TeacherSchedules
    //                        where a.UserId == teacherId
    //                        && a.MarkingPeriodId == mpId && a.SchoolYear == year
    //                        select a).ToList();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public static List<Teacher> GetSectionsTeachers(string secName, string className, string subjName, int schoolId, int mpId, string year)
    //    {
    //        try
    //        {
    //            using (SmacEntities context = new SmacEntities())
    //            {
    //                return (from a in context.TeacherSchedules
    //                        where a.SectionName == secName && a.ClassName == className
    //                        && a.SubjectName == subjName && a.SchoolId == schoolId
    //                        && a.MarkingPeriodId == mpId && a.SchoolYear == year
    //                        select a.Teacher).ToList();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    }
}
