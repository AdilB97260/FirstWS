<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment.aspx.cs" Inherits="Payment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Parish Raffle Payment Page</title>
    <!-- html5 support in IE8 and later -->
    <!-- CSS file links -->
    <link href="/css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="/css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="/css/responsive.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/plugins/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.alerts.css" rel="stylesheet" />
    <script src="/js/html5shiv.js"></script>
    
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.0/js/bootstrap.min.js"></script>
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <style>
        .credit-card-box .panel-title {
            display: inline;
            font-weight: bold;
        }

        .credit-card-box .form-control.error {
            border-color: #c44747;
        }

        .credit-card-box label.error {
            font-weight: bold;
            color: red;
            padding: 2px 8px;
            margin-top: 2px;
        }

        .credit-card-box .payment-errors {
            font-weight: bold;
            color: red;
            padding: 2px 8px;
            margin-top: 2px;
        }

        .credit-card-box label {
            display: block;
        }
        /* The old "center div vertically" hack */
        .credit-card-box .display-table {
            display: table;
        }

        .credit-card-box .display-tr {
            display: table-row;
        }

        .credit-card-box .display-td {
            display: table-cell;
            vertical-align: middle;
            width: 50%;
        }
        /* Just looks nicer */
        .credit-card-box .panel-heading img {
            min-width: 180px;
        }
    </style>


