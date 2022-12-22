<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory/Inventory.master" AutoEventWireup="true" CodeFile="AddInventory.aspx.cs" Inherits="Inventory_AddInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode != 46 && (charCode < 48 || charCode > 57)))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <asp:HiddenField ID="hdnInventoryID" runat="server" />
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1>Manage Wine</h1>
                            <span style="float: right; padding-left: 10px;">
                                <asp:Button Text="Back" ID="btnBack" runat="server" CssClass="buttonGrey xlarge" OnClick="btnBack_Click" /></span>
                            <div class="divider" style="margin-top: 1px">
                            </div>
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
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Wine Name
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWineName" ErrorMessage="Please enter Wine Name" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtWineName" ClientIDMode="Static" placeholder="Enter Wine Name" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Barcode
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtBarcode" ClientIDMode="Static" placeholder="Enter Barcode" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                             <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Rec No
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtRecNo" ClientIDMode="Static" placeholder="Enter Rec No" class="formcontrol" TextMode="Number"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <div class="col-lg-10 col-md-10 col-sm-10" style="padding-left:0px!important; padding-right:0px!important;">
                                                    <label>
                                                        Category 
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Please Select Category" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                        </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2" style="padding-right:0px!important; padding-left:0px!important;  padding-top:2.3%">
                                                        <span style="float:left; margin-left:10px; width:94%">
                                                        <asp:Button Text="Add New Category" ID="btnCategory" runat="server" CssClass="buttonColorSmall" OnClick="btnCategory_Click"  />
                                                            </span>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        AlfaSort
                                                    </label>

                                                    <asp:TextBox runat="server" ID="txtDescription" ClientIDMode="Static" placeholder="Enter Description" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <div class="col-lg-10 col-md-10 col-sm-10" style="padding-left:0px!important; padding-right:0px!important;">
                                                        <label>
                                                            Bottle Size
                                                        </label>
                                                        <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlBottleSizeId" ErrorMessage="Please Select Bottle Size" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        <asp:DropDownList ID="ddlBottleSizeId" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2" style="padding-right:0px!important; padding-left:0px!important;  padding-top:2.3%">
                                                        <span style="float:left; margin-left:10px; width:94%">
                                                        <asp:Button Text="Add New Bottle" ID="BtnAddBottleSize" runat="server" CssClass="buttonColorSmall" OnClick="BtnAddBottleSize_Click" />
                                                            </span>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Bottle Price
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBottleprice" ErrorMessage="Please enter Price" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtBottleprice" ClientIDMode="Static" onkeypress="return isNumberKey(event)" placeholder="Enter Bottle Price" class="formcontrol"
                                                        MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Cost Wholesale
                                                    </label>
                                                    
                                                    
                                                    <asp:TextBox runat="server" ID="txtWholesale" ClientIDMode="Static" placeholder="Enter Cost Wholesale " onkeypress="return isNumberKey(event)" class="formcontrol"
                                                        MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                     <div class="col-lg-10 col-md-10 col-sm-10" style="padding-left:0px!important; padding-right:0px!important;">
                                                    <label>
                                                        Heading
                                                    </label>
                                                   <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlHeadingId" ErrorMessage="Please Select Heading" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    
                                                    <asp:DropDownList ID="ddlHeadingId" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11">
                                                    </asp:DropDownList>
                                                         </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2" style="padding-right:0px!important; padding-left:0px!important;  padding-top:2.3%">

                                                         <span style="float:left; margin-left:10px; width:94%">
                                                        <asp:Button Text="Add New Heading " ID="BtnaddHeading" runat="server" CssClass="buttonColorSmall" OnClick="BtnaddHeading_Click" /></span>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                     <div class="col-lg-10 col-md-10 col-sm-10" style="padding-left:0px!important; padding-right:0px!important;">
                                                    <label>
                                                        Sub Heading
                                                    </label>
                                                   
                                                    <asp:DropDownList ID="ddlSubHeadingId" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11">
                                                    </asp:DropDownList>
                                                         </div>
                                                    <div class="col-lg-2 col-md-2 col-sm-2" style="padding-right:0px!important; padding-left:0px!important;  padding-top:2.3%">
                                                        <span style="float:left; margin-left:10px; width:94%">
                                                         <asp:Button Text="Add New SubHeading " ID="btnSubHeading" runat="server" CssClass="buttonColorSmall" OnClick="btnSubHeading_Click" /></span>
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Location1 
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLocName" ErrorMessage="Please enter Location1" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtLocName" ClientIDMode="Static" placeholder="Enter Location1 " class="formcontrol" 
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Location1 Qty
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLoc1Qty" ErrorMessage="Please enter Location1  Qty" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtLoc1Qty" ClientIDMode="Static" placeholder="Enter Location1 Qty" class="formcontrol" TextMode="Number"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Location2 
                                                    </label>


                                                    <asp:TextBox runat="server" ID="txtLoc2Name" ClientIDMode="Static"  placeholder="Enter Location2 " class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Location2 Qty
                                                    </label>


                                                    <asp:TextBox runat="server" ID="txtLoc2Qty" ClientIDMode="Static" TextMode="Number" placeholder="Enter Location2 Qty" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Location3
                                                    </label>


                                                    <asp:TextBox runat="server" ID="txtLoc3Name" ClientIDMode="Static"  placeholder="Enter Location3 " class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                        Location3 Qty
                                                    </label>


                                                    <asp:TextBox runat="server" ID="txtLoc3Qty" ClientIDMode="Static" TextMode="Number"  placeholder="Enter Location3 Qty" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>

                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()" ValidationGroup="vldExp" OnClick="btnSave_Click" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="btncancel" runat="server" CssClass="buttonGrey xlarge" OnClick="btncancel_Click" />
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

