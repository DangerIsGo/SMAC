<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Threads.aspx.cs" Inherits="SMAC.Threads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('.newThreadClick').on('click', function () {
                console.log('foo');
                var secId = getParameterByName('secId');

                window.location.href = '/NewThread.aspx?secId=' + secId;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="pmBreadCrumbs"><i class="fa fa-arrow-circle-left"></i><a href="Classes.aspx" class="bread"> Back to Classes</a></div>
    <div class="newThread"><span class="newThreadClick"><i class="fa fa-plus"></i><span>New Thread</span></span></div>
    <form runat="server">
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
                            <td><a href="/Threads.aspx?threadId=<%#Eval("Id")%>"><%#Eval("Title")%></a></td>
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
    </form>
</asp:Content>
