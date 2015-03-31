using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class ThreadEntity
    {
        public static void CreateThread(string userId, string title, string secName, string className, 
            string subjName, int schoolId, string content, int? repliedTo)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var thread = new Thread()
                    {
                        Section = SectionEntity.GetSection(schoolId, subjName, className, secName),
                        RepliedTo = repliedTo,
                        DateTimePosted = DateTime.Now,
                        Content = content,
                        ThreadTitle = title,
                        User = UserEntity.GetUser(userId)
                    };

                    context.Threads.Add(thread);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateThread(int threadId, string content)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var thread = GetThread(threadId);
                    thread.Content = content;
                    context.Entry(thread).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Thread GetThread(int threadId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Threads where a.ThreadId == threadId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Thread> GetAllPostsByUser(string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Threads where a.UserId == userId select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Thread> GetAllOriginalPostsByUser(string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Threads where a.UserId == userId && a.RepliedTo == null select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Thread> GetAllPostsOfThread(int threadId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Threads where a.ThreadId == threadId || a.RepliedTo == threadId select a)
                        .OrderBy(t => t.DateTimePosted).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<usp_GetSectionThreads_Result> GetSectionPosts(string secName, string className, string subjName, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return context.usp_GetSectionThreads(secName, className, subjName, schoolId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Thread> GetNewPosts(string secName, string className, string subjName, int schoolId, DateTime lastLoggedOut)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Threads
                            where a.ClassName == className && a.SubjectName == subjName
                            && a.SectionName == secName && a.SchoolId == schoolId
                            && a.DateTimePosted > lastLoggedOut
                            select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteThread(int threadId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var thread = GetThread(threadId);

                    if (thread != null)
                    {
                        context.Threads.Remove(thread);
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
