using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class LatestNewsEntity
    {
        public static List<LatestNews> GetLatestNews(int schoolId, int? count)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {

                    if (count.HasValue)
                        return (from a in context.LatestNewsSet where a.SchoolId == schoolId select a).Include(t=>t.User).OrderByDescending(a => a.PostedAt).ToList().Take(count.Value).ToList();
                    else
                        return (from a in context.LatestNewsSet where a.SchoolId == schoolId select a).Include(t => t.User).OrderByDescending(a => a.PostedAt).ToList().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static LatestNews GetNews(int id)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.LatestNewsSet where a.LatestNewsId == id select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteLatestNews(int newsId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var ln = (from a in context.LatestNewsSet where a.LatestNewsId == newsId select a).FirstOrDefault();

                    context.LatestNewsSet.Remove(ln);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateLatestNews(int newsId, string news, string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var ln = (from a in context.LatestNewsSet where a.LatestNewsId == newsId select a).FirstOrDefault();

                    ln.Content = news;
                    ln.PostedAt = DateTime.Now;
                    ln.User = (from a in context.Users where a.UserId == userId select a).FirstOrDefault();

                    context.Entry(ln).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateLatestNews(int schoolId, string news, string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    LatestNews ln = new LatestNews()
                    {
                        Content = news,
                        PostedAt = DateTime.Now,
                        School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault(),
                        User = (from a in context.Users where a.UserId == userId select a).FirstOrDefault()
                    };

                    context.LatestNewsSet.Add(ln);
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
