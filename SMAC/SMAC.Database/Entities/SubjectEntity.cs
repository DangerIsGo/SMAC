using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class SubjectEntity
    {
        public static void CreateSubject(int schoolId, string subjName)
        {
            CreateEditSubject("ADD", schoolId, subjName, null);
        }

        public static void UpdateSubject(int schoolId, int subjectId, string subjName)
        {
            CreateEditSubject("EDIT", schoolId, subjName, subjectId);
        }

        public static Subject GetSubject(int subjectId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Subjects where a.SubjectId == subjectId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Subject GetSubject(int schoolId, string subjName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Subjects where a.SubjectName == subjName && a.SchoolId == schoolId select a).FirstOrDefault();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    var school = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault();

                    return school.Subjects.OrderBy(t=>t.SubjectName).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteSubject(int subjectId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var subject = (from a in context.Subjects where a.SubjectId == subjectId select a).FirstOrDefault();
                    context.Subjects.Remove(subject);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditSubject(string op, int? schoolId, string subjName, int? subjectId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if ((from a in context.Subjects where a.SchoolId == schoolId.Value && a.SubjectName == subjName select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Subject was not created.  Subject name already exists for this school.");
                        }
                        else
                        {
                            Subject subject = new Subject()
                            {
                                School = (from a in context.Schools where a.SchoolId == schoolId.Value select a).FirstOrDefault(),
                                SubjectName = subjName
                            };

                            context.Subjects.Add(subject);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if ((from a in context.Subjects where a.SubjectId == subjectId.Value select a).FirstOrDefault() == null)
                        {
                            throw new Exception("Subject was not updated.  Subject name not found.");
                        }
                        else if ((from a in context.Subjects where a.SchoolId == schoolId && a.SubjectName == subjName && a.SubjectId != subjectId.Value select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Subject was not updated.  New subject name already exists in database.");
                        }
                        else
                        {
                            var subject = (from a in context.Subjects where a.SubjectId == subjectId select a).FirstOrDefault();

                            subject.SubjectName = subjName;
                            context.Entry(subject).State = System.Data.Entity.EntityState.Modified;
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
