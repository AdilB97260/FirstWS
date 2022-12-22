<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory/Inventory.master" AutoEventWireup="true" CodeFile="InventorySearch.aspx.cs" Inherits="Inventory_InventorySearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                            <h1>Wine Search</h1>
                            <div class="divider">
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
                                            <div class="col-lg-3 col-md-3 col-sm-3">
                                                <div class="formBlock">
                                                    <label>
                                                        Wine Name
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtWineName" ClientIDMode="Static" placeholder="Enter Id" class="formcontrol"
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
                                                            <asp:Button Text="Search" ID="btnSave" runat="server" CssClass="buttonColorSmall" OnClick="btnSave_Click" />
                                                        </div>
                                                        <div class="col-lg-3 col-md-3">
                                                            <asp:Button Text="Reset" ID="btncancel" runat="server" CssClass="buttonGreySmall" Style="font-weight: 500" OnClick="btncancel_Click" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnUserId" runat="server" />
                                        </div>
                                    </div>


                                    <div class="col-lg-12 col-md-12" style="padding-top: 2%; padding-right:0!important">
                                        <div class="col-lg-2 col-md-2" style="padding: 0!important">
                                            <h3>Wine Details</h3>
                                        </div>

                                        <div class="col-lg-2 col-md-2">
                                            &nbsp;
                                        </div>
                                        <div class="col-lg-2 col-md-2">
                                            <asp:Button ID="btnImport" runat="server" CssClass="buttonGreySmall" Text=" Import Data" OnClick="btnImport_Click"  />
                                        </div>
                                        <div class="col-lg-2 col-md-2">
                                            <asp:Button ID="btnExport" runat="server" CssClass="buttonGreySmall" Text=" Export CSV" OnClick="btnExport_Click" />
                                             
                                        </div>
                                        <div class="col-lg-2 col-md-2">
                                           <asp:Button ID="btnStockUpdate" runat="server" CssClass="buttonColorSmall" Text=" Stock Update" OnClick="btnStockUpdate_Click"  />
                                        </div>
                                        <div class="col-lg-2 col-md-2">
                                            <asp:Button Text="Add Wine" ID="btnAddInv" runat="server" CssClass="buttonColorSmall"
                                                OnClick="btnAddInv_Click" />
                                        </div>

                                    </div>
                                    <div class="col-lg-12 col-md-12">
                                        <div class="table-responsive" style="padding-bottom: 15px; font-family: 'montserratregular', sans-serif; font-size: 12px;">
                                            <table class="table table-striped table-bordered table-hover" id="dataTables-example" style="width: 100%; padding-top: 15px;">
                                                <thead>
                                                    <tr>
                                                        <th>Wine Name
                                                        </th>

                                                        <th>Rec No
                                                        </th>
                                                        <th>Bottle Size
                                                        </th>

                                                        <th>Location1 Qty
                                                        </th>
                                                        <th>Location2 Qty
                                                        </th>
                                                        <th>Location3 Qty
                                                        </th>
                                                        <th style="width: 11%">Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptWine">
                                                        <ItemTemplate>
                                                            <tr class="odd gradeX">
                                                                <td>

                                                                    <a href="/Inventory/UpdateStock.aspx?id=<%# Eval("Inventory_ID").ToString() %>">
                                                                        <%# Eval("Wine_name")%>
                                                                    </a>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("RecNo") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("BottleSizeName") %>
                                                                </td>

                                                                <td>
                                                                    <%# Eval("Location1_Qty") %>
                                                                </td>
                              |                                  <td>
                                                                    <%# Eval("Location2_Qty") %>
                                                                </td>

                                                                <td>
                                                                    <%# Eval("Location3_qty") %>
                                                                </td>

                                                                <td style="min-width: 5%">
                                                                    <a href="/Inventory/AddInventory.aspx?id=<%# Eval("Inventory_ID").ToString() %>" class='btn btn-xs btn-go'>
                                                                        <span style='font-size: 11px; color: Maroon'>Edit</span> </a>
                                                                    &nbsp;&nbsp;<a href="javascript:;" id="adelete" style="color: Maroon; padding-top: 1px" onclick="DeleteInventory('<%# Eval("Inventory_ID").ToString() %>')">Delete</a> 
                                                                </td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td colspan="7" style="text-align: left; color: maroon"></td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>

                                </div>

                            </div>
                        </div>
                        <!-- end row -->


                        <!-- end form -->
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function DeleteInventory(id) {
            jConfirm('Are you sure for delete Inventory ?', 'Confirmation', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "/Inventory/InventorySearch.aspx/DeleteInventory",
                        data: "{tid: '" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d == "true") {
                                jAlert("Inventory deleted successfully.", "Delete Successs", true, window.location.href);
                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                        error: function (response) {
                            //alert(response.d);
                        }
                    });
                }

                return false;
            });
        }
    </script>

    <script type="text/javascript" src="/js/plugins/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="/js/plugins/dataTables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var table = $('#example').DataTable({
                responsive: true
            });

            $('#dataTables-example').dataTable({
                "pageLength": 2,
                "bSort": true,
                "bprocessing": true,
                "bserverSide": true,
                //"serverSide": true,
                "columns": [null, null, null, null, null, null, { "orderable": false }]
            });


        });

        //        $(document).ready(function () { $('#example').DataTable({ "order": [[3, "desc"]] }); });



        $(document).ajaxStart(function () {
            $('#overlay').show();
        }).ajaxSuccess(function () {
            $('#overlay').hide();
        }).ajaxComplete(function () {
            $('#overlay').hide();
        }).ajaxError(function () {
            $('#overlay').hide();
        }).ajaxStop(function () {
            $('#overlay').hide();
        });


    </script>

</asp:Content>

