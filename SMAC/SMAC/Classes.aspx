<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Classes.aspx.cs" Inherits="SMAC.Classes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <div class="gradeBody">
            <div class="gradeSelect">
                <div class="gradeHeader" id="yearLbl">Select School Year:</div>
                <asp:DropDownList ID="yearList" runat="server"></asp:DropDownList>
            </div>
            <div class="gradeSelect">
                <div class="gradeHeader" id="mpLbl">Select Marking Period:</div>
                <asp:DropDownList ID="periodList" runat="server"></asp:DropDownList>
            </div>
            <asp:ListView ID="classListView" runat="server">
                <ItemTemplate>
                    <div class="privMsgBlock" data-secId="<%#Eval("Id")%>" data-unread="<%#Eval("UnRead")%>">
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
    </form>
</asp:Content>
