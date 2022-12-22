<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drawing.aspx.cs" Inherits="Drawing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Raffle Tracking Ticket</title>
    <link href="/css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/css/responsive.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.alerts.css" rel="stylesheet" />
    <script src="/js/html5shiv.js"></script>
    <script src="/js/jquery-1.4.1.min.js"></script>
    <script src="/js/jquery.alerts.js"></script>
    <script src="/js/jquery.js" type="text/javascript"></script>
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
                    <a class="navbar-brand" href="/">
                        <img src="/images/logo.jpg" alt="Raffle Ticket System" height="85" /></a>

                </div>

                <div class="navbar-collapse collapse" style="padding-top: 25px">
                    <ul class="nav navbar-nav" id="liLogin" runat="server" visible="true" style="float:left!important; padding-left:0px;"> 
                        
                        <li id="liDash" runat="server" visible="true" style="font-size:24px; font-weight:600; padding-top:13px; color:#4c9c9c; font-family:Cursive;">Raffle Ticket Tracking</li>
                        
                   
                        
                    </ul>

               


                  

                 


                </div>


                <!--/.navbar-collapse -->
            </div>
            <!-- end header container -->
     </header>
    <section class="properties">
            <div class="container" style="min-height: 585px; text-align:center">
    
        <object width="640" height="385">
            <iframe src="https://www.youtube.com/embed/TJc7L2MA8sw" height="515" width="760"
                allowfullscreen="true" frameborder="0"></iframe>
            <%-- <b>Method2</b>:
            <embed src="https://www.youtube.com/watch?v=TJc7L2MA8sw=en_US&fs=1&" type="application/x-shockwave-flash"
                allowscriptaccess="always" allowfullscreen="true" width="640" height="385">
            </embed>--%>
        </object>
        </div>
                
            <!-- end container -->
    </section>
    <div class="bottomBar">
        <div class="container">
            <p class="copyright">
                &copy;2017 - Raffle Tracking Ticket</p>
        </div>
    </div>
    </form>
</body>
<script src="/js/bootstrap.min.js" type="text/javascript"></script>
<!-- bootstrap 3.0 -->
<script src="/js/respond.js" type="text/javascript"></script>
<script src="/js/jquery.bxslider.min.js" type="text/javascript"></script>
<script src="/js/jquery.alerts.js" type="text/javascript"></script>
<script src="/js/jquery.prettyPhoto.js" type="text/javascript"></script>
<script src="/js/jquery.timepicker.js" type="text/javascript"></script>
<script src="/js/Custome.js" type="text/javascript"></script>
<script src="/js/jquery-ui.js" type="text/javascript"></script>
<!-- Jquery -->
<!-- bootstrap 3.0 -->
<script src="/js/respond.js"></script>
<script src="/js/jquery.timepicker.js"></script>
<script src="/js/Custome.js"></script>
<script src="/js/jquery-ui.js"></script>
<!-- bxslider -->
</html>
