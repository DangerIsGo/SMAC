var list;
var subjects;
var classes;
var sections;
var disposition;

//Enables the Accept action button when item is selected
function EnableActionButton() {
    if ($('#entityActionList option:checked').val() != '-') {
        $('#acceptAction').removeAttr('disabled');
    }
    else {
        $('#acceptAction').attr('disabled', 'disabled');
    }
}

//Enables the Accept entity type button when item is selected
function EnableTypeButton() {
    if ($('#entityTypeList option:checked').val() != '-') {
        $('#acceptEntityType').removeAttr('disabled');
    }
    else {
        $('#acceptEntityType').attr('disabled', 'disabled');
    }
}

function ShowEntityTypes() {
    if ($('#entityActionList').val() != '-') {

        $('#acceptAction').attr('disabled', 'disabled');
        $('#entityTypeGroup').show();

        list = undefined;

        $('#entityGroup').hide();
        $('#entityGroup').empty();

        $('#entitySelectGroup').hide();
        $('#entitySelectGroup').empty();

        $('#entityTypeList').prop('selectedIndex', 0);
    }
}

function PopulateEntityList() {
    $('#entityPanel').show();

    $('#acceptEntityType').attr('disabled', 'disabled');

    var eType = $('#entityTypeList').val();

    if ($('#entityActionList option:checked').val() == 'create') {
        // Just show empty form

        if (eType == 1) {
            //Class
            DrawClassForm();
        }
        else if (eType == 2) {
            //Club
            DrawClubForm();
        }
        else if (eType == 3) {
            //Club Enrollment
            DrawClubEnrollmentForm_Pre();
        }
        else if (eType == 4) {
            //Club Schedule
            DrawClubScheduleForm_Pre();
        }
        else if (eType == 5) {
            //Grade
            DrawGradeForm();
        }
        else if (eType == 6) {
            //Marking Period
            DrawMarkingPeriodForm();
        }
        else if (eType == 7) {
            //News 
            DrawLatestNewsForm();
        }
        else if (eType == 8) {
            //Section
            CreateSectionForm_Pre();
        }
        else if (eType == 9) {
            //Section Schedule

        }
        else if (eType == 10) {
            //School Year
            DrawSchoolYearForm();
        }
        else if (eType == 11) {
            //Student Enrollment

        }
        else if (eType == 12) {
            //Subject
            DrawSubjectForm();
        }
        else if (eType == 13) {
            //Time Span
            DrawTimeSlotForm();
        }
        else if (eType == 14) {
            //User
            DrawUserForm();
        }
    }
    else if (($('#entityActionList option:checked').val() == 'update') ||
        ($('#entityActionList option:checked').val() == 'delete')) {
        // Show List

        if (eType == 1) {
            //Class
            GetSubjectList(DrawClassSubjectSelect);
        }
        else if (eType == 2) {
            //Club
            GetClubList(DrawClubSelect);

        }
        else if (eType == 3) {
            //Club Enrollment
            DrawClubEnrollmentForm_Pre();
        }
        else if (eType == 4) {
            //Club Schedule
            DrawClubScheduleSelect();
        }
        else if (eType == 5) {
            //Grade
            DrawGradeSelect();
        }
        else if (eType == 6) {
            //Marking Period
            GetSchoolYearList(DrawMarkingPeriodSelect);
        }
        else if (eType == 7) {
            //News 
            GetLatestNewsList(DrawLatestNewsSelect);
        }
        else if (eType == 8) {
            //Section
            CreateSectionSelect();
        }
        else if (eType == 9) {
            //Section Schedule

        }
        else if (eType == 10) {
            //School Year
            GetSchoolYearList(DrawSchoolYearSelect);
        }
        else if (eType == 11) {
            //Student Enrollment

        }
        else if (eType == 12) {
            //Subject
            GetSubjectList(DrawSubjectSelect);
        }
        else if (eType == 13) {
            //Time Span
            GetTimeSlotList(DrawTimeSlotSelect);
        }
        else if (eType == 14) {
            //User
            GetUserList(DrawUserSelect);
        }
    }
}




function DrawSubjectSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectSelectLabel').text('Select a Subject');
    var subjCont = $('<div>').addClass('col-md-3');
    var subjectList = $('<select>').addClass('form-control').attr('id', 'subjectSelect').on('change', function () {
        if ($('#subjectSelect option:checked').val() != '-') {
            $('#acceptSubject').removeAttr('disabled');
        }
        else {
            $('#acceptSubject').attr('disabled', 'disabled');
        }
    });

    subjectList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        subjectList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    list = undefined;

    subjectList.appendTo(subjCont);
    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptSubject').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitSubject);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);
    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'subjectStatus');
    status.appendTo(statusCont);

    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}

function DrawSubjectForm() {
    var form = $('#entityGroup');

    var group = $('<div>').addClass('form-group');

    var subject = list;
    console.log(subject);

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectLabel').text('Subject Name');
    var nameCont = $('<div>').addClass('col-md-3');
    var name = $('<input>').attr('type', 'text').attr('id', 'subjectName').addClass('form-control');
    if (list != undefined) { name.val(subject.name); }
    name.appendTo(nameCont);
    var btnCont = $('<div>').addClass('col-md-1');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createSubjectButton').attr('name', 'createSubject').addClass('btn btn-success').click(CreateEditSubject);
    if (subject != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);
    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'subjectStatus');
    status.appendTo(statusCont);

    list = undefined;

    group.append(lbl);
    group.append(nameCont);
    group.append(btnCont);
    group.append(clearer);
    group.append(statusCont);
    group.appendTo(form);
    form.show();
}

function CreateEditSubject() {
    if ($('#subjectName').val() != '') {
        if ($('#createSubjectButton').val() == 'Create') {
            CreateSubject($('#subjectName').val(), CreateSubjectCallback);
        }
        else {
            UpdateSubject($('#subjectSelect option:checked').val(), $('#subjectName').val(), UpdateSubjectCallback);
        }
    }
}

function SubmitSubject() {
    if ($('#subjectSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteSubject($('#subjectSelect option:checked').val(), DeleteSubjectCallback);
        }
        else {
            GetSubject($('#subjectSelect option:checked').val(), DrawSubjectForm);
        }
    }
}

function DeleteSubjectCallback() {
    if (disposition == 'success') {
        $('#subjectStatus').text('Success!  Your subject was successfully deleted');

        GetSubjectList(DeleteSubjectRepopulate);
    }
    else {
        $('#subjectStatus').text(disposition);
    }
}

function DeleteSubjectRepopulate() {
    var subjectList = $('#subjectSelect');
    subjectList.empty();

    subjectList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        subjectList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    $('#acceptSubject').attr('disabled', 'disabled');
}

function UpdateSubjectCallback() {
    if (disposition == 'success') {
        $('#subjectStatus').text('Success!  Your subject was successfully updated');
    }
    else {
        $('#subjectStatus').text(disposition);
    }
}

function CreateSubjectCallback() {
    if (disposition == 'success') {
        $('#subjectStatus').text('Success!  Your subject was successfully created');
    }
    else {
        $('#subjectStatus').text(disposition);
    }
}

function DeleteSubject(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteSubject",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function UpdateSubject(id, name, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateSubject",
        data: "{'id':'" + id + "', 'name':'" + name + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function CreateSubject(name, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateSubject",
        data: "{'name':'" + name + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetSubjectList(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSubjectList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function GetSubject(id, callback) {
    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSubject",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}





















function DrawClubForm() {
    var form = $('#entityGroup');

    var mainGroup = $('<div>').addClass('form-group');
    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group');
    var group3 = $('<div>').addClass('form-group');
    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group3);

    var club = list;

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'clubLabel').text('Club Name');
    var nameCont = $('<div>').addClass('col-md-3');
    var name = $('<input>').attr('type', 'text').attr('id', 'clubName').addClass('form-control');
    if (list != undefined) { name.val(club.name); }
    name.appendTo(nameCont);

    var lblDesc = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'clubDescLabel').text('Club Description');
    var descCont = $('<div>').addClass('col-md-4');
    var desc = $('<input>').attr('type', 'text').attr('id', 'clubDescription').addClass('form-control').attr('placeholder', 'Optional');
    if (list != undefined) { desc.val(club.desc); }
    desc.appendTo(descCont);

    var btnCont = $('<div>').addClass('col-md-1');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createClubButton').attr('name', 'createClub').addClass('btn btn-success').click(CreateEditClub);
    if (club != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);
    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'clubStatus');
    status.appendTo(statusCont);

    list = undefined;

    group1.append(lbl);
    group1.append(nameCont);
    group1.append(btnCont);
    group1.append(clearer1);
    group2.append(lblDesc);
    group2.append(descCont);
    group2.append(clearer2);
    group3.append(statusCont);
    group3.append(clearer3);
    mainGroup.appendTo(form);
    form.show();
}

function DrawClubSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'clubSelectLabel').text('Select a Club');
    var subjCont = $('<div>').addClass('col-md-3');
    var clubList = $('<select>').addClass('form-control').attr('id', 'clubSelect').on('change', function () {
        if ($('#clubSelect option:checked').val() != '-') {
            $('#acceptClub').removeAttr('disabled');
        }
        else {
            $('#acceptClub').attr('disabled', 'disabled');
        }
    });

    clubList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        clubList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    list = undefined;

    clubList.appendTo(subjCont);
    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptClub').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitClub);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);
    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'clubStatus');
    status.appendTo(statusCont);

    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}

function CreateEditClub() {
    if ($('#clubName').val() != '') {
        if ($('#createClubButton').val() == 'Create') {
            CreateClub($('#clubName').val(), $('#clubDescription').val(), CreateClubCallback);
        }
        else {
            UpdateClub($('#clubSelect option:checked').val(), $('#clubName').val(), $('#clubDescription').val(), UpdateClubCallback);
        }
    }
}

function SubmitClub() {
    if ($('#clubSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteClub($('#clubSelect option:checked').val(), DeleteClubCallback);
        }
        else {
            GetClub($('#clubSelect option:checked').val(), DrawClubForm);
        }
    }
}

function DeleteClubCallback() {
    if (disposition == 'success') {
        $('#clubStatus').text('Success!  Your club was successfully deleted');

        GetClubList(DeleteClubRepopulate);
    }
    else {
        $('#clubStatus').text(disposition);
    }
}

function DeleteClubRepopulate() {
    var clubList = $('#clubSelect');
    clubList.empty();

    clubList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        clubList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    $('#acceptClub').attr('disabled', 'disabled');
}

function UpdateClubCallback() {
    if (disposition == 'success') {
        $('#clubStatus').text('Success!  Your club was successfully updated');
    }
    else {
        $('#clubStatus').text(disposition);
    }
}

