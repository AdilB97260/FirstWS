<%@ Page Title="" Language="C#" MasterPageFile="../RaffleTracker.master" AutoEventWireup="true"
    CodeFile="RaffleRegistration.aspx.cs" Inherits="RaffleRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript">

        function ValidateForm() {


            var IsValid = true;
            var arrControls = ["txtUserName", "txtUserName", "txtTicketEnd", "txtTicketStart", "txtTotTicket"];

            $.each(arrControls, function (index, value) {

                var _controlName = "#main_" + value;

                if ($(_controlName).val() == "") {
                    IsValid = false;
                    $(_controlName).closest("div").addClass("has-error");
                }
                else if ($(_controlName).val() == "0") {
                    IsValid = false;
                    $(_controlName).closest("div").addClass("has-error");
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
                    $("#main_txtTotTicket").val($("#main_txtTicketEnd").val() - $("#main_txtTicketStart").val() + 1)
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
                                Raffle Registration</h1>
                                <span style="float: right; padding-left: 10px;">
                                <asp:Button Text="Back" ID="Button1" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btnBack_click" /></span>
                            <div class="divider" style="margin-top: 1px">
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
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Name Of Raffle
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtUserName" placeholder="Enter Name Of Raffle" class="formcontrol"
                                                        MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter Email" class="formcontrol"
                                                        MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6" id="divFromTic" runat="server">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket Number Start:
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtTicketStart" placeholder="Ticket Number Start"
                                                        type="number" class="formcontrol" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6" id="divToTic" runat="server">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket Number End:</label>
                                                    <asp:TextBox runat="server" ID="txtTicketEnd" placeholder="Ticket Number End" class="formcontrol"
                                                        type="number" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        UserName (For Login)</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtLoginUserName" Text="" placeholder="Enter User Name for login to the system"
                                                        class="formcontrol" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Password</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtPassword" Text="" placeholder="Enter Password"
                                                        class="formcontrol" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Address</label>
                                                    <asp:TextBox runat="server" ID="txtAdd" TextMode="SingleLine" placeholder="Enter Address Line"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        City</label>
                                                    <asp:TextBox runat="server" ID="txtCity" TextMode="SingleLine" placeholder="Enter City Name"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        State</label>
                                                    <asp:TextBox runat="server" ID="txtState" TextMode="SingleLine" placeholder="Enter State Name"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Zip</label>
                                                    <asp:TextBox runat="server" ID="txtZip" TextMode="SingleLine" placeholder="Enter Zip Name"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtPhone" Text="" placeholder="Enter Phone Number"
                                                        class="formcontrol" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                           <%-- <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Mobile Number</label>
                                                    <asp:TextBox runat="server" ID="txtMobileNo" Text="" placeholder="Enter Mobile Number"
                                                        class="formcontrol" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>--%>
                                            <div class="col-lg-6 col-md-6 col-sm-6" id="divTotTic" runat="server">
                                                <div class="formBlock">
                                                    <label>
                                                        Total Ticket(s):</label>
                                                    <asp:TextBox runat="server" ID="txtTotTicket" placeholder="Total Ticket" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <!-- end row -->
                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()"
                                                                OnClick="btnSave_click" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                                                OnClick="btncancel_click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnUserId" runat="server" />
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
