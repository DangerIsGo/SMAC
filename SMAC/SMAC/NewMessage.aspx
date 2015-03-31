<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewMessage.aspx.cs" Inherits="SMAC.NewMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#MainContent_textInput').keyup(EnableSendButton);
            $('#ddl_AllUsers').change(EnableSendButton);
            $('.userFilter').on('click', FilterDropDown);
            $('#allusersFilter').attr('checked', 'checked');
        });

        function FilterDropDown() {
            var selected = $('.userFilter:checked').attr('data-type');
            
            
            $('#ddl_AllUsers option').show();

            if (selected == 'student') {
                $('#ddl_AllUsers :not(option[data-role=student])').hide();
            }
            else if (selected == 'teacher') {
                $('#ddl_AllUsers :not(option[data-role=teacher])').hide();
            }
            else if (selected == 'staff') {
                $('#ddl_AllUsers :not(option[data-role=staff])').hide();
            }
            else if (selected == 'admin') {
                $('#ddl_AllUsers :not(option[data-role=admin])').hide();
            }

            $('#ddl_AllUsers option[data-role=-]').show();
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
            <asp:TextBox runat="server" ID="textInput" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="newMsgBlock">
            <div>3. Send your message!</div>
            <asp:Button runat="server" ID="textSubmit" Text="Send" Enabled="false" CssClass="sendButton" OnClick="textSubmit_Click" EnableViewState="true" />
        </div>
        <div><asp:Label ID="sendStatus" runat="server"></asp:Label></div>
    </form>
</asp:Content>
