<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SMAC.Home" %>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="blockRow">
        <div class="block" id="schoolBlock">
            <div>
                <div class="blockHeader">School Info</div>
                <div class="blockCols">
                    <div class="blockColLbls">
                        <div class="blockRow">School Name:</div>
                        <div class="blockRow">Street Address:</div>
                        <div class="blockRow">City:</div>
                        <div class="blockRow">State:</div>
                        <div class="blockRow">Zip Code:</div>
                        <div class="blockRow">Phone Number:</div>
                    </div>
                    <div class="blockColVals">
                        <div class="blockRow" runat="server" id="SchoolNameDiv">Chatham Middle School</div>
                        <div class="blockRow" runat="server" id="SchoolAddrDiv">480 Main Street</div>
                        <div class="blockRow" runat="server" id="SchoolCityDiv">Chatham</div>
                        <div class="blockRow" runat="server" id="SchoolStateDiv">NJ</div>
                        <div class="blockRow" runat="server" id="SchoolZipDiv">07928</div>
                        <div class="blockRow" runat="server" id="SchoolPhoneDiv">(973) 457-2506</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="block" id="newsBlock">
            <div>
                <div class="blockHeader">Latest News</div>
                <div class="listCont">
                    <asp:ListView ID="latestNewsListView" runat="server">
                        <ItemTemplate>
                                <li><div><%#Eval("News")%></div><div><%#Eval("Author")%></div></li>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <div class="blockRow" >
        <div class="block" id="resourceBlock">
            <div class="blockHeader">Learning Resources</div>
            <div class="listCont">
                <ul>
                    <li>
                        <a href="https://www.khanacademy.org">Khan Academy</a>
                    </li>
                    <li>
                        <a href="http://www.coursera.org">Coursera</a>
                    </li>
                    <li>
                        <a href="http://www.alison.com">ALISON</a>
                    </li>
                    <li>
                        <a href="https://www.edx.org">edX</a>
                    </li>
                    <li>
                        <a href="http://ocw.mit.edu">MIT Open Courseware</a>
                    </li>
                    <li>
                        <a href="http://oedb.org/open">OEDB</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="block" id="khanBlock">
            <div class="blockHeader">Khan Academy</div>
            <div class="khanSearch">
                <span>Search Khan Academy: <input type="text" id="khanQuery" /><input type="button" value="Search" /></span>
            </div>
        </div>
    </div>
</asp:Content>
