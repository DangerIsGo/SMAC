using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class ClubEnrollmentEntity
    {
        public static void CreateEnrollment(string sId, string clubName, int schoolId, bool isLeader)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    ClubEnrollment enroll = new ClubEnrollment()
                    {
                        User = UserEntity.GetUser(sId),
                        Club = GetClub(clubName, schoolId),
                        IsLeader = isLeader
                    };

                    context.ClubEnrollments.Add(enroll);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteEnrollment(string userId, int clubId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var enroll = GetEnrollment(clubId, userId);
                    if (enroll != null)
                    {
                        context.ClubEnrollments.Remove(enroll);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ClubEnrollment GetEnrollment(int clubId, string sId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.ClubEnrollments where a.ClubId == clubId && a.UserId == sId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Student> GetStudentsInClub(string clubName, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    List<Student> students = new List<Student>();
                    return GetClub(clubName, schoolId).ClubEnrollments.Select(t => t.User.Student).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Club GetClub(string clubName, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Clubs where a.ClubName == clubName && a.SchoolId == schoolId select a).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Club> GetClubs(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return SchoolEntity.GetSchool(schoolId).Clubs.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
