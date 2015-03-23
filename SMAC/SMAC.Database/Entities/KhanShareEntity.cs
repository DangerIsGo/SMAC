using System;
using System.Linq;

namespace SMAC.Database
{
    public class KhanShareEntity
    {
        private static SmacEntities context;

        public static KhanShare CreateKhanShare(string title, string url, string apiId)
        {
            try
            {
                context = new SmacEntities();

                var ks = GetKhanShare(title, url, apiId);

                if (ks != null)
                {
                    KhanShare newKs = new KhanShare()
                    {
                        ApiId = apiId,
                        Url = url,
                        Title = title
                    };

                    context.KhanShares.Add(newKs);
                    context.SaveChanges();                    
                }

                return GetKhanShare(title, url, apiId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static KhanShare GetKhanShare(string title, string url, string apiId)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.KhanShares where a.ApiId == apiId && a.Url == url && a.Title == title select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static KhanShare GetKhanShare(int id)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.KhanShares where a.KhanShareId == id select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteKhanShare(int id)
        {
            try
            {
                context = new SmacEntities();

                var ks = GetKhanShare(id);

                if (ks != null)
                {
                    context.KhanShares.Remove(ks);
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
