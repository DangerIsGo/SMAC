using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class MarkingPeriodEntity
    {
        public static void CreateMarkingPeriod(string period, bool fullYear, int schoolYearId, DateTime start, DateTime end)
        {
            CreateEditMarkingPeriod("ADD", null, period, fullYear, schoolYearId, start, end);
        }

        public static void UpdateMarkingPeriod(int markingPeriodId, string period, bool fullYear, int schoolYearId, DateTime start, DateTime end)
        {
            CreateEditMarkingPeriod("EDIT", markingPeriodId, period, fullYear, schoolYearId, start, end);
        }

        public static MarkingPeriod GetMarkingPeriod(int markingPeriodId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.MarkingPeriods
                            where a.MarkingPeriodId == markingPeriodId
                            select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MarkingPeriod GetMarkingPeriod(string period, bool fullYear, int schoolYearId, int markingPeriodId, SmacEntities context)
        {
            try
            {
                return (from a in context.MarkingPeriods
                        where a.Period == period &&
                        a.FullYear == fullYear &&
                        a.SchoolYearId == schoolYearId
                        && a.MarkingPeriodId != markingPeriodId
                        select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MarkingPeriod> GetMarkingPeriods(int schoolYearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.MarkingPeriods where a.SchoolYearId == schoolYearId select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteMarkingPeriod(int id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var period = (from a in context.MarkingPeriods where a.MarkingPeriodId == id select a).FirstOrDefault();

                    context.MarkingPeriods.Remove(period);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditMarkingPeriod(string op, int? markingPeriodId, string period, bool fullYear, int schoolYearId, DateTime start, DateTime end)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if ((from a in context.MarkingPeriods where a.Period == period && a.FullYear == fullYear && a.SchoolYearId == schoolYearId select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Marking period was not created.  Marking period already exists for this school year.");
                        }

                        MarkingPeriod MarkingPeriod = new MarkingPeriod()
                        {
                            SchoolYear = (from a in context.SchoolYears where a.SchoolYearId == schoolYearId select a).FirstOrDefault(),
                            Period = fullYear ? null : period,
                            FullYear = fullYear,
                            StartDate = start,
                            EndDate = end
                        };

                        context.MarkingPeriods.Add(MarkingPeriod);
                        context.SaveChanges();
                    }
                    else if (op.Equals("EDIT"))
                    {
                        var mPeriod = (from a in context.MarkingPeriods where a.MarkingPeriodId == markingPeriodId.Value select a).FirstOrDefault();

                        if (mPeriod == null)
                        {
                            throw new Exception("Marking period was not updated.  Marking period not found.");
                        }
                        
                        if (GetMarkingPeriod(period, fullYear, schoolYearId, markingPeriodId.Value, context) != null)
                        {
                            throw new Exception("Marking period was not updated.  New marking period values already exist.");
                        }

                        mPeriod.Period = fullYear ? null : period;
                        mPeriod.FullYear = fullYear;
                        mPeriod.StartDate = start;
                        mPeriod.EndDate = end;
                        mPeriod.SchoolYear = (from a in context.SchoolYears where a.SchoolYearId == schoolYearId select a).FirstOrDefault();

                        context.Entry(mPeriod).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
