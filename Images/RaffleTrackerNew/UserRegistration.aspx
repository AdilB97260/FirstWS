<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="UserRegistration.aspx.cs" Inherits="UserRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    
    <link href="css/bootstrap.min1.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=main_lstReport]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            //BindUserData();
        });

        function ValidateForm() {
            var IsValid = true;
            var arrControls = ["txtUserName", "txtPassword", "ddlDistUser"];

            $.each(arrControls, function (index, value) {
                var _controlName = "#main_" + value;
                if ($(_controlName).val() == "") {
                    IsValid = false;
                    $(_controlName).closest("div").addClass("has-error");
                }
                else if ($("ddlDistUser").val() == "0") {
                    IsValid = false;
                    $("ddlDistUser").closest("div").addClass("has-error");
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



        //        $(function () {
        //            $("#main_dllDistUserType").change(function () {
        //                $.ajax({
        //                    type: "POST",
        //                    url: "/UserRegistration.aspx/GetUserList",
        //                    data: "{id: '" + $("#main_dllDistUserType").val() + "'}",
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: function (response) {
        //                        var obj = JSON.parse(response.d);
        //                        var ddlUsers = $("[id*=ddlDistUser]");
        //                        ddlUsers.empty().append('<option selected="selected" value="0">Please select</option>');
        //                        for (var i = 0; i < obj.length; i++) {
        //                            ddlUsers.append('<option value=' + obj[i].user_pk + '>' + obj[i].name + '</option>');
        //                        }

        //                        $("#main_hdnDistUserFk").val("0");

        //                    },
        //                    failure: function (response) {
        //                        alert("Distribution User Not found");
        //                    },
        //                    error: function (response) {
        //                        alert(response.d);
        //                    }
        //                });

        //            });

        //            $("#ddlDistUser").change(function () {
        //                $("#main_hdnDistUserFk").val($("#ddlDistUser").val());
        //            });





        //        });


        //        function BindUserData() {

        //            if ($("#main_hdnUserId").val() != '' && $("#main_hdnUserId").val() != "0") {
        //                $.ajax({
        //                    type: "POST",
        //                    url: "UserRegistration.aspx/GetUserDetails",
        //                    data: "{id: '" + $("#main_hdnUserId").val() + "'}",
        //                    contentType: "application/json; charset=utf-8",
        //                    dataType: "json",
        //                    success: OnSuccess,
        //                    failure: function (response) {
        //                        alert(response.d);
        //                    },
        //                    error: function (response) {
        //                        alert(response.d);
        //                    }
        //                });
        //            }
        //        }

        //        function OnSuccess(response) {
        //            var obj = JSON.parse(response.d);
        //            
        //            $.ajax({
        //                type: "POST",
        //                url: "/UserRegistration.aspx/GetUserList",
        //                data: "{id: '" + obj.DistUserType + "'}",
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "json",
        //                async:false,
        //                success: function (response) {
        //                    var obj = JSON.parse(response.d);
        //                    var ddlUsers = $("[id*=ddlDistUser]");
        //                    ddlUsers.empty().append('<option selected="selected" value="0">Please select</option>');
        //                    for (var i = 0; i < obj.length; i++) {
        //                        ddlUsers.append('<option value=' + obj[i].user_pk + '>' + obj[i].name + '</option>');
        //                    }

        //                    $("#main_hdnDistUserFk").val(obj.DistUser_Fk);

        //                  

        //                },
        //                failure: function (response) {
        //                    alert("Distribution User Not found");
        //                },
        //                error: function (response) {
        //                    alert(response.d);
        //                }
        //            });

        //            $("#ddlDistUser").val(obj.DistUser_Fk);

        //        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="container">
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
                                <h1 style="text-transform: inherit">
                                    Manage Users
                                </h1>
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
                                        <div class="col-lg-12 col-md-12">
                                            <div class="row">
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="formBlock">
                                                        <label>
                                                            Full Name
                                                        </label>
                                                        <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                        <asp:TextBox runat="server" ID="txtName" placeholder="Enter Name" class="formcontrol"
                                                            MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="formBlock">
                                                        <label>
                                                            User Authority Level
                                                        </label>
                                                        <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                        <asp:DropDownList runat="server" ID="ddlRoleType" class="formDropdown">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="formBlock">
                                                        <label>
                                                            Login Name</label>
                                                        <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                        <asp:TextBox runat="server" ID="txtLogin" placeholder="Enter Login User Name" class="formcontrol"
                                                            MaxLength="100"></asp:TextBox>
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
                                                            Email</label>
                                                        <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter Email" class="formcontrol"
                                                            MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="formBlock">
                                                        <label>
                                                            Address</label>
                                                        <asp:TextBox runat="server" ID="txtAdd" TextMode="SingleLine" placeholder="Enter Full Address"
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
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <div class="formBlock">
                                                        <label>
                                                            Reports View</label>
                                                            <br />
                                                        <asp:ListBox ID="lstReport" runat="server" SelectionMode="Multiple">
                                                            <asp:ListItem Text="Sales by Dist Report" Value="SALESDISTREP" />
                                                            <asp:ListItem Text="Sales by Date Range" Value="SALESDATERANGEREP" />
                                                            <asp:ListItem Text="Distribution Report" Value="DISTREP" />
                                                            <asp:ListItem Text="View Ticket Dist" Value="TICKDISTREP" />
                                                        </asp:ListBox>
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
                        </div>
                    </div>
                </div>
            </div>
            <!-- end row -->
            <!-- end form -->
        </div>
    </div>
    <!-- end login form -->
</asp:Content>
