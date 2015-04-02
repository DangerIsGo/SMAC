<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="SMAC.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('[data-unread="true"]').addClass('btn-warning');

            var msgId = getParameterByName('msgId');

            $('#replySubmit').attr('disabled', 'disabled');

            if (msgId != '') {
                $('#messageList').hide();
                $('#newMessage').hide();

                $('.msgSend').attr('disabled', 'disabled');
                $('#replyInput').keyup(function () {
                    if ($('#replyInput').val() != '') {
                        $('#replySubmit').removeAttr('disabled');
                    }
                    else {
                        $('#replySubmit').attr('disabled', 'disabled');
                    }
                });
            }
            else {
                $('#convoList').hide();
                $('.threadReply').hide();
                $('#errMsg').hide();
                $('#replySubmit').hide();
                $('.threadTitle').hide();
            }

            $('.privMsgBlock').on('click', function () {

                var baseUrl = window.location.href.substring(0, window.location.href.indexOf('?'));
                window.location.href = baseUrl + "?msgId=" + $(this).attr('data-msgid');
            });

            $('#replySubmit').on('click', SendMessage);

            $('#newMessage').on('click', function () {
                window.location.href = '/NewMessage.aspx'
            });
        });        

        function CreateMessageBlock(sender, date, content) {
            var convBlk = $('<div>').addClass('convoBlock');
            var info = $('<span>').addClass('convoInfo');
            var sender = $('<span>').addClass('convoSender').text(sender + ' said:');
            var date = $('<span>').addClass('convoDate').text('On ' + date + ',');

            info.append(date);
            info.append(sender);

            convBlk.append(info);

            var contentBlk = $('<span>').addClass('convoContent').text(content);

            convBlk.append(contentBlk);

            $('#convoList').append(convBlk);
        }

        function getMsgSubStr(line) {
            if (line.length > 47) {
                return line.substring(0, 47) + '...';
            }
            else {
                return line;
            }
        }

        function SendMessage() {
            var msgId = getParameterByName('msgId');
            var message = $('#replyInput').val();

            //Disable button
            //Disable inputbox
            $('#replyInput').attr('disabled', 'disabled');
            $('#replySubmit').attr('disabled', 'disabled');
            $('#errMsg').text('');

            if (message.trim() != '') {

                $.ajax({
                    type: "POST",
                    url: "Services.asmx/SendPrivateMessage",
                    contentType: 'application/json; charset=utf-8',
                    data: "{'toUserId': '" + $('#toUserId').val() + "', 'content': '" + message + "'}",
                    success: function (data) {
                        var res = JSON.parse(data.d);

                        if (res.data == 'success') {
                            // Append onto MessageList.  clear input box
                            window.location.reload();
                        }
                        else {
                            // Show error message
                            $('#errMsg').text('An internal error has occurred.  Message not sent.');
                        }
                    },
                    error: function(jqXHR, textStatus, errorThrown) {
                        $('#errMsg').text('An internal error has occurred.  Message not sent.');
                    },
                    complete: function (jqXHR, textStatus ) {
                        $('#replyInput').removeAttr('disabled');
                        $('.msgSend').removeAttr('disabled');
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="messageList">
        <div class="btn privMsgBlock" id="newMessage">
            <div class="privMsgBlkUsr newMsg">
                <i class="fa fa-plus-circle"></i>
                <span>New Message</span>
            </div>
            <div class="privMsgBlkSnip"><%#Eval("LastMessage")%></div>
        </div>
        <asp:ListView ID="messageListView" runat="server">
            <ItemTemplate>
                <div class="btn privMsgBlock" data-msgId="<%#Eval("Id")%>" data-unread="<%#Eval("UnRead")%>">
                    <div class="privMsgBlkUsr">
                        <span class="privMsgUsr"><%#Eval("Name")%></span>
                        <span class="privMsgSent"><%#Eval("Date")%></span>
                    </div>
                    <div class="privMsgBlkSnip"><%#Eval("LastMessage")%></div>
                </div>
            </ItemTemplate>
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            </LayoutTemplate>
        </asp:ListView>
    </div>

    <div class="threadTitle">
            <i class="fa fa-caret-down"></i>
            <asp:Label ID="threadTitle" runat="server" ClientIDMode="Static"></asp:Label>
        </div>
    <div id="convoList">
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
    </div>
    <div class="threadReply">
        <div><span>Message:</span></div>
        <div><textarea id="replyInput"></textarea></div>
    </div>
    <input type="button" id="replySubmit" value="Send Reply" class="btn btn-xs btn-primary" />
    <div><span id="errMsg"></span></div>
    <asp:HiddenField ID="toUserId" runat="server" ClientIDMode="Static" />
</asp:Content>
