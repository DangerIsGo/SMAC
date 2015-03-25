<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="SMAC.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {

            var userid = '<%= Session["UserId"] %>'

            var idx = getParameterByName('idx');
            idx = idx == '' ? '1' : idx;

            $.ajax({
                type: "POST",
                url: "Services.asmx/GetUserMessages",
                contentType: 'application/json; charset=utf-8',
                data: "{'userId': '" + userid + "', 'pageIndex': '" + idx  + "'}",
                success: function (res) {
                    var content = JSON.parse(res.d);
                    window.location.href = content.redir;
                }
            });
        });

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="messageBody">
         <div id="loading">
             <div>Please wait while your messages are loading</div>
             <img src="Images/ajax-loader-white.gif" />
         </div>
        <div id="messageList"></div>
    </div>
</asp:Content>
