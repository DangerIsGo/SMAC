using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class SectionEntity
    {
        public static void CreateSection(int schoolId, string subjName, string className, string sectionName, string description)
        {
            CreateEditSection("ADD", schoolId, subjName, className, sectionName, description, null);
        }

        public static void UpdateSection(int schoolId, string subjName, string className, string sectionName, string description, string newSectionName)
        {
            CreateEditSection("EDIT", schoolId, subjName, className, sectionName, description, newSectionName);
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

        public static List<Section> GetSections(int schoolId, string subjName, string className)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var mClass = ClassEntity.GetClass(schoolId, subjName, className);
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

        public static Section GetSection(int schoolId, string subjName, string className, string secName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Sections
                            where a.SchoolId == schoolId && a.SubjectName == subjName
                                && a.ClassName == className && a.SectionName == secName
                            select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditSection(string op, int schoolId, string subjName, string className, string secName, string description, string newSectionName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if (GetSection(schoolId, subjName, className, secName) != null)
                        {
                            throw new Exception("Section was not created.  Section name already exists for this school/subject/class.");
                        }
                        else
                        {
                            Section section = new Section()
                            {
                                SectionName = secName,
                                Class = ClassEntity.GetClass(schoolId, subjName, className),
                                Description = description
                            };

                            context.Sections.Add(section);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (GetSection(schoolId, subjName, className, secName) == null)
                        {
                            throw new Exception("Section was not updated.  Section name not found.");
                        }
                        else if (GetSection(schoolId, subjName, className, newSectionName) != null)
                        {
                            throw new Exception("Section was not updated.  New section name already exists in system.");
                        }
                        else
                        {
                            var mSection = GetSection(schoolId, subjName, className, secName);

                            mSection.SectionName = newSectionName;
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

        public static void DeleteSection(int schoolId, string subjName, string className, string secName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var section = GetSection(schoolId, subjName, className, secName);
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
