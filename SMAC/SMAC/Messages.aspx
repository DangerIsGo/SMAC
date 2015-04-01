<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="SMAC.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('[data-unread="true"]').addClass('unread');

            var msgId = getParameterByName('msgId');

            if (msgId != '') {
                $('#pmBreadCrumbs').show();
                $('#messageList').hide();
                $('#convoList').show();
                $('#messageInput').show();
                $('#errMsg').show();
                $('#newMessage').hide();

                $('.msgSend').attr('disabled', 'disabled');
                $('#text-input').keyup(function () {
                    if ($('#text-input').val() != '') {
                        $('.msgSend').removeAttr('disabled');
                    }
                    else {
                        $('.msgSend').attr('disabled', 'disabled');
                    }
                });
            }
            else {
                $('#convoList').hide();
                $('#messageList').show();
                $('#pmBreadCrumbs').hide();
                $('#messageInput').hide();
                $('#errMsg').hide();
                $('#newMessage').show();
            }

            $('.privMsgBlock').on('click', function () {

                var baseUrl = window.location.href.substring(0, window.location.href.indexOf('?'));
                window.location.href = baseUrl + "?msgId=" + $(this).attr('data-msgid');
            });

            $('.msgSend').on('click', function () {
                //Send message!
                SendMessage();
            });

            $('#newMessage').on('click', function () {
                window.location.href = '/NewMessage.aspx'
            });
        });

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

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

            console.log(convBlk);

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
            var message = $('#text-input').val();

            //Disable button
            //Disable inputbox
            $('#text-input').attr('disabled', 'disabled');
            $('.msgSend').attr('disabled', 'disabled');
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
                            CreateMessageBlock('You', res.date, message);

                            $('#text-input').val('');
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
                        $('#text-input').removeAttr('disabled');
                        $('.msgSend').removeAttr('disabled');
                    }
                });
            }
            else {
                console.log('empty message');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pmBreadCrumbs"><i class="fa fa-arrow-circle-left"></i><a href="Messages.aspx" class="bread"> Back to messages</a></div>
    <div id="messageList">
        <div class="privMsgBlock" id="newMessage">
            <div class="privMsgBlkUsr newMsg">
                <i class="fa fa-plus-circle"></i>
                <span>New Message</span>
            </div>
            <div class="privMsgBlkSnip"><%#Eval("LastMessage")%></div>
        </div>
        <asp:ListView ID="messageListView" runat="server">
            <ItemTemplate>
                <div class="privMsgBlock" data-msgId="<%#Eval("Id")%>" data-unread="<%#Eval("UnRead")%>">
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
    <div id="convoList">
        <asp:ListView ID="convoListView" runat="server">
            <ItemTemplate>
                <div class="convoBlock">
                    <span class="convoInfo">
                        <span class="convoDate">On <%#Eval("Date")%>,</span>
                        <span class="convoSender"><%#Eval("Author")%> said:</span>
                    </span>
                    <span class="convoContent"><%#Eval("Content")%></span>
                </div>
            </ItemTemplate>
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            </LayoutTemplate>
        </asp:ListView>
    </div>
    <div id="messageInput">
        <div class="convoBlock input">
            <textarea class="convoInput" id="text-input"></textarea>
            <input type="button" class="msgSend" value="Send">
        </div>
    </div>
    <div><span id="errMsg"></span></div>
    <form runat="server">
        <asp:HiddenField ID="toUserId" runat="server" ClientIDMode="Static" />
    </form>
</asp:Content>
