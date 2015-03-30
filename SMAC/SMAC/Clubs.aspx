<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clubs.aspx.cs" Inherits="SMAC.Clubs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#clubScheduleSub').hide();
            $('#AllClubs').attr('checked', 'checked');
            $('#AllClubs').on('click', ShowAllClubs);
            $('#MyClubs').on('click', ShowMyClubs);

            $('.clubBlock').on('click', function () {
                LoadClubSchedule($(this).attr('data-name'));
                $('#clubScheduleSub').show();

                $('.clubBlock').removeClass('selected');
                $(this).addClass('selected');
            });
        });

        function ShowMyClubs() {
            $('#clubScheduleSub').hide();
            $('.clubBlock').removeClass('selected');

            $('#AllClubs').removeAttr('checked');

            var myClubs = $('#MainContent_myClubNames').val().split(';');

            var allClubs = $('.clubBlock');

            $.each(allClubs, function (j, elm) {
                var block = $(elm);
                var found = false;

                $.each(myClubs, function (i, el) {
                    if (block.attr('data-name') == el) {
                        found = true;
                    }
                });

                if (!found) {
                    block.hide();
                }
            });
        }

        function ShowAllClubs() {
            $('#clubScheduleSub').hide();
            $('.clubBlock').removeClass('selected');

            $('#MyClubs').removeAttr('checked');

            $('.clubBlock').show();
        }

        function LoadClubSchedule(clubName) {
            $('#clubScheduleSub').empty();

            var hdnVal = $('#MainContent_clubScheduleHdn').val();
            
            var schedules = JSON.parse(hdnVal);

            $.each(schedules, function (i, el) {
                if (el.name == clubName) {

                    if (el.schedule.length == 0) {
                        $('#clubScheduleSub').append("This club does not have a schedule yet.");
                        return;
                    }

                    $.each(el.schedule, function (k, sch) {
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="clubRadioSelect">
        <input type="radio" id="AllClubs"/>
        <label for="AllClubs" >Show All Clubs</label>
        <input type="radio" id="MyClubs"/>
        <label for="MyClubs" >Show My Clubs</label>
    </div>
    <div id="clubList">
        <asp:ListView runat="server" ID="clubListView">
            <ItemTemplate>
                <div class="clubBlock" data-name="<%#Eval("DataName")%>">
                    <div class="clubName"><%#Eval("ClubName")%></div>
                    <div class="clubDesc"><%#Eval("ClubDesc")%></div>
                </div>
            </ItemTemplate>
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>
        </asp:ListView>
    </div>

    <div id="clubSchedule">
        <div id="clubScheduleSub"></div>
    </div>
    <form runat="server">
        <asp:HiddenField ID="clubScheduleHdn" runat="server" />
        <asp:HiddenField ID="myClubNames" runat="server" />
    </form>
</asp:Content>
