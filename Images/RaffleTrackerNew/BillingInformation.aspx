<%@ Page Title="" Language="C#" MasterPageFile="~/PaymentSite.master" AutoEventWireup="true" CodeFile="BillingInformation.aspx.cs" Inherits="BillingInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .credit-card-box .panel-title {
            display: inline;
            font-weight: bold;
        }

        .credit-card-box .form-control.error {
            border-color: red;
            outline: 0;
            box-shadow: inset 0 1px 1px rgba(0,0,0,0.075),0 0 8px rgba(255,0,0,0.6);
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server" Text=""> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 400px; border: 1px solid #000">
        <div class="col-lg-8 col-md-8" style="padding-top: 15px">

            <div class="row display-tr" style="text-align:center; font-weight:600; margin-left:0%">
                        <h3 class="panel-title display-td" style="padding-bottom: 0">Billing Information</h3>
                    </div>


            <div class="col-lg-12 col-sm-12">
                <div style="width: 100%; text-align: left; padding-top: 10px; font-size: 14px; font-weight: 600">
                    <div class="panel panel-default credit-card-box" style="padding: 1% 1% 0 1%; margin-left: 2%; width: 90%">
                        <div class="panel-body" style="background-color: #f5f5f5">
                            <div class="row" style="text-align: left; font-weight: 600; color: #464646">
                                <div class="col-lg-12" style="color: maroon">
                                    <div style="float: left; width: 25%; padding-left: 3%; text-align: left;">
                                        Church Name &nbsp;:
                                    </div>
                                    <div style="float: left; width: 75%; padding-left: 10px; text-align: left">
                                        <asp:Label ID="lblRaffle" runat="server" Text="-"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-12" style="margin-top:10px">
                                    <div class="col-lg-2" style="text-align: left">
                                        Tickets :
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Label ID="lblTicketTot" runat="server" Text="0.00"></asp:Label>
                                    </div>
                                    <div class="col-lg-3" style="text-align: left">
                                        Amount :
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblAmount" runat="server" Text="0.00"></asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div style="width: 100%; font-size: 13px; font-weight: 600; text-align: left; margin-left: 1.7%;">
                    
                    <div class="panel panel-default credit-card-box" style="width: 90%">
                        <div class="panel-body" style="background-color: #f5f5f5">

                            <div class="col-lg-12" style="padding-top: 0px;">
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <div class="formBlock">
                                        <label>
                                            First Name <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>

                                        <asp:TextBox runat="server" ID="txtFirstName" placeholder="Enter First Name" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <div class="formBlock">
                                        <label>
                                            Last Name <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtLastName" placeholder="Enter Last Name" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-12" style="padding-top: 10px;">

                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="formBlock">
                                        <label>
                                            Street Address
                                        </label>
                                        <asp:TextBox runat="server" ID="txtAddress" placeholder="Enter Street Address" class="formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-12" style="padding-top: 10px;">

                                <div class="col-lg-6 col-md-6 col-sm-6">
                                     <div class="formBlock">
                                        <label>
                                            Town / City <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtCity" placeholder="Enter Town/City" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                     <div class="formBlock">
                                        <label>
                                            State <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtState" placeholder="Enter State" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-12" style="padding-top: 10px;">

                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <div class="formBlock">
                                        <label>
                                            Zip Code <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtZip" placeholder="Enter Zip Code" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <div class="formBlock">
                                        <label>
                                            Phone <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtPhone" placeholder="Enter Phone" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-12" style="padding-top: 10px;">

                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <div class="formBlock">
                                        <label>
                                            Email <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtEmail" type="email" placeholder="Enter Email" class="vldBillInfo formcontrol"
                                            MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="form form-horizontal">
                    <div class="form-group">
                        <div class="col-sm-3 col-lg-offset-3" style="margin-top: 3%; padding-bottom: 5%">
                            <input type="button" value="Back" title="Back" class="btn btn-outline btn-default" onclick="cancel();" />
                        </div>
                        <div class="col-sm-3" style="margin-top: 3%">
                            <asp:Button ID="btnSave" runat="server" Text="Make Payment" CssClass="btn btn-outline btn-primary" OnClientClick="return validateSave();" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-4 col-md-4" style="padding-top: 5%; padding-right: 5%">
            <img src="images/coupan.jpg" alt="coupan" width="320" height="320" />
        </div>

        <div class="col-lg-4 col-md-4" style="padding-top: 5%; padding-right: 5%">
        </div>

    </div>
    <script>

        function cancel() {
            window.location.href = 'TicketBuy.aspx';
        }

        function validateSave() {
            var IsValid = true;
            var arrControls = ["txtFirstName", "txtLastName", "txtCity", "txtZip", "txtPhone", "txtEmail","txtState"];
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

        $(".vldBillInfo").on("focusout", function () {
            if ($(this).val() == '') {
                $(this).addClass("error");
            }
            else {
                $(this).removeClass("error");
            }
        });

        $(".vldBillInfo").on("focus", function () {
            if ($(this).val() == '') {
                $(this).removeClass("error");
            }
        });

    </script>

</asp:Content>

