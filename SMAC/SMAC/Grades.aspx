<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="SMAC.Grades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#yearLbl').hide();
            $('#schoolYearDD').hide();
            $('#mpLbl').hide();
            $('#markingPeriodDD').hide();
            $('#gradeTable').empty();
            $('#gradeTable').hide();

            $('#MainContent_schoolList').on('change', function () {

                $('#schoolYearDD').empty();
                $('#markingPeriodDD').empty();
                $('#gradeTable').empty();

                $('#yearLbl').hide();
                $('#schoolYearDD').hide();
                $('#mpLbl').hide();
                $('#markingPeriodDD').hide();
                $('#gradeTable').hide();

                LoadSchoolYears();
            });

            $('#schoolYearDD').on('change', function () {

                $('#markingPeriodDD').empty();
                $('#mpLbl').hide();
                $('#markingPeriodDD').hide();

                LoadMarkingPeriods();
            });

            $('#markingPeriodDD').on('change', function () {
                DrawGradesTable();
            });
        });

        function DrawGradesTable() {
            var id = '<%= Session["UserId"] %>';
            var sl = $('#MainContent_schoolList option:selected').val();
            var sy = $('#schoolYearDD option:selected').val();
            var mp = $('#markingPeriodDD option:selected').val();

            if ((sl != '') && (sy != '') && (mp != '')) {
                $('#gradeTable').empty();


                $.ajax({
                    type: "POST",
                    url: "Services.asmx/FillStudentGradeTable",
                    data: "{'userId':'" + id + "', 'schoolId': '" + sl + "', 'year':'" + sy + "', 'markingPeriod':'" + mp + "'}",
                    contentType: "application/json; charset=UTF-8",
                    success: function (data) {
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

        function LoadMarkingPeriods() {
            $.ajax({
                type: "POST",
                url: "Services.asmx/GetSchoolsMPs",
                data: "{'schoolId': '" + $('#MainContent_schoolList option:selected').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    //Populate news info in block
                    var result = JSON.parse(data.d);
                    $('#markingPeriodDD').append('<option>-----------------</option>');

                    console.log(result);

                    if (result != '') {
                        $.each(result, function (i, el) {
                            if (el.fy == 'true') {
                                $('#markingPeriodDD').append('<option value="fy">' + el.mp + '</option>');
                            }
                            else {
                                $('#markingPeriodDD').append('<option value="'+el.val+'">' + el.mp + '</option>');
                            }
                        });
                    }

                    $('#mpLbl').show();
                    $('#markingPeriodDD').show();
                }
            });
        }

        function LoadSchoolYears() {

            $.ajax({
                type: "POST",
                url: "Services.asmx/GetSchoolsYears",
                data: "{'schoolId': '" + $('#MainContent_schoolList option:selected').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    //Populate news info in block
                    var result = JSON.parse(data.d);
                    $('#schoolYearDD').append('<option>-----------------</option>');
                    
                    if (result != '') {
                        $.each(result, function (i, el) {
                            $('#schoolYearDD').append('<option>' + el.year + '</option>');
                        });
                    }

                    $('#yearLbl').show();
                    $('#schoolYearDD').show();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <div class="gradeBody">
            <div class="gradeHeader" id="schoolLbl">Select School:</div>
            <div class="gradeSelect"><asp:DropDownList ID="schoolList" runat="server"></asp:DropDownList></div>
            <div class="gradeHeader" id="yearLbl">Select School Year:</div>
            <div class="gradeSelect"><select id="schoolYearDD"></select></div>
            <div class="gradeHeader" id="mpLbl">Select Marking Period:</div>
            <div class="gradeSelect"><select id="markingPeriodDD"></select></div>
            <div><table id="gradeTable"></table></div>
        </div>
    </form>
</asp:Content>
