using System;
using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class ClubEntity
    {
        private static SmacEntities context;

        public static void CreateClub(int schoolId, string clubName)
        {
            CreateEditClub("ADD", schoolId, clubName, null);
        }

        public static void UpdateClub(int schoolId, string clubName, string newClubName)
        {
            CreateEditClub("EDIT", schoolId, clubName, newClubName);
        }

        public static bool ClubExists(string clubName)
        {
            try
            {
                context = new SmacEntities();
                return (from a in context.Clubs where a.ClubName == clubName select a).FirstOrDefault() != null;
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

        public static List<Club> GetAllClubs(int schoolId)
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

        public static void DeleteClub(string clubName, int schoolId)
        {
            try
            {
                context = new SmacEntities();

                var Club = GetClub(clubName, schoolId);
                context.Clubs.Remove(Club);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void CreateEditClub(string op, int schoolId, string clubName, string newClubName)
        {
            try
            {
                context = new SmacEntities();

                if (op.Equals("ADD"))
                {
                    if (ClubExists(clubName))
                    {
                        throw new Exception("Club was not created.  Club name already exists for this school.");
                    }
                    else
                    {
                        Club Club = new Club()
                        {
                            School = SchoolEntity.GetSchool(schoolId),
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
