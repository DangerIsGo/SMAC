using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class SectionEntity
    {
        public static void CreateSection(int schoolId, int subjectId, int classId, string sectionName, string description)
        {
            CreateEditSection("ADD", schoolId, subjectId, classId, null, sectionName, description);
        }

        public static void UpdateSection(int schoolId, int subjectId, int classId, int sectionId, string sectionName, string description)
        {
            CreateEditSection("EDIT", schoolId, subjectId, classId, sectionId, sectionName, description);
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
                    return (from a in context.Sections where a.ClassId == classId select a).ToList();
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
                        if ((from a in context.Sections where a.SchoolId == schoolId && a.SubjectId == subjectId && a.ClassId == classId && a.SectionName == sectionName select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Section was not created.  Section name already exists for this school/subject/class.");
                        }
                        else
                        {
                            Section section = new Section()
                            {
                                Class = (from a in context.Classes where a.ClassId == classId select a).FirstOrDefault(),
                                SectionName = sectionName,
                                Description = description
                            };

                            context.Sections.Add(section);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        var mSection = (from a in context.Sections where a.SectionId == sectionId.Value select a).FirstOrDefault();
                        if (mSection == null)
                        {
                            throw new Exception("Section was not updated.  Section name not found.");
                        }
                        
                        if ((from a in context.Sections where a.SchoolId == schoolId && a.SubjectId == subjectId && a.ClassId == classId && a.SectionName == sectionName && a.SectionId != sectionId.Value select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Section was not created as it already exists for this school/subject/class.");
                        }

                        mSection.SectionName = sectionName;
                        mSection.Description = description;
                        context.Entry(mSection).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
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
                    var section = (from a in context.Sections where a.SectionId == sectionId select a).FirstOrDefault();
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
