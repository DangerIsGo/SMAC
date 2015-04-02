<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchoolSelection.aspx.cs" Inherits="SMAC.SchoolSelection" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#SchoolSelectSubmit').attr('disabled', 'disabled');
            $('#ddList_SchoolSelect').on('change', ToggleSubmit);
        });

        function ToggleSubmit() {

            if ($('#ddList_SchoolSelect option:selected').val().indexOf('----') == -1) {
                $('#SchoolSelectSubmit').removeAttr('disabled');
            }
            else {
                $('#SchoolSelectSubmit').attr('disabled', 'disabled');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label class="">You are enrolled in multiple schools.  Please select a school</label><br /><br />
        <asp:DropDownList ID="ddList_SchoolSelect" runat="server" CssClass="schoolSelectDdl" ClientIDMode="Static"></asp:DropDownList><br />
        <asp:Button runat="server" ID="SchoolSelectSubmit" Text="Select" OnClick="SchoolSelectSubmit_Click" CssClass="btn btn-primary" ClientIDMode="Static"/>
    </form>
</asp:Content>
