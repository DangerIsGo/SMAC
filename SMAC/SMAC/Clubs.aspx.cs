﻿using Newtonsoft.Json;
using SMAC.Database;
using System;
using System.Collections.Generic;
using System.Data;

namespace SMAC
{
    public partial class Clubs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var schoolId = int.Parse(Request.Cookies["SmacCookie"]["SchoolId"]);
            var userId = Request.Cookies["SmacCookie"]["UserId"];

            if (!IsPostBack)
            {
                var clubs = ClubEntity.GetAllClubs(schoolId);
                var myClubs = ClubEntity.GetMyClubs(schoolId, userId);
                if (myClubs.Count > 0)
                {
                    string myClubList = string.Empty;

                    foreach (var club in myClubs)
                    {
                        myClubList += club.ClubId + ";";
                    }

                    this.myClubNames.Value = myClubList.Substring(0, myClubList.LastIndexOf(";"));
                }

                var clubObj = new object[clubs.Count];

                for (int i = 0; i < clubs.Count; ++i)
                {
                    var scheds = ClubScheduleEntity.GetClubSchedule(clubs[i].ClubId);

                    scheds.Sort(new DayOfWeekComparer());

                    var schdObj = new object[scheds.Count];

                    for (int j = 0; j < scheds.Count; ++j)
                    {
                        var scheduleObj = new
                        {
                            day = scheds[j].Day,
                            times = Helpers.NormalizeTimespanString(scheds[j].TimeSpans)
                        };

                        schdObj[j] = scheduleObj;
                    }

                    var Cobj = new
                    {
                        id = clubs[i].ClubId,
                        name = clubs[i].ClubName,
                        desc = clubs[i].Description,
                        schedule = schdObj
                    };

                    clubObj[i] = Cobj;
                }

                this.clubScheduleHdn.Value = JsonConvert.SerializeObject(clubObj);

                var list = new List<string> { "ClubId", "ClubName", "ClubDesc" };

                DataTable table = new DataTable();

                foreach (var item in list)
                    table.Columns.Add(item, typeof(string));

                //Now add some rows(which will be repeated in the ItemTemplate)
                foreach (var club in clubs)
                {
                    table.Rows.Add(club.ClubId.ToString(), club.ClubName, club.Description);
                }

                clubListView.DataSource = table;
                clubListView.DataBind();
            }
        }
    }
}