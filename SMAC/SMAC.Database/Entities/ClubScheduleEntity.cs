using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class ClubScheduleEntity
    {
        public static void CreateSchedule(int clubId, int tsId, string day, int yearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    ClubSchedule sch = new ClubSchedule()
                    {
                        Club = (from a in context.Clubs where a.ClubId == clubId select a).FirstOrDefault(),
                        Day = (from a in context.Days where a.DayValue == day select a).FirstOrDefault(),
                        TimeSlot = (from a in context.TimeSlots where a.TimeSlotId == tsId select a).FirstOrDefault(),
                        SchoolYear = (from a in context.SchoolYears where a.SchoolYearId == yearId select a).FirstOrDefault()
                    };

                    context.ClubSchedules.Add(sch);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSchedule(int clubId, int tsId, string day, int yearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = (from a in context.ClubSchedules
                               where a.ClubId == clubId && a.TimeSlotId == tsId && a.DayValue == day && a.SchoolYearId == yearId
                               select a).FirstOrDefault();

                    if (sch != null)
                    {
                        context.ClubSchedules.Remove(sch);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<usp_GetClubSchedule_Result> GetClubSchedule(int clubId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return context.usp_GetClubSchedule(clubId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ClubSchedule> GetClubScheduleAlt(int clubId, int yearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.ClubSchedules where a.ClubId == clubId && a.SchoolYearId == yearId select a).Include(t => t.TimeSlot).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ClubSchedule> GetDaySchedule(int schoolId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.ClubSchedules
                            where a.DayValue == day
                            && a.Club.SchoolId == schoolId
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
