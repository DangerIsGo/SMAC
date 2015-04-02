<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Threads.aspx.cs" Inherits="SMAC.Threads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {

            var secId = getParameterByName('secId');
            var threadId = getParameterByName('threadId');

            $('#replySubmit').attr('disabled', 'disabled');
            $('#replyInput').keyup(EnableSubmitButton);
            $('#replySubmit').on('click', SubmitNewThread);

            $('#spinner').hide();

            if (threadId != '') {
                $('#threadTable').hide();
                $('.newThread').hide();
                $('.bread').text(' Back to Thread List');
                $('.bread').attr('href', 'Threads.aspx?secId=' + secId);
            }
            else {
                $('.threadTitle').hide();
                $('#convoListView').hide();
                $('.threadReply').hide();
                $('#replySubmit').hide();
            }

            $('.newThreadClick').on('click', function () {
                var secId = getParameterByName('secId');

                window.location.href = '/NewThread.aspx?secId=' + secId;
            });
        });

        function EnableSubmitButton() {
            if ($('#replyInput').val() != '') {
                $('#replySubmit').removeAttr('disabled');
            }
            else {
                $('#replySubmit').attr('disabled', 'disabled');
            }
        }

        function SubmitNewThread() {
            var threadId = getParameterByName('threadId');
            var secId = getParameterByName('secId');
            var content = $('#replyInput').val();

            $.ajax({
                type: "POST",
                url: "Services.asmx/PostNewThreadReply",
                data: "{'threadId':'" + threadId + "', 'sectionId':'" + secId + "', 'content':'" + content + "'}",
                contentType: "application/json; charset=UTF-8",
                beforeSend: function() {
                    $('#spinner').show();
                },
                success: function (data) {
                    window.location.reload();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="newThread">
        <span class="btn btn-xs btn-warning newThreadClick">
            <i class="fa fa-plus"></i>
            <span>New Thread</span>
        </span>
    </div>
    <div class="threadTitle">
        <i class="fa fa-caret-down"></i>
        <asp:Label ID="threadTitle" runat="server" ClientIDMode="Static"></asp:Label>
    </div>
    <asp:ListView ID="convoListView" runat="server" ClientIDMode="Static">
        <ItemTemplate>
            <div class="threadBlock">
                <div class="threadPostDate"><%#Eval("Date")%></div>
                <div class="threadAuthor"><%#Eval("Author")%></div>
                <div class="threadContent"><%#Eval("Content")%></div>
            </div>
        </ItemTemplate>
        <LayoutTemplate>
            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
        </LayoutTemplate>
    </asp:ListView>
    <div class="threadReply">
        <div><span>Message:</span></div>
        <div><textarea id="replyInput"></textarea></div>
    </div>
    <input type="button" id="replySubmit" value="Post Reply" class="btn btn-xs btn-primary" />
    <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
        
    <table id="threadTable">
        <thead>
            <tr>
                <th>Thread Title</th>
                <th>Author</th>
                <th>Last Post</th>
                <th>Replies</th>
            </tr>
        </thead>
        <tbody>
            <asp:ListView ID="threadTableBody" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><a href="/Threads.aspx?secId=<%#Eval("SecId")%>&threadId=<%#Eval("Id")%>"><%#Eval("Title")%></a></td>
                        <td><%#Eval("Author")%></td>
                        <td><div>At <%#Eval("LastPostAt")%></div><div>By <%#Eval("LastPostBy")%></div></td>
                        <td><%#Eval("Replies")%></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                </LayoutTemplate>
            </asp:ListView>
        </tbody>
    </table>
</asp:Content>
