using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMAC.Database
{
    public class PrivateMessageEntity
    {
        private static SmacEntities context;

        public static void SendPrivateMessage(string toUserId, string fromUserId, string content)
        {
            try
            {
                context = new SmacEntities();

                PrivateMessage pm = new PrivateMessage()
                {
                    Content = content,
                    DateSent = DateTime.Now,
                    DateRead = null,
                    UserSentFrom = UserEntity.GetUser(fromUserId),
                    UserSentTo = UserEntity.GetUser(toUserId)
                };

                context.PrivateMessages.Add(pm);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static PrivateMessage GetPrivateMessage(int id)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.PrivateMessages where a.PrivateMessageId == id select a).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeletePrivateMessage(int id)
        {
            try
            {
                context = new SmacEntities();

                var pm = GetPrivateMessage(id);

                if (pm != null)
                {
                    context.PrivateMessages.Remove(pm);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PrivateMessage> GetLatestPrivateMessages(string userId, int pageIndex, int pageCount)
        {
            try
            {
                context = new SmacEntities();

                var toList = (from a in context.PrivateMessages where a.ToUser == userId select a)
                    .GroupBy(t=>t.FromUser)
                    .Select(t=>t.Last()).ToList();
                var fromList = (from a in context.PrivateMessages where a.FromUser == userId select a)
                    .GroupBy(t => t.ToUser)
                    .Select(t => t.Last()).ToList();
                return toList.Union(fromList).OrderByDescending(t => t.DateSent).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PrivateMessage> GetPrivateMessageThread(string userId1, string userId2)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.PrivateMessages 
                        where (a.FromUser == userId1 && a.ToUser == userId2) || (a.FromUser == userId2 && a.ToUser == userId1) 
                        select a).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int CheckForNewMessages(string userId)
        {
            try
            {
                context = new SmacEntities();

                return (from a in context.PrivateMessages where a.ToUser == userId && a.DateRead == null select a).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
