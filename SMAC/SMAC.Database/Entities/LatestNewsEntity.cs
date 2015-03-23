using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class LatestNewsEntity
    {
        private static SmacEntities context;

        public static List<LatestNews> GetLatestNews(int schoolId, int? count)
        {
            try
            {
                context = new SmacEntities();

                if (count.HasValue)
                    return (from a in context.LatestNewsSet where a.SchoolId == schoolId select a).OrderByDescending(a => a.PostedAt).ToList().Take(count.Value).ToList();
                else
                    return (from a in context.LatestNewsSet where a.SchoolId == schoolId select a).OrderByDescending(a => a.PostedAt).ToList().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