function CreateClubCallback() {
    if (disposition == 'success') {
        $('#clubStatus').text('Success!  Your club was successfully created');
    }
    else {
        $('#clubStatus').text(disposition);
    }
}


function CreateClub(name, desc, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateClub",
        data: "{'name':'" + name + "', 'description':'"+desc+"'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetClubList(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClubList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function GetClub(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClub",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}

function UpdateClub(id, name, description, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateClub",
        data: "{'id':'" + id + "', 'name':'" + name + "', 'description':'"+description+"'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function DeleteClub(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteClub",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}


















function DrawSchoolYearForm() {
    var form = $('#entityGroup');

    var mainGroup = $('<div>').addClass('form-group');
    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group');
    var group3 = $('<div>').addClass('form-group');
    var group4 = $('<div>').addClass('form-group');
    var group5 = $('<div>').addClass('form-group');

    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group3);
    mainGroup.append(group4);
    mainGroup.append(group5);

    var schoolYear = list;

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'schoolYearLabel').text('School Year');
    var nameCont = $('<div>').addClass('col-md-3');
    var name = $('<input>').attr('type', 'text').attr('id', 'schoolYearName').addClass('form-control');
    if (list != undefined) { name.val(schoolYear.name); }
    name.appendTo(nameCont);

    var lblStart = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'schoolYearStartDateLabel').text('Start Date');
    var startCont = $('<div>').addClass('col-md-4');
    var start = $('<input>').attr('type', 'text').attr('id', 'schoolYearStartDate').addClass('form-control');
    start.datepicker();
    if (list != undefined) { start.val(schoolYear.start); }
    start.appendTo(startCont);

    var lblEnd = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'schoolYearEndDateLabel').text('End Date');
    var endCont = $('<div>').addClass('col-md-4');
    var end = $('<input>').attr('type', 'text').attr('id', 'schoolYearEndDate').addClass('form-control');
    end.datepicker();
    if (list != undefined) { end.val(schoolYear.end); }
    end.appendTo(endCont);

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createSchoolYearButton').attr('name', 'createSchoolYear').addClass('btn btn-success').click(CreateEditSchoolYear);
    if (schoolYear != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');
    var clearer5 = $('<div>').addClass('clearer');

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'schoolYearStatus');
    status.appendTo(statusCont);

    list = undefined;

    group1.append(lbl);
    group1.append(nameCont);
    group1.append(clearer1);

    group2.append(lblStart);
    group2.append(startCont);
    group2.append(clearer2);

    group3.append(lblEnd);
    group3.append(endCont);
    group3.append(clearer3);

    group4.append(btnCont);
    group4.append(clearer4);

    group5.append(statusCont);
    group5.append(clearer5);

    mainGroup.appendTo(form);
    form.show();
}

function DrawSchoolYearSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'schoolYearSelectLabel').text('Select a School Year');
    var subjCont = $('<div>').addClass('col-md-3');
    var schoolYearList = $('<select>').addClass('form-control').attr('id', 'schoolYearSelect').on('change', function () {
        if ($('#schoolYearSelect option:checked').val() != '-') {
            $('#acceptSchoolYear').removeAttr('disabled');
        }
        else {
            $('#acceptSchoolYear').attr('disabled', 'disabled');
        }
    });

    schoolYearList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        schoolYearList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    list = undefined;

    schoolYearList.appendTo(subjCont);
    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptSchoolYear').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitSchoolYear);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);

    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'schoolYearStatus');
    status.appendTo(statusCont);

    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}


function CreateEditSchoolYear() {
    if ($('#schoolYearName').val() != '') {
        if ($('#createSchoolYearButton').val() == 'Create') {
            CreateSchoolYear($('#schoolYearName').val(), $('#schoolYearStartDate').val(), $('#schoolYearEndDate').val(), CreateSchoolYearCallback);
        }
        else {
            UpdateSchoolYear($('#schoolYearSelect option:checked').val(), $('#schoolYearName').val(), $('#schoolYearStartDate').val(), $('#schoolYearEndDate').val(), UpdateSchoolYearCallback);
        }
    }
}

function SubmitSchoolYear() {
    if ($('#schoolYearSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteSchoolYear($('#schoolYearSelect option:checked').val(), DeleteSchoolYearCallback);
        }
        else {
            GetSchoolYear($('#schoolYearSelect option:checked').val(), DrawSchoolYearForm);
        }
    }
}

function CreateSchoolYearCallback() {
    if (disposition == 'success') {
        $('#schoolYearStatus').text('Success!  Your school year was successfully created');
    }
    else {
        $('#schoolYearStatus').text(disposition);
    }
}


function UpdateSchoolYearCallback() {
    if (disposition == 'success') {
        $('#schoolYearStatus').text('Success!  Your school year was successfully updated');
    }
    else {
        $('#schoolYearStatus').text(disposition);
    }
}

function DeleteSchoolYearCallback() {
    if (disposition == 'success') {
        $('#schoolYearStatus').text('Success!  Your school year was successfully deleted');

        GetSchoolYearList(DeleteSchoolYearRepopulate);
    }
    else {
        $('#schoolYearStatus').text(disposition);
    }
}

function DeleteSchoolYearRepopulate() {
    var schoolYearList = $('#schoolYearSelect');
    schoolYearList.empty();

    schoolYearList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        schoolYearList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    $('#acceptSchoolYear').attr('disabled', 'disabled');
}


function CreateSchoolYear(name, start, end, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateSchoolYear",
        data: "{'name':'" + name + "', 'start':'" + start + "', 'end':'" + end + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetSchoolYearList(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSchoolYearList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function GetSchoolYear(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSchoolYear",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}

function UpdateSchoolYear(id, name, start, end, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateSchoolYear",
        data: "{'id':'" + id + "', 'name':'" + name + "', 'start':'" + start + "', 'end':'" + end + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function DeleteSchoolYear(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteSchoolYear",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}














function DrawTimeSlotSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'timeSlotSelectLabel').text('Select a Time Slot');
    var subjCont = $('<div>').addClass('col-md-3');
    var timeSlotList = $('<select>').addClass('form-control').attr('id', 'timeSlotSelect').on('change', function () {
        if ($('#timeSlotSelect option:checked').val() != '-') {
            $('#acceptTimeSlot').removeAttr('disabled');
        }
        else {
            $('#acceptTimeSlot').attr('disabled', 'disabled');
        }
    });

    timeSlotList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        timeSlotList.append('<option value="' + el.id + '">' + el.start + ' - ' + el.end + '</option>');
    });

    list = undefined;

    timeSlotList.appendTo(subjCont);
    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptTimeSlot').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitTimeSlot);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);

    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'timeSlotStatus');
    status.appendTo(statusCont);

    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}


function DrawTimeSlotForm() {
    var form = $('#entityGroup');

    var mainGroup = $('<div>').addClass('form-group');
    var group1 = $('<div>').addClass('form-group');
    var group3 = $('<div>').addClass('form-group');
    var group4 = $('<div>').addClass('form-group');

    mainGroup.append(group1);
    mainGroup.append(group3);
    mainGroup.append(group4);

    var timeSlot = list;

    var lblStart = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'timeSlotStartTimeLabel').text('Start Time');
    var startCont = $('<div>').addClass('col-md-2');
    var start = $('<input>').attr('type', 'text').attr('id', 'timeSlotStartTime').addClass('form-control');
    start.timepicker();
    if (list != undefined) { start.val(timeSlot.start); }
    start.appendTo(startCont);

    var lblEnd = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'timeSlotEndTimeLabel').text('End Time');
    var endCont = $('<div>').addClass('col-md-2');
    var end = $('<input>').attr('type', 'text').attr('id', 'timeSlotEndTime').addClass('form-control');
    end.timepicker();
    if (list != undefined) { end.val(timeSlot.end); }
    end.appendTo(endCont);

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createTimeSlotButton').attr('name', 'createTimeSlot').addClass('btn btn-success').click(CreateEditTimeSlot);
    if (timeSlot != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var clearer1 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'timeSlotStatus');
    status.appendTo(statusCont);

    list = undefined;

    group1.append(lblStart);
    group1.append(startCont);

    group1.append(lblEnd);
    group1.append(endCont);
    group1.append(clearer1);

    group3.append(btnCont);
    group3.append(clearer3);

    group4.append(statusCont);
    group4.append(clearer4);

    mainGroup.appendTo(form);
    form.show();
}


function CreateEditTimeSlot() {
    if ($('#timeSlotName').val() != '') {
        if ($('#createTimeSlotButton').val() == 'Create') {
            CreateTimeSlot($('#timeSlotStartTime').val(), $('#timeSlotEndTime').val(), CreateTimeSlotCallback);
        }
        else {
            UpdateTimeSlot($('#timeSlotSelect option:checked').val(), $('#timeSlotStartTime').val(), $('#timeSlotEndTime').val(), UpdateTimeSlotCallback);
        }
    }
}

function SubmitTimeSlot() {
    if ($('#timeSlotSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteTimeSlot($('#timeSlotSelect option:checked').val(), DeleteTimeSlotCallback);
        }
        else {
            GetTimeSlot($('#timeSlotSelect option:checked').val(), DrawTimeSlotForm);
        }
    }
}


function CreateTimeSlotCallback() {
    if (disposition == 'success') {
        $('#timeSlotStatus').text('Success!  Your time slot was successfully created');
    }
    else {
        $('#timeSlotStatus').text(disposition);
    }
}


function UpdateTimeSlotCallback() {
    if (disposition == 'success') {
        $('#timeSlotStatus').text('Success!  Your time slot was successfully updated');
    }
    else {
        $('#timeSlotStatus').text(disposition);
    }
}

function DeleteTimeSlotCallback() {
    if (disposition == 'success') {
        $('#timeSlotStatus').text('Success!  Your time slot was successfully deleted');

        GetTimeSlotList(DeleteTimeSlotRepopulate);
    }
    else {
        $('#timeSlotStatus').text(disposition);
    }
}

function DeleteTimeSlotRepopulate() {
    var timeSlotList = $('#timeSlotSelect');
    timeSlotList.empty();

    timeSlotList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        timeSlotList.append('<option value="' + el.id + '">' + el.start + ' - ' + el.end + '</option>');
    });

    $('#acceptTimeSlot').attr('disabled', 'disabled');
}


