using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class ClubEnrollmentEntity
    {
        private static SmacEntities context;

        public static void CreateEnrollment(string sId, string clubName, int schoolId, bool isLeader)
        {
            try
            {
                context = new SmacEntities();

                ClubEnrollment enroll = new ClubEnrollment()
                {
                    User = UserEntity.GetUser(sId),
                    Club = GetClub(clubName, schoolId),
                    IsLeader = isLeader
                };

                context.ClubEnrollments.Add(enroll);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteEnrollment(string sId, string clubName, int schoolId)
        {
            try
            {
                context = new SmacEntities();
                var enroll = GetEnrollment(clubName, schoolId, sId);
                if (enroll != null)
                {
                    context.ClubEnrollments.Remove(enroll);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ClubEnrollment GetEnrollment(string clubName, int schoolId, string sId)
        {
            try
            {
                context = new SmacEntities();
                return (from a in context.ClubEnrollments where a.SchoolId == schoolId && a.ClubName == clubName && a.UserId == sId select a).FirstOrDefault();
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
                context = new SmacEntities();
                List<Student> students = new List<Student>();
                return GetClub(clubName, schoolId).ClubEnrollments.Select(t => t.User.Student).ToList();

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
                context = new SmacEntities();
                return (from a in context.Clubs where a.ClubName == clubName && a.SchoolId == schoolId select a).FirstOrDefault();
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
                context = new SmacEntities();
                return SchoolEntity.GetSchool(schoolId).Clubs.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
