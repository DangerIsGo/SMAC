﻿using System;
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
        public string DeleteClub(string id)
        {
            try
            {
                ClubEntity.DeleteClub(int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateClub(string name, string description)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                ClubEntity.CreateClub(int.Parse(schoolId), name, description);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchClubList()
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var clubs = ClubEntity.GetAllClubs(int.Parse(schoolId));

                var rtnObj = new object[clubs.Count];

                for (int i = 0; i < clubs.Count; ++i)
                {
                    var obj = new
                    {
                        name = clubs[i].ClubName,
                        id = clubs[i].ClubId
                    };

                    rtnObj[i] = obj;
                }

                return JsonConvert.SerializeObject(rtnObj);
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchClub(string id)
        {
            try
            {
                var club = ClubEntity.GetClub(int.Parse(id));

                var obj = new
                {
                    name = club.ClubName,
                    desc = club.Description
                };

                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateClub(string id, string name, string description)
        {
            try
            {
                ClubEntity.UpdateClub(name, description, int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #region Subject

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteSubject(string id)
        {
            try
            {
                SubjectEntity.DeleteSubject(int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateSubject(string id, string name)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                SubjectEntity.UpdateSubject(int.Parse(schoolId), int.Parse(id), name);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateSubject(string name)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                SubjectEntity.CreateSubject(int.Parse(schoolId), name);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchSubject(string subjectId)
        {
            try
            {
                var subject = SubjectEntity.GetSubject(int.Parse(subjectId));

                var obj = new
                {
                    name = subject.SubjectName,
                    id = subject.SubjectId
                };

                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchSubjectList()
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var subjects = SubjectEntity.GetAllSubjects(int.Parse(schoolId));

                var rtnObj = new object[subjects.Count];

                for (int i = 0; i < subjects.Count; ++i)
                {
                    var obj = new
                    {
                        name = subjects[i].SubjectName,
                        id = subjects[i].SubjectId
                    };

                    rtnObj[i] = obj;
                }

                return JsonConvert.SerializeObject(rtnObj);
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        #endregion

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PostNewThreadReply(string threadId, string sectionId, string content)
        {
            try
            {
                var userId = Session["UserId"].ToString();

                ThreadEntity.CreatePost(userId, null, int.Parse(sectionId), content, int.Parse(threadId));

                return JsonConvert.SerializeObject("fgh");
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PopulateMarkingPeriods(string yearId)
        {
            try
            {
                var periods = MarkingPeriodEntity.GetMarkingPeriods(int.Parse(yearId));

                var rtnObj = new object[periods.Count];

                for (int i = 0; i < periods.Count; ++i)
                {
                    var obj = new
                    {
                        period = periods[i].FullYear ? "All Year" : periods[i].Period,
                        id = periods[i].MarkingPeriodId
                    };

                    rtnObj[i] = obj;
                }

                return JsonConvert.SerializeObject(rtnObj);
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string PopulateClassTable(string mp, string year)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();
                var userId = Session["UserId"].ToString();

                var enrollments = EnrollmentEntity.GetStudentEnrollments(userId, int.Parse(schoolId), int.Parse(mp));

                var rtnObj = new object[enrollments.Count];

                for (int i=0; i<enrollments.Count; ++i)
                {
                    var obj = new
                    {
                        sectionname = enrollments[i].Section.SectionName,
                        sectionid = enrollments[i].SectionId,
                        classname = enrollments[i].Section.Class.ClassName,
                        subjectname = enrollments[i].Section.Class.Subject.SubjectName
                    };

                    rtnObj[i] = obj;
                }

                return JsonConvert.SerializeObject(rtnObj);
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SendPrivateMessage(string toUserId, string content)
        {
            try
            {
                var userId = Session["UserId"].ToString();

                PrivateMessageEntity.SendPrivateMessage(toUserId, userId, content);

                return JsonConvert.SerializeObject(new { data = "success", date = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt") });
            }
            catch
            {
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FillStudentGradeTable(string markingPeriodId)
        {
            var schoolId = Session["SchoolId"].ToString();
            var userId = Session["UserId"].ToString();

            var enrollments = EnrollmentEntity.GetStudentEnrollments(userId, int.Parse(schoolId), int.Parse(markingPeriodId));

            var rtnObj = new object[enrollments.Count];

            for (int i = 0; i < enrollments.Count; ++i)
            {
                var obj = new
                {
                    classname = enrollments[i].Section.Class.ClassName,
                    section = enrollments[i].Section.SectionName,
                    grade = enrollments[i].GradeValue
                };

                rtnObj[i] = obj;
            }

            return JsonConvert.SerializeObject(rtnObj);
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
                return JsonConvert.SerializeObject("An internal error has occurred.  Please try again later or contact an administrator.");
            }
        }
    }
}
