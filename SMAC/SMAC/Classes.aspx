<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Classes.aspx.cs" Inherits="SMAC.Classes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
            $('#spinner').hide();

            $('select').prop('selectedIndex', 0);

            $('#yearList').on('change', PopulateMarkingPeriods);
            $('#periodList').on('change', EnableSearchButton);

            $('#periodList').append('<option value="-">-----------------------------</option>');
            $('#periodList').attr('disabled', 'disabled');
            $('#searchButton').attr('disabled', 'disabled');
            $('#periodList').css('background-color', 'silver');

            $('#searchButton').on('click', GetClassList);
        });

        function EnableSearchButton() {

            if ($('#yearList option:selected').val() != '-' && $('#periodList option:selected').val() != '-') {
                $('#searchButton').removeAttr('disabled');
            }
            else {
                $('#searchButton').attr('disabled', 'disabled');
            }
        }

        function PopulateMarkingPeriods() {
            var sy = $('#yearList option:selected').val();

            if ($('#yearList').prop('selectedIndex') == 0) {
                $('#periodList').css('background-color', 'silver');
            }

            $('#periodList').prop('selectedIndex', 0);
            EnableSearchButton();
            $('#periodList').empty();
            $('#periodList').append('<option value="-">-----------------------------</option>');
            $('#periodList').attr('disabled', 'disabled');

            $.ajax({
                type: "POST",
                url: "Services.asmx/PopulateMarkingPeriods",
                data: "{'yearId':'" + sy + "'}",
                contentType: "application/json; charset=UTF-8",
                success: function (data) {
                    var res = JSON.parse(data.d);

                    $.each(res, function (i, el) {

                        var opt = $('<option>').val(el.id).text(el.period);

                        $('#periodList').append(opt);
                    });

                    $('#periodList').removeAttr('disabled');
                    $('#periodList').css('background-color', 'white');
                }
            });
        }

        function GetClassList() {
            var sy = $('#yearList option:selected').val();
            var mp = $('#periodList option:selected').val();

            if ((sy.indexOf('-') == -1) && (mp.indexOf('-') == -1)) {

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
        }

        function RenderClassList(data) {
            $('#classList').empty();

            $.each(data, function (i, el) {
                var block = $('<div>').addClass('classBlock');
                block.attr('data-section', el.sectionid);
                var subBlock1 = $('<div>').addClass('classHeader').text(el.subjectname);
                var subBlock2 = $('<div>').addClass('classBody').text(el.classname + " - " + el.sectionname);

                block.append(subBlock1);
                block.append(subBlock2);

                block.on('click', function () {
                    var id = $(this).data('section');
                    window.location.href = '/Threads.aspx?secId=' + id;
                });

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
            <div class="gradeSelect" id="periodCont">
                <div class="gradeHeader" id="mpLbl">Select Marking Period:</div>
                <select id="periodList"></select>
            </div>
            <input type="button" id="searchButton" class="sendButton inline" value="Search" />
            <span><asp:Image ImageUrl="~/Images/ajax-loader-white.gif" runat="server" ID="spinner" ClientIDMode="Static" /></span>
            <div id="classList"></div>
        </div>
    </form>
</asp:Content>
