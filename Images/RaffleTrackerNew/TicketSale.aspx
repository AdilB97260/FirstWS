<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="TicketSale.aspx.cs" Inherits="TicketSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript">

        function ValidateForm() {
            var IsValid = true;
            var arrControls = ["txtTicketEnd", "txtTicketStart", "txtTotTicket", "txtGiveTo"];
            $.each(arrControls, function (index, value) {
                var _controlName = "#main_" + value;
                if ($(_controlName).val() == "") {

                    IsValid = false;
                    $(_controlName).closest("div").addClass("has-error");
                }
                else if (_controlName = "#main_dllDistribution") {
                    if ($(_controlName).val() == "0") {
                        IsValid = false;
                        $(_controlName).closest("div").addClass("has-error");
                    }
                }

                else
                    $(_controlName).closest("div").removeClass("has-error");

            });


            return IsValid;
        }


        $(function () {
            $("#main_txtTicketEnd").blur(function () {
                if (parseInt($("#main_txtTicketEnd").val()) < parseInt($("#main_txtTicketStart").val())) {
                    jAlert("Invalid Ticket numbers", "Error in validation");
                    return false;
                }
                else {
                    $("#main_txtTotTicket").val(parseInt($("#main_txtTicketEnd").val() - $("#main_txtTicketStart").val()) + 1)
                    $("#main_txtAmount").val(parseFloat($("#main_txtTotTicket").val()) * parseFloat($("#main_hdnRate").val()));

                    $.ajax({
                        type: "POST",
                        url: "/TicketSale.aspx/GetDistUser",
                        data: "{ftick: '" + $("#main_txtTicketStart").val() + "',ttick: '" + $("#main_txtTicketEnd").val() + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //if (eval(response.d).length > 0) {
                            var obj = JSON.parse(response.d);
                            //alert(obj.LastDistUserName);

                            $("#main_hdnLastDist").val(obj.LastDistUserFk);
                            $("#txtLastDistributer").val(obj.LastDistUserName);

                            // }
                        },
                        failure: function (response) {
                            alert("Last Distribution User Not found");
                        },
                        error: function (response) {
                            alert(response.d);
                        }
                    });



                }
            });
        });
           

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1>
                                Ticket Sale Entry Form</h1>
                            <div class="divider">
                            </div>
                            <div class="row" id="divError" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12">
                                    <div class="alertBox error">
                                        <h4>
                                            ERROR! <span>
                                                <asp:Literal ID="lblErrorMsg" runat="server"></asp:Literal></span></h4>
                                    </div>
                                </div>
                            </div>
                            <!-- start login form -->
                            <div class="login-form">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12" style="padding-bottom: 20px; font-weight: bolder;
                                        font-size: 12px">
                                        Tracking of sold tickets
                                    </div>
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket(s) sold to:
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtGiveTo" placeholder="Enter Name of sold to" class="formcontrol"
                                                        MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email address of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter  Email address of buyer"
                                                        type="Email" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Street address of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtAdd" placeholder="Enter Street address of buyer"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        City of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtCity" placeholder="Enter City of buyer" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        State of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtState" placeholder="Enter State of buyer" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Zip of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtZip" placeholder="Enter Zip of buyer" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket Number Start:
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtTicketStart" placeholder="Enter Ticket Number Start"
                                                        type="Number" class="formcontrol" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket Number End:</label>
                                                    <asp:TextBox runat="server" ID="txtTicketEnd" placeholder="Enter Ticket Number End"
                                                        type="Number" class="formcontrol" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Total Ticket(s):</label>
                                                    <asp:TextBox runat="server" ID="txtTotTicket" placeholder="Total Ticket(s)" type="Number"
                                                        class="formcontrol" disabled="disabled"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Amount:</label>
                                                    <asp:TextBox runat="server" ID="txtAmount" placeholder="Amount" type="Number" class="formcontrol"
                                                        disabled="disabled"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnRate" runat="server" Value="25" />
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Phone Number of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtPhone" placeholder="Enter Phone Number of buyer"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        <b>Last Distributer: </b>
                                                    </label>
                                                    <input type="text" id="txtLastDistributer" class="formcontrol" disabled="disabled" />
                                                    <asp:HiddenField ID="hdnLastDist" runat="server" />
                                                </div>
                                            </div>
                                            <!-- end row -->
                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClick="btnSave_click"
                                                                OnClientClick="return ValidateForm()" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="btnNo" runat="server" CssClass="buttonGrey xlarge"
                                                                OnClick="btncancel_click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-5 col-md-5 col-sm-5">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <span style="float: right; padding-right: 10px;">
                                                            <asp:Button Text="Import Sale Data" ID="bntImportSale" runat="server" CssClass="buttonColor"
                                                                OnClick="btnImportClick" /></span> <span style="float: right; padding-right: 10px;">
                                                                    <asp:Button Text="Add Multiple Sale" ID="btnMultipleSale" runat="server" CssClass="buttonColor"
                                                                        OnClick="btn_MultipleSale_Click" /></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end login form -->
                                </div>
                                <!-- end col -->
                            </div>
                        </div>
                        <!-- end row -->
                        <!-- end form -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end login form -->
</asp:Content>
