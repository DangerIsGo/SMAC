using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class GradeEntity
    {
        public static void CreateGrade(string val, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if ((from a in context.Grades where a.SchoolId == schoolId && a.GradeValue == val select a).FirstOrDefault() != null)
                    {
                        throw new Exception("Grade not created as it already exists.");
                    }

                    Grade grade = new Grade()
                    {
                        GradeValue = val,
                        School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault()
                    };

                    context.Grades.Add(grade);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Grade> GetGrades(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Grades where a.SchoolId == schoolId select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteGrade(string grade, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var gr = (from a in context.Grades where a.GradeValue == grade && a.SchoolId == schoolId select a).FirstOrDefault();

                    context.Grades.Remove(gr);
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
