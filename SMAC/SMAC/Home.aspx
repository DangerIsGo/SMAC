<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SMAC.Home" %>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {

            $('.loading-image').show();
            LoadSchoolInfo();
        });

        var schoolIdx = 1;
        var schoolIds = new Array();

        function LoadSchoolInfo(school) {
            var userid = '<%= Session["UserId"] %>'
            if (school === undefined) school = -1;
            $.ajax({
                type: "POST",
                url: "Services.asmx/GetSchoolInfo",
                data: "{'userId': '" + userid + "', 'schoolId': '" + school + "'}",
                contentType: "application/json; charset=UTF-8",
                dataType: "json",
                success: function (data) {
                    //Populate school info in block
                    var sch = JSON.parse(data.d);

                    PopulateSchoolInfo(sch);
                    LoadNewsInfo(sch.schoolid);
                },
                complete: function () {
                    var block = $('#schoolBlock');
                    block.removeClass('loading');
                    $(block.find('.loading-image')).hide();
                }
            });
        }

        function GetNextSchool() {
            schoolIdx += 1;
            LoadSchoolInfo(schoolIds[schoolIdx-1]);
        }

        function GetPrevSchool() {
            schoolIdx -= 1;
            LoadSchoolInfo(schoolIds[schoolIdx-1]);
        }

        function PopulateSchoolInfo(schoolInfo) {

            $('#schoolBlock').empty();

            if (schoolInfo.schoolids != undefined) {
                var ids = schoolInfo.schoolids.split(':');
                $.each(ids, function (i, el) {
                    schoolIds.push(el);
                });
            }

            var cont = $('<div>');
            var head = $('<div>').addClass('blockHeader').text('School Info');

            var cols = $('<div>').addClass('blockCols');

            var labels = $('<div>').addClass('blockColLbls');
            var values = $('<div>').addClass('blockColVals');

            var name = $('<div>').addClass('blockRow').text('School Name:');
            var addr = $('<div>').addClass('blockRow').text('Street Address:');
            var city = $('<div>').addClass('blockRow').text('City:');
            var state = $('<div>').addClass('blockRow').text('State:');
            var zip = $('<div>').addClass('blockRow').text('Zip Code:');
            var phone = $('<div>').addClass('blockRow').text('Phone Number:');
            var multiple = $('<div>').addClass('blockRow').addClass('multSch');

            var left = $('<span>').addClass('arrow');
            var middle = $('<span>').text('Viewing ' + schoolIdx + ' of ' + schoolIds.length);
            var right = $('<span>').addClass('arrow').addClass('rightArrow');


            var leftI = $('<i>').addClass('fa').addClass('fa-arrow-circle-left');
            var rightI = $('<i>').addClass('fa').addClass('fa-arrow-circle-right');

            if (schoolIdx == 1 && schoolIds.length > 1) {
                rightI.addClass('active');
                rightI.on('click', GetNextSchool);
            }
            else if (schoolIdx < schoolIds.length && schoolIds.length > 1)
            {
                rightI.addClass('active');
                rightI.on('click', GetNextSchool);
                leftI.addClass('active');
                leftI.on('click', GetPrevSchool);
            }
            else if (schoolIdx == schoolIds.length)
            {
                leftI.addClass('active');
                leftI.on('click', GetPrevSchool);
            }

            left.append(leftI);
            right.append(rightI);

            multiple.append(left);
            multiple.append(middle);
            multiple.append(right);

            labels.append(name);
            labels.append(addr);
            labels.append(city);
            labels.append(state);
            labels.append(zip);
            labels.append(phone);

            name = $('<div>').addClass('blockRow').text(schoolInfo.schoolname);
            addr = $('<div>').addClass('blockRow').text(schoolInfo.schooladdr);
            city = $('<div>').addClass('blockRow').text(schoolInfo.schoolcity);
            state = $('<div>').addClass('blockRow').text(schoolInfo.schoolstate);
            zip = $('<div>').addClass('blockRow').text(schoolInfo.schoolzip);
            phone = $('<div>').addClass('blockRow').text(schoolInfo.schoolphone);

            values.append(name);
            values.append(addr);
            values.append(city);
            values.append(state);
            values.append(zip);
            values.append(phone);

            cols.append(labels);
            cols.append(values);

            head.appendTo(cont);
            cols.appendTo(cont);
            multiple.appendTo(cont);

            $('#schoolBlock').append(cont);
        }

        function LoadNewsInfo(school) {

            $.ajax({
                type: "POST",
                url: "Services.asmx/GetNewsInfo",
                data: "{'schoolId': '" + school + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    //Populate news info in block
                    var news = JSON.parse(data.d);

                    PopulateLatestNewsInfo(news);
                },
                complete: function () {
                    var block = $('#newsBlock');
                    block.removeClass('loading');
                    $(block.find('.loading-image')).hide();
                }
            });
        }

        function PopulateLatestNewsInfo(latestNews) {
            $('#newsBlock').empty();

            var cont = $('<div>');
            var head = $('<div>').addClass('blockHeader').text('Latest News');

            cont.append(head);

            var listCont = $('<div>').addClass('listCont');

            var list = $('<ul>');

            $.each(latestNews, function (i, el) {
                var item = $('<li>').addClass('newsItem');
                var content = $('<div>').text(el.content);
                var posted = $('<div>').text('Posted by ' + el.poster + ' at ' + el.date);
                item.append(content);
                item.append(posted);

                item.appendTo(list);
            });

            listCont.append(list);

            listCont.appendTo(cont);

            $('#newsBlock').append(cont);
        }
    </script>
</asp:Content>


<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="blockRow">
        <div class="block loading" id="schoolBlock">
            <img class="loading-image" src="Images/ajax-loader.gif" alt="loading.." />
        </div>
        <div class="block loading" id="newsBlock">
            <img class="loading-image"" src="Images/ajax-loader.gif" alt="loading.." />
        </div>
    </div>
    <div class="blockRow" >
        <div class="block" id="resourceBlock">
            <div class="blockHeader">Learning Resources</div>
            <div class="listCont">
                <ul>
                    <li>
                        <a href="https://www.khanacademy.org">Khan Academy</a>
                    </li>
                    <li>
                        <a href="http://www.coursera.org">Coursera</a>
                    </li>
                    <li>
                        <a href="http://www.alison.com">ALISON</a>
                    </li>
                    <li>
                        <a href="https://www.edx.org">edX</a>
                    </li>
                    <li>
                        <a href="http://ocw.mit.edu">MIT Open Courseware</a>
                    </li>
                    <li>
                        <a href="http://oedb.org/open">OEDB</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="block" id="khanBlock">
            <div class="blockHeader">Khan Academy</div>
            <div class="khanSearch">
                <span>Search Khan Academy: <input type="text" id="khanQuery" /><input type="button" value="Search" /></span>
            </div>
        </div>
    </div>
</asp:Content>
