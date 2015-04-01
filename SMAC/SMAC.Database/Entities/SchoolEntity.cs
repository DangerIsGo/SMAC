using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SMAC.Database
{
    public class SchoolEntity
    {
        public static void CreateSchool(string schName, string addr, string city, string state, string zip, string phone)
        {
            CreateEditSchool("ADD", schName, addr, city, state, zip, phone, null);
        }

        public static void UpdateSchool(int schoolId, string schName, string addr, string city, string state, string zip, string phone)
        {
            CreateEditSchool("EDIT", schName, addr, city, state, zip, phone, schoolId);
        }

        public static List<School> GetUsersSchools(string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Users where a.UserId == userId select a).FirstOrDefault().Schools.ToList().OrderBy(t => t.SchoolName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<School> GetAllSchools()
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Schools select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static School GetSchool(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Schools where a.SchoolId == schoolId select a).Include(a => a.SchoolYears).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditSchool(string op, string schName, string addr, string city, string state, string zip, string phone, int? schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {

                    if (op.Equals("ADD"))
                    {
                        School school = new School()
                        {
                            SchoolName = schName,
                            StreetAddress = addr,
                            City = city,
                            State = state,
                            ZipCode = zip,
                            PhoneNumber = phone
                        };

                        context.Schools.Add(school);
                        context.SaveChanges();
                    }
                    else if (op.Equals("EDIT"))
                    {
                        var school = GetSchool(schoolId.Value);

                        school.SchoolName = schName;
                        school.StreetAddress = addr;
                        school.City = city;
                        school.State = state;
                        school.ZipCode = zip;
                        school.PhoneNumber = phone;
                        context.Entry(school).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSchool(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var school = GetSchool(schoolId);
                    context.Schools.Remove(school);
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
