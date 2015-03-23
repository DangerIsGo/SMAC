<%@ Page Title="SMAC Login" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SMAC._Default" %>

<html>
    <head>
        <title>SMAC Login</title>

        <script src="//code.jquery.com/jquery-2.1.3.min.js" ></script>
        <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
        <script src="Scripts/bootbox.min.js"></script>

        <link href="//code.jquery.com/ui/1.11.4/jquery-ui.min.js" rel="stylesheet" />
        <link href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css" rel="stylesheet" />
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">
        <link href="/Content/Site.less" rel="stylesheet" /> 

        <script>
            $(document).ready(function () {
                $('#submit').on('click', testAuth)
            });

            function validateFields() {

                $('#loginStatus').text('');
                if ($('#username').val() == '' || $('#password').val() == '') {
                    if ($('#username').val() == '') {
                        $('#username').css('background-color', 'red');
                    }
                    else {
                        $('#username').css('background-color', 'inherit');
                    }
                    if ($('#password').val() == '') {
                        $('#password').css('background-color', 'red');
                    }
                    else {
                        $('#username').css('background-color', 'inherit');
                    }
                    return false;
                }
                else
                    return true;
            }

            function testAuth() {
                $.ajax({
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    data: "{'username': '" + $('#username').val() + "', 'password': '"+$('#password').val()+"'}",
                    url: "Services.asmx/AuthUser",
                    success: function (res) {
                        var content = JSON.parse(res.d);
                        window.location.href = content.redir;
                    }
                });
            }
        </script>
    </head>
    <body>
        <form runat="server">
            <div class="mainContent">
                <div class="contentContainer">
                    <div class="banner"><span>SMAC - Student Management And Collaboration</span></div>
                    <div class="loginPanel">
                        <div class="loginText">Please enter your credentials to log in:</div>
                        <div class="loginInput"><span>Username:</span><input type="text" id="username" name="username" placeholder="Please enter your username" /></div>
                        <div class="loginInput"><span>Password:</span><input type="password" id="password" name="password" placeholder="Please enter your password" /></div>
                        <div class="loginInput"><asp:Button ID="Submit" runat="server" Text="I'm Feeling Lucky Punk" OnClick="Submit_Click" OnClientClick="return validateFields()" /></div>
                        <div><asp:Label ID="loginStatus" runat="server"></asp:Label></div>
                    </div>
                </div>
            </div>
        </form>
    </body>
</html>
