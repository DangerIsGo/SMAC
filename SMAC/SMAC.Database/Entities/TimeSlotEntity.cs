using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class TimeSlotEntity
    {
        public static void CreateTimeSlot(int schoolId, DateTime start, DateTime end)
        {
            try
            {
                //using (SmacEntities context = new SmacEntities())
                //{
                //    TimeSlot ts = new TimeSlot()
                //    {
                //        StartTime = start,
                //        EndTime = end,
                //        School = SchoolEntity.GetSchool(schoolId)
                //    };

                //    context.TimeSlots.Add(ts);
                //    context.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateTimeSlot(int schoolId, DateTime start, DateTime end, int id)
        {
            try
            {
                //using (SmacEntities context = new SmacEntities())
                //{
                //    var ts = GetTimeSlot(id);

                //    ts.StartTime = start;
                //    ts.EndTime = end;
                //    context.Entry(ts).State = System.Data.Entity.EntityState.Modified;
                //    context.SaveChanges();
                //}
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
                    return SchoolEntity.GetSchool(schoolId).TimeSlots.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