function CreateTimeSlot(start, end, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateTimeSlot",
        data: "{'start':'" + start + "', 'end':'" + end + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function UpdateTimeSlot(id, start, end, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateTimeSlot",
        data: "{'id':'" + id + "', 'start':'" + start + "', 'end':'" + end + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetTimeSlotList(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchTimeSlotList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function DeleteTimeSlot(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteTimeSlot",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetTimeSlot(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchTimeSlot",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}









function DrawLatestNewsSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'latestNewsSelectLabel').text('Select a Latest News');
    var subjCont = $('<div>').addClass('col-md-3');
    var latestNewsList = $('<select>').addClass('form-control').attr('id', 'latestNewsSelect').on('change', function () {
        if ($('#latestNewsSelect option:checked').val() != '-') {
            $('#acceptLatestNews').removeAttr('disabled');
        }
        else {
            $('#acceptLatestNews').attr('disabled', 'disabled');
        }
    });

    latestNewsList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        latestNewsList.append('<option value="' + el.id + '">' + el.date + ' - ' + el.content + '</option>')
    });

    list = undefined;

    latestNewsList.appendTo(subjCont);
    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptLatestNews').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitLatestNews);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);
    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'latestNewsStatus');
    status.appendTo(statusCont);

    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}

function DrawLatestNewsForm() {
    var form = $('#entityGroup');

    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group');
    var group3 = $('<div>').addClass('form-group');
    group1.appendTo(form);
    group2.appendTo(form);
    group3.appendTo(form);

    var latestNews = list;
    console.log(latestNews);

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'latestNewsLabel').text('Content');
    var nameCont = $('<div>').addClass('col-md-9');
    var name = $('<input>').attr('type', 'text').attr('id', 'latestNewsName').addClass('form-control');
    if (list != undefined) { name.val(latestNews.content); }
    name.appendTo(nameCont);

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createLatestNewsButton').attr('name', 'createLatestNews').addClass('btn btn-success').click(CreateEditLatestNews);
    if (latestNews != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'latestNewsStatus');
    status.appendTo(statusCont);

    list = undefined;

    group1.append(lbl);
    group1.append(nameCont);
    group1.append(clearer1);

    group2.append(btnCont);
    group2.append(clearer2);

    group3.append(statusCont);

    form.show();
}

function CreateEditLatestNews() {
    if ($('#latestNewsName').val() != '') {
        if ($('#createLatestNewsButton').val() == 'Create') {
            CreateLatestNews($('#latestNewsName').val(), CreateLatestNewsCallback);
        }
        else {
            UpdateLatestNews($('#latestNewsSelect option:checked').val(), $('#latestNewsName').val(), UpdateLatestNewsCallback);
        }
    }
}

function SubmitLatestNews() {
    if ($('#latestNewsSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteLatestNews($('#latestNewsSelect option:checked').val(), DeleteLatestNewsCallback);
        }
        else {
            GetLatestNews($('#latestNewsSelect option:checked').val(), DrawLatestNewsForm);
        }
    }
}

function DeleteLatestNewsCallback() {
    if (disposition == 'success') {
        $('#latestNewsStatus').text('Success!  Your news was successfully deleted');

        GetLatestNewsList(DeleteLatestNewsRepopulate);
    }
    else {
        $('#latestNewsStatus').text(disposition);
    }
}

function DeleteLatestNewsRepopulate() {
    var latestNewsList = $('#latestNewsSelect');
    latestNewsList.empty();

    latestNewsList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        latestNewsList.append('<option value="' + el.id + '">' + el.date + ' - ' + el.content + '</option>')
    });

    $('#acceptLatestNews').attr('disabled', 'disabled');
}

function UpdateLatestNewsCallback() {
    if (disposition == 'success') {
        $('#latestNewsStatus').text('Success!  Your news was successfully updated');
    }
    else {
        $('#latestNewsStatus').text(disposition);
    }
}

function CreateLatestNewsCallback() {
    if (disposition == 'success') {
        $('#latestNewsStatus').text('Success!  Your news was successfully created');
    }
    else {
        $('#latestNewsStatus').text(disposition);
    }
}

function DeleteLatestNews(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteLatestNews",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function UpdateLatestNews(id, content, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateLatestNews",
        data: "{'id':'" + id + "', 'content':'" + content + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function CreateLatestNews(content, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateLatestNews",
        data: "{'content':'" + content + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetLatestNewsList(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchLatestNewsList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function GetLatestNews(id, callback) {
    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchLatestNews",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}






function DrawUserSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'userSelectLabel').text('Select a User');
    var subjCont = $('<div>').addClass('col-md-3');
    var userList = $('<select>').addClass('form-control').attr('id', 'userSelect').on('change', function () {
        if ($('#userSelect option:checked').val() != '-') {
            $('#acceptUser').removeAttr('disabled');
        }
        else {
            $('#acceptUser').attr('disabled', 'disabled');
        }
    });

    userList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        userList.append('<option value="' + el.id + '">' + el.id + ' - ' + el.fName + ' ' + el.lName + '</option>')
    });

    list = undefined;

    userList.appendTo(subjCont);
    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptUser').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitUser);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);
    var clearer = $('<div>').addClass('clearer');
    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'userStatus');
    status.appendTo(statusCont);

    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}

function DrawUserForm() {
    var form = $('#entityGroup');

    var mainGroup = $('<div>').addClass('form-group');
    var group1 = $('<div>').addClass('form-group'); //Names
    var group2 = $('<div>').addClass('form-group'); //Email
    var group3 = $('<div>').addClass('form-group'); //Phone/Gender/Active
    var group4 = $('<div>').addClass('form-group'); //Start/End Dates
    var group5 = $('<div>').addClass('form-group'); //Username/Pass/Role
    var group6 = $('<div>').addClass('form-group'); //Save
    var group7 = $('<div>').addClass('form-group'); //Status Label

    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group3);
    mainGroup.append(group4);
    mainGroup.append(group5);
    mainGroup.append(group6);
    mainGroup.append(group7);

    var user = list;

    //Labels
    var idLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'startDateLabel').text('ID');
    var startLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'startDateLabel').text('Start Date');
    var endLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'endDateLabel').text('End Date');

    var fNameLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'firstNameLabel').text('First Name');
    var mNameLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'middleNameLabel').text('Middle Name');
    var lNameLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'lastNameLabel').text('Last Name');

    var emailLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'emailAddrLabel').text('Email Address');

    var phoneLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'phoneLabel').text('Phone Number');
    var genderLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'genderLabel').text('Gender');
    var activeLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'activeLabel').text('Is Active');

    var uNameLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'userNameLabel').text('User Name');
    var passLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'passwordLabel').text('Password');
    var roleLbl = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'roleLabel').text('Role');

    //Containers
    var idCont = $('<div>').addClass('col-md-2');
    var startCont = $('<div>').addClass('col-md-2');
    var endCont = $('<div>').addClass('col-md-2');

    var fNameCont = $('<div>').addClass('col-md-2');
    var mNameCont = $('<div>').addClass('col-md-2');
    var lNameCont = $('<div>').addClass('col-md-2');

    var emailCont = $('<div>').addClass('col-md-10');

    var phoneCont = $('<div>').addClass('col-md-2');
    var genderCont = $('<div>').addClass('col-md-2');
    var activeCont = $('<div>').addClass('col-md-2');

    var uNameCont = $('<div>').addClass('col-md-2');
    var passCont = $('<div>').addClass('col-md-2');
    var roleCont = $('<div>').addClass('col-md-2');

    //Clearers
    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');
    var clearer5 = $('<div>').addClass('clearer');
    var clearer6 = $('<div>').addClass('clearer');
    var clearer7 = $('<div>').addClass('clearer');


    //Controls
    var id = $('<input>').attr('type', 'text').attr('id', 'userId').addClass('form-control');
    if (user != undefined) { id.val(user.id); }
    var start = $('<input>').attr('type', 'text').attr('id', 'startDate').addClass('form-control');
    start.datepicker();
    if (user != undefined) { start.val(user.start); }
    var end = $('<input>').attr('type', 'text').attr('id', 'endDate').addClass('form-control');
    end.datepicker();
    if (user != undefined) { end.val(user.end); }


    var fName = $('<input>').attr('type', 'text').attr('id', 'firstName').addClass('form-control');
    if (user != undefined) { fName.val(user.first); }
    var mName = $('<input>').attr('type', 'text').attr('id', 'middleName').addClass('form-control');
    if (user != undefined) { mName.val(user.middle); }
    var lName = $('<input>').attr('type', 'text').attr('id', 'lastName').addClass('form-control');
    if (user != undefined) { lName.val(user.last); }


    var email = $('<input>').attr('type', 'text').attr('id', 'emailAddress').addClass('form-control');
    if (user != undefined) { email.val(user.email); }


    var phone = $('<input>').attr('type', 'text').attr('id', 'phoneNumber').addClass('form-control');
    phone.mask("(999) 999-9999");
    if (user != undefined) { phone.val(user.phone); }
    var gender = $('<select>').attr('id', 'genderList').addClass('form-control');
    var male = $('<option>').val('male').text('Male');
    var female = $('<option>').val('female').text('Female');
    var blankGender = $('<option>').val('-').text('----------');
    gender.append(blankGender);
    gender.append(female);
    gender.append(male);
    if (user != undefined) { gender.val(user.gender); }
    var active = $('<input>').attr('type', 'checkbox').attr('id', 'isActive').addClass('form-control');
    if (user != undefined) { if (user.active) { active.attr('checked', 'checked'); } }


    var uName = $('<input>').attr('type', 'text').attr('id', 'userName').addClass('form-control');
    if (user != undefined) { uName.val(user.username); }
    var pass = $('<input>').attr('type', 'text').attr('id', 'password').addClass('form-control');
    var roleList = $('<select>').attr('id', 'roleList').addClass('form-control');
    var blankRole = $('<option>').val('-').text('----------');
    var adminRole = $('<option>').val('admin').text('Admin');
    var staffRole = $('<option>').val('staff').text('Staff');
    var studentRole = $('<option>').val('student').text('Student');
    var teacherRole = $('<option>').val('teacher').text('Teacher');
    blankRole.appendTo(roleList);
    adminRole.appendTo(roleList);
    staffRole.appendTo(roleList);
    studentRole.appendTo(roleList);
    teacherRole.appendTo(roleList);
    if (user != undefined) { roleList.val(user.role); }

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createUserButton').attr('name', 'createUserButton').addClass('btn btn-success').click(CreateEditUser);
    if (list != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'userStatus');
    status.appendTo(statusCont);


    fNameCont.append(fName);
    mNameCont.append(mName);
    lNameCont.append(lName);

    emailCont.append(email);

    phoneCont.append(phone);
    genderCont.append(gender);
    activeCont.append(active);

    startCont.append(start);
    endCont.append(end);
    idCont.append(id);

    uNameCont.append(uName);
    passCont.append(pass);
    roleCont.append(roleList);

    group1.append(idLbl);
    group1.append(idCont);
    group1.append(startLbl);
    group1.append(startCont);
    group1.append(endLbl);
    group1.append(endCont);
    group1.append(clearer4);

    group2.append(fNameLbl);
    group2.append(fNameCont);
    group2.append(mNameLbl);
    group2.append(mNameCont);
    group2.append(lNameLbl);
    group2.append(lNameCont);
    group2.append(clearer1);

    group3.append(emailLbl);
    group3.append(emailCont);
    group3.append(clearer2);

    group4.append(phoneLbl);
    group4.append(phoneCont);
    group4.append(genderLbl);
    group4.append(genderCont);
    group4.append(activeLbl);
    group4.append(activeCont);
    group4.append(clearer3);

    group5.append(uNameLbl);
    group5.append(uNameCont);
    group5.append(passLbl);
    group5.append(passCont);
    group5.append(roleLbl);
    group5.append(roleCont);
    group5.append(clearer5);

    group6.append(btnCont);
    group6.append(clearer6);

    group7.append(statusCont);
    group7.append(clearer7);

    mainGroup.appendTo(form);
    form.show();
}

