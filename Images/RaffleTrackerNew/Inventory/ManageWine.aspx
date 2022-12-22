<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory/Inventory.master" AutoEventWireup="true" CodeFile="ManageWine.aspx.cs" Inherits="Inventory_ManageWine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
                            <h1>Manage WineStock</h1>
                            <span style="float: right; padding-left: 10px;">
                                <asp:Button Text="Back" ID="btnBack" runat="server" CssClass="buttonGrey xlarge" /></span>
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
                                                        Category
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCategory" ErrorMessage="Please Select catagory" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11">
                                                       
                                                    </asp:DropDownList>

                                                </div>
                                            </div>  

                                         <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                       Wine Type
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlWineType" ErrorMessage="Please Select Wine Type" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:DropDownList ID="ddlWineType" runat="server" CssClass="formcontrol formDropdown1" Font-Size="11">
                                                       
                                                    </asp:DropDownList>

                                                </div>
                                            </div>  
                                            
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                   Location1 Qty
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" TextMode="Number" runat="server" ControlToValidate="txtL1Qty" ErrorMessage="Please enter Location1  Qty" Display="Dynamic" ValidationGroup="vldExp" Font-Size="9" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:TextBox runat="server" ID="txtL1Qty" ClientIDMode="Static" placeholder="Enter Location1 Qty" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                          
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                   Location2 Qty
                                                    </label>
                                                    
                                                  
                                                    <asp:TextBox runat="server" ID="txtL2Qty" TextMode="Number" ClientIDMode="Static" placeholder="Enter Location2 Qty" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                           
                                            <div class="col-lg-12 col-md-12 col-sm-12">
                                                <div class="formBlock">
                                                    <label>
                                                   Location3 Qty
                                                    </label>
                                                   
                                                    
                                                    <asp:TextBox runat="server" ID="txtL3Qty" TextMode="Number" ClientIDMode="Static" placeholder="Enter Location3 Qty" class="formcontrol"
                                                        MaxLength="200"></asp:TextBox>

                                                </div>
                                            </div>
                                             
                                             
                                             <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()" ValidationGroup="vldExp" OnClick="btnSave_Click"
                                                                />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                                                />
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
</asp:Content>

