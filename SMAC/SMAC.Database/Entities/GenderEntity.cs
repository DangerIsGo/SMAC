using System.Collections.Generic;
using System.Linq;

namespace SMAC.Database
{
    public class GenderEntity
    {
        private static SmacEntities context;

        public static void CreateGender(string gType)
        {
            context = new SmacEntities();

            Gender gender = new Gender()
            {
                GenderType = gType
            };

            context.Genders.Add(gender);
            context.SaveChanges();
        }

        public static Gender GetGender(string gType)
        {
            context = new SmacEntities();

            return (from a in context.Genders where a.GenderType == gType select a).FirstOrDefault();
        }

        public static List<Gender> GetGenders()
        {
            context = new SmacEntities();

            return (from a in context.Genders select a).ToList();
        }

        public static void UpdateGender(string gType, string newValue)
        {
            context = new SmacEntities();

            Gender gender = (from a in context.Genders where a.GenderType == gType select a).FirstOrDefault();

            if (gender != null)
            {
                gender.GenderType = newValue;
            }

            context.SaveChanges();
        }
    }
}
