<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="SMAC.Grades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#spinner').hide();

            $('select').prop('selectedIndex', 0);

            $('#yearList').on('change', PopulateMarkingPeriods);
            $('#periodList').on('change', EnableSearchButton);

            $('#periodList').append('<option value="-">-----------------------------</option>');
            $('#periodList').attr('disabled', 'disabled');
            $('#searchButton').attr('disabled', 'disabled');
            $('#periodList').css('background-color', 'silver');

            $('#searchButton').on('click', DrawGradesTable);
        });

        function EnableSearchButton() {

            if ($('#yearList option:selected').val() != '-' && $('#periodList option:selected').val() != '-') {
                $('#searchButton').removeAttr('disabled');
            }
            else {
                $('#searchButton').attr('disabled', 'disabled');
            }
        }

        function PopulateMarkingPeriods() {
            var sy = $('#yearList option:selected').val();

            if ($('#yearList').prop('selectedIndex') == 0) {
                $('#periodList').css('background-color', 'silver');
            }
            
            $('#periodList').prop('selectedIndex', 0);
            EnableSearchButton();
            $('#periodList').empty();
            $('#periodList').append('<option value="-">-----------------------------</option>');
            $('#periodList').attr('disabled', 'disabled');

            $.ajax({
                type: "POST",
                url: "Services.asmx/PopulateMarkingPeriods",
                data: "{'yearId':'" + sy + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var res = JSON.parse(data.d);

                    $.each(res, function (i, el) {

                        var opt = $('<option>').val(el.id).text(el.period);

                        $('#periodList').append(opt);
                    });

                    $('#periodList').removeAttr('disabled');
                    $('#periodList').css('background-color', 'white');
                }
            });
        }

        function DrawGradesTable() {
            var sy = $('#yearList option:selected').val();
            var mp = $('#periodList option:selected').val();

            if ((sy.indexOf('-') == -1) && (mp.indexOf('-') == -1)) {

                $('#gradeTable').empty();

                $.ajax({
                    type: "POST",
                    url: "Services.asmx/FillStudentGradeTable",
                    data: "{'markingPeriodId':'" + mp + "'}",
                    contentType: "application/json; charset=UTF-8",
                    beforeSend: function () {
                        $('#spinner').show();
                    },
                    success: function (data) {
                        $('#spinner').hide();

                        //Populate news info in block
                        var result = JSON.parse(data.d);

                        var table = $('#gradeTable');

                        if (result == '') {
                            table.append($('<tr>').append($('<td>').text('You are not enrolled in any classes this marking period.')));
                            table.removeClass('enroll');
                        }
                        else {
                            table.addClass('enroll');
                            var headerRow = $('<tr>').addClass('gradeTableHeader')
                                    .append($('<th>').text('Class Name'))
                                    .append($('<th>').text('Section'))
                                    .append($('<th>').text('Grade'));

                            table.append(headerRow);

                            $.each(result, function (i, el) {
                                var row = $('<tr>').addClass('gradeTableRow');
                                var className = $('<td>').text(el.classname);
                                var section = $('<td>').text(el.section);
                                var grade = $('<td>').text(el.grade);
                                row.append(className).append(section).append(grade);

                                table.append(row);
                            });
                        }

                        $('#gradeTable').show();
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="gradeBody">
        <div class="gradeSelect">
            <div class="gradeHeader" id="yearLbl">Select School Year:</div>
            <asp:DropDownList ID="yearList" runat="server" ClientIDMode="Static"></asp:DropDownList>
        </div>
        <div class="gradeSelect" id="periodCont">
            <div class="gradeHeader" id="mpLbl">Select Marking Period:</div>
            <select id="periodList"></select>
        </div>
        <input type="button" id="searchButton" class="btn btn-primary searchButton" value="Search" />
        <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
        <div><table id="gradeTable" class=""></table></div>
    </div>
</asp:Content>
