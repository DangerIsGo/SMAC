<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewThread.aspx.cs" Inherits="SMAC.NewThread" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var param;

        $(document).ready(function () {
            $('#submitNewThread').attr('disabled', 'disabled');
            $('#threadTitle').keyup(EnableSubmit);
            $('#threadInput').keyup(EnableSubmit);

            param = getParameterByName('secId');

            $('#breadCrumbs a').attr('href', '/Threads.aspx?secId=' + param);
        });

        function EnableSubmit() {
            var title = $('#threadTitle').val();
            var input = $('#threadInput').val();

            if (title != '' && input != '') {
                $('#submitNewThread').removeAttr('disabled');
            }
            else {
                $('#submitNewThread').attr('disabled', 'disabled');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="newThreadHeader">
        <span>Post new thread</span>
        <asp:Label runat="server" ID="threadHeader" ClientIDMode="Static"></asp:Label>
    </div>
    <form runat="server">
        <div class="newThreadContainer">
            <div class="newThreadTitle">
                <div>Title:</div>
                <div><asp:TextBox ID="threadTitle" runat="server" ClientIDMode="Static"></asp:TextBox></div>
            </div>
            <div class="newThreadBody">
                <div>Message:</div>
                <asp:TextBox ClientIDMode="Static" ID="threadInput" runat="server" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <asp:Button ID="submitNewThread" runat="server" ClientIDMode="Static" CssClass="btn btn-xs btn-primary" Text="Post New Thread" OnClick="submitNewThread_Click" />
    </form>
</asp:Content>
