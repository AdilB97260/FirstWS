<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true" CodeFile="ManageExpencess.aspx.cs" Inherits="admin_ManageExpencess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
     
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function ValidateForm() {

            var IsValid = true;
            var arrControls = ["txtUserName", "txtExpensesName", "txtToWhomeGiven", "txtAmount"];

            $.each(arrControls, function (index, value) {
                var _controlName = "main_" + value;
                
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

        


        $('#txtExpDate').datepicker(
            {
                dateFormat: 'mm/dd/yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '2018:2030'
            });
         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
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
                                Expenses</h1>
                            <span style="float: right; padding-left: 10px;">
                                <asp:Button Text="Back" ID="btnBack" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btnBack_Click" /></span>
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
                                             <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Date Of Expenses
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span> <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtExpDate" ErrorMessage="Please enter Expenses Date" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtExpDate" ClientIDMode="Static" placeholder="Enter To Amount" class="formcontrol"
                                                        MaxLength="12"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Category 
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExpensesName" ErrorMessage="Please enter Expenses Name" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11">
                                                        <asp:ListItem Text="Prizes" Value="Prizes"></asp:ListItem>
                                                        <asp:ListItem Text="Mailings & Postage" Value="Mailings & Postage"></asp:ListItem>
                                                        <asp:ListItem Text="IT" Value="IT"></asp:ListItem>
                                                        <asp:ListItem Text="Compliance" Value="Compliance"></asp:ListItem>
                                                        <asp:ListItem Text="Office supplies" Value="Office supplies"></asp:ListItem>
                                                        <asp:ListItem Text="Advertising" Value="Advertising"></asp:ListItem>
                                                        <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Expenses Name
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpensesName" ErrorMessage="Please enter Expenses Name" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtExpensesName" ClientIDMode="Static" placeholder="Enter Name Expenses" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                           <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        To Whom Given
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtToWhomeGiven" ErrorMessage="Please enter Given To" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtToWhomeGiven" ClientIDMode="Static" placeholder="Enter To Whom Given" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                           <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Amount
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span> <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please enter Amount" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtAmount" type="number" step="any" ClientIDMode="Static" placeholder="Enter To Amount" class="formcontrol"
                                                        MaxLength="7"></asp:TextBox>
                                                    
                                                </div>
                                            </div>
                                           
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Description
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;"></span>
                                                    <asp:TextBox runat="server" ID="txtDesc" placeholder="Enter Description" class="formcontrol"
                                                        MaxLength="1000"></asp:TextBox>
                                                </div>
                                            </div>

                                             <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Receipt (Upload only pdf file)
                                                    </label>
                                                     <span><input id="oFile" type="file" runat="server" name="oFile" /></span>
                                                </div>
                                            </div>

                                            <!-- end row -->
                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()" ValidationGroup="vldExp"
                                                                OnClick="btnSave_Click" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                                                OnClick="btncancel_Click" />
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

