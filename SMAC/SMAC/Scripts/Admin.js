var list;
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
        subjectList.append('<option value="'+el.id+'">'+el.name+'</option>')
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

function PopulateEntityList() {
    $('#entityPanel').show();

    $('#acceptEntityType').attr('disabled', 'disabled');

    var eType = $('#entityTypeList').val();

    if ($('#entityActionList').val() == 'create') {
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

        }
        else if (eType == 7) {
            //Section

        }
        else if (eType == 8) {
            //Section Schedule

        }
        else if (eType == 9) {
            //School

        }
        else if (eType == 10) {
            //Student Enrollment

        }
        else if (eType == 11) {
            //Subject
            DrawSubjectForm();
        }
        else if (eType == 22) {
            //Time Span

        }
        else if (eType == 13) {
            //User

        }
    }
    else if (($('#entityActionList').val() == 'update') ||
        ($('#entityActionList').val() == 'delete')) {
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

        }
        else if (eType == 7) {
            //Section

        }
        else if (eType == 8) {
            //Section Schedule

        }
        else if (eType == 9) {
            //School

        }
        else if (eType == 10) {
            //Student Enrollment

        }
        else if (eType == 11) {
            //Subject
            GetSubjectList(DrawSubjectSelect);
        }
        else if (eType == 22) {
            //Time Span

        }
        else if (eType == 13) {
            //User

        }
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
        data: "{'id':'"+id+"', 'name':'" + name + "'}",
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
        data: "{'name':'"+name+"'}",
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
        data: "{'subjectId':'" + id + "'}",
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