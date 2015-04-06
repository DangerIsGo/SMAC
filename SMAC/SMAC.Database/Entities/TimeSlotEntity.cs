using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class TimeSlotEntity
    {
        public static void CreateTimeSlot(int schoolId, TimeSpan start, TimeSpan end)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if ((from a in context.TimeSlots where a.StartTime == start && a.EndTime == end && a.SchoolId == schoolId select a).FirstOrDefault() != null)
                    {
                        throw new Exception("Time slot already exists.  Time slot not created.");
                    }

                    TimeSlot ts = new TimeSlot()
                    {
                        School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault(),
                        StartTime = start,
                        EndTime = end
                    };

                    context.TimeSlots.Add(ts);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateTimeSlot(int schoolId, TimeSpan start, TimeSpan end, int id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if ((from a in context.TimeSlots where a.TimeSlotId == id select a) == null)
                    {
                        throw new Exception("Time slot could not be found");
                    }
                    
                    if ((from a in context.TimeSlots where a.StartTime == start && a.EndTime == end && a.SchoolId == schoolId && a.TimeSlotId != id select a).FirstOrDefault() != null)
                    {
                        throw new Exception("Time slot already exists.  Time slot not updated.");
                    }

                    TimeSlot ts = (from a in context.TimeSlots where a.TimeSlotId == id select a).FirstOrDefault();
                    ts.StartTime = start;
                    ts.EndTime = end;

                    context.Entry(ts).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static TimeSlot GetTimeSlot(int id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.TimeSlots where a.TimeSlotId == id select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<TimeSlot> GetTimeSlots(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.TimeSlots where a.SchoolId == schoolId select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTimeSlot(int timeSlotId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var ts = (from a in context.TimeSlots where a.TimeSlotId == timeSlotId select a).FirstOrDefault();
                    context.TimeSlots.Remove(ts);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
