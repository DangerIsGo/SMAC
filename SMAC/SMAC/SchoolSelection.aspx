<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchoolSelection.aspx.cs" Inherits="SMAC.SchoolSelection" EnableViewState="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <label class="">You are enrolled in multiple schools.  Please select a school</label><br /><br />
        <asp:DropDownList ID="ddList_SchoolSelect" runat="server"></asp:DropDownList><br />
        <asp:Button runat="server" ID="SchoolSelectSubmit" Text="Submit" OnClick="SchoolSelectSubmit_Click" />
    </form>
</asp:Content>
