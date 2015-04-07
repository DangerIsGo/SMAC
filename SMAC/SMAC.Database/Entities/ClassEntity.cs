using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class ClassEntity
    {
        public static void CreateClass(int schoolId, int subjectId, string className, string description)
        {
            CreateEditClass("ADD", schoolId, subjectId, null, description, className);
        }

        public static void UpdateClass(int schoolId, int subjectId, string className, string description, int classId)
        {
            CreateEditClass("EDIT", schoolId, subjectId, classId, description, className);
        }

        public static List<Class> GetClassesForSubject(int subjectId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Classes where a.SubjectId == subjectId select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Class> GetAllClassesInSchool(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = SchoolEntity.GetSchool(schoolId);
                    if (sch != null)
                    {
                        List<Class> classList = new List<Class>();
                        sch.Subjects.ToList().ForEach(t => t.Classes.ToList().ForEach(m => classList.Add(m)));
                        return classList;
                    }
                    else
                        throw new Exception("School name not found.  Aborting.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Class GetClass(int schoolId, int subjectId, string className)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Classes where a.ClassName == className && a.SubjectId == subjectId && a.SchoolId == schoolId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Class GetClass(int classId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Classes where a.ClassId == classId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditClass(string op, int schoolId, int subjectId, int? classId, string description, string className)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if ((from a in context.Classes where a.ClassName == className && a.SchoolId == schoolId && a.SubjectId == subjectId select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Class was not created.  Class name already exists for this school/subject.");
                        }
                        else
                        {
                            Class Class = new Class()
                            {
                                ClassName = className,
                                Subject = (from a in context.Subjects where a.SubjectId == subjectId select a).FirstOrDefault(),
                                Description = string.IsNullOrWhiteSpace(description) ? null : description
                            };

                            context.Classes.Add(Class);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        var mClass = (from a in context.Classes where a.ClassId == classId.Value select a).FirstOrDefault();

                        if (mClass == null)
                        {
                            throw new Exception("Class was not updated.  Class name not found.");
                        }
                        
                        if ((from a in context.Classes where a.ClassName == className && a.SubjectId == mClass.SubjectId && a.ClassId != classId.Value select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Class was not updated.  Class name already exists for subject/school.");
                        }
                        
                        mClass.ClassName = className;
                        mClass.Subject = (from a in context.Subjects where a.SubjectId == subjectId select a).FirstOrDefault();
                        mClass.Description = string.IsNullOrWhiteSpace(description) ? null : description;

                        context.Entry(mClass).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteClass(int classId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var Class = (from a in context.Classes where a.ClassId == classId select a).FirstOrDefault();
                    context.Classes.Remove(Class);
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
