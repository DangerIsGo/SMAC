<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="SMAC.Messages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('[data-unread="true"]').addClass('unread');

            var msgId = getParameterByName('msgId');

            if (msgId != '') {
                $('#pmBreadCrumbs').show();
            }
            else {
                $('#pmBreadCrumbs').hide();
                $('#messageInput').hide();
                $('#errMsg').hide();
            }

            $('.privMsgBlock').on('click', function () {

                var baseUrl = window.location.href.substring(0, window.location.href.indexOf('?'));
                window.location.href = baseUrl + "?msgId=" + $(this).attr('data-msgid');
            });
        });

        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        <%--$(document).ready(function () {

            var userid = '<%= Session["UserId"] %>'

            var idx = getParameterByName('idx');
            idx = idx == '' ? '1' : idx;

            var msgId = getParameterByName('msgId');

            $('#pmBreadCrumbs').hide();

            if (msgId != '') {

                $.ajax({
                    type: "POST",
                    url: "Services.asmx/GetConversation",
                    contentType: 'application/json; charset=utf-8',
                    data: "{'userId': '" + userid + "', 'msgId': '" + msgId + "'}",
                    success: function (data) {
                        var res = JSON.parse(data.d);

                        $('#loading img').hide();
                        $('#loading .pleaseWait').hide();

                        $.each(res, function (i, el) {
                            CreateMessageBlock(el.sender, el.date, el.content);
                        });

                        var convBlk = $('<div>').addClass('convoBlock').addClass('input');

                        // Append on input 
                        var txtInput = $('<textarea>').addClass('convoInput').attr('id', 'text-input');

                        convBlk.append(txtInput);

                        var txtInputSend = $('<input type="button">').addClass('msgSend').val('Send');
                        txtInputSend.on('click', function () {
                            //Send message!
                            SendMessage();
                        });
                        convBlk.append(txtInputSend)

                        $('#messageInput').append(convBlk);
                        $('#pmBreadCrumbs').show();
                    }
                });
            }
            else {

                $.ajax({
                    type: "POST",
                    url: "Services.asmx/GetUserMessages",
                    contentType: 'application/json; charset=utf-8',
                    data: "{'userId': '" + userid + "', 'pageIndex': '" + idx + "'}",
                    success: function (data) {
                        var res = JSON.parse(data.d);

                        $('#loading img').hide();
                        $('#loading .pleaseWait').hide();

                        $.each(res, function (i, el) {
                            var msgBlk = $('<div>').addClass('privMsgBlock');

                            // Unread?  Highlight them!
                            if (el.read == 'false')
                                msgBlk.addClass('unread');

                            var fromUsr = $('<div>').addClass('privMsgBlkUsr');
                            var usr = $('<span>').addClass('privMsgUsr').text(el.from);
                            var when = $('<span>').addClass('privMsgSent').text(el.date);
                            usr.appendTo(fromUsr);
                            when.appendTo(fromUsr);
                            var snippet = $('<div>').addClass('privMsgBlkSnip').text(getMsgSubStr(el.content));

                            msgBlk.on('click', function () {
                                var baseUrl = window.location.href.substring(0, window.location.href.indexOf('?'));
                                window.location.href = baseUrl + "?msgId=" + el.id;
                            });

                            msgBlk.append(fromUsr).append(snippet).appendTo($('#messageList'));
                        });
                    }
                });
            }
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

            $('#messageList').append(convBlk);
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
            var userid = '<%= Session["UserId"] %>'

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
                    data: "{'userId': '" + userid + "', 'toUserId': '" + msgId + "', 'content': '" + message + "'}",
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
        }--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="pmBreadCrumbs"><i class="fa fa-arrow-circle-left"></i><a href="Messages.aspx" class="bread"> Back to messages</a></div>
    <div id="messageList">
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
    <div id="messageInput"></div>
    <div><span id="errMsg"></span></div>
</asp:Content>
