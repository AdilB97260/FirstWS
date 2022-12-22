<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Raffle Tracking Ticket</title>
    <!-- html5 support in IE8 and later -->
    <!-- CSS file links -->
    <link href="css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/responsive.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/jquery.alerts.css" rel="stylesheet" />
    <script src="js/html5shiv.js"></script>
    <script src="js/jquery-1.4.1.min.js"></script>
    <script src="js/jquery.alerts.js"></script>
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

             <a class="navbar-brand" href="/default.aspx">
                        <img src="images/logo.jpg" alt="Raffle Ticket System" height="85" /></a>
                       
                         

            </div>
            </div>
            
    </header>
        <section class="properties">
            <div class="container" style="min-height: 580px; padding-top:5%">
                 <div class="container">
                       <div class="row">
            <div class="col-lg-4 col-lg-offset-4">
                <h1 style="color:#464646!important">Login</h1>
                <div class="divider"></div>
                <div class="row" id="divError" runat="server" visible="false">
                    <div class="col-lg-12 col-md-12">
                        <div class="alertBox error">
                            <h4>ERROR! <span>
                                <asp:Literal ID="lblErrorMsg" runat="server"></asp:Literal></span></h4>
                        </div>
                    </div>
                </div>
                <!-- start login form -->
                <div class="login-form">

                    <div class="row">
                        <div class="col-lg-12 col-md-12 col-sm-6">
                            <div class="formBlock">
                                <label for="login">User Name<em>*</em></label>&nbsp; &nbsp;<asp:RequiredFieldValidator ID="requiredUserName" runat="server" ValidationGroup="login" ControlToValidate="txtUsrname" Display="Dynamic" ErrorMessage="User Name is required" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:TextBox ID="txtUsrname" runat="server" class="form-control" placeholder="User Name" ClientIDMode="Static"></asp:TextBox>
                                
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-6">
                            <div class="formBlock">
                                <label for="pass">Password<em>*</em></label> &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="login" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtPassword" runat="server" class="form-control"  TextMode="Password" placeholder="password" ClientIDMode="Static"></asp:TextBox>
                                
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-6">
                            <div class="formBlock">

                                <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="buttonColor" ValidationGroup="login" OnClick="btnLogin_Click" />

                            </div>
                        </div>
                        <%--<div class="col-lg-12 col-md-12 col-sm-6">
                            <div class="formBlock" style="text-align:left; padding-top:5px;">
                               
                               <a href="javascript:void(0);" runat="server" onserverclick="downloadPermit_click">Download 2020 Permit</a>
                            </div>
                        </div>
                        --%>
                         <div class="col-lg-12 col-md-12 col-sm-6">
                            <div class="formBlock" style="text-align:left; padding-top:5px;">
                               
                               <a href="/Draw.aspx" >Watch Raffle Draw held on 14th August 2021</a>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-6">
                            <div class="formBlock" style="text-align:left; padding-top:5px;">
                               
                               <a href="/WinnerList.aspx" >Raffle-2021 Winner List</a>
                            </div>
                        </div>

                        <div style="clear: both;"></div>
                    </div>
                    <!-- end row -->

                    <!-- end form -->
                </div>
                <!-- end login form -->
            </div>
            <!-- end col -->

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
