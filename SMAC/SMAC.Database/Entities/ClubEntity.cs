using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class ClubEntity
    {
        public static void CreateClub(int schoolId, string clubName, string description)
        {
            CreateEditClub("ADD", schoolId, clubName, description, null);
        }

        public static void UpdateClub(int schoolId, string clubName, string description, string newClubName)
        {
            CreateEditClub("EDIT", schoolId, clubName, description, newClubName);
        }

        public static bool ClubExists(string clubName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Clubs where a.ClubName == clubName select a).FirstOrDefault() != null;
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

        public static List<Club> GetAllClubs(int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.Clubs where a.SchoolId == schoolId select a).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Club> GetMyClubs(int schoolId, string userId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    return (from a in context.ClubEnrollments where a.UserId == userId && a.SchoolId == schoolId select a.Club).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteClub(string clubName, int schoolId)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    var Club = GetClub(clubName, schoolId);
                    context.Clubs.Remove(Club);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditClub(string op, int schoolId, string clubName, string description, string newClubName)
        {
            try
            {
                using (SmacEntities context = new SmacEntities())
                {
                    if (op.Equals("ADD"))
                    {
                        if ((from a in context.Clubs where a.SchoolId == schoolId && a.ClubName == clubName select a).FirstOrDefault() != null)
                        {
                            throw new Exception("Club was not created.  Club name already exists for this school.");
                        }
                        else
                        {
                            Club Club = new Club()
                            {
                                School = (from a in context.Schools where a.SchoolId == schoolId select a).FirstOrDefault(),
                                Description = !string.IsNullOrWhiteSpace(description) ? description : null,
                                ClubName = clubName
                            };

                            context.Clubs.Add(Club);
                            context.SaveChanges();
                        }
                    }
                    else if (op.Equals("EDIT"))
                    {
                        if (!ClubExists(clubName))
                        {
                            throw new Exception("Club was not updated.  Club name not found.");
                        }
                        else if (ClubExists(newClubName))
                        {
                            throw new Exception("Club was not updated.  New club name already exists in database.");
                        }
                        else
                        {
                            var Club = GetClub(clubName, schoolId);

                            Club.ClubName = newClubName;
                            Club.Description = !string.IsNullOrWhiteSpace(description) ? description : null;
                            context.Entry(Club).State = System.Data.Entity.EntityState.Modified;
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
    }
}
