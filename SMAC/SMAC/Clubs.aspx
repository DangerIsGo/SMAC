<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clubs.aspx.cs" Inherits="SMAC.Clubs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var localStore = undefined;

        function OnSchoolSelectorChange() {
            if ($('#schoolSelect option:selected').val() != '') {
                $('#clubList').empty();
                $('#clubSchedule').empty();

                LoadClubs($('#schoolSelect option:selected').val());
            }
        }

        function LoadClubs(schoolId) {
            $.each(localStore, function (i, el) {
                if (el.id == schoolId) {
                    $.each(el.clubs, function (j, elm) {
                    });
                }
            });
        }

        $(document).ready(function () {
            //Load school List
            //If one school, just load club list
            //If multiple, load school selector

            var userid = '<%= Session["UserId"] %>'

            $('#schoolSelect').on('change', OnSchoolSelectorChange);

            $.ajax({
                type: "POST",
                url: "Services.asmx/GetUserSchoolsAndClubs",
                contentType: 'application/json; charset=utf-8',
                data: "{'userId': '" + userid + "'}",
                success: function (data) {
                    var res = JSON.parse(data.d);

                    localStore = res;

                    console.log(res);

                    if (res.length == 1) {
                        // Just load
                        $.each(res, function (i, el) {
                            $('#schoolSelect').append('<option value="' + el.id + '">' + el.name + '</option>');
                            LoadClubs(el.id);
                        });
                    }
                    else if (res.length > 1) {
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

    </div>
</asp:Content>