function CreateEditUser() {
    if ($('#createUserButton').val() == 'Create') {
        CreateUser($('#userId').val(), $('#firstName').val(), $('#middleName').val(), $('#lastName').val(),
            $('#emailAddress').val(), $('#phoneNumber').val(), $('#genderList option:checked').val(), $('#isActive').is(':checked'),
            $('#startDate').val(), $('#endDate').val(), $('#userName').val(), $('#password').val(), $('#roleList option:checked').val(), CreateUserCallback);
    }
    else {
        UpdateUser($('#userSelect option:checked').val(), $('#userId').val(), $('#firstName').val(), $('#middleName').val(), $('#lastName').val(),
            $('#emailAddress').val(), $('#phoneNumber').val(), $('#genderList option:checked').val(), $('#isActive').is(':checked'),
            $('#startDate').val(), $('#endDate').val(), $('#userName').val(), $('#password').val(), $('#roleList option:checked').val(), UpdateUserCallback);
    }
}

function SubmitUser() {
    if ($('#userSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteUser($('#userSelect option:checked').val(), DeleteUserCallback);
        }
        else {
            GetUser($('#userSelect option:checked').val(), DrawUserForm);
        }
    }
}

function DeleteUserCallback() {
    if (disposition == 'success') {
        $('#userStatus').text('Success!  Your user was successfully deleted');

        GetUserList(DeleteUserRepopulate);
    }
    else {
        $('#userStatus').text(disposition);
    }
}

function DeleteUserRepopulate() {
    var userList = $('#userSelect');
    userList.empty();

    userList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        userList.append('<option value="' + el.id + '">' + el.id + ' - ' + el.fName + ' ' + el.lName + '</option>')
    });

    $('#acceptUser').attr('disabled', 'disabled');
}

function UpdateUserCallback() {
    if (disposition == 'success') {
        $('#userStatus').text('Success!  Your user was successfully updated');
    }
    else {
        $('#userStatus').text(disposition);
    }
}

function CreateUserCallback() {
    if (disposition == 'success') {
        $('#userStatus').text('Success!  Your user was successfully created');
    }
    else {
        $('#userStatus').text(disposition);
    }
}

function CreateUser(id, fn, mn, ln, em, ph, gender, active, start, end, un, pw, role, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateUser",
        data: "{'id':'" + id + "', 'fn':'" + fn + "', 'mn':'" + mn + "', 'ln':'" + ln + "', 'em':'" + em +
            "', 'pn':'" + ph + "', 'gender':'" + gender + "', 'start':'" + start + "', 'end':'" + end +
            "', 'act':'" + active + "', 'un':'" + un + "', 'pw':'" + pw + "', 'role':'" + role + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function UpdateUser(id, newId, fn, mn, ln, em, ph, gender, active, start, end, un, pw, role, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateUser",
        data: "{'id':'" + id + "', 'newId':'" + newId + "', 'fn':'" + fn + "', 'mn':'" + mn + "', 'ln':'" + ln + "', 'em':'" + em +
            "', 'pn':'" + ph + "', 'gender':'" + gender + "', 'start':'" + start + "', 'end':'" + end +
            "', 'act':'" + active + "', 'un':'" + un + "', 'pw':'" + pw + "', 'role':'" + role + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetUserList(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchUserList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function GetUser(id, callback) {
    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchUser",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}

function DeleteUser(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteUser",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}















function DrawClassSubjectSelect() {

    var selectGroup = $('<div>').addClass('form-group');

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectSelectLabel').text('Select a Subject');
    var subjCont = $('<div>').addClass('col-md-3');
    var subjectList = $('<select>').addClass('form-control').attr('id', 'subjectSelectSearch').on('change', DrawClassSelect);

    subjectList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        subjectList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    list = undefined;

    var clearer = $('<div>').addClass('clearer');

    subjCont.append(subjectList);
    
    selectGroup.append(lbl);
    selectGroup.append(subjCont);
    selectGroup.append(clearer);

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}

function DrawClassSelect() {
    if ($('#subjectSelectSearch option:checked').val() != '-') {
        GetClassList($('#subjectSelectSearch option:checked').val(), DrawClassSelectCallback);
    }
}

function DrawClassSelectCallback() {

    var selectGroup = $('<div>').addClass('form-group');

    $($($('#classSelect').parent()).parent()).remove();

    var lbl = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectSelectLabel').text('Select a Class');
    var classCont = $('<div>').addClass('col-md-3');
    var classList = $('<select>').addClass('form-control').attr('id', 'classSelect').on('change', function () {
        if ($('#classSelect option:checked').val() != '') {
            $('#acceptClass').removeAttr('disabled');
        }
        else {
            $('#acceptClass').attr('disabled', 'disabled');
        }
    });

    classList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        classList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    list = undefined;

    var clearer1 = $('<div>').addClass('clearer');

    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptClass').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitClass);
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);

    var clearer2 = $('<div>').addClass('clearer');
    var statusCont = $('<div>').addClass('col-md-12');
    var status = $('<label>').addClass('form-control admin').attr('id', 'classStatus');
    status.appendTo(statusCont);

    classCont.append(classList);

    selectGroup.append(lbl);
    selectGroup.append(classCont);
    selectGroup.append(selCont);
    selectGroup.append(clearer1);

    if ($('#entityActionList option:checked').val() == 'delete') {
        selectGroup.append(statusCont);
    }

    $('#entitySelectGroup').append(selectGroup);
    $('#entitySelectGroup').show();
}

function DrawClassForm() {
    GetSubjectListForClass(DrawClassFormCallback);
}

function DrawClassFormCallback() {
    var form = $('#entityGroup');

    var mClass = list;

    var mainGroup = $('<div>').addClass('form-group');
    var group1 = $('<div>').addClass('form-group'); //Subject List
    var group2 = $('<div>').addClass('form-group'); //Class Name
    var group3 = $('<div>').addClass('form-group'); //Description
    var group4 = $('<div>').addClass('form-group'); //Save
    var group5 = $('<div>').addClass('form-group'); //Status Label

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');
    var clearer5 = $('<div>').addClass('clearer');

    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group3);
    mainGroup.append(group4);
    mainGroup.append(group5);

    // Containers
    var subjectCont = $('<div>').addClass('col-md-3');
    var nameCont = $('<div>').addClass('col-md-4');
    var descCont = $('<div>').addClass('col-md-8');

    //Labels
    var SubjectLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectLabel').text('Subject');
    var classNameLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'classNameLabel').text('Class Name');
    var descriptionLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'descriptionLabel').text('Description');

    group1.append(SubjectLabel);
    group2.append(classNameLabel);
    group3.append(descriptionLabel);


    var subjectList = $('<select>').addClass('form-control').attr('id', 'subjectList');

    subjectList.append('<option value="-">-----------------------</option>');

    $.each(subjects, function (i, el) {
        subjectList.append('<option value="' + el.id + '">' + el.name + '</option>');
    });
    if (mClass != undefined) { subjectList.val(mClass.subjId); subjectList.attr('disabled', 'disabled').css('background-color', 'lightgrey'); }

    var name = $('<input>').attr('type', 'text').attr('id', 'className').addClass('form-control');
    if (mClass != undefined) { name.val(mClass.name); }

    var desc = $('<input>').attr('type', 'text').attr('id', 'classDescription').addClass('form-control');
    if (mClass != undefined) { desc.val(mClass.desc); }

    subjectCont.append(subjectList);
    nameCont.append(name);
    descCont.append(desc);

    group1.append(subjectCont);
    group1.append(clearer1);

    group2.append(nameCont);
    group2.append(clearer2);

    group3.append(descCont);
    group3.append(clearer3);

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createClassButton').attr('name', 'createClassButton').addClass('btn btn-success').click(CreateEditClass);
    if (mClass != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'classStatus');
    status.appendTo(statusCont);

    mClass = undefined;

    group4.append(btnCont);
    group4.append(clearer4);

    group5.append(statusCont);
    group5.append(clearer5);

    mainGroup.appendTo(form);
    form.show();
}

function SubmitClass() {
    if ($('#classSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            DeleteClass($('#classSelect option:checked').val(), DeleteClassCallback);
        }
        else {
            GetClass($('#classSelect option:checked').val(), DrawClassForm);
        }
    }
}

function CreateEditClass() {
    if ($('#className').val() != '') {
        if ($('#createClassButton').val() == 'Create') {
            CreateClass($('#subjectList option:checked').val(), $('#className').val(), $('#classDescription').val(), CreateClassCallback);
        }
        else {
            UpdateClass($('#classSelect option:checked').val(), $('#subjectList option:checked').val(), $('#className').val(), $('#classDescription').val(), UpdateClassCallback);
        }
    }
}

function DeleteClassCallback() {
    if (disposition == 'success') {
        $('#classStatus').text('Success!  Your class was successfully deleted');

        GetClassList($('#subjectSelectSearch option:checked').val(), DeleteClassRepopulate);
    }
    else {
        $('#classStatus').text(disposition);
    }
}

function DeleteClassRepopulate() {
    var classList = $('#classSelect');
    classList.empty();

    classList.append('<option value="-">-----------------------</option>');

    $.each(list, function (i, el) {
        classList.append('<option value="' + el.id + '">' + el.name + '</option>')
    });

    $('#acceptClass').attr('disabled', 'disabled');
}

function CreateClassCallback() {
    if (disposition == 'success') {
        $('#classStatus').text('Success!  Your class was successfully created');
    }
    else {
        $('#classStatus').text(disposition);
    }
}

function UpdateClassCallback() {
    if (disposition == 'success') {
        $('#classStatus').text('Success!  Your class was successfully updated');
    }
    else {
        $('#classStatus').text(disposition);
    }
}

function UpdateClass(id, subjectId, name, desc, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/UpdateClass",
        data: "{'id':'" + id + "', 'subjectId':'" + subjectId + "', 'name':'" + name + "', 'description':'" + desc + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function CreateClass(subjectId, name, desc, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateClass",
        data: "{'subjectId':'" + subjectId + "', 'name':'" + name + "', 'description':'" + desc + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}

function GetSubjectListForClass(callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSubjectList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            subjects = json;
            callback();
        }
    });
}

