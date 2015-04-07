using Newtonsoft.Json;
using SMAC.Database;
using System;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;

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
        public string CreateMarkingPeriod(string yearId, string name, string allyear, string start, string end)
        {
            try
            {
                MarkingPeriodEntity.CreateMarkingPeriod(name, bool.Parse(allyear), int.Parse(yearId), DateTime.Parse(start), DateTime.Parse(end));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #region Class

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchClass(string id)
        {
            try
            {
                var mClass = ClassEntity.GetClass(int.Parse(id));

                var obj = new
                {
                    name = mClass.ClassName,
                    desc = mClass.Description,
                    subjId = mClass.SubjectId
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
        public string FetchClassList(string subjectId)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var classes = ClassEntity.GetClassesForSubject(int.Parse(subjectId));

                var rtnObj = new object[classes.Count];

                for (int i = 0; i < classes.Count; ++i)
                {
                    var obj = new
                    {
                        id = classes[i].ClassId,
                        name = classes[i].ClassName                      
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
        public string CreateClass(string subjectId, string name, string description)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                ClassEntity.CreateClass(int.Parse(schoolId), int.Parse(subjectId), name, description);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateClass(string id, string subjectId, string name, string description)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                ClassEntity.UpdateClass(int.Parse(schoolId), int.Parse(subjectId), name, description, int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteClass(string id)
        {
            try
            {
                ClassEntity.DeleteClass(int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #endregion

        #region User

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteUser(string id)
        {
            try
            {
                UserEntity.DeleteUser(id);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchUser(string id)
        {
            try
            {
                var user = UserEntity.GetUser(id);

                var obj = new
                {
                    id = user.UserId,
                    start = user.StartDate.ToShortDateString(),
                    end = user.EndDate.HasValue ? user.EndDate.Value.ToShortDateString() : string.Empty,
                    first = user.FirstName,
                    middle = user.MiddleName,
                    last = user.LastName,
                    email = user.EmailAddress,
                    phone = user.PhoneNumber,
                    gender = user.GenderType.ToLower(),
                    active = user.IsActive.ToString(),
                    username = user.UserCredential.UserName,
                    role = user.Admin != null ? "admin" : user.Staff != null ? "staff" : user.Student != null ? "student" : user.Teacher != null ? "teacher" : ""
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
        public string FetchUserList()
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var users = UserEntity.GetAllUsersInSchool(int.Parse(schoolId), string.Empty);

                var rtnObj = new object[users.Count];

                for (int i = 0; i < users.Count; ++i)
                {
                    var obj = new
                    {
                        id = users[i].UserId,
                        fName = users[i].FirstName,
                        lName = users[i].LastName
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
        public string UpdateUser(string id, string newId, string fn, string mn, string ln, string em, string pn, string gender, string start, string end, string act, string un, string pw, string role)
        {
            try
            {
                DateTime endTemp = DateTime.Now;

                if (DateTime.TryParse(end, out endTemp))
                {
                    UserEntity.UpdateUser(id, newId, fn, mn, ln, em, pn, gender, bool.Parse(act), un, pw, DateTime.Parse(start), endTemp, role);
                }
                else
                {
                    UserEntity.UpdateUser(id, newId, fn, mn, ln, em, pn, gender, bool.Parse(act), un, pw, DateTime.Parse(start), null, role);
                }

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string CreateUser(string id, string fn, string mn, string ln, string em, string pn, string gender, string start, string end, string act, string un, string pw, string role)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                DateTime endTemp = DateTime.Now;

                if (DateTime.TryParse(end, out endTemp)) 
                {
                    UserEntity.CreateUser(id, fn, mn, ln, em, pn, gender, bool.Parse(act), un, pw, DateTime.Parse(start), endTemp, role, int.Parse(schoolId));
                }
                else
                {
                    UserEntity.CreateUser(id, fn, mn, ln, em, pn, gender, bool.Parse(act), un, pw, DateTime.Parse(start), null, role, int.Parse(schoolId));
                }

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #endregion

        #region Latest News

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteLatestNews(string id)
        {
            try
            {
                LatestNewsEntity.DeleteLatestNews(int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateLatestNews(string id, string content)
        {
            try
            {
                var userId = Session["UserId"].ToString();

                LatestNewsEntity.UpdateLatestNews(int.Parse(id), content, userId);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchLatestNews(string id)
        {
            try
            {
                var news = LatestNewsEntity.GetNews(int.Parse(id));

                var obj = new
                {
                    id = news.LatestNewsId,
                    content = news.Content
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
        public string FetchLatestNewsList()
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var news = LatestNewsEntity.GetLatestNews(int.Parse(schoolId), null);

                var rtnObj = new object[news.Count];

                for (int i = 0; i < news.Count; ++i)
                {
                    var obj = new
                    {
                        id = news[i].LatestNewsId,
                        content = news[i].Content,
                        date = news[i].PostedAt.ToString("m/d/yyyy h:mm:ss tt")
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
        public string CreateLatestNews(string content)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();
                var userId = Session["UserId"].ToString();

                LatestNewsEntity.CreateLatestNews(int.Parse(schoolId), content, userId);

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #endregion

        #region Time Slots

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteTimeSlot(string id)
        {
            try
            {
                TimeSlotEntity.DeleteTimeSlot(int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject("School Year is currently in use and cannot be deleted.");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateTimeSlot(string id, string start, string end)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                TimeSlotEntity.UpdateTimeSlot(int.Parse(schoolId), TimeSpan.Parse(start), TimeSpan.Parse(end), int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchTimeSlot(string id)
        {
            try
            {
                var timeSlot = TimeSlotEntity.GetTimeSlot(int.Parse(id));

                var obj = new
                {
                    id = timeSlot.TimeSlotId,
                    start = DateTime.Today.Add(timeSlot.StartTime).ToString("HH:mm"),
                    end = DateTime.Today.Add(timeSlot.EndTime).ToString("HH:mm")
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
        public string FetchTimeSlotList()
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var timeSlots = TimeSlotEntity.GetTimeSlots(int.Parse(schoolId));

                var rtnObj = new object[timeSlots.Count];

                for (int i = 0; i < timeSlots.Count; ++i)
                {
                    var obj = new
                    {
                        id = timeSlots[i].TimeSlotId,
                        start = DateTime.Today.Add(timeSlots[i].StartTime).ToString("HH:mm"),
                        end = DateTime.Today.Add(timeSlots[i].EndTime).ToString("HH:mm")
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
        public string CreateTimeSlot(string start, string end)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                TimeSlotEntity.CreateTimeSlot(int.Parse(schoolId), TimeSpan.Parse(start), TimeSpan.Parse(end));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #endregion

        #region School Year

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string DeleteSchoolYear(string id)
        {
            try
            {
                SchoolYearEntity.DeleteSchoolYear(int.Parse(id));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject("School Year is currently in use and cannot be deleted.");
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string UpdateSchoolYear(string id, string name, string start, string end)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                SchoolYearEntity.UpdateSchoolYear(int.Parse(schoolId), int.Parse(id), name, DateTime.Parse(start), DateTime.Parse(end));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string FetchSchoolYear(string id)
        {
            try
            {
                var year = SchoolYearEntity.GetSchoolYear(int.Parse(id));

                var obj = new
                {
                    name = year.Year,
                    id = year.SchoolYearId,
                    start = year.StartDate.ToShortDateString(),
                    end = year.EndDate.ToShortDateString()
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
        public string FetchSchoolYearList()
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                var years = SchoolYearEntity.GetSchoolYears(int.Parse(schoolId));

                var rtnObj = new object[years.Count];

                for (int i = 0; i < years.Count; ++i)
                {
                    var obj = new
                    {
                        name = years[i].Year,
                        id = years[i].SchoolYearId
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
        public string CreateSchoolYear(string name, string start, string end)
        {
            try
            {
                var schoolId = Session["SchoolId"].ToString();

                SchoolYearEntity.CreateSchoolYear(int.Parse(schoolId), name, DateTime.Parse(start), DateTime.Parse(end));

                return JsonConvert.SerializeObject("success");
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(ex.Message);
            }
        }

        #endregion

        #region Club

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

        #endregion

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
                UserEntity.UpdateUser(userId, null, fName, mName, lName, email, phone, gender, null, uName, nPass, null, null, null);

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
