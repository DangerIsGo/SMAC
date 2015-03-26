<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clubs.aspx.cs" Inherits="SMAC.Clubs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var localStore = undefined;
        var selectedClub = undefined;

        function OnSchoolSelectorChange() {
            if ($('#schoolSelect option:selected').val() != '') {
                $('#clubList').empty();
                $('#clubScheduleSub').hide();
                $('#clubScheduleSub').empty();
                selectedClub = undefined;

                LoadClubs($('#schoolSelect option:selected').val());
            }
        }

        function LoadClubs(schoolId) {
            $('#clubList').empty();
            $('#clubScheduleSub').empty();

            $.each(localStore, function (i, el) {
                if (el.id == schoolId) {
                    $.each(el.clubs, function (j, elm) {
                        var blk = $('<div>').addClass('clubBlock');
                        var name = $('<div>').addClass('clubName').text(elm.name);
                        var desc = $('<div>').addClass('clubDesc').text(elm.desc);
                        blk.attr('data-name', elm.name);
                        blk.on('click', function () {
                            LoadClubSchedule($(this).attr('data-name'));
                            $('#clubScheduleSub').show();

                            $('.clubBlock').removeClass('selected');
                            $(this).addClass('selected');
                        });

                        name.appendTo(blk);
                        desc.appendTo(blk);

                        blk.appendTo($('#clubList'));
                    });
                }
            });
        }

        function LoadClubSchedule(clubName) {
            var schoolId = $('#schoolSelect option:selected').val();

            $('#clubScheduleSub').empty();

            $.each(localStore, function (i, el) {
                if (el.id == schoolId) {
                    $.each(el.clubs, function (j, elm) {
                        if (elm.name == clubName) {
                            $.each(elm.schedule, function (k, sch) {
                                var dayBlock = $('<div>').addClass('dayBlock');
                                var day = $('<div>').addClass('day').text(sch.day)
                                
                                day.appendTo(dayBlock);

                                var times = sch.times.split(',');

                                $.each(times, function (l, ts) {
                                    var timeslot = $('<div>').addClass('timeSlot').text('- ' + ts);
                                    timeslot.appendTo(dayBlock);
                                });

                                $('#clubScheduleSub').append(dayBlock);
                            });
                        }
                    });
                }
            });
        }

        $(document).ready(function () {

            var userid = '<%= Session["UserId"] %>'

            $('#schoolSelect').on('change', OnSchoolSelectorChange);
            $('#clubScheduleSub').hide();
            $('#clubScheduleSub').empty();

            $.ajax({
                type: "POST",
                url: "Services.asmx/GetUserSchoolsAndClubs",
                contentType: 'application/json; charset=utf-8',
                data: "{'userId': '" + userid + "'}",
                success: function (data) {
                    var res = JSON.parse(data.d);

                    console.log(res);

                    localStore = res;

                    if (res.length > 0) {
                        $('#schoolSelect').append('<option>Select a School</option>');
                        $('#schoolSelect').append('<option>---------------</option>');
                        $.each(res, function (i, el) {
                            $('#schoolSelect').append('<option value="' + el.id + '">' + el.name + '</option>');
                        });
                    }
                    else {
                        // You are a lazy mofo and not enrolled in any clubs
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="schSel"><select id="schoolSelect"></select></div>
    <div id="clubList">

    </div>

    <div id="clubSchedule">
        <div id="clubScheduleSub"></div>
    </div>
</asp:Content>
