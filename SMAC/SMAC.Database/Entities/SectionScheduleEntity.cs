using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class SectionScheduleEntity
    {
        public static void CreateSchedule(int sectionId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    SectionSchedule sch = new SectionSchedule()
                    {
                        Section = SectionEntity.GetSection(sectionId),
                        DayValue = day,
                        TimeSlot = TimeSlotEntity.GetTimeSlot(tsId)
                    };

                    context.SectionSchedules.Add(sch);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSchedule(int sectionId, int classId, int subjectId, int schoolId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = (from a in context.SectionSchedules
                               where a.SectionId == sectionId && a.ClassId == classId
                               && a.SubjectId == subjectId && a.SchoolId == schoolId && a.TimeSlotId == tsId && a.DayValue == day
                               select a).FirstOrDefault();

                    if (sch != null)
                    {
                        context.SectionSchedules.Remove(sch);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SectionSchedule> GetSectionSchedule(int sectionId, int classId, int subjectId, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.SectionSchedules
                            where a.SectionId == sectionId && a.ClassId == classId
                               && a.SubjectId == subjectId && a.SchoolId == schoolId
                            select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SectionSchedule> GetDaySchedule(int sectionId, int classId, int subjectId, int schoolId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.SectionSchedules
                            where a.SectionId == sectionId && a.ClassId == classId
                               && a.SubjectId == subjectId && a.SchoolId == schoolId && a.DayValue == day
                            select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
