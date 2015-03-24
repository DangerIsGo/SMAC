using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class MarkingPeriodEntity
    {
        public static void CreateMarkingPeriod(int schoolId, string mPeriod, bool fullYear)
        {
            CreateEditMarkingPeriod("ADD", schoolId, mPeriod, fullYear, null);
        }

        public static void UpdateMarkingPeriod(int schoolId, string mPeriod, bool fullYear, int? Id)
        {
            CreateEditMarkingPeriod("EDIT", schoolId, mPeriod, fullYear, Id);
        }

        public static MarkingPeriod GetMarkingPeriod(int schoolId, int Id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.MarkingPeriods where a.SchoolId == schoolId && a.MarkingPeriodId == Id select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static MarkingPeriod GetMarkingPeriod(int schoolId, string mPeriod, bool fullYear)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.MarkingPeriods
                            where a.SchoolId == schoolId && a.Period == mPeriod
                                && a.FullYear == fullYear
                            select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<MarkingPeriod> GetMarkingPeriods(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return SchoolEntity.GetSchool(schoolId).MarkingPeriods.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditMarkingPeriod(string op, int schoolId, string mPeriod, bool fullYear, int? Id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetMarkingPeriod(schoolId, mPeriod, fullYear) != null)
                        {
                            throw new Exception("Marking period was not created.  Marking period already exists for this school.");
                        }
                        else
                        {
                            MarkingPeriod MarkingPeriod = new MarkingPeriod()
                            {
                                School = SchoolEntity.GetSchool(schoolId),
                                Period = fullYear ? null : mPeriod,
                                FullYear = fullYear
                            };

                            context.MarkingPeriods.Add(MarkingPeriod);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetMarkingPeriod(schoolId, Id.Value) == null)
                        {
                            throw new Exception("Marking period was not updated.  Marking period not found.");
                        }
                        else if (GetMarkingPeriod(schoolId, mPeriod, fullYear) != null)
                        {
                            throw new Exception("Marking period was not updated.  New marking period values already exists in database.");
                        }
                        else
                        {
                            var period = GetMarkingPeriod(schoolId, Id.Value);

                            period.Period = mPeriod;
                            period.FullYear = fullYear;
                            context.Entry(period).State = System.Data.Entity.EntityState.Modified;
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
