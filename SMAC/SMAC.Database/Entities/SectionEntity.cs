using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class SectionEntity
    {
        public static void CreateSection(int schoolId, string subjName, string className, string sectionName, string description)
        {
            //CreateEditSection("ADD", schoolId, subjName, className, sectionName, description, null);
        }

        public static void UpdateSection(int schoolId, string subjName, string className, string sectionName, string description, string newSectionName)
        {
            //CreateEditSection("EDIT", schoolId, subjName, className, sectionName, description, newSectionName);
        }

        public static List<Section> GetAllSchoolSections(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var sch = SchoolEntity.GetSchool(schoolId);
                    if (sch != null)
                    {
                        List<Section> sections = new List<Section>();
                        sch.Subjects.ToList().ForEach(t => t.Classes.ToList().ForEach(m => m.Sections.ToList().ForEach(g => sections.Add(g))));
                        return sections;
                    }
                    else
                        throw new Exception("School does not exist.  Aborting.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Section> GetSections(int classId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var mClass = ClassEntity.GetClass(classId);
                    if (mClass != null)
                    {
                        return mClass.Sections.ToList();
                    }
                    else
                        throw new Exception("Class/Subject/School does not exist.  Aborting.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Section GetSection(int sectionId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Sections
                            where a.SectionId == sectionId
                            select a).Include(t=>t.Enrollments).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Section GetSection(int sectionId, SmacEntities context)
        {
            try
            {
                return (from a in context.Sections
                        where a.SectionId == sectionId
                        select a).Include(t => t.Enrollments).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Section GetSection(int schoolId, int subjectId, int classId, string sectionName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Sections
                            where a.SchoolId == schoolId && a.SubjectId == subjectId
                                && a.ClassId == classId && a.SectionName == sectionName
                            select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditSection(string op, int schoolId, int subjectId, int classId, int? sectionId, string sectionName, string description)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetSection(schoolId, subjectId, classId, sectionName) != null)
                        {
                            throw new Exception("Section was not created.  Section name already exists for this school/subject/class.");
                        }
                        else
                        {
                            Section section = new Section()
                            {
                                SectionName = sectionName,
                                Class = ClassEntity.GetClass(classId),
                                Description = description
                            };

                            context.Sections.Add(section);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetSection(sectionId.Value) == null)
                        {
                            throw new Exception("Section was not updated.  Section name not found.");
                        }
                        else
                        {
                            var mSection = GetSection(sectionId.Value);

                            mSection.SectionName = sectionName;
                            mSection.Description = description;
                            context.Entry(mSection).State = System.Data.Entity.EntityState.Modified;
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

        public static void DeleteSection(int sectionId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var section = GetSection(sectionId);
                    context.Sections.Remove(section);
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
