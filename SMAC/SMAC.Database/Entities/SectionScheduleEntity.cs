using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class SectionScheduleEntity
    {
        private static SmacEntities context;

        public static void CreateSchedule(string secName, string className, string subjName, int schoolId, int tsId, string day)
        {
            try
            {
                context = new SmacEntities();

                SectionSchedule sch = new SectionSchedule()
                {
                    Section = SectionEntity.GetSection(schoolId, subjName, className, secName),
                    DayValue = day,
                    TimeSlot = TimeSlotEntity.GetTimeSlot(tsId)
                };

                context.SectionSchedules.Add(sch);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSchedule(string secName, string className, string subjName, int schoolId, int tsId, string day)
        {
            try
            {
                context = new SmacEntities();

                var sch = (from a in context.SectionSchedules
                           where a.SectionName == secName && a.ClassName == className 
                           && a.SubjectName == subjName && a.SchoolId == schoolId && a.TimeSlotId == tsId && a.DayValue == day
                           select a).FirstOrDefault();

                if (sch != null)
                {
                    context.SectionSchedules.Remove(sch);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SectionSchedule> GetSectionSchedule(string secName, string className, string subjName, int schoolId)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.SectionSchedules where a.SectionName == secName && a.ClassName == className 
                            && a.SubjectName == subjName && a.SchoolId == schoolId select a).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SectionSchedule> GetDaySchedule(string secName, string className, string subjName, int schoolId, string day)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.SectionSchedules
                        where a.SectionName == secName && a.ClassName == className
                            && a.SubjectName == subjName && a.SchoolId == schoolId && a.DayValue == day
                        select a).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
