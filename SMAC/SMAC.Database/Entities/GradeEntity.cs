using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database.Entities
{
    public class GradeEntity
    {
        public static void UpdateGrade(string grade, int schoolId, string newGrade)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var gr = (from a in context.Grades where a.GradeValue == grade && a.SchoolId == schoolId select a).FirstOrDefault();

                    if (gr != null)
                    {
                        gr.GradeValue = newGrade;
                        context.Entry(gr).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateGrade(string val, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    Grade grade = new Grade()
                    {
                        GradeValue = val,
                        School = SchoolEntity.GetSchool(schoolId)
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
                    return SchoolEntity.GetSchool(schoolId).Grades.ToList();
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
