using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class MarkingPeriodEntity
    {
        public static void CreateMarkingPeriod(string period, bool fullYear, int schoolYearId)
        {
            CreateEditMarkingPeriod("ADD", null, period, fullYear, schoolYearId);
        }

        public static void UpdateMarkingPeriod(int markingPeriodId, string period, bool fullYear, int schoolYearId)
        {
            CreateEditMarkingPeriod("EDIT", markingPeriodId, period, fullYear, schoolYearId);
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

        public static MarkingPeriod GetMarkingPeriod(string period, bool fullYear, int schoolYearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.MarkingPeriods
                            where a.Period == period &&
                            a.FullYear == fullYear &&
                            a.SchoolYearId == schoolYearId
                            select a).FirstOrDefault();
                }
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

        private static void CreateEditMarkingPeriod(string op, int? markingPeriodId, string period, bool fullYear, int schoolYearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetMarkingPeriod(period, fullYear, schoolYearId) != null)
                        {
                            throw new Exception("Marking period was not created.  Marking period already exists for this school year.");
                        }
                        else
                        {
                            MarkingPeriod MarkingPeriod = new MarkingPeriod()
                            {
                                SchoolYear = SchoolYearEntity.GetSchoolYear(schoolYearId),
                                Period = fullYear ? null : period,
                                FullYear = fullYear
                            };

                            context.MarkingPeriods.Add(MarkingPeriod);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetMarkingPeriod(markingPeriodId.Value) == null)
                        {
                            throw new Exception("Marking period was not updated.  Marking period not found.");
                        }
                        else if (GetMarkingPeriod(period, fullYear, schoolYearId) != null)
                        {
                            throw new Exception("Marking period was not updated.  New marking period values already exist.");
                        }
                        else
                        {
                            var mPeriod = GetMarkingPeriod(markingPeriodId.Value);

                            mPeriod.Period = period;
                            mPeriod.FullYear = fullYear;
                            context.Entry(mPeriod).State = System.Data.Entity.EntityState.Modified;
                            context.SaveChanges();
                        }
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
