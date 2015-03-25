using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class SchoolYearEntity
    {
        public static void CreateSchoolYear(int schoolId, string year)
        {
            CreateEditSchoolYear("ADD", schoolId, year, null);
        }

        public static void UpdateSchoolYear(int schoolId, string year, string newYear)
        {
            CreateEditSchoolYear("EDIT", schoolId, year, newYear);
        }

        public static SchoolYear GetSchoolYear(int schoolId, string year)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.SchoolYears where a.SchoolId == schoolId && a.Year == year select a).FirstOrDefault();
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

        private static void CreateEditSchoolYear(string op, int schoolId, string year, string newYear)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetSchoolYear(schoolId, year) != null)
                        {
                            throw new Exception("School year was not created.  School year already exists for this school.");
                        }
                        else
                        {
                            SchoolYear schoolyear = new SchoolYear()
                            {
                                School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault(),
                                Year = year
                            };

                            context.SchoolYears.Add(schoolyear);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetSchoolYear(schoolId, year) == null)
                        {
                            throw new Exception("Subject was not updated.  Subject name not found.");
                        }
                        else if (GetSchoolYear(schoolId, newYear) != null)
                        {
                            throw new Exception("Subject was not updated.  New subject name already exists in database.");
                        }
                        else
                        {
                            var schYear = GetSchoolYear(schoolId, year);

                            schYear.Year = newYear;
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
