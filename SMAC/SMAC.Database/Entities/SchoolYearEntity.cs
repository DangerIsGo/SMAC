using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class SchoolYearEntity
    {
        public static void CreateSchoolYear(int schoolId, string year, DateTime start, DateTime end)
        {
            CreateEditSchoolYear("ADD", schoolId, year, start, end, null);
        }

        public static void UpdateSchoolYear(int schoolId, int schoolYearId, string year, DateTime start, DateTime end)
        {
            CreateEditSchoolYear("EDIT", schoolId, year, start, end, schoolYearId);
        }

        public static SchoolYear GetSchoolYear(int schoolYearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.SchoolYears where a.SchoolYearId == schoolYearId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<SchoolYear> GetSchoolYears(int schoolId)
        {
            try
            {
                return SchoolEntity.GetSchool(schoolId).SchoolYears.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSchoolYear(int schoolYearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var year = (from a in context.SchoolYears where a.SchoolYearId == schoolYearId select a).FirstOrDefault();
                    context.SchoolYears.Remove(year);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditSchoolYear(string op, int schoolId, string year, DateTime start, DateTime end, int? schoolYearId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if ((from a in context.SchoolYears where a.Year == year select a).FirstOrDefault() != null)
                        {
                            throw new Exception("School year was not created.  School year already exists for this school.");
                        }
                        else
                        {
                            SchoolYear schoolyear = new SchoolYear()
                            {
                                School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault(),
                                Year = year,
                                StartDate = start,
                                EndDate = end
                            };

                            context.SchoolYears.Add(schoolyear);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if ((from a in context.SchoolYears where a.SchoolYearId == schoolYearId.Value select a).FirstOrDefault() == null)
                        {
                            throw new Exception("Subject was not updated.  Subject name not found.");
                        }
                        else if ((from a in context.SchoolYears where a.SchoolYearId != schoolYearId.Value && a.SchoolId == schoolId && a.Year == year select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Subject was not updated.  New subject name already exists in database.");
                        }
                        else
                        {
                            var schYear = (from a in context.SchoolYears where a.SchoolYearId == schoolYearId.Value select a).FirstOrDefault();

                            schYear.Year = year;
                            schYear.StartDate = start;
                            schYear.EndDate = end;
                            context.Entry(schYear).State = System.Data.Entity.EntityState.Modified;
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