function GetClassList(subjectId, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClassList",
        data: "{'subjectId':'" + subjectId + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            list = json;
            callback();
        }
    });
}

function GetClass(id, callback) {
    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClass",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            list = json;
            callback();
        }
    });
}

function DeleteClass(id, callback) {

    $.ajax({
        type: "POST",
        url: "Services.asmx/DeleteClass",
        data: "{'id':'" + id + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            disposition = json;
            callback();
        }
    });
}













function DrawMarkingPeriodSelect() {

    var selectGroup = $('<div>').addClass('form-group').attr('id', 'mPeriodSelectGroup');

    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group').attr('id', 'mPeriodGroup2');

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');

    var yearCont = $('<div>').addClass('col-md-3');

    selectGroup.append(group1);
    selectGroup.append(group2);
    group1.append(yearCont);

    var yearLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'periodLabel').text('Select a School Year');

    var yearList = $('<select>').addClass('form-control').attr('id', 'yearListSelect').on('change', function () {
        if ($('#yearList option:checked').val() != '-') {

            $('#entityGroup').empty();
            $($($('#acceptPeriod').parent()).parent()).remove();

            var mPeriodGroup2 = $('#mPeriodGroup2');
            var mPeriodSelectGroup = $('#mPeriodSelectGroup');
            mPeriodGroup2.empty();

            var mPeriodLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'mPeriodLabel').text('Select a Marking Period');

            var periodCont = $('<div>').addClass('col-md-4');

            var mpList = $('<select>').addClass('form-control').attr('id', 'markingPeriodSelect');
            mpList.append($('<option>').val('-').text('-----------------------'));
            mpList.on('change', function () {
                if ($('#markingPeriodSelect option:checked').val() != '-') {
                    $('#acceptPeriod').removeAttr('disabled');
                }
                else {
                    $('#acceptPeriod').attr('disabled', 'disabled');
                }
            });

            var selCont = $('<div>').addClass('col-md-1');
            var select = $('<input>').attr('type', 'button').attr('id', 'acceptPeriod').attr('disabled', 'disabled').addClass('btn btn-primary').click(SubmitMarkingPeriod);
            if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
            select.appendTo(selCont);

            var statusCont = $('<div>').addClass('col-md-12');
            var status = $('<label>').addClass('form-control admin').attr('id', 'markingPeriodStatus');
            status.appendTo(statusCont);

            var group3 = $('<div>').addClass('form-group');
            var group4 = $('<div>').addClass('form-group');

            var clearer3 = $('<div>').addClass('clearer');
            var clearer4 = $('<div>').addClass('clearer');
            
            group3.append(selCont);
            group3.append(clearer3);
            group4.append(statusCont);
            group4.append(clearer4);

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchMarkingPeriodList",
                data: "{'yearId':'" + $('#yearListSelect option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    $.each(json, function (i, el) {
                        mpList.append($('<option>').val(el.id).text((el.name == null ? 'All Year' : el.name) + ': ' + el.start + ' - ' + el.end));
                    });

                    mPeriodGroup2.append(mPeriodLabel);
                    mPeriodGroup2.append(periodCont);
                    periodCont.append(mpList);
                    mPeriodSelectGroup.append(group3);

                    if ($('#entityActionList option:checked').val() == 'delete') {
                        mPeriodSelectGroup.append(group4);
                    }
                }
            });
        }
    });

    yearList.append($('<option>').val('-').text('-----------------------'));

    group1.append(yearLabel);
    group1.append(yearCont);
    yearCont.append(yearList);
    group1.append(clearer1);

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSchoolYearList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            $.each(json, function (i, el) {
                yearList.append($('<option>').val(el.id).text(el.name));
            });

            $('#entitySelectGroup').append(selectGroup);
            $('#entitySelectGroup').show();
        }
    });
}

function DrawMarkingPeriodForm() {

    var form = $('#entityGroup');
    form.empty();

    var mPeriod = list;

    var mainGroup = $('<div>').addClass('form-group');

    var group1 = $('<div>').addClass('form-group'); //School Year Select
    var group2 = $('<div>').addClass('form-group'); //Period // Full Year
    var group3 = $('<div>').addClass('form-group'); //Start/End Dates
    var group4 = $('<div>').addClass('form-group'); //Button
    var group5 = $('<div>').addClass('form-group'); //Status

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');
    var clearer5 = $('<div>').addClass('clearer');

    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group3);
    mainGroup.append(group4);
    mainGroup.append(group5);

    // Containers for controls
    var yearCont = $('<div>').addClass('col-md-3');
    var periodCont = $('<div>').addClass('col-md-3');
    var fullYearCont = $('<div>').addClass('col-md-3');
    var startCont = $('<div>').addClass('col-md-3');
    var endCont = $('<div>').addClass('col-md-3');

    //Labels
    var yearLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'periodLabel').text('School Year');
    
    var periodLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'periodLabel').text('Marking Period');
    var fullYearLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'fullYearLabel').text('All Year');

    var startDateLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'startDateLabel').text('Start Date');
    var endDateLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'endDateLabel').text('End Date');

    var period = $('<input>').attr('type', 'text').attr('id', 'periodName').addClass('form-control');
    if (mPeriod != undefined) { period.val(mPeriod.period); if (mPeriod.allyear) { period.attr('disabled', 'disabled'); } }
    var allyear = $('<input>').attr('type', 'checkbox').attr('id', 'allyear').addClass('form-control');
    if (mPeriod != undefined) { if (mPeriod.allyear) { allyear.attr('checked', 'checked'); } }

    allyear.on('click', function () {
        if ($('#allyear').is(':checked')) {
            $('#periodName').val('');
            $('#periodName').attr('disabled', 'disabled');
        }
        else {
            $('#periodName').val('');
            $('#periodName').removeAttr('disabled');
        }
    });

    var start = $('<input>').attr('type', 'text').attr('id', 'startDate').addClass('form-control');
    start.datepicker();
    if (mPeriod != undefined) { start.val(mPeriod.start); }
    var end = $('<input>').attr('type', 'text').attr('id', 'endDate').addClass('form-control');
    end.datepicker();
    if (mPeriod != undefined) { end.val(mPeriod.end); }

    var yearList = $('<select>').addClass('form-control').attr('id', 'yearList');
    yearList.append($('<option>').val('-').text('-----------------------'));
    yearList.appendTo(yearCont);

    periodCont.append(period);
    fullYearCont.append(allyear);
    startCont.append(start);
    endCont.append(end);

    group1.append(yearLabel);
    group1.append(yearCont);
    group1.append(clearer1);

    group2.append(periodLabel);
    group2.append(periodCont);
    group2.append(fullYearLabel);
    group2.append(fullYearCont);
    group2.append(clearer2);

    group3.append(startDateLabel);
    group3.append(startCont);
    group3.append(endDateLabel);
    group3.append(endCont);
    group3.append(clearer3);

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createMarkingPeriodButton').attr('name', 'createMarkingPeriodButton').addClass('btn btn-success').click(CreateEditMarkingPeriod);
    if (mPeriod != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'markingPeriodStatus');
    status.appendTo(statusCont);

    group4.append(btnCont);
    group4.append(clearer4);
    group5.append(statusCont);
    group5.append(clearer5);

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSchoolYearList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            $.each(json, function (i, el) {
                yearList.append($('<option>').val(el.id).text(el.name));
            });
            if (mPeriod != undefined) { yearList.val(mPeriod.yearId); }

            mPeriod == undefined;

            mainGroup.appendTo(form);
            form.show();
        }
    });
}

function SubmitMarkingPeriod() {
    if ($('#markingPeriodSelect option:checked').val() != '-') {
        if ($('#entityActionList option:checked').val() == 'delete') {
            $.ajax({
                type: "POST",
                url: "Services.asmx/DeleteMarkingPeriod",
                data: "{'id':'" + $('#markingPeriodSelect option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    if (json == 'success') {
                        $('#markingPeriodStatus').text('Success!  Your marking period was successfully deleted');
                    }
                    else {
                        $('#markingPeriodStatus').text(json);
                    }

                    var mpList = $('#markingPeriodSelect');
                    mpList.empty();
                    mpList.append($('<option>').val('-').text('-----------------------'));
                    


                    $.ajax({
                        type: "POST",
                        url: "Services.asmx/FetchMarkingPeriodList",
                        data: "{'yearId':'" + $('#yearListSelect option:checked').val() + "'}",
                        contentType: "application/json; charset=UTF-8",
                        success: function (periods) {
                            var periodJson = JSON.parse(periods.d);

                            $.each(periodJson, function (i, el) {
                                mpList.append($('<option>').val(el.id).text((el.name == null ? 'All Year' : el.name) + ': ' + el.start + ' - ' + el.end));
                            });
                        }
                    });
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchMarkingPeriod",
                data: "{'id':'" + $('#markingPeriodSelect option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    list = json;
                    DrawMarkingPeriodForm();
                }
            });
        }
    }
}

function CreateEditMarkingPeriod() {
    if ($('#markingPeriodName').val() != '') {
        if ($('#createMarkingPeriodButton').val() == 'Create') {
            $.ajax({
                type: "POST",
                url: "Services.asmx/CreateMarkingPeriod",
                data: "{'yearId':'" + $('#yearList option:checked').val() + "', 'name':'" + $('#periodName').val() + "', 'allyear':'" + $('#allyear').is(':checked') + "', 'start':'" + $('#startDate').val() + "', 'end':'" + $('#endDate').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    if (json == 'success') {
                        $('#markingPeriodStatus').text('Success!  Your marking period was successfully created');
                    }
                    else {
                        $('#markingPeriodStatus').text(disposition);
                    }
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: "Services.asmx/UpdateMarkingPeriod",
                data: "{'id':'" + $('#markingPeriodSelect option:checked').val() + "', 'yearId':'" + $('#yearList option:checked').val() +
                    "', 'name':'" + $('#periodName').val() + "', 'allyear':'" + $('#allyear').is(':checked') + "', 'start':'" + $('#startDate').val() + "', 'end':'" + $('#endDate').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    if (json == 'success') {
                        $('#markingPeriodStatus').text('Success!  Your marking period was successfully created');
                    }
                    else {
                        $('#markingPeriodStatus').text(json);
                    }
                }
            });
        }
    }
}









function DrawClubScheduleSelect() {

    if ($('#entityActionList option:checked').val() == 'update') {
        var noUpdate = $('<label>').addClass('col-md-12 control-label').attr('id', 'noUpdate').text('Only create and delete actions are available for club schedule.');
        $('#entitySelectGroup').append(noUpdate);
        $('#entitySelectGroup').show();
        return;
    }

    var selectGroup = $('<div>').addClass('form-group').attr('id', 'clubSchSelectGroup');

    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group').attr('id', 'clubSchGroup2');
    var group3 = $('<div>').addClass('form-group').attr('id', 'clubSchGroup3');

    var clearer1 = $('<div>').addClass('clearer');

    selectGroup.append(group1);
    selectGroup.append(group2);
    selectGroup.append(group3);

    var yearCont = $('<div>').addClass('col-md-3');

    var yearLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'periodLabel').text('Select a School Year');

    var yearList = $('<select>').addClass('form-control').attr('id', 'yearListSelect').on('change', function () {
        if ($('#yearList option:checked').val() != '-') {

            $('#entityGroup').empty();
            $($($('#acceptPeriod').parent()).parent()).remove();
            var clearer2 = $('<div>').addClass('clearer');

            var clubSchGroup2 = $('#clubSchGroup2');
            var clubSchSelectGroup = $('#clubSchSelectGroup');
            clubSchGroup2.empty();

            var clubLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'clubLabel').text('Select a Club');

            var clubCont = $('<div>').addClass('col-md-4');

            var clubList = $('<select>').addClass('form-control').attr('id', 'clubSelect')
                .append($('<option>').val('-').text('-----------------------'))
                .on('change', function () {
                    var clubSchGroup3 = $('#clubSchGroup3');
                    clubSchGroup3.empty();

                    var schedLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'schedLabel').text('Select a Schedule');
                    var schedCont = $('<div>').addClass('col-md-4');
                    var schedList = $('<select>').addClass('form-control').attr('id', 'schedSelect')
                        .append($('<option>').val('-').text('-----------------------')).on('change', function () {
                        if ($('#schedSelect option:checked').val() != '-') {
                            $('#acceptClubSchedule').removeAttr('disabled');
                        }
                        else {
                            $('#acceptClubSchedule').attr('disabled', 'disabled');
                        }
                    });
                    schedList.appendTo(schedCont);

                    var selCont = $('<div>').addClass('col-md-1');
                    var select = $('<input>').attr('type', 'button').attr('id', 'acceptClubSchedule').attr('disabled', 'disabled').addClass('btn btn-primary').val('Delete').click(function () {
                        //Delete Club Schedule
                        var ids = $('#schedSelect option:checked').val().split(':');
                        $.ajax({
                            type: "POST",
                            url: "Services.asmx/DeleteClubSchedule",
                            data: "{'id':'" + $('#clubSelect option:checked').val() + "', 'yearId':'" + $('#yearListSelect option:checked').val() + "', 'day':'" + ids[1] + "', 'tsId':'" + ids[0] + "'}",
                            contentType: "application/json; charset=UTF-8",
                            success: function (data) {
                                var json = JSON.parse(data.d);

                                if (json == 'success') {
                                    $('#clubScheduleStatus').text('Success!  Your club schedule was successfully deleted');
                                }
                                else {
                                    $('#clubScheduleStatus').text(disposition);
                                }
                            }
                        });
                    });
                    select.appendTo(selCont);

                    var statusCont = $('<div>').addClass('col-md-12');
                    var status = $('<label>').addClass('form-control admin').attr('id', 'clubScheduleStatus');
                    status.appendTo(statusCont);

                    var group3 = $('<div>').addClass('form-group');
                    var group4 = $('<div>').addClass('form-group');

                    var clearer3 = $('<div>').addClass('clearer');
                    var clearer4 = $('<div>').addClass('clearer');


                    $.ajax({
                        type: "POST",
                        url: "Services.asmx/FetchClubSchedule",
                        data: "{'id':'" + $('#clubSelect option:selected').val() + "', 'yearId':'" + $('#yearListSelect option:selected').val() + "'}",
                        contentType: "application/json; charset=UTF-8",
                        success: function (data) {
                            var json = JSON.parse(data.d);

                            $.each(json, function (i, el) {
                                schedList.append($('<option>').val(el.id + ':' + el.day).text(el.day + ': ' + el.start + ' - ' + el.end));
                            });

                            clubSchGroup3.append(schedLabel);
                            clubSchGroup3.append(schedCont);
                            clubSchGroup3.append(selCont);
                            clubSchGroup3.append(clearer3);

                            group4.append(statusCont);
                            group4.append(clearer4);

                            clubSchSelectGroup.append(group4);
                        }
                    });
                });

            clubList.appendTo(clubCont);

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchClubList",
                data: "{}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    clubSchGroup2.append(clubLabel);
                    clubSchGroup2.append(clubCont);
                    clubSchGroup2.append(clearer2);

                    $.each(json, function (i, el) {
                        clubList.append($('<option>').val(el.id).text(el.name));
                    });
                }
            });

        }
    });
    yearList.append($('<option>').val('-').text('-----------------------'));

    group1.append(yearLabel);    
    group1.append(yearCont);
    yearList.appendTo(yearCont);
    group1.append(clearer1);

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSchoolYearList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            $.each(json, function (i, el) {
                yearList.append($('<option>').val(el.id).text(el.name));
            });

            $('#entitySelectGroup').append(selectGroup);
            $('#entitySelectGroup').show();
        }
    });
}


function DrawClubScheduleForm_Pre() {
    var clubs;
    var timeslots;
    var years;

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClubList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);
            clubs = json;

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchTimeSlotList",
                data: "{}",
                contentType: "application/json; charset=UTF-8",
                success: function (ts) {
                    var json = JSON.parse(ts.d);
                    timeslots = json;

                    $.ajax({
                        type: "POST",
                        url: "Services.asmx/FetchSchoolYearList",
                        data: "{}",
                        contentType: "application/json; charset=UTF-8",
                        success: function (ts) {
                            var json = JSON.parse(ts.d);
                            years = json;

                            DrawClubScheduleForm(clubs, timeslots, years);
                        }
                    });
                }
            });
        }
    });
}

function DrawClubScheduleForm(clubs, timeslots, years) {
    var form = $('#entityGroup');
    form.empty();

    var mPeriod = list;

    var mainGroup = $('<div>').addClass('form-group');

    var group1 = $('<div>').addClass('form-group'); //Selects
    var group1a = $('<div>').addClass('form-group');//Selects
    var group2 = $('<div>').addClass('form-group'); //Create
    var group3 = $('<div>').addClass('form-group'); //Status

    var clearer1 = $('<div>').addClass('clearer');
    var clearer1a = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');

    mainGroup.append(group1);
    mainGroup.append(group1a);
    mainGroup.append(group2);
    mainGroup.append(group3);

    // Containers for controls
    var clubCont = $('<div>').addClass('col-md-3');
    var dayCont = $('<div>').addClass('col-md-3');
    var timeCont = $('<div>').addClass('col-md-3');
    var yearCont = $('<div>').addClass('col-md-3');

    //Labels
    var clubLabel = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'clubLabel').text('Club');
    var timeLabel = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'timeLabel').text('Time Slot');
    var dayLabel = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'dayLabel').text('Day');
    var yearLabel = $('<label>').addClass('col-md-2 control-label admin').attr('id', 'yearLabel').text('School Year');

    var clubList = $('<select>').addClass('form-control').attr('id', 'clubSelect');
    var timeList = $('<select>').addClass('form-control').attr('id', 'timeSelect');
    var dayList = $('<select>').addClass('form-control').attr('id', 'daySelect');
    var yearList = $('<select>').addClass('form-control').attr('id', 'yearSelect');

    dayList.append($('<option>').val('-').text('-----------------------'));
    timeList.append($('<option>').val('-').text('-----------------------'));
    clubList.append($('<option>').val('-').text('-----------------------'));
    yearList.append($('<option>').val('-').text('-----------------------'));

    dayList.append($('<option>').val('Monday').text('Monday'));
    dayList.append($('<option>').val('Tuesday').text('Tuesday'));
    dayList.append($('<option>').val('Wednesday').text('Wednesday'));
    dayList.append($('<option>').val('Thursday').text('Thursday'));
    dayList.append($('<option>').val('Friday').text('Friday'));
    dayList.append($('<option>').val('Saturday').text('Saturday'));
    dayList.append($('<option>').val('Sunday').text('Sunday'));

    $.each(clubs, function (i, el) {
        clubList.append($('<option>').val(el.id).text(el.name));
    });

    $.each(timeslots, function (i, el) {
        timeList.append($('<option>').val(el.id).text(el.start + ' - ' + el.end));
    });

    $.each(years, function (i, el) {
        yearList.append($('<option>').val(el.id).text(el.name));
    });

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createClubScheduleButton').attr('name', 'createClubScheduleButton').addClass('btn btn-success').click(CreateClubSchedule).val('Create');
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'clubScheduleStatus');
    status.appendTo(statusCont);

    group2.append(btnCont);
    group2.append(clearer2);

    group3.append(statusCont);
    group3.append(clearer3);

    clubList.appendTo(clubCont);
    timeList.appendTo(timeCont);
    dayList.appendTo(dayCont);
    yearList.appendTo(yearCont);

    group1.append(yearLabel);
    group1.append(yearCont);
    group1.append(clubLabel);
    group1.append(clubCont);
    group1.append(clearer1);

    group1a.append(timeLabel);
    group1a.append(timeCont);
    group1a.append(dayLabel);
    group1a.append(dayCont);
    group1a.append(clearer1a);

    mainGroup.appendTo(form);
    form.show();
}

function CreateClubSchedule() {
    $.ajax({
        type: "POST",
        url: "Services.asmx/CreateClubSchedule",
        data: "{'clubId':'" + $('#clubSelect option:checked').val() + "', 'timeId':'" + $('#timeSelect option:checked').val() + "', 'day':'" + $('#daySelect option:checked').val() + "', 'yearId':'" + $('#yearSelect option:checked').val() + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            if (json == 'success') {
                $('#clubScheduleStatus').text('Success!  Your club schedule was successfully created');
            }
            else {
                $('#clubScheduleStatus').text(json);
            }
        }
    });
}










