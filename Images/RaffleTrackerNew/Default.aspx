<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Parish Raffle</title>
    <!-- html5 support in IE8 and later -->
    <!-- CSS file links -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/responsive.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/jquery.alerts.css" rel="stylesheet" />
    <style>
        .dataTable tbody > tr:first-child {
            color: Maroon !important;
            font-size: 13px;
            font-weight: 600;
        }
    </style>
    <style>
        blink {
            animation: blinker 1s linear infinite;
            color: Maroon;
            font-size: 16px;
        }

        .bl
        {
            background-color: #428bca;
        }

        .buttonColor
        {
            background-color:#207dce;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .blink-one {
            animation: blinker-one 5s linear infinite;
        }

        @keyframes blinker-one {
            0% {
                opacity: 0;
            }
        }

        .blink-two {
            animation: blinker-two 5.4s linear infinite;
        }

        @keyframes blinker-two {
            100% {
                opacity: 0;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="navbar navbar-default" style="height: 120px">
            <div class="topBar">
            </div>
            <div class="container">



                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>

                    <a class="navbar-brand" href="/login.aspx">
                        <img src="images/logo.jpg" alt="Raffle Ticket System" height="85" /></a>

                    <span class="navbar-brand1" style="float: left; font-size: 32px; font-weight: 600; padding: 30px 0 0 10px; font-family: Cursive;">Raffle Ticket Tracking</span>

                </div>


            </div>

        </header>
        <section class="properties" style="background-color: #e3e9ef!important">
            <div class="container" style="min-height: 540px; padding-top: 1%">
                <div class="container">
                    <div class="row">
                        <div style="background-color: #428bca;">
                            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
                        </div>
                    </div>
                    <div class="row" style="background: white; margin-bottom: 10px; min-height: 400px">
                        <div class="col-lg-12 col-md-12" style="padding-top: 15%">
                            <div class="col-lg-12 col-sm-12 col-lg-offset-4">
                                <div class="col-lg-2 col-sm-2">
                                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                                    <asp:Button Text="Login" ID="btnLogin" runat="server" CssClass="buttonColor xlarge bl" 
                                    OnClick="btnLogin_Click" />
                                 </div>
                                </div>
                                <div class="col-lg-3 col-sm-3">
                                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                                    <asp:Button Text="Watch Raffle Live" ID="btnWathLive" runat="server" CssClass="buttonColor xlarge bl"
                                    OnClick="btnWathLive_Click" />
                                 </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end container -->
        </section>
        <div class="bottomBar">
            <div class="container">
                <p class="copyright">
                    &copy;2021 - Raffle Tracking Ticket
                </p>
            </div>
        </div>
    </form>
</body>
</html>