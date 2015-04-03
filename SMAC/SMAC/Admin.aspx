<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SMAC.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#spinner').hide();
            $('#acceptAction').click(PopulateEntityList);
        });

        function PopulateEntityList() {
            $.ajax({
                type: "POST",
                url: "Services.asmx/FetchEntityList",
                data: "{'entityName':'" + $('#entityTypeList').text() + "'}",
                contentType: "application/json; charset=UTF-8",
                beforeSend: function () {
                    $('#spinner').show();
                },
                success: function (data) {
                    $('#spinner').hide();
                    var json = JSON.parse(data.d);

                    $.each(json, function (i, el) {
                        var option = '<option value="'+el.id+'">'+el.name+'</option>';
                    });
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="adminPanel">

        <div class="form-group">
            <label class="col-md-3 control-label admin" for="entityTypeList">Select an entity type</label>
            <div class="col-md-3">
                <select id="entityTypeList" name="entityTypeList" class="form-control">
                    <option value="1">Class</option>
                    <option value="2">Club</option>
                    <option value="3">Club Enrollment</option>
                    <option value="4">Club Schedule</option>
                    <option value="5">Marking Period</option>
                    <option value="6">News</option>
                    <option value="7">Section</option>
                    <option value="8">Section Schedule</option>
                    <option value="9">School Year</option>
                    <option value="10">Student Enrollment</option>
                    <option value="11">Subject</option>
                    <option value="12">Time Slot</option>
                    <option value="13">User</option>
                </select>
            </div>
            <div class="col-md-1">
                <input type="button" id="acceptType" name="acceptType" value="Accept" class="btn btn-primary" />
            </div>
            <div class="clearer"></div>
        </div>

        <div class="form-group">
            <label class="col-md-3 control-label admin" for="entityActionList">Select an action</label>
            <div class="col-md-3">
                <select id="entityActionList" name="entityActionList" class="form-control">
                    <option value="create">Create New</option>
                    <option value="update">Update Existing</option>
                    <option value="delete">Delete Existing</option>
                </select>
            </div>
            <div class="col-md-1">
                <input type="button" id="acceptAction" name="acceptAction" value="Accept" class="btn btn-primary" />
                <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
            </div>
            <div class="clearer"></div>
        </div>

        <div class="form-group">
            <label class="col-md-3 control-label admin" for="entityList">Select an entity</label>
            <div class="col-md-3">
                <select id="entityList" class="form-control"></select>
            </div>
            <div class="col-md-1">
                <input type="button" id="acceptEntity" name="acceptEntity" value="Accept" class="btn btn-primary" />
            </div>
            <div class="clearer"></div>
        </div>
    </div>
</asp:Content>
