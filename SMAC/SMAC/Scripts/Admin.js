﻿var list;
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

        }
        else if (eType == 2) {
            //Club
            DrawClubForm();
        }
        else if (eType == 3) {
            //Club Enrollment
        }
        else if (eType == 4) {
            //Club Schedule

        }
        else if (eType == 5) {
            //Marking Period

        }
        else if (eType == 6) {
            //News 
            DrawLatestNewsForm();
        }
        else if (eType == 7) {
            //Section

        }
        else if (eType == 8) {
            //Section Schedule

        }
        else if (eType == 9) {
            //School Year
            DrawSchoolYearForm();
        }
        else if (eType == 10) {
            //Student Enrollment

        }
        else if (eType == 11) {
            //Subject
            DrawSubjectForm();
        }
        else if (eType == 12) {
            //Time Span
            DrawTimeSlotForm();
        }
        else if (eType == 13) {
            //User
            DrawUserForm();
        }
    }
    else if (($('#entityActionList option:checked').val() == 'update') ||
        ($('#entityActionList option:checked').val() == 'delete')) {
        // Show List

        if (eType == 1) {
            //Class

        }
        else if (eType == 2) {
            //Club
            GetClubList(DrawClubSelect);

        }
        else if (eType == 3) {
            //Club Enrollment
        }
        else if (eType == 4) {
            //Club Schedule

        }
        else if (eType == 5) {
            //Marking Period

        }
        else if (eType == 6) {
            //News 
            GetLatestNewsList(DrawLatestNewsSelect);
        }
        else if (eType == 7) {
            //Section

        }
        else if (eType == 8) {
            //Section Schedule

        }
        else if (eType == 9) {
            //School Year
            GetSchoolYearList(DrawSchoolYearSelect);
        }
        else if (eType == 10) {
            //Student Enrollment

        }
        else if (eType == 11) {
            //Subject
            GetSubjectList(DrawSubjectSelect);
        }
        else if (eType == 12) {
            //Time Span
            GetTimeSlotList(DrawTimeSlotSelect);
        }
        else if (eType == 13) {
            //User

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
        timeSlotList.append('<option value="' + el.id + '">' + el.start + ' - ' + el.end + '</option>')
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
    if (list != undefined) { id.val(name.id); }
    var start = $('<input>').attr('type', 'text').attr('id', 'startDate').addClass('form-control');
    start.datepicker();
    if (list != undefined) { start.val(name.start); }
    var end = $('<input>').attr('type', 'text').attr('id', 'endDate').addClass('form-control');
    end.datepicker();
    if (list != undefined) { end.val(name.end); }


    var fName = $('<input>').attr('type', 'text').attr('id', 'firstName').addClass('form-control');
    if (list != undefined) { fName.val(name.first); }
    var mName = $('<input>').attr('type', 'text').attr('id', 'middleName').addClass('form-control');
    if (list != undefined) { mName.val(name.middle); }
    var lName = $('<input>').attr('type', 'text').attr('id', 'lastName').addClass('form-control');
    if (list != undefined) { lName.val(name.last); }


    var email = $('<input>').attr('type', 'text').attr('id', 'emailAddress').addClass('form-control');
    if (list != undefined) { email.val(name.email); }


    var phone = $('<input>').attr('type', 'text').attr('id', 'phoneNumber').addClass('form-control');
    phone.mask("(999) 999-9999");
    if (list != undefined) { phone.val(name.phone); }
    var gender = $('<select>').attr('id', 'genderList').addClass('form-control');
    var male = $('<option>').val('male').text('Male');
    var female = $('<option>').val('female').text('Female');
    var blankGender = $('<option>').val('-').text('----------');
    gender.append(blankGender);
    gender.append(female);
    gender.append(male);
    if (list != undefined) { gender.val(name.gender); }
    var active = $('<input>').attr('type', 'checkbox').attr('id', 'isActive').addClass('form-control');
    if (list != undefined) { if (name.active) { active.attr('checked', 'checked'); } }


    var uName = $('<input>').attr('type', 'text').attr('id', 'userName').addClass('form-control');
    if (list != undefined) { uName.val(name.username); }
    var pass = $('<input>').attr('type', 'text').attr('id', 'password').addClass('form-control');
    if (list != undefined) { pass.val(name.password); }
    var role = $('<select>').attr('id', 'roleList').addClass('form-control');
    var blankRole = $('<option value="-">----------</option>');
    var adminRole = $('<option value="admin">Admin</option>');
    var staffRole = $('<option value="staff">Staff</option>');
    var studentRole = $('<option value="student">Student</option>');
    var teacherRole = $('<option value="teacher">Teacher</option>');
    if (list != undefined) { role.val(name.role); }

    var btnCont = $('<div>').addClass('col-md-12');
    var btn = $('<input>').attr('type', 'button').attr('id', 'createUserButton').attr('name', 'createUserButton').addClass('btn btn-success').click(CreateEditUser);
    if (list != undefined) { btn.val('Update'); } else { btn.val('Create'); }
    btn.appendTo(btnCont);

    var statusCont = $('<div>');
    var status = $('<label>').addClass('form-control admin').attr('id', 'userStatus');
    status.appendTo(statusCont);

    blankRole.appendTo(role);
    adminRole.appendTo(role);
    staffRole.appendTo(role);
    studentRole.appendTo(role);
    teacherRole.appendTo(role);

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
    roleCont.append(role);

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
        UpdateLatestNews($('#userSelect option:checked').val(), $('#firstName').val(), $('#middleName').val(), $('#lastName').val(),
            $('#emailAddress').val(), $('#phoneNumber').val(), $('#genderList option:checked').val(), $('#isActive').attr('checked'),
            $('#startDate').val(), $('#endDate').val(), $('#userName').val(), $('#password').val(), $('#roleList option:checked').val(), UpdateUserCallback);
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
        userList.append('<option value="' + el.id + '">' + el.date + ' - ' + el.content + '</option>')
    });
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