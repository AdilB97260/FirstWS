<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="ChangeProfile.aspx.cs" Inherits="ChangeProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
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
                            <h1 style="padding-top: 10px; padding-bottom: 15px">
                                Church Profile</h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" />
                            </span>
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
                            <div class="row" id="divMsg" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12">
                                    <div class="alertBox success">
                                        <h4>
                                            Success ! <span>
                                                <asp:Literal ID="lblMsg" runat="server"></asp:Literal></span></h4>
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
                                                        Name Of Church
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtUserName" placeholder="Enter Name Of Raffle" class="formcontrol"
                                                        MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Name Of Church Adminstrator
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtAdminName" placeholder="Enter Name Of Church Administrator"
                                                        class="formcontrol" MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter Email" class="formcontrol"
                                                        MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Address</label>
                                                    <asp:TextBox runat="server" ID="txtAdd" TextMode="SingleLine" placeholder="Enter Full Address"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        City</label>
                                                    <asp:TextBox runat="server" ID="txtCity" TextMode="SingleLine" placeholder="Enter City Name"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        State</label>
                                                    <asp:TextBox runat="server" ID="txtState" TextMode="SingleLine" placeholder="Enter State Name"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Zip</label>
                                                    <asp:TextBox runat="server" ID="txtZip" TextMode="SingleLine" placeholder="Enter Zip Name"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtPhone" Text="" placeholder="Enter Phone Number"
                                                        class="formcontrol" MaxLength="50" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        User Name (For Login)</label>
                                                    <asp:TextBox runat="server" ID="txtLogin" Text="" placeholder="Enter User name for login"
                                                        class="formcontrol" MaxLength="50" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()"
                                                                OnClick="btnSave_click" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="Button1" runat="server" CssClass="buttonGrey xlarge"
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
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1 style="padding-top: 10px; padding-bottom: 15px">
                                Change password</h1>
                            <div class="divider">
                            </div>
                            <div class="row" id="divErrPass" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12">
                                    <div class="alertBox error">
                                        <h4>
                                            ERROR! <span>
                                                <asp:Literal ID="lblErrPass" runat="server"></asp:Literal></span></h4>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divSuccpass" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12">
                                    <div class="alertBox success">
                                        <h4>
                                            Success ! <span>
                                                <asp:Literal ID="lblSuccPass" runat="server"></asp:Literal></span></h4>
                                    </div>
                                </div>
                            </div>
                            <div class="login-form">
                                <div class="row">
                                    <div class="col-lg-6 col-md-6">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Old Password
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtOldPassword" placeholder="Enter Old Password"
                                                        class="formcontrol" MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        New Password
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtNewPass" placeholder="Enter New Password" class="formcontrol"
                                                        MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Confirm Password
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtConfirmPass" placeholder="Enter Confirm Password"
                                                        class="formcontrol" MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-11 col-md-12 col-sm-11">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-5 col-md-5 ">
                                                            <asp:Button Text="Save" ID="btnSavePass" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()"
                                                                OnClick="btnSavePass_click" />
                                                        </div>
                                                        <div class="col-lg-5 col-md-5">
                                                            <asp:Button Text="Cancel" ID="btnCancel1" runat="server" CssClass="buttonGrey xlarge"
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
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
    </div>
</asp:Content>
