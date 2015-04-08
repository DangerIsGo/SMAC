<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SMAC.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#spinner').hide();

            $('#acceptAction').click(ShowEntityTypes);
            $('#acceptEntityType').click(PopulateEntityList);

            $('#entityActionList').on('change', EnableActionButton);
            $('#entityTypeList').on('change', EnableTypeButton);

            $('#entityGroup').hide();
            $('#entityTypeGroup').hide();

            $('#acceptAction').attr('disabled', 'disabled');
            $('#acceptEntityType').attr('disabled', 'disabled');
            $('#acceptEntity').attr('disabled', 'disabled');
        });
    </script>
    <script src="Scripts/Admin.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="adminPanel">

        <div class="form-group">
            <label class="col-md-3 control-label admin" for="entityActionList">Select an action</label>
            <div class="col-md-3">
                <select id="entityActionList" name="entityActionList" class="form-control">
                    <option value="-">--------------------</option>
                    <option value="create">Create New</option>
                    <option value="update">Update Existing</option>
                    <option value="delete">Delete Existing</option>
                </select>
            </div>
            <div class="col-md-1">
                <input type="button" id="acceptAction" name="acceptAction" value="OK" class="btn btn-primary" />
                <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
            </div>
            <div class="clearer"></div>
        </div>

        <div class="form-group" id="entityTypeGroup">
            <label class="col-md-3 control-label admin" for="entityTypeList">Select an entity type</label>
            <div class="col-md-3">
                <select id="entityTypeList" name="entityTypeList" class="form-control">
                    <option value="-">--------------------</option>
                    <option value="1">Class</option>
                    <option value="2">Club</option>
                    <option value="3">Club Enrollment</option>
                    <option value="4">Club Schedule</option>
                    <option value="5">Grade</option>
                    <option value="6">Marking Period</option>
                    <option value="7">News</option>
                    <option value="8">Section</option>
                    <option value="9">Section Schedule</option>
                    <option value="10">School Year</option>
                    <option value="11">Student Enrollment</option>
                    <option value="12">Subject</option>
                    <option value="13">Time Slot</option>
                    <option value="14">User</option>
                </select>
            </div>
            <div class="col-md-1">
                <input type="button" id="acceptEntityType" name="acceptEntityType" value="OK" class="btn btn-primary" />
            </div>
            <div class="clearer"></div>
        </div>

        <div id="entitySelectGroup">

        </div>

        <div id="entityGroup">

        </div>
    </div>
</asp:Content>
