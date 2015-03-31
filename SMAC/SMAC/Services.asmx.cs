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
using System.Web.Security;

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
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            catch (Exception)
            {
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SendPrivateMessage(string userId, string msgId, string content)
        {
            try
            {
                var pm = PrivateMessageEntity.GetPrivateMessage(int.Parse(msgId));

                PrivateMessageEntity.SendPrivateMessage(pm.ToUser == userId ? pm.FromUser : pm.ToUser, userId, content);

                return JsonConvert.SerializeObject(new { data = "success", date = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt") });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FillStudentGradeTable(string year, string markingPeriod)
        {
            int tmpSchId = -1;
            int tmpMpId = -1;

            var schoolId = Session["SchoolId"].ToString();
            var userId = Session["UserId"].ToString();

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
                Session["UpdateCookie"] = "yes";

                return JsonConvert.SerializeObject("Success!  All data was updated.");
            }
            catch(Exception)
            {
                return JsonConvert.SerializeObject("An error has occurred.  Please try again later or contact an administrator.");
            }
        }
    }
}