function CreateSectionSelect() {
    var selectGroup = $('<div>').addClass('form-group').attr('id', 'sectionSelectGroup');

    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group').attr('id', 'sectionGroup2');
    var group3 = $('<div>').addClass('form-group').attr('id', 'sectionGroup3');
    var group4 = $('<div>').addClass('form-group').attr('id', 'sectionGroup4');
    var group5 = $('<div>').addClass('form-group').attr('id', 'sectionGroup5');
    var group6 = $('<div>').addClass('form-group').attr('id', 'sectionGroup6');

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');

    selectGroup.append(group1);
    selectGroup.append(group2);
    selectGroup.append(group3);
    selectGroup.append(group4);
    selectGroup.append(group5);
    selectGroup.append(group6);

    var subjCont = $('<div>').addClass('col-md-3');
    var classCont = $('<div>').addClass('col-md-3');
    var sectionCont = $('<div>').addClass('col-md-3');
    var nameCont = $('<div>').addClass('col-md-3');
    var descCont = $('<div>').addClass('col-md-3');

    var subjectSelectLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectSelectLabel').text('Select a Subject');
    var classSelectLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'classSelectLabel').text('Select a Class');
    var sectionSelectLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'sectionSelectLabel').text('Select a Section');
    var nameLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'nameLabel').text('Section Name');
    var descriptionLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'descriptionLabel').text('Description');

    var sectionName = $('<input>').attr('type', 'text').attr('id', 'sectionName').addClass('form-control');
    var description = $('<input>').attr('type', 'text').attr('id', 'description').addClass('form-control');

    var sectionList = $('<select>').addClass('form-control').attr('id', 'sectionList').on('change', function () {
        if ($('#sectionList option:checked').val() != '-') {
            $('#acceptSection').removeAttr('disabled');
        }
        else {
            $('#acceptSection').attr('disabled', 'disabled');
        }
    });
    sectionList.appendTo(sectionCont);

    var statusCont = $('<div>').addClass('col-md-12');
    var status = $('<label>').addClass('form-control admin').attr('id', 'sectionStatus');
    status.appendTo(statusCont);

    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptSection').attr('disabled', 'disabled').addClass('btn btn-primary').click(function () {

        if ($('#entityActionList option:checked').val() == 'delete') {
            $.ajax({
                type: "POST",
                url: "Services.asmx/DeleteSection",
                data: "{'id':'" + $('#sectionList option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    var sectionGroup4 = $('#sectionGroup4');
                    statusCont.appendTo(sectionGroup4);

                    if (json == 'success') {
                        $('#sectionStatus').text('Success!  Your section was successfully deleted');
                        $('#sectionList option[value="' + $('#sectionList option:checked').val() + '"]').remove();
                        $('#sectionList').prop('selectedIndex', 0);
                    }
                    else {
                        $('#sectionStatus').text(json);
                    }
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchSection",
                data: "{'id':'" + $('#sectionList option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    var sectionGroup4 = $('#sectionGroup4');
                    var sectionGroup5 = $('#sectionGroup5');
                    var sectionGroup6 = $('#sectionGroup6');
                    var clearer5 = $('<div>').addClass('clearer');
                    var clearer6 = $('<div>').addClass('clearer');

                    sectionName.val(json.name);
                    description.val(json.desc);

                    sectionGroup4.append(nameLabel);
                    sectionGroup4.append(nameCont);
                    nameCont.append(sectionName);
                    sectionGroup4.append(descriptionLabel);
                    sectionGroup4.append(descCont);
                    descCont.append(description);
                    sectionGroup4.append(clearer4);

                    var btnCont = $('<div>').addClass('col-md-12');
                    var btn = $('<input>').attr('type', 'button').attr('id', 'updateSectionButton').attr('name', 'updateSectionButton').addClass('btn btn-success').val('Update').click(function () {
                        $.ajax({
                            type: "POST",
                            url: "Services.asmx/UpdateSection",
                            data: "{'subjectId':'" + $('#subjList option:checked').val() + "', 'classId':'" + $('#classList option:checked').val() + "', 'id':'" + $('#sectionList option:checked').val() +
                                "', 'name':'" + $('#sectionName').val() + "', 'desc':'" + $('#description').val() + "'}",
                            contentType: "application/json; charset=UTF-8",
                            success: function (data) {
                                var json = JSON.parse(data.d);

                                if (json == 'success') {
                                    $('#sectionStatus').text('Success!  Your section was successfully created');
                                }
                                else {
                                    $('#sectionStatus').text(json);
                                }
                            }
                        });
                    });
                    btn.appendTo(btnCont);

                    var statusCont = $('<div>');
                    var status = $('<label>').addClass('form-control admin').attr('id', 'sectionStatus');
                    status.appendTo(statusCont);

                    btnCont.appendTo(sectionGroup5);
                    clearer5.appendTo(sectionGroup5);

                    statusCont.appendTo(sectionGroup6);
                    clearer6.appendTo(sectionGroup6);
                }
            });
        }
    });
    if ($('#entityActionList option:checked').val() != 'delete') { select.val('OK') } else { select.val('Delete') }
    select.appendTo(selCont);
    

    var classList = $('<select>').addClass('form-control').attr('id', 'classList').append($('<option>').val('-').text('-----------------------')).on('change', function () {
        if ($('#classList option:checked').val() != '-') {

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchSectionList",
                data: "{'classId':'" + $('#classList option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    sectionList.empty();
                    sectionList.append($('<option>').val('-').text('-----------------------'));

                    $.each(json, function (i, el) {
                        sectionList.append($('<option>').val(el.id).text(el.name));
                    });

                    group3.append(sectionSelectLabel);
                    group3.append(sectionCont);
                    group3.append(selCont);
                    group3.append(clearer3);
                }
            });
        }
    });
    classList.appendTo(classCont);

    var subjList = $('<select>').addClass('form-control').attr('id', 'subjList').append($('<option>').val('-').text('-----------------------')).on('change', function () {
        if ($('#subjList option:checked').val() != '-') {

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchClassList",
                data: "{'subjectId':'" + $('#subjList option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    sectionList.empty();
                    sectionList.append($('<option>').val('-').text('-----------------------'));

                    classList.empty();
                    classList.append($('<option>').val('-').text('-----------------------'));

                    $.each(json, function (i, el) {
                        classList.append($('<option>').val(el.id).text(el.name));
                    });

                    group2.append(classSelectLabel);
                    group2.append(classCont);
                    group2.append(clearer2);
                }
            });
        }
    });

    group1.append(subjectSelectLabel);
    group1.append(subjCont);
    subjList.appendTo(subjCont);
    group1.append(clearer1);

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSubjectList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            $.each(json, function (i, el) {
                subjList.append($('<option>').val(el.id).text(el.name));
            });

            $('#entitySelectGroup').append(selectGroup);
            $('#entitySelectGroup').show();
        }
    });
}


function CreateSectionForm_Pre() {
    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchSubjectList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            CreateSectionForm(json);
        }
    });
}

function CreateSectionForm(sections) {

    var form = $('#entityGroup');
    form.empty();

    var mPeriod = list;

    var mainGroup = $('<div>').addClass('form-group');

    var group1 = $('<div>').addClass('form-group'); //Class Select
    var group2 = $('<div>').addClass('form-group'); //Name/Description
    var group3 = $('<div>').addClass('form-group'); //Button
    var group4 = $('<div>').addClass('form-group'); //Status

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');

    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group3);
    mainGroup.append(group4);

    // Containers for controls
    var subjectCont = $('<div>').addClass('col-md-3');
    var classCont = $('<div>').addClass('col-md-3');
    var nameCont = $('<div>').addClass('col-md-3');
    var descCont = $('<div>').addClass('col-md-3');

    //Labels
    var subjectLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'subjectLabel').text('Subject');
    var classLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'classLabel').text('Class');
    var nameLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'nameLabel').text('Section Name');
    var descLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'descLabel').text('Description');

    group1.append(subjectLabel);
    group1.append(subjectCont);
    group1.append(classLabel);
    group1.append(classCont);
    group1.append(clearer1);

    group2.append(nameLabel);
    group2.append(nameCont);
    group2.append(descLabel);
    group2.append(descCont);
    group2.append(clearer2);

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createSectionButton').attr('name', 'createSectionButton').addClass('btn btn-success').click(CreateEditSection);
    if (mPeriod != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'sectionStatus');
    status.appendTo(statusCont);

    group3.append(btnCont);
    group3.append(clearer3);
    group4.append(statusCont);
    group4.append(clearer4);

    var sectionName = $('<input>').attr('type', 'text').attr('id', 'sectionName').addClass('form-control');
    var description = $('<input>').attr('type', 'text').attr('id', 'description').addClass('form-control');

    nameCont.append(sectionName);
    descCont.append(description);

    var subjectSelect = $('<select>').addClass('form-control').attr('id', 'subjectSelect');
    var classSelect = $('<select>').addClass('form-control').attr('id', 'classSelect').attr('disabled', 'disabled').css('background-color', 'lightgrey');

    subjectSelect.append($('<option>').val('-').text('-----------------------'));
    classSelect.append($('<option>').val('-').text('-----------------------'));

    $.each(sections, function (i, el) {
        subjectSelect.append($('<option>').val(el.id).text(el.name));
    });

    subjectSelect.on('change', function () {
        if ($('#subjectSelect option:checked').val() != '-') {
            classSelect.removeAttr('disabled').css('background-color', 'white');
            classSelect.empty();
            classSelect.append($('<option>').val('-').text('-----------------------'));

            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchClassList",
                data: "{'subjectId':'" + $('#subjectSelect option:checked').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    $.each(json, function (i, el) {
                        classSelect.append($('<option>').val(el.id).text(el.name));
                    });
                }
            });
        }
    });

    subjectSelect.appendTo(subjectCont);
    classSelect.appendTo(classCont);

    mainGroup.appendTo(form);
    form.show();
}

function CreateEditSection() {
    if ($('#sectionName').val() != '') {
        if ($('#createSectionButton').val() == 'Create') {

            $.ajax({
                type: "POST",
                url: "Services.asmx/CreateSection",
                data: "{'subjectId':'" + $('#subjectSelect option:checked').val() + "', 'classId':'" + $('#classSelect option:checked').val() + "', 'name':'" + $('#sectionName').val() + "', 'desc':'" + $('#description').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var json = JSON.parse(data.d);

                    if (json == 'success') {
                        $('#sectionStatus').text('Success!  Your section \'' + $('#sectionName').val() + '\' was successfully created');
                    }
                    else {
                        $('#sectionStatus').text(json);
                    }
                }
            });
        }
    }
}











