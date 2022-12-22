<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="DistributionForm.aspx.cs" Inherits="DistributionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript">

        function ValidateForm() {
            var IsValid = true;
            var arrControls = ["txtTicketEnd", "txtTicketStart", "txtTotTicket"];
            $.each(arrControls, function (index, value) {
                var _controlName = "#main_" + value;
                if ($(_controlName).val() == "") {

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
                //alert($("#main_txtTicketEnd").val());
                if (parseInt($("#main_txtTicketEnd").val()) < parseInt($("#main_txtTicketStart").val())) {
                    jAlert("Invalid Ticket numbers", "Error in validation");
                    return false;
                }
                else {
                    $("#main_txtTotTicket").val(parseInt(parseInt($("#main_txtTicketEnd").val()) - parseInt($("#main_txtTicketStart").val())) + 1)
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
                                <asp:Label ID="lblDistName" runat="server"></asp:Label></h1>
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
                                        Tickets distributed
                                        <asp:HiddenField id="hdnUserId" runat="server" />
                                    </div>
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket(s) given to:
                                                    </label>
                                                    <asp:TextBox ID="txtDistName" runat="server" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email Address of Person:</label>
                                                    <asp:TextBox runat="server" ID="txtEmail" Text="" placeholder="Email Address of Person"
                                                        type="Email" Enabled="false" class="formcontrol" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket Number Start:
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtTicketStart" placeholder="Ticket Number Start"
                                                        type="number" class="formcontrol" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket Number End:</label>
                                                    <asp:TextBox runat="server" ID="txtTicketEnd" placeholder="Ticket Number End" class="formcontrol"
                                                        type="number" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>

                                           <%-- <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Distributed Date</label>
                                                    <asp:TextBox runat="server" ID="txtCreated" placeholder="Ticket Number End" class="formcontrol"
                                                        type="datetime" MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>--%>

                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Total Ticket(s):</label>
                                                    <asp:TextBox runat="server" ID="txtTotTicket" placeholder="Total Ticket" 
                                                        class="formcontrol"></asp:TextBox>
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
                                                            <asp:Button Text="Cancel" ID="btnCancel" runat="server" CssClass="buttonGrey xlarge"
                                                                OnClick="btncancel_click" />
                                                        </div>
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
