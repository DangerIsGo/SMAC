using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class TeacherScheduleEntity
    {
        private static SmacEntities context;

        public static void CreateTeacherSchedule(string teacherId, string secName, string className, string subjName, int schoolId, int mpId, string year)
        {
            try
            {
                context = new SmacEntities();

                TeacherSchedule sch = new TeacherSchedule()
                {
                    Section = SectionEntity.GetSection(schoolId, subjName, className, secName),
                    Teacher = TeacherEntity.GetTeacher(teacherId),
                    MarkingPeriodId = mpId,
                    SchoolYear = year
                };

                context.TeacherSchedules.Add(sch);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTeacherSchedule(string teacherId, string secName, string className, string subjName, int schoolId, int mpId, string year)
        {
            try
            {
                context = new SmacEntities();

                var sch = (from a in context.TeacherSchedules
                           where a.SectionName == secName && a.ClassName == className
                           && a.SubjectName == subjName && a.SchoolId == schoolId
                           && a.MarkingPeriodId == mpId && a.SchoolYear == year
                           select a).FirstOrDefault();

                if (sch != null)
                {
                    context.TeacherSchedules.Remove(sch);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TeacherSchedule> GetTeacherSchedule(string teacherId, int mpId, string year)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.TeacherSchedules
                 where a.UserId == teacherId
                 && a.MarkingPeriodId == mpId && a.SchoolYear == year
                 select a).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Teacher> GetSectionsTeachers(string secName, string className, string subjName, int schoolId, int mpId, string year)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.TeacherSchedules
                        where a.SectionName == secName && a.ClassName == className
                        && a.SubjectName == subjName && a.SchoolId == schoolId
                        && a.MarkingPeriodId == mpId && a.SchoolYear == year
                        select a.Teacher).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
