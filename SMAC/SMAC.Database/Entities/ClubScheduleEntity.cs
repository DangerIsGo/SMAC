using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class ClubScheduleEntity
    {
        public static void CreateSchedule(string clubName, int schoolId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    ClubSchedule sch = new ClubSchedule()
                    {
                        Club = ClubEntity.GetClub(clubName, schoolId),
                        DayValue = day,
                        TimeSlot = TimeSlotEntity.GetTimeSlot(tsId)
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

        public static void DeleteSchedule(string clubName, int schoolId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = (from a in context.ClubSchedules
                               where a.ClubName == clubName && a.SchoolId == schoolId && a.TimeSlotId == tsId && a.DayValue == day
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

        public static List<ClubSchedule> GetClubSchedule(string clubName, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.ClubSchedules where a.ClubName == clubName && a.SchoolId == schoolId select a).ToList();
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
                            where a.SchoolId == schoolId && a.DayValue == day
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