</head>
<body>
    <form id="paymentform" runat="server">
        <!-- Start Header -->
        <header class="navbar navbar-default" style="height: 120px">
            <div class="topBar">
            </div>
            <div class="container">
                <a class="navbar-brand" href="/login.aspx">
                    <img src="images/logo.jpg" alt="Raffle Ticket System" height="85" /></a>
                <span class="navbar-brand1" style="float: left; font-size: 32px; font-weight: 600; padding: 30px 0 0 10px; font-family: Cursive;">Parish Raffle</span>
                

            </div>
        </header>
        <!-- End Header -->
        <section class="properties">
            <div class="container" style="min-height: 585px; width: 1040px">
                <div class="row">
                    <div style="background-color: #428bca;">
                        <asp:Literal ID="ltrBredCrumb" runat="server" Text=""> </asp:Literal>
                    </div>
                </div>
                <div class="row" style="background: white; margin-bottom: 10px; min-height: 400px; border: 1px solid #000">
                    <div class="col-lg-7">
                        <div class="panel panel-default credit-card-box" style="padding: 1%; margin-left: 2%">
                            <div class="panel-heading display-table">
                                <div class="row display-tr">
                                    <h3 class="panel-title display-td">Payment Details</h3>
                                    <div class="display-td">
                                        <img class="img-responsive pull-right" src="/images/card_accepted.png" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <label for="cardNumber">CARD NAME</label>
                                            <asp:TextBox runat="server" ID="txtCardName" placeholder="Enter Card Holder Name" CssClass="vldPay formcontrol"  
                                                MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <label for="cardNumber">CARD NUMBER</label>
                                            <asp:TextBox runat="server" ID="txtCardNumber" placeholder="Enter Valid Card Number" CssClass="vldPay formcontrol" TextMode="Number" MaxLength="16" 
                                                ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-xs-6 col-md-6" style="padding-left: 0px!important;">
                                        <div class="col-xs-5 col-md-5">
                                            <div class="form-group">
                                                <label for="cardCVC">Exp.Month</label>
                                                <select id="ddlMonth" runat="server" class="form-control vldPay" style="width: 80px; color: #868686">
                                                    <option value="" selected="selected">Month</option>
                                                    <option value="01">01</option>
                                                    <option value="02">02 </option>
                                                    <option value="03">03</option>
                                                    <option value="04">04</option>
                                                    <option value="05">05</option>
                                                    <option value="06">06</option>
                                                    <option value="07">07</option>
                                                    <option value="08">08</option>
                                                    <option value="09">09</option>
                                                    <option value="10">10</option>
                                                    <option value="11">11</option>
                                                    <option value="12">12</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div style="width:3%; float:left">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <label style="padding-top:10px">/</label>
                                                
                                            </div>
                                        </div>
                                        <div class="col-xs-5 col-md-5">
                                            <label for="cardCVC">Exp.Year</label>
                                            <select id="ddlYear" runat="server" class="form-control vldPay" style="width: 110px; color: #868686">
                                                <option value="" selected="selected">Year</option>
                                                <option value="21">2021</option>
                                                <option value="22">2022</option>
                                                <option value="23">2023</option>
                                                <option value="24">2024</option>
                                                <option value="25">2025</option>
                                                <option value="26">2026</option>
                                                <option value="27">2027</option>
                                                <option value="28">2028</option>
                                                <option value="29">2029</option>
                                                <option value="30">2030</option>
                                                <option value="25">2021</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-md-6">
                                        <div class="form-group">
                                            <label for="cardCVC">CV CODE</label>
                                            <asp:TextBox type="tel" runat="server" ID="txtCVCode" placeholder="Enter CVC Code " CssClass="vldPay formcontrol"
                                                MaxLength="4" ClientIDMode="Static" autocomplete="cc-csc"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row" style="color:red; padding: 1% 3%; font-size:14px;">
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-3 col-lg-offset-3" style="margin-top: 3%;">
                                        <input type="button" value="Back" title="Back" class="btn btn-outline btn-default" onclick="cancel();" />
                                    </div>
                                    <div class="col-xs-4" style="padding-top: 3%; font-weight: 600">
                                        <%--<button class="btn btn-primary btn-block" type="button" onclick="return validateSave();">Pay</button>--%>
                                        <asp:Button ID="btnSave" runat="server" Text="Pay" CssClass="btn btn-outline btn-primary" OnClientClick="return validateSave();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5">

                        <div class="panel-heading" style="padding-bottom: 0px!important">
                            <h3 class="panel-title display-td" style="padding-bottom: 0px!important; font-size: 14px; font-weight: 600">Church Details</h3>
                        </div>

                        <div class="panel panel-default credit-card-box" style="padding: 1%; margin-left: 2%">
                            <div class="panel-body" style="background-color: #f5f5f5">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-lg-12" style="text-align: left; font-size:13px; font-weight: 600; color: #464646">
                                            Church Name&nbsp;: &nbsp; &nbsp;<asp:Label ID="lblChurchName" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel-heading" style="padding-bottom: 0px!important">
                            <h3 class="panel-title display-td" style="padding-bottom: 0px!important; font-size: 14px; font-weight: 600">Amount Details</h3>
                        </div>

                        <div class="panel panel-default credit-card-box" style="padding: 1%; margin-left: 2%">
                            <div class="panel-body" style="background-color: #f5f5f5">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="col-lg-3" style="text-align: left; font-size:13px; font-weight: 600; color: #464646">
                                            Tickets :
                                        </div>
                                        <div class="col-lg-2" style="font-size:13px; font-weight: 600; color:#808080">
                                            <asp:Label ID="lblTicketTot" runat="server" Text="0.00"></asp:Label>
                                        </div>

                                        <div class="col-lg-4" style="text-align: left; font-size:13px; font-weight: 600; color: #464646">
                                            Amount :
                                        </div>
                                        <div class="col-lg-3" style="font-size:13px; font-weight: 600; color:#808080">
                                            <asp:Label ID="lblAmount" runat="server" Text="0.00"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel-heading" style="padding-bottom: 0px!important">
                            <h3 class="panel-title display-td" style="padding-bottom: 0px!important; font-size: 14px; font-weight: 600">Billing Information</h3>
                        </div>

                        <div class="panel panel-default credit-card-box" style="padding: 1%; margin-left: 2%">
                            <div class="panel-body" style="background-color: #f5f5f5">
                                <div class="row" style="text-align: left; font-weight: 600; color: #464646">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <div class="col-lg-4">
                                                Billing Name
                                            </div>
                                            <div class="col-lg-1">
                                                :
                                            </div>
                                            <div class="col-lg-6" style="font-weight: normal">
                                                <asp:Label ID="lblFullName" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" style="padding-top: 10px">
                                        <div class="col-lg-4">
                                            City :
                                        </div>
                                        <div class="col-lg-1">
                                            :
                                        </div>
                                        <div class="col-lg-6" style="font-weight: normal">
                                            <asp:Label ID="lblCity" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" style="padding-top: 10px">
                                        <div class="col-lg-4">
                                            Zip :
                                        </div>
                                        <div class="col-lg-1">
                                            :
                                        </div>
                                        <div class="col-lg-6" style="font-weight: normal">
                                            <asp:Label ID="lblZip" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" style="padding-top: 10px">
                                        <div class="col-lg-4">
                                            Email :
                                        </div>
                                        <div class="col-lg-1">
                                            :
                                        </div>
                                        <div class="col-lg-6" style="font-weight: normal">
                                            <asp:Label ID="lblEmail" runat="server" Text="0.00"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-xs-12" style="padding-top: 10px">
                                        <div class="col-lg-4">
                                            Phone :
                                        </div>
                                        <div class="col-lg-1">
                                            :
                                        </div>
                                        <div class="col-lg-6" style="font-weight: normal">
                                            <asp:Label ID="lblPhone" runat="server" Text="0.00"></asp:Label>
                                        </div>
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
                    &copy;2021 - Raffle
                    &nbsp;&nbsp;&nbsp; <a style="color: #868686; font-size: 12px" href="login.aspx">Login</a>

                </p>
            </div>
        </div>
    </form>

    <script>

        function cancel() {
            window.location.href = 'TicketBuy.aspx';
        }

        function validateSave() {
            var IsValid = true;
            var arrControls = ["txtCardName", "txtCardNumber", "txtCVCode", "ddlMonth", "ddlYear"];
            $.each(arrControls, function (index, value) {
                var _controlName = "#" + value;
                if ($(_controlName).val() == "") {
                    IsValid = false;
                    $(_controlName).addClass("error");
                }
                else {
                    $(_controlName).removeClass("error");
                }

            });
            return IsValid;
        }

        $(".vldPay").on("focusout", function () {
            if ($(this).val() == '') {
                $(this).addClass("error");
            }
            else {
                $(this).removeClass("error");
            }
        });

        $(".vldPay").on("focus", function () {
            if ($(this).val() == '') {
                $(this).removeClass("error");
            }
        });

    </script>

</body>
</html>
