<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Classes.aspx.cs" Inherits="SMAC.Classes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#periodList').on('change', EnableButton);
            $('#yearList').on('change', EnableButton);
            $('#searchButton').attr('disabled', 'disabled');
            $('#spinner').hide();
        });

        function EnableButton() {
            if ($('#periodList').prop('selectedIndex') > 0 && $('#yearList').prop('selectedIndex') > 0) {
                $('#searchButton').removeAttr('disabled');
            }
            else {
                $('#searchButton').attr('disabled', 'disabled');
            }
        }

        function GetClassList() {
            var sy = $('#yearList option:selected').val();
            var mp = $('#periodList option:selected').val();

            $.ajax({
                type: "POST",
                url: "Services.asmx/PopulateClassTable",
                data: "{'mp':'" + mp + "', 'year':'" + sy + "'}",
                contentType: "application/json; charset=UTF-8",
                beforeSend: function () {
                    $('#spinner').show();
                },
                success: function (data) {
                    $('#spinner').hide();
                    var json = JSON.parse(data.d);

                    RenderClassList(json);
                }
            }); 
        }

        function StoreClass() {
            var sy = $('#yearList option:selected').val();
            var mp = $('#periodList option:selected').val();
            var blk = $(this);
            $.ajax({
                type: "POST",
                url: "Services.asmx/StoreSectionInClass",
                data: "{'sectionname':'" + blk.attr('data-sec') + "', 'classname':'" + blk.attr('data-class') + "', 'subject':'" + blk.attr('data-subj') + "', 'mp':'" + mp + "', 'year':'" + sy + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    window.location.href = "/Threads.aspx";
                }
            });
        }

        function RenderClassList(data) {
            $('#classList').empty();

            $.each(data, function (i, el) {
                var block = $('<div>').addClass('classBlock');
                block.attr('data-sec', el.sectionname);
                block.attr('data-class', el.classname);
                block.attr('data-subj', el.subjectname);
                var subBlock1 = $('<div>').addClass('classHeader').text(el.subjectname);
                var subBlock2 = $('<div>').addClass('classBody').text(el.classname + " - " + el.sectionname);

                block.append(subBlock1);
                block.append(subBlock2);

                block.on('click', StoreClass);

                block.appendTo($('#classList'));
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form runat="server">
        <div class="gradeBody">
            <div class="gradeSelect">
                <div class="gradeHeader" id="yearLbl">Select School Year:</div>
                <asp:DropDownList ID="yearList" runat="server" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <div class="gradeSelect">
                <div class="gradeHeader" id="mpLbl">Select Marking Period:</div>
                <asp:DropDownList ID="periodList" runat="server" ClientIDMode="Static"></asp:DropDownList>
            </div>
            <asp:Button ID="searchButton" runat="server" CssClass="sendButton inline" Text="Search" ClientIDMode="Static" UseSubmitBehavior="false" OnClientClick="return GetClassList()" />
            <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
            <div id="classList"></div>
        </div>
    </form>
</asp:Content>
