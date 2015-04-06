using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class ClubScheduleEntity
    {
        public static void CreateSchedule(int clubId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    ClubSchedule sch = new ClubSchedule()
                    {
                        Club = (from a in context.Clubs where a.ClubId == clubId select a).FirstOrDefault(),
                        DayValue = day,
                        TimeSlot = (from a in context.TimeSlots where a.TimeSlotId == tsId select a).FirstOrDefault()
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

        public static void DeleteSchedule(int clubId, int tsId, string day)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = (from a in context.ClubSchedules
                               where a.ClubId == clubId && a.TimeSlotId == tsId && a.DayValue == day
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