function DrawGradeSelect() {

    if ($('#entityActionList option:checked').val() == 'update') {

        var noUpdate = $('<label>').addClass('col-md-12 control-label').attr('id', 'noUpdate').text('Only create and delete actions are available for grades.');
        $('#entitySelectGroup').append(noUpdate);
        $('#entitySelectGroup').show()
        return;
    }
    var selectGroup = $('<div>').addClass('form-group').attr('id', 'gradeSelectGroup');

    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group');

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');

    selectGroup.append(group1);
    selectGroup.append(group2);

    var gradeCont = $('<div>').addClass('col-md-3');

    var gradeLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'gradeLabel').text('Select a Grade');

    var gradeList = $('<select>').addClass('form-control').attr('id', 'gradeSelect').on('change', function () {
        if ($('#gradeSelect option:checked').val() != '') {
            $('#acceptGrade').removeAttr('disabled');
        }
        else {
            $('#acceptGrade').attr('disabled', 'disabled');
        }
    });

    var selCont = $('<div>').addClass('col-md-1');
    var select = $('<input>').attr('type', 'button').attr('id', 'acceptGrade').attr('disabled', 'disabled').addClass('btn btn-primary').click(function () {
        $.ajax({
            type: "POST",
            url: "Services.asmx/DeleteGrade",
            data: "{'grade':'"+$('#gradeSelect option:checked').val()+"'}",
            contentType: "application/json; charset=UTF-8",
            success: function (data) {
                var json = JSON.parse(data.d);

                if (json == 'success') {
                    $('#gradeStatus').text('Success!  Your grade \'' + $('#gradeSelect option:checked').val() + '\' was successfully deleted');
                    $('#gradeSelect option:checked').remove();
                    $('#gradeSelect').prop('selectedIndex', 0);
                    if ($('#gradeSelect option:checked').val() == '-') {
                        $('#acceptGrade').attr('disabled', 'disabled');
                    }
                }
                else {
                    $('#gradeStatus').text(json);
                }
            }
        });
    }).val('Delete');
    select.appendTo(selCont);

    gradeList.append($('<option>').val('-').text('----------'));
    gradeList.appendTo(gradeCont);

    group1.append(gradeLabel);
    group1.append(gradeCont);
    group1.append(selCont);
    group1.append(clearer1);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'gradeStatus');
    status.appendTo(statusCont);

    group2.append(statusCont);
    group2.append(clearer2);

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchGradeList",
        data: "{}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            $('#entitySelectGroup').append(selectGroup);
            $('#entitySelectGroup').show();

            $.each(json, function (i, el) {
                gradeList.append($('<option>').val(el.grade).text(el.grade));
            });
        }
    });
}



function DrawGradeForm() {
    var form = $('#entityGroup');
    form.empty();

    var mPeriod = list;

    var mainGroup = $('<div>').addClass('form-group');

    var group1 = $('<div>').addClass('form-group'); //Grade/Button
    var group2 = $('<div>').addClass('form-group'); //Status

    var clearer1 = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');

    mainGroup.append(group1);
    mainGroup.append(group2);

    // Containers for controls
    var gradeCont = $('<div>').addClass('col-md-3');

    //Labels
    var gradeLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'gradeLabel').text('Grade');
    var grade = $('<input>').attr('type', 'text').attr('id', 'grade').addClass('form-control');

    group1.append(gradeLabel);
    gradeCont.append(grade);
    group1.append(gradeCont);

    var btnCont = $('<div>').addClass('col-md-1');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createGradeButton').attr('name', 'createGradeButton').addClass('btn btn-success').val('Create').click(function () {
        $.ajax({
            type: "POST",
            url: "Services.asmx/CreateGrade",
            data: "{'grade':'" + $('#grade').val() + "'}",
            contentType: "application/json; charset=UTF-8",
            success: function (data) {
                var json = JSON.parse(data.d);

                if (json == 'success') {
                    $('#gradeStatus').text('Success!  Your grade \'' + $('#grade').val() + '\' was successfully created');
                }
                else {
                    $('#gradeStatus').text(json);
                }
            }
        });
    });
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'gradeStatus');
    status.appendTo(statusCont);

    group1.append(btnCont);
    group1.append(clearer1);

    group1.append(statusCont);


    mainGroup.appendTo(form);
    form.show();
}












function DrawClubEnrollmentForm_Pre() {
    $('#entitySelectGroup').empty();
    $('#entityGroup').empty();

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClubList",
        data: "{'grade':'" + $('#grade').val() + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            DrawClubEnrollmentForm(json);
        }
    });
}

function DrawClubEnrollmentForm(clubs) {
 
    var form = $('#entitySelectGroup');

    form.empty();

    var mPeriod = list;

    var mainGroup = $('<div>').addClass('form-group');

    var group1 = $('<div>').addClass('form-group'); //Grade/Button

    var clearer1 = $('<div>').addClass('clearer');

    mainGroup.append(group1);

    // Containers for controls
    var clubCont = $('<div>').addClass('col-md-3');

    //Labels
    var clubLabel = $('<label>').addClass('col-md-3 control-label admin').attr('id', 'clubLabel').text('Select a Club');

    var clubList = $('<select>').addClass('form-control').attr('id', 'clubSelect');
    clubList.append($('<option>').val('-').text('-----------------------'));

    $.each(clubs, function (i, el) {
        clubList.append($('<option>').val(el.id).text(el.name));
    });

    var btnCont = $('<div>').addClass('col-md-1');
    var btn = $('<input>').attr('type', 'button').attr('id', 'populateClubButton').attr('name', 'populateClubButton').addClass('btn btn-primary').click(PopulateClubRoster_Pre).val('OK');
    btn.appendTo(btnCont);

    clubList.appendTo(clubCont);
    clubLabel.appendTo(group1);
    clubCont.appendTo(group1);
    btnCont.appendTo(group1);
    clearer1.appendTo(group1);

    mainGroup.appendTo(form);
    form.show();
}

function PopulateClubRoster_Pre() {

    $.ajax({
        type: "POST",
        url: "Services.asmx/FetchClubRoster",
        data: "{'id':'" + $('#clubSelect option:checked').val() + "'}",
        contentType: "application/json; charset=UTF-8",
        success: function (data) {
            var json = JSON.parse(data.d);

            PopulateClubRoster(json);
        }
    });
}

function PopulateClubRoster(roster) {
    var form = $('#entityGroup');
    form.empty();

    var mainGroup = $('<div>').addClass('form-group');

    var group1 = $('<div>').addClass('form-group');
    var group2 = $('<div>').addClass('form-group');
    var group2a = $('<div>').addClass('form-group');
    var group3 = $('<div>').addClass('form-group');
    var group4 = $('<div>').addClass('form-group');

    var clearer1 = $('<div>').addClass('clearer');
    var btnClearer = $('<div>').addClass('clearer');
    var clearer2 = $('<div>').addClass('clearer');
    var clearer2a = $('<div>').addClass('clearer');
    var clearer3 = $('<div>').addClass('clearer');
    var clearer4 = $('<div>').addClass('clearer');

    mainGroup.append(group1);
    mainGroup.append(group2);
    mainGroup.append(group2a);
    mainGroup.append(group3);
    mainGroup.append(group4);

    // Containers for controls
    var availCont = $('<div>').addClass('col-md-4');
    var enrollCont = $('<div>').addClass('col-md-4');
    var btnCont = $('<div>').addClass('col-md-2');
    var saveBtnCont = $('<div>').addClass('col-md-11 admin').css('text-align', 'right');
    var padCont = $('<div>').addClass('col-md-1');
    var leaderCont = $('<div>').addClass('col-md-1');

    var availLabel = $('<label>').addClass('col-md-6 control-label').attr('id', 'availLabel').text('Available Users');
    var enrollLabel = $('<label>').addClass('col-md-6 control-label').attr('id', 'enrollLabel').text('Enrolled Users');
    var leaderLabel = $('<label>').addClass('col-md-10 control-label admin').attr('id', 'leaderLabel').text('Is Leader?');

    var avail = $('<select>').attr('size', '10').attr('id', 'avail').addClass('form-control');
    var enroll = $('<select>').attr('size', '10').attr('id', 'enroll').addClass('form-control').on('click', function () {
        if ($('#enroll option:selected').attr('data-leader') == '1') {
            $('#leaderChk').prop('checked', true)
        }
        else {
            $('#leaderChk').prop('checked', false);
        }
    });

    var leader = $('<input>').attr('type', 'checkbox').attr('id', 'leaderChk').addClass('form-control').on('click', function () {
        if ($('#leaderChk').is(':checked')) {
            $('#enroll option:selected').attr('data-leader', '1');
        }
        else {
            $('#enroll option:selected').attr('data-leader', '0');
        }
    });
    leader.appendTo(leaderCont);

    var addBtn = $('<input>').attr('type', 'button').addClass('btn btn-primary add').val('Add').click(function () {
        var user = $('#avail option:selected');
        user.remove();
        user.removeAttr('data-leader');
        $('#enroll').append(user);
        $('#enroll option:selected').removeAttr('selected');
    });
    var delBtn = $('<input>').attr('type', 'button').addClass('btn btn-warning remove').val('Remove').click(function () {
        var user = $('#enroll option:selected');
        user.remove();
        user.attr('data-leader', '0');
        $('#avail').append(user);
        $('#avail option:selected').removeAttr('selected');
    });

    addBtn.appendTo(btnCont);
    btnClearer.appendTo(btnCont);
    delBtn.appendTo(btnCont);

    $.each(roster, function (i, el) {
        if (el.enrolled) {
            enroll.append($('<option>').val(el.id).attr('data-leader', el.leader).text(el.name));
        }
        else {
            avail.append($('<option>').val(el.id).text(el.name));
        }
    });

    availLabel.appendTo(group1);
    enrollLabel.appendTo(group1);
    clearer1.appendTo(group1)

    padCont.appendTo(group2)
    avail.appendTo(availCont);
    availCont.appendTo(group2);
    btnCont.appendTo(group2);
    enroll.appendTo(enrollCont);
    enrollCont.appendTo(group2);
    clearer2.appendTo(group2);

    leaderLabel.appendTo(group2a);
    leaderCont.appendTo(group2a);
    clearer2a.appendTo(group2a);

    saveBtnCont.appendTo(group3);
    clearer3.appendTo(group3);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'enrollStatus');
    status.appendTo(statusCont);

    group4.append(statusCont);
    group4.append(clearer4);

    var saveBtn = $('<input>').attr('type', 'button').addClass('btn btn-success wide').val('Save').click(function () {

        var ids = '';

        $.each($('#enroll option'), function (i, el) {
            var option = $(el);
            ids += option.val() + ':' + (option.attr('data-leader') == '1' ? 'true' : 'false') + ',';
        });

        $.ajax({
            type: "POST",
            url: "Services.asmx/UpdateClubRoster",
            data: "{'clubId':'" + $('#clubSelect option:checked').val() + "', 'ids':'" + ids + "'}",
            contentType: "application/json; charset=UTF-8",
            success: function (data) {
                var json = JSON.parse(data.d);

                if (json == 'success') {
                    $('#enrollStatus').text('Success!  Club enrollment was successfully updated.');
                }
                else {
                    $('#enrollStatus').text(json);
                }
            }
        });
    });

    saveBtn.appendTo(saveBtnCont);

    mainGroup.appendTo(form);
    form.show();
}