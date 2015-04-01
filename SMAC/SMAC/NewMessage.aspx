<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="SMAC.NewMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>

        var localList = "";

        $(document).ready(function () {
            $('#spinner').hide();

            $('#textSubmit').attr('disabled', 'disabled');
            $('#allusersFilter').attr('checked', 'checked');
            $('#textInput').val('');

            $('#ddl_AllUsers').change(EnableSendButton);
            $('.userFilter').on('click', FilterDropDown);
            $('#textSubmit').on('click', SendMessage);
            $('#textInput').keyup(EnableSendButton);

            SaveAllUsersList();
        });

        function SaveAllUsersList() {
            var options = $('#ddl_AllUsers option');

            var json = '[';

            $.each(options, function (i, el) {
                json += '{"role":"' + $(el).attr('data-role') + '","name":"' +
                            $(el).text() + '","id":"' + $(el).val() + '"},';
            });

            json = json.substr(0, json.length - 1);
            json += ']';
            localList = json;
        }

        function FilterDropDown() {
            if (localList != undefined) {
                var selected = $('.userFilter:checked').attr('data-type');
                var json = JSON.parse(localList);

                var ddl = $('#ddl_AllUsers');
                ddl.empty();

                $.each(json, function (i, el) {
                    if (el.role == '-' || el.role == selected || selected == 'all') {
                        ddl.append('<option value="'+el.id+'" data-role="'+el.role+'">'+el.name+'</option>')
                    }
                });
            }
        }

        function SendMessage() {
            $.ajax({
                type: "POST",
                url: "Services.asmx/SendPrivateMessage",
                data: "{'toUserId':'" + $('#ddl_AllUsers option:selected').val() + "', 'content':'" + $('#textInput').val() + "'}",
                contentType: "application/json; charset=UTF-8",
                beforeSend: function () {
                    $('#spinner').show();
                },
                success: function (data) {
                    $('#spinner').hide();
                    var json = JSON.parse(data.d);
                    
                    if (json.data == 'success') {
                        $('#sendStatus').text('Message was sent successfully!');
                        $('#sendStatus').css('color', 'green');
                        $('#textInput').val('');
                        $('#ddl_AllUsers').prop('selectedIndex', 0);
                    }
                    else {
                        $('#sendStatus').text('An internal error has occurred.  Please notify your administrator.');
                        $('#sendStatus').css('color', 'red');
                    }
                }
            });
        }

        function EnableSendButton() {

            $('#toUserId').val($('#ddl_AllUsers option:selected').val());

            if ($('#ddl_AllUsers').prop('selectedIndex') > 0 && $('#textInput').val() != '') {
                $('#textSubmit').removeAttr('disabled');
            }
            else {
                $('#textSubmit').attr('disabled', 'disabled');
            }
        }

        function ClearStatus() {
            $('#sendStatus').val('');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pmBreadCrumbs"><i class="fa fa-arrow-circle-left"></i><a href="Messages.aspx" class="bread"> Back to messages</a></div>
    <form runat="server">
        <div class="newMsgBlock">
            <div>1. Who would you like to send this message to?</div>
            <div class="filters">
                <span>Filter:&nbsp;</span>
                <input type="radio" class="userFilter" id="allusersFilter" name="filter" data-type="all" /><label for="allusersFilter" class="filterLabel">All Users</label>
                <input type="radio" class="userFilter" id="studentFilter" name="filter" data-type="student" /><label for="studentFilter" class="filterLabel">Students</label>
                <input type="radio" class="userFilter" id="teacherFilter" name="filter" data-type="teacher" /><label for="teacherFilter" class="filterLabel">Teachers</label>
                <input type="radio" class="userFilter" id="staffFilter" name="filter"  data-type="staff" /><label for="staffFilter" class="filterLabel">Staff</label>
                <input type="radio" class="userFilter" id="adminFilter" name="filter" data-type="admin" /><label for="adminFilter" class="filterLabel">Admins</label>
            </div>
            <select id="ddl_AllUsers">
                <asp:ListView ID="receiverListView" runat="server">
                    <ItemTemplate>
                        <option value="<%#Eval("Id")%>" data-role="<%#Eval("Role")%>"><%#Eval("Name")%></option>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                    </LayoutTemplate>
                </asp:ListView>
            </select>
        </div>
        <div class="newMsgBlock">
            <div>2. Enter your message.</div>
            <textarea id="textInput"></textarea>
        </div>
        <div class="newMsgBlock">
            <div>3. Send your message!</div>
            <input type="button" id="textSubmit" class="sendButton" value="Send" />
        </div>
        <div><label id="sendStatus"></label></div>
        <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
        <asp:HiddenField ID="toUserId" runat="server" ClientIDMode="Static" />
    </form>
</asp:Content>
