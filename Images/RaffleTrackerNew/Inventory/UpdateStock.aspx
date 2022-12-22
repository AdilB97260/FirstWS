<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory/Inventory.master" AutoEventWireup="true" CodeFile="UpdateStock.aspx.cs" Inherits="Inventory_UpdateStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        ._23FHuj {
            width: 40px;
            height: 40px;
            background: linear-gradient(#fff,#f9f9f9);
            display: inline-block;
            border: 1px solid #c2c2c2;
            cursor: pointer;
            font-size: 16px;
            border-radius: 50%;
            padding-top: 1px;
            line-height: 1;
        }

        ._3dY_ZR ._26HdzL ._253qQJ {
            border: none;
            width: 100%;
            font-size: 14px;
            font-weight: 500;
            vertical-align: middle;
            text-align: center;
        }

        ._3dY_ZR ._26HdzL {
            display: inline-block;
            padding: 3px 6px;
            width: calc(100% - 60px);
            height: 100%;
            width: 60px;
            height: 28px;
            background-color: #fff;
            margin: 0 5px;
        }

        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        input[type=number] {
            -moz-appearance: textfield;
        }
    </style>
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
                        <asp:HiddenField ID="hdnInventoryID" runat="server" />
                        <div class="col-lg-12 col-md-12">
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12" runat="server" id="divError">
                        <div class="alertBox error">
                            <h4>ERROR! <span>
                                <asp:Literal ID="lblErrorMsg" runat="server"></asp:Literal></span></h4>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12 col-md-12">
            <div class="login-form" style="float: left">
                <div class="row" style="float: left">


                    <div class="col-lg-12 col-md-12" id="dvSearch" runat="server">

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="formBlock">
                                <label>
                                    Rec No
                                </label>
                                <asp:TextBox runat="server" ID="txtRecNo" ClientIDMode="Static" placeholder="Enter recNo" class="formcontrol"
                                    MaxLength="200"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-3">
                            <div class="formBlock">
                                <label>
                                    Barcode
                                </label>
                                <asp:TextBox runat="server" ID="txtBarcode" ClientIDMode="Static" placeholder="Enter Barcode" class="formcontrol"
                                    MaxLength="200"></asp:TextBox>

                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-12">
                            <div class="formBlock" style="padding-top: 20px;">
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 ">
                                        <asp:Button Text="Search" ID="btnSearch" runat="server" CssClass="buttonColorSmall" OnClick="btnSearch_Click" />
                                    </div>
                                    <div class="col-lg-3 col-md-3">
                                        <asp:Button Text="Reset" ID="btnreset" runat="server" CssClass="buttonGreySmall" Style="font-weight: 500" OnClick="btnreset_Click" />
                                    </div>

                                </div>
                            </div>
                        </div>


                    </div>


                    <div class="row"">
                        <div class="col-lg-9 col-md-9 col-sm-12" style="margin-top:15px">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <label>
                                        <b>Wine Name:-</b>
                                    </label>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-9">
                                    <asp:Label ID="lblWineName" runat="server" Font-Size="11" class="formcontrol" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    <label>
                                        <b>Bottle Size:-</b>
                                    </label>
                                </div>
                                <div class="col-lg-9 col-md-9 col-sm-9">
                                    <asp:Label ID="lblBottleSize" runat="server" Font-Size="11" class="formcontrol" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-12" style="margin-top:15px">
                            <div class="col-lg-6 col-md-6 col-sm-12" style="float: right;">
                                <asp:Button Text="Save" ID="btnAddInv" runat="server" CssClass="buttonColorSmall" OnClick="btnAddInv_Click" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-12" style="float: right;">
                                <asp:Button Text="Back" ID="btnBack" runat="server" CssClass="buttonGreySmall" OnClick="btnBack_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: center; margin-top: 3%; float: left">
                        <label style="text-align: center">
                            <b>Adjust location(s) and quantities here</b>
                        </label>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-12" style="text-align: center; float: left">

                        <div class="col-lg-4 col-md-4 col-sm-4" style="padding-top: 3%">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                Location 1 
                             <asp:TextBox ID="txtloc1Name" placeholder="Enter Location1" Width="130px" ClientIDMode="Static" runat="server"></asp:TextBox>

                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="_3dY_ZR" style="padding-top: 5%">
                                    <button class="_23FHuj" onclick="RemoveQty('1');">–</button>
                                    <div class="_26HdzL">
                                        <asp:TextBox ID="txtQty1" type="number" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </div>
                                    <button class="_23FHuj" onclick="AddQty('1');">+</button>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4" style="padding-top: 3%">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                Location 2 
                             <asp:TextBox ID="txtLoc2Name" placeholder="Enter Location2" Width="130px" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="_3dY_ZR" style="padding-top: 5%">
                                    <button class="_23FHuj" onclick="RemoveQty('2');">–</button>
                                    <div class="_26HdzL">
                                        <asp:TextBox ID="txtQty2" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <button class="_23FHuj" onclick="AddQty('2');">+</button>
                                </div>

                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4" style="padding-top: 3%">
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                Location 3 
                             <asp:TextBox ID="txtloc3Name" placeholder="Enter Location3" Width="130px" ClientIDMode="Static" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12">
                                <div class="_3dY_ZR" style="padding-top: 5%">
                                    <button class="_23FHuj" onclick="RemoveQty('3');">–</button>
                                    <div class="_26HdzL">
                                        <asp:TextBox ID="txtQty3" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <button class="_23FHuj" onclick="AddQty('3');">+</button>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script>
        function AddQty(loc) {
            if (loc == "1") {
                var qty = (parseInt($('#<%= txtQty1.ClientID %>').val()));
                if (isNaN(qty)) qty = 0;
                $('#<%= txtQty1.ClientID %>').val(qty + 1);
            }
            else if (loc == "2") {
                var qty = (parseInt($('#<%= txtQty2.ClientID %>').val()));
                if (isNaN(qty)) qty = 0;
                $('#<%= txtQty2.ClientID %>').val(qty + 1);
            }
            else if (loc == "3") {
                var qty = (parseInt($('#<%= txtQty3.ClientID %>').val()));
                if (isNaN(qty)) qty = 0;
                if (qty == NaN) qty = 0;
                $('#<%= txtQty3.ClientID %>').val(qty + 1);
            }
}
function RemoveQty(loc) {
    if (loc == "1") {
        var qty = (parseInt($('#<%= txtQty1.ClientID %>').val()));
        if (isNaN(qty)) qty = 0;
        if (qty != 0) {
            $('#<%= txtQty1.ClientID %>').val(qty - 1);
        }
    }
    else if (loc == "2") {
        var qty = (parseInt($('#<%= txtQty2.ClientID %>').val()));
        if (isNaN(qty)) qty = 0;
        if (qty != 0) {
            $('#<%= txtQty2.ClientID %>').val(qty - 1);
        }
    }
    else if (loc == "3") {
        var qty = (parseInt($('#<%= txtQty3.ClientID %>').val()));
        if (isNaN(qty)) qty = 0;
        if (qty != 0) {
            $('#<%= txtQty3.ClientID %>').val(qty - 1);
        }
    }
}

    </script>

</asp:Content>

