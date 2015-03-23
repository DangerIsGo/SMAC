using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class SubjectEntity
    {
        private static SmacEntities context;

        public static void CreateSubject(int schoolId, string subjName)
        {
            CreateEditSubject("ADD", schoolId, subjName, null);
        }

        public static void UpdateSubject(int schoolId, string subjName, string newSubjName)
        {
            CreateEditSubject("EDIT", schoolId, subjName, newSubjName);
        }

        public static Subject GetSubject(int schoolId, string subjName)
        {
            try
            {
                context = new SmacEntities();
                return (from a in context.Subjects where a.SubjectName == subjName && a.SchoolId == schoolId select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Subject> GetAllSubjects(int schoolId)
        {
            try
            {
                context = new SmacEntities();
                return SchoolEntity.GetSchool(schoolId).Subjects.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSubject(int schoolId, string subjName)
        {
            try
            {
                context = new SmacEntities();

                var subject = GetSubject(schoolId, subjName);
                context.Subjects.Remove(subject);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditSubject(string op, int schoolId, string subjName, string newSubjName)
        {
            try
            {
                context = new SmacEntities();

                if (op.Equals("ADD"))
                {
                    if (GetSubject(schoolId, subjName) != null)
                    {
                        throw new Exception("Subject was not created.  Subject name already exists for this school.");
                    }
                    else
                    {
                        Subject subject = new Subject()
                        {
                            School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault(),
                            SubjectName = subjName
                        };

                        context.Subjects.Add(subject);
                        context.SaveChanges();
                    }
                }
                else if (op.Equals("EDIT"))
                {
                    if (GetSubject(schoolId, subjName) == null)
                    {
                        throw new Exception("Subject was not updated.  Subject name not found.");
                    }
                    else if (GetSubject(schoolId, newSubjName) != null)
                    {
                        throw new Exception("Subject was not updated.  New subject name already exists in database.");
                    }
                    else
                    {
                        var subject = GetSubject(schoolId, subjName);

                        subject.SubjectName = newSubjName;

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
