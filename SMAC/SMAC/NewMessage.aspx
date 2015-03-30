<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="SMAC.NewMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#MainContent_textInput').keypress(EnableSendButton);
            $('#MainContent_ddl_AllUsers').change(EnableSendButton);
        });

        function EnableSendButton() {
            if ($('#MainContent_ddl_AllUsers').prop('selectedIndex') > 0 && $('#MainContent_textInput').val() != '') {
                $('#MainContent_textSubmit').removeAttr('disabled');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pmBreadCrumbs"><i class="fa fa-arrow-circle-left"></i><a href="Messages.aspx" class="bread"> Back to messages</a></div>
    <form runat="server">
        <div class="newMsgBlock">
            <div>1. Who would you like to send this message to?</div>
            <asp:DropDownList ID="ddl_AllUsers" runat="server"></asp:DropDownList><br />
        </div>
        <div class="newMsgBlock">
            <div>2. Enter your message.</div>
            <asp:TextBox runat="server" ID="textInput" TextMode="MultiLine"></asp:TextBox><br />
        </div>
        <div class="newMsgBlock">
            <div>3. Send your message!</div>
            <asp:Button runat="server" ID="textSubmit" Text="Send" Enabled="false" CssClass="sendButton" />
        </div>
        
    </form>
</asp:Content>
