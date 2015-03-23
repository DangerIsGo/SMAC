using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using SMAC.Database;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Text;

namespace SMAC
{
    /// <summary>
    /// Summary description for Services
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class Services : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void LogOut()
        {
            try
            {
                Session["UserId"] = null;
                Session["FirstName"] = null;
                Session["MiddleName"] = null;
                Session["LastName"] = null;
                Session["PhoneNumber"] = null;
                Session["Email"] = null;
                Session["UserName"] = null;
                Session["Gender"] = null;
                Session["Genders"] = null;
            }
            catch (Exception)
            {
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSchoolsYears(string schoolId)
        {
            try
            {
                int tmpSchId = -1;

                if (int.TryParse(schoolId, out tmpSchId))
                {
                    var sch = SchoolYearEntity.GetSchoolYears(tmpSchId);

                    var rtnObj = new object[sch.Count];

                    for (int i = 0; i < sch.Count; ++i )
                    {
                        var obj = new
                        {
                            year = sch[i].Year
                        };

                        rtnObj[i] = obj;
                    }

                    return JsonConvert.SerializeObject(rtnObj);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSchoolsMPs(string schoolId)
        {
            try
            {
                int tmpSchId = -1;

                if (int.TryParse(schoolId, out tmpSchId))
                {
                    var sch = MarkingPeriodEntity.GetMarkingPeriods(tmpSchId);

                    var rtnObj = new object[sch.Count];

                    for (int i = 0; i < sch.Count; ++i)
                    {
                        var obj = new
                        {
                            mp = sch[i].FullYear ? "All Year" : "Period: " + sch[i].Period,
                            fy = sch[i].FullYear ? "true" : "false",
                            val = sch[i].Period
                        };

                        rtnObj[i] = obj;
                    }

                    return JsonConvert.SerializeObject(rtnObj);
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FillStudentGradeTable(string userId, string schoolId, string year, string markingPeriod)
        {
            int tmpSchId = -1;
            int tmpMpId = -1;


            if (int.TryParse(schoolId, out tmpSchId) && ((markingPeriod != "fy" && int.TryParse(markingPeriod, out tmpMpId)) || (markingPeriod == "fy")))
            {
                var fullYear = MarkingPeriodEntity.GetMarkingPeriod(tmpSchId, null, true);
                var enrollments = EnrollmentEntity.GetStudentEnrollments(userId, tmpSchId, markingPeriod == "fy" ? fullYear.MarkingPeriodId : tmpMpId, year);

                var rtnObj = new object[enrollments.Count];

                for (int i = 0; i < enrollments.Count; ++i)
                {
                    var obj = new
                    {
                        classname = enrollments[i].ClassName,
                        section = enrollments[i].SectionName,
                        grade = enrollments[i].GradeValue
                    };

                    rtnObj[i] = obj;
                }

                return JsonConvert.SerializeObject(rtnObj);
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetSchoolInfo(string userId, string schoolId)
        {
            int tmpSchId = -1;

            if (int.TryParse(schoolId, out tmpSchId))
            {
                if (tmpSchId > 0)
                {
                    var sch = SchoolEntity.GetSchool(tmpSchId);

                    var rtnSch = new
                    {
                        schoolid = sch.SchoolId,
                        schoolname = sch.SchoolName,
                        schooladdr = sch.StreetAddress,
                        schoolcity = sch.City,
                        schoolstate = sch.State,
                        schoolzip = sch.ZipCode,
                        schoolphone = sch.PhoneNumber
                    };

                    return JsonConvert.SerializeObject(rtnSch);
                }
            }

            // This is a one time event on initial page load to get all 
            var schools = SchoolEntity.GetUsersSchools(userId);
            

            if (schools.Count > 0)
            {
                var sb = new StringBuilder();
                schools.ForEach(m => sb.Append(m.SchoolId.ToString() + ":"));
                sb.Remove(sb.Length - 1, 1);

                var rtnObj = new
                {
                    schoolid = schools.First().SchoolId,
                    schoolname = schools.First().SchoolName,
                    schooladdr = schools.First().StreetAddress,
                    schoolcity = schools.First().City,
                    schoolstate = schools.First().State,
                    schoolzip = schools.First().ZipCode,
                    schoolphone = schools.First().PhoneNumber,
                    schoolcount = schools.Count,
                    schoolids = sb.ToString()
                };
                return JsonConvert.SerializeObject(rtnObj);
            }

            return null;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateUserInfo(string userId, string fName, string mName, string lName, string email, string phone, string gender, string uName, string cPass, string nPass)
        {
            try
            {
                // Current pass entered, update credentials
                if (!string.IsNullOrWhiteSpace(cPass))
                {
                    var cred = UserCredentialEntity.GetUserCred(userId);
                    if (cred.Password != UserCredentialEntity.GetSHA256Hash(cPass))
                    {
                        return JsonConvert.SerializeObject("Current password entered is invalid.  No data was updated.");
                    }
                }

                UserCredentialEntity.UpdateUserCred(userId, uName, !string.IsNullOrWhiteSpace(nPass) ? nPass : null);
                UserEntity.UpdateUser(userId, fName, mName, lName, email, phone, gender, null, null, null);

                Session["UserId"] = userId;
                Session["FirstName"] = fName;
                Session["MiddleName"] = mName;
                Session["LastName"] = lName;
                Session["PhoneNumber"] = phone;
                Session["Email"] = email;
                Session["UserName"] = uName;
                Session["Gender"] = gender;

                return JsonConvert.SerializeObject("Success!  All data was updated.");
            }
            catch(Exception)
            {
                return JsonConvert.SerializeObject("An error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetNewsInfo(string schoolId)
        {
            int tmpSchId = -1;

            if (int.TryParse(schoolId, out tmpSchId))
            {
                if (tmpSchId > 0)
                {
                    var news = LatestNewsEntity.GetLatestNews(tmpSchId, 10);

                    var newsObj = new object[news.Count];

                    for (int i=0; i< news.Count; ++i)
                    {
                        var obj = new 
                        {
                            content = news[i].Content,
                            date = news[i].PostedAt.ToString("M/d/yyyy h:mm tt"),
                            poster = news[i].User.FirstName + " " + news[i].User.LastName
                        };

                        newsObj[i] = obj;
                    }

                    return JsonConvert.SerializeObject(newsObj);
                }
            }
            return string.Empty;
        }
    }
}
