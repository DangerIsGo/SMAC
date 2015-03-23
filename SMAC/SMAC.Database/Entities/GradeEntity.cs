using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database.Entities
{
    public class GradeEntity
    {
        private static SmacEntities context;

        public static void UpdateGrade(string grade, int schoolId, string newGrade)
        {
            try
            {
                context = new SmacEntities();

                var gr = (from a in context.Grades where a.GradeVal == grade && a.SchoolId == schoolId select a).FirstOrDefault();

                if (gr != null)
                {
                    gr.GradeVal = newGrade;

                    context.SaveChanges();
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
                context = new SmacEntities();

                Grade grade = new Grade()
                {
                    GradeVal = val,
                    School = SchoolEntity.GetSchool(schoolId)
                };

                context.Grades.Add(grade);
                context.SaveChanges();
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
                context = new SmacEntities();

                return SchoolEntity.GetSchool(schoolId).Grades.ToList();
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
                context = new SmacEntities();

                var gr = (from a in context.Grades where a.GradeVal == grade && a.SchoolId == schoolId select a).FirstOrDefault();

                context.Grades.Remove(gr);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
