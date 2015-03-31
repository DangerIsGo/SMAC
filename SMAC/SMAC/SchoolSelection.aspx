<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchoolSelection.aspx.cs" Inherits="SMAC.SchoolSelection" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#MainContent_SchoolSelectSubmit').attr('disabled', 'disabled');
            $('#MainContent_ddList_SchoolSelect').on('change', ToggleSubmit);
        });

        function ToggleSubmit() {
            console.log($('#MainContent_ddList_SchoolSelect option:selected').val());
            if ($('#MainContent_ddList_SchoolSelect option:selected').val().indexOf('----') == -1) {
                $('#MainContent_SchoolSelectSubmit').removeAttr('disabled');
            }
            else {
                $('#MainContent_SchoolSelectSubmit').attr('disabled', 'disabled');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label class="">You are enrolled in multiple schools.  Please select a school</label><br /><br />
        <asp:DropDownList ID="ddList_SchoolSelect" runat="server" CssClass="schoolSelectDdl"></asp:DropDownList><br />
        <asp:Button runat="server" ID="SchoolSelectSubmit" Text="Submit" OnClick="SchoolSelectSubmit_Click" CssClass="sendButton" />
    </form>
</asp:Content>
