<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="SMAC.NewMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>

        var localList = "";

        $(document).ready(function () {
            $('#MainContent_textInput').keyup(EnableSendButton);
            $('#ddl_AllUsers').change(EnableSendButton);
            $('.userFilter').on('click', FilterDropDown);
            $('#allusersFilter').attr('checked', 'checked');

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

        function EnableSendButton() {
            if ($('#ddl_AllUsers').prop('selectedIndex') > 0 && $('#MainContent_textInput').val() != '') {
                $('#MainContent_textSubmit').removeAttr('disabled');
            }
            else {
                $('#MainContent_textSubmit').attr('disabled', 'disabled');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
            <asp:TextBox runat="server" ID="textInput" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="newMsgBlock">
            <div>3. Send your message!</div>
            <asp:Button runat="server" ID="textSubmit" Text="Send" Enabled="false" CssClass="sendButton" OnClick="textSubmit_Click" EnableViewState="true" />
        </div>
        <div><asp:Label ID="sendStatus" runat="server"></asp:Label></div>
    </form>
</asp:Content>
