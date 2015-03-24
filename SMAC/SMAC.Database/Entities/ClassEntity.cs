using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class ClassEntity
    {
        public static void CreateClass(int schoolId, string subjName, string className, string description)
        {
            CreateEditClass("ADD", schoolId, subjName, className, description, null);
        }

        public static void UpdateClass(int schoolId, string subjName, string className, string description, string newClassName)
        {
            CreateEditClass("EDIT", schoolId, subjName, className, description, newClassName);
        }

        public static List<Class> GetAllClasses(int schoolId, string subjName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return SubjectEntity.GetSubject(schoolId, subjName).Classes.ToList();
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

        public static Class GetClass(int schoolId, string subjName, string className)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Classes where a.ClassName == className && a.SubjectName == subjName && a.SchoolId == schoolId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditClass(string op, int schoolId, string subjName, string className, string description, string newClassName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetClass(schoolId, subjName, className) != null)
                        {
                            throw new Exception("Class was not created.  Class name already exists for this school/subject.");
                        }
                        else
                        {
                            Class Class = new Class()
                            {
                                ClassName = className,
                                Subject = SubjectEntity.GetSubject(schoolId, subjName),
                                Description = description
                            };

                            context.Classes.Add(Class);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetClass(schoolId, subjName, className) == null)
                        {
                            throw new Exception("Class was not updated.  Class name not found.");
                        }
                        else if (GetClass(schoolId, subjName, newClassName) != null)
                        {
                            throw new Exception("Class was not updated.  Class name already exists in system.");
                        }
                        else
                        {
                            var mClass = GetClass(schoolId, subjName, className);

                            mClass.ClassName = newClassName;
                            mClass.Description = description;
                            context.Entry(mClass).State = System.Data.Entity.EntityState.Modified;
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

        public static void DeleteClass(string className, string subjName, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var Class = GetClass(schoolId, subjName, className);
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
