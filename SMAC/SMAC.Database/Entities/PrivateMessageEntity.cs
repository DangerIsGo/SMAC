using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace SMAC.Database
{
    public class PrivateMessageEntity
    {
        public static void SendPrivateMessage(string toUserId, string fromUserId, string content)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    PrivateMessage pm = new PrivateMessage()
                    {
                        Content = content,
                        DateSent = DateTime.Now,
                        DateRead = null,
                        UserSentFrom = UserEntity.GetUser(fromUserId, context),
                        UserSentTo = UserEntity.GetUser(toUserId, context),
                        ToUser = toUserId,
                        FromUser = fromUserId
                    };

                    context.PrivateMessages.Add(pm);
                    context.SaveChanges();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.PrivateMessages where a.PrivateMessageId == id select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void MarkAllConvosAsRead(string userId1, string userId2)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var msgs = (from a in context.PrivateMessages
                                where (a.FromUser == userId1 && a.ToUser == userId2) || (a.FromUser == userId2 && a.ToUser == userId1)
                                select a).Include(a => a.UserSentFrom).Include(a => a.UserSentTo).ToList();

                    foreach (var pm in msgs)
                    {
                        if (pm.DateRead != null)
                        {
                            pm.DateRead = DateTime.Now;
                            context.Entry(pm).State = EntityState.Modified;
                        }
                    }

                    context.SaveChanges();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    var pm = GetPrivateMessage(id);

                    if (pm != null)
                    {
                        context.PrivateMessages.Remove(pm);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<usp_GetLatestPrivateMessages_Result> GetLatestPrivateMessages(string userId, int pageIndex, int pageCount)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var msgs = context.usp_GetLatestPrivateMessages(userId).ToList();

                    return msgs;
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.PrivateMessages
                            where (a.FromUser == userId1 && a.ToUser == userId2) || (a.FromUser == userId2 && a.ToUser == userId1)
                            select a).Include(a => a.UserSentFrom).Include(a => a.UserSentTo).ToList();
                }
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
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.PrivateMessages where a.ToUser == userId && a.DateRead == null select a).Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
