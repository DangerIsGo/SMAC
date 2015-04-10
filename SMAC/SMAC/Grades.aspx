<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Grades.aspx.cs" Inherits="SMAC.Grades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>

        var gradeList = undefined;

        $(document).ready(function () {
            $('#spinner').hide();

            $('select').prop('selectedIndex', 0);

            $('#yearList').on('change', PopulateMarkingPeriods);
            $('#periodList').on('change', PopulateClasses);
            $('#classList').on('change', EnableSearchButton);

            $('#periodList').append('<option value="-">-----------------------------</option>');
            $('#periodList').attr('disabled', 'disabled');
            $('#searchButton').attr('disabled', 'disabled');
            $('#periodList').css('background-color', 'silver');

            $('#submitGrades').hide().click(SubmitGrades);

            if ($('#userRole').val() != 'teacher') {
                $('#classCont').hide();
                $('#searchButton').on('click', DrawGradesTable);
                $('#gradeStatus').hide();
            }
            else {
                $('#classList').append('<option value="-">-----------------------------</option>');
                $('#classList').attr('disabled', 'disabled');
                $('#classList').css('background-color', 'silver');
                $('#searchButton').on('click', DrawSetGradesTable);
            }
        });

        function SubmitGrades() {
            var rows = $('#gradeTable tbody tr:not(.gradeTableHeader)');
            var grades = '';

            $.each(rows, function (i, el) {
                grades += $(el).data('id') + ':' + $(el).find('select option:checked').val() + ','
            });

            $.ajax({
                type: "POST",
                url: "Services.asmx/UpdateTeacherGrades",
                data: "{'periodId':'" + $('#periodList option:checked').val() + "', 'sectionId':'" + $('#classList option:checked').val() + "', 'grades':'"+grades+"'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);
                    
                    if (json == 'success') {
                        $('#gradeStatus').text('Success!  Grades have been successfully updated.');
                    }
                    else {
                        $('#gradeStatus').text(json);
                    }
                }
            });
        }

        function PopulateClasses() {
            if ($('#userRole').val() == 'teacher') {
                $.ajax({
                    type: "POST",
                    url: "Services.asmx/PopulateTeachersClasses",
                    data: "{'periodId':'" + $('#periodList option:checked').val() + "'}",
                    contentType: "application/json; charset=UTF-8",
                    success: function (data) {
                        var res = JSON.parse(data.d);

                        $.each(res, function (i, el) {
                            $('#classList').append($('<option>').val(el.id).text(el.name));
                        });

                        $('#classList').removeAttr('disabled');
                        $('#classList').css('background-color', 'white');
                    }
                });
            }
            else {
                EnableSearchButton();
            }
        }

        function EnableSearchButton() {
            if ($('#userRole').val() != 'teacher') {
                if ($('#yearList option:selected').val() != '-' && $('#periodList option:selected').val() != '-') {
                    $('#searchButton').removeAttr('disabled');
                }
                else {
                    $('#searchButton').attr('disabled', 'disabled');
                }
            }
            else {
                if ($('#yearList option:selected').val() != '-' && $('#periodList option:selected').val() != '-' && $('#classList option:selected').val() != '-') {
                    $('#searchButton').removeAttr('disabled');
                }
                else {
                    $('#searchButton').attr('disabled', 'disabled');
                }
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

        function DrawSetGradesTable() {
            var mp = $('#periodList option:selected').val();
            var sId = $('#classList option:selected').val();

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchGradeList",
                data: "{}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    gradeList = JSON.parse(data.d);

                    $.ajax({
                        type: "POST",
                        url: "Services.asmx/FillTeacherGradeTable",
                        data: "{'periodId':'" + mp + "', 'sectionId':'" + sId + "'}",
                        contentType: "application/json; charset=UTF-8",
                        success: function (data) {
                            //Populate news info in block
                            var result = JSON.parse(data.d);

                            var table = $('#gradeTable');

                            if (result == '') {
                                table.append($('<tr>').append($('<td>').text('There are no students enrolled in this class.')));
                                table.removeClass('enroll');
                            }
                            else {
                                table.addClass('enroll');
                                var headerRow = $('<tr>').addClass('gradeTableHeader')
                                        .append($('<th>').addClass('teacher').text('Student'))
                                        .append($('<th>').addClass('teacher').text('Grade'));

                                table.append(headerRow);

                                $.each(result, function (i, el) {
                                    var row = $('<tr>').addClass('gradeTableRow').attr('data-id', el.id);
                                    var name = $('<td>').text(el.name);

                                    var gradeSel = $('<select>').addClass('form-control').append($('<option>').val('-').text('---'));

                                    $.each(gradeList, function (i, elm) {
                                        gradeSel.append($('<option>').val(elm.grade).text(elm.grade));
                                    });

                                    gradeSel.val(el.grade)

                                    var grade = $('<td>').append(gradeSel);
                                    row.append(name).append(grade);

                                    table.append(row);
                                });
                            }

                            $('#gradeTable').show();
                            $('#submitGrades').show();
                            $('#gradeStatus').show();
                        }
                    });
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
        <div class="gradeSelect" id="classCont">
            <div class="gradeHeader" id="classLbl">Select Class:</div>
            <select id="classList"></select>
        </div>
        <input type="button" id="searchButton" class="btn btn-primary searchButton" value="Search" />
        <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
        <div><table id="gradeTable" class=""></table></div>
        <div class="form-group col-md-8" style="text-align: right; margin-top: 10px;"><input type="button" id="submitGrades" class="btn btn-success searchButton" value="Submit" /></div>
        <div class="form-group col-md-12"><label class="control-label admin" id="gradeStatus"></label></div>
        <asp:HiddenField ID="userRole" runat="server" ClientIDMode="Static" />
    </div>
</asp:Content>
