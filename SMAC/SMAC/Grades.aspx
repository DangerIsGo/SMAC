<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="SMAC.Grades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>

        $(document).ready(function () {
            $('#MainContent_yearList').on('change', DrawGradesTable);
            $('#MainContent_periodList').on('change', DrawGradesTable);
        });

        function DrawGradesTable() {
            var sy = $('#MainContent_yearList option:selected').val();
            var mp = $('#MainContent_periodList option:selected').val();

            if ((sy.indexOf('---') == -1) && (mp.indexOf('---') == -1)) {

                console.log(sy);
                console.log(mp);
                $('#gradeTable').empty();

                $.ajax({
                    type: "POST",
                    url: "Services.asmx/FillStudentGradeTable",
                    data: "{'year':'" + sy + "', 'markingPeriod':'" + mp + "'}",
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <div class="gradeBody">
            <div class="gradeSelect">
                <div class="gradeHeader" id="yearLbl">Select School Year:</div>
                <asp:DropDownList ID="yearList" runat="server"></asp:DropDownList>
            </div>
            <div class="gradeSelect">
                <div class="gradeHeader" id="mpLbl">Select Marking Period:</div>
                <asp:DropDownList ID="periodList" runat="server"></asp:DropDownList>
            </div>
            <div><table id="gradeTable"></table></div>
        </div>
    </form>
</asp:Content>
