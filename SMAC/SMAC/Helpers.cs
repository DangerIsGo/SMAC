using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SMAC
{
    public class Helpers
    {
        public static string NormalizeTimespanString(string timespans)
        {
            var splitString = timespans.Split(',');
            var sb = new StringBuilder();
            var delim = " to ";

            //TimeSpan,TimeSpan,...

            for (int i = 0; i < splitString.Length; ++i)
            {
                var splitTS = splitString[i].Split(new string[] { delim }, StringSplitOptions.None);

                if (splitTS.Length != 2)
                    return string.Empty;
                //0 = Start
                //1 = End

                for (int j = 0; j < splitTS.Length; j++)
                {
                    var splitTime = splitTS[j].Split(':');

                    //0 = Hours
                    //1 = Minutes
                    //2 = Seconds
                    var IsAfternoon = false;

                    if (int.Parse(splitTime[0]) >= 12)
                    {
                        IsAfternoon = true;
                        if (int.Parse(splitTime[0]) >= 13)
                        {
                            splitTime[0] = (int.Parse(splitTime[0]) - 12).ToString();
                        }
                    }

                    if (j == 0)
                        sb.Append(splitTime[0] + ":" + splitTime[1] + " " + (IsAfternoon ? "PM" : "AM") + delim);
                    else
                        sb.Append(splitTime[0] + ":" + splitTime[1] + " " + (IsAfternoon ? "PM" : "AM"));
                }

                if (i < splitString.Length - 1)
                    sb.Append(',');
            }
            return sb.ToString();
        }
    }

    public class DayOfWeekComparer : IComparer<usp_GetClubSchedule_Result>
    {
        public DayOfWeekComparer() { }

        int IComparer<usp_GetClubSchedule_Result>.Compare(usp_GetClubSchedule_Result x, usp_GetClubSchedule_Result y)
        {
            if (DayLookup(x.Day) < DayLookup(y.Day))
                return -1;
            else
                return 1;
        }

        private int DayLookup(string day)
        {
            switch (day)
            {
                case "Sunday":
                    return 0;
                case "Monday":
                    return 1;
                case "Tuesday":
                    return 2;
                case "Wednesday":
                    return 3;
                case "Thursday":
                    return 4;
                case "Friday":
                    return 5;
                case "Saturday":
                    return 6;
                default:
                    return 0;
            }
        }
    }
}