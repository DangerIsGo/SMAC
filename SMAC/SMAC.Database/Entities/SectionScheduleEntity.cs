using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SMAC.Database
{
    public class SectionScheduleEntity
    {
        public static void CreateSchedule(int sectionId, int tsId, string day, int periodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if ((from a in context.SectionSchedules where a.SectionId == sectionId && a.TimeSlotId == tsId && a.DayValue == day && a.MarkingPeriodId == periodId select a).FirstOrDefault() != null)
                    {
                        throw new Exception("Schedule already exists for this section.");
                    }

                    SectionSchedule sch = new SectionSchedule()
                    {
                        Section = (from a in context.Sections where a.SectionId == sectionId select a).FirstOrDefault(),
                        Day = (from a in context.Days where a.DayValue == day select a).FirstOrDefault(),
                        TimeSlot = (from a in context.TimeSlots where a.TimeSlotId == tsId select a).FirstOrDefault(),
                        MarkingPeriod = (from a in context.MarkingPeriods where a.MarkingPeriodId == periodId select a).FirstOrDefault()
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

        public static List<SectionSchedule> GetSectionSchedulAlt(int sectionId, int periodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.SectionSchedules where a.SectionId == sectionId && a.MarkingPeriodId == periodId select a).Include(t => t.TimeSlot).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSchedule(int sectionId, int periodId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = (from a in context.SectionSchedules
                               where a.SectionId == sectionId && a.MarkingPeriodId == periodId && a.DayValue == day && a.TimeSlotId == tsId
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
