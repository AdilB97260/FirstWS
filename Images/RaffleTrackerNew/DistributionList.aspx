<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="DistributionList.aspx.cs" Inherits="DistributionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 540px">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="row">
                            <div class="col-lg-12" style="padding-left: 20px; padding-top: 5px; text-align: center">
                                <div class="col-lg-8" style="padding-left: 0px!important">
                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                        <div style="float: left; text-align: left; padding-top: 8px; vertical-align: middle;
                                            width: 15%">
                                            <b>Search Ticket:</b>
                                            <asp:RequiredFieldValidator ID="reqVal" runat="server" ControlToValidate="txtSearch"
                                                Display="Dynamic" Text="*" ForeColor="Red" ValidationGroup="VlGreqTicket"></asp:RequiredFieldValidator>
                                        </div>
                                        <div style="float: left; width: 27%">
                                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Ticket No" class="formcontrol talcenter"
                                                Width="125" type="number"></asp:TextBox>
                                        </div>
                                        <div style="float: left; width: 17%">
                                            <asp:Button Text="Search" ID="btnSearch" runat="server" class="buttonColorSmall"
                                                ValidationGroup="VlGreqTicket" OnClick="btnSearch_Click" />
                                        </div>
                                        <div style="float: left; width: 18%">
                                            <asp:Button Text="RESET" ID="Button1" runat="server" class="buttonGreySmall" OnClick="btnReset_Click"
                                                Width="80" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 20px">
                            <div class="panel-body" style="padding-top: 0px!important">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                        id="dataTables-example">
                                        <thead>
                                            <tr>
                                                <th>
                                                    Time Stamp
                                                </th>
                                                <th>
                                                    Distributor Name
                                                </th>
                                                <th>
                                                    Address
                                                </th>
                                                <th style="min-width: 80px">
                                                    State
                                                </th>
                                                <th style="min-width: 80px">
                                                    Zip
                                                </th>
                                                <th style="min-width: 80px">
                                                    Phone
                                                </th>
                                                <th>
                                                    Ticket From
                                                </th>
                                                <th>
                                                    Ticket To
                                                </th>
                                                <th>
                                                    Total Ticket
                                                </th>
                                                <th style="min-width: 50px">
                                                    Action
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptSale">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                        <td style="min-width: 110px">
                                                            <%# Eval("strCreatedDate")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ToDistUserName")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("address")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("state")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("zip")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("phone")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FromTicket")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("ToTicket")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TotalTickets")%>
                                                        </td>
                                                        <td style="min-width: 40px">
                                                            <a class='btn btn-xs btn-go clsView' data-id="<%# Eval("TicketDistribution_PK").ToString() %>">
                                                                <span style='font-size: 11px; color: Maroon'>VIEW</span> </a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-lg-12" style="text-align: left; float: left; margin-top: 10px; padding-left: 0px!important;">
                                    <b>Total Ticket: </b>&nbsp;<asp:Label ID="lblTotalTick" runat="server" Font-Bold="true"></asp:Label>
                                    <%--  &nbsp;&nbsp;&nbsp; <b>Total Amount: </b>$<asp:Label ID="lblTotAmt" runat="server"
                                        Font-Bold="true"></asp:Label>--%>
                                </div>
                                <!-- /.table-responsive -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="overlay" style="display: none">
        <img id="loading-image" src="images/wait.GIF" alt="Loading..." />
    </div>
    <div class="modal fade" id="ModalSalesDetails" tabindex="-1" role="dialog" aria-hidden="true"
        style="display: none;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="background: white;">
                <div class="modal-header" style="background-color: #428bca">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">
                        <asp:Literal ID="ltrRFCResiName" runat="server" Text="" />
                        <span style="color: #fff; font-size: 14px;">View Ticket Distribution History</span></h4>
                </div>
                <div class="modal-body row" style="padding: 20px">
                    <div class="form-group" id="divRFC">
                        <div class="row" style="float: left; padding-left: 42px; padding-top: 30px; font-weight: bold;
                            width: 100%">
                            Distribution History
                        </div>
                        <div class="row" style="float: left; padding-left: 42px; padding-top: 10px;">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover" id="tblData" style="width: 95%;
                                    padding-top: 15px;">
                                    <thead>
                                        <tr>
                                            <th style="width: 14%">
                                                Time Stamp
                                            </th>
                                          <%--  <th style="width: 14%">
                                                From Distribute
                                            </th>--%>
                                            <th style="width: 14%">
                                                Distribute Name
                                            </th>
                                            <th style="width: 10%">
                                                Ticket From
                                            </th>
                                            <th style="width: 9%">
                                                Ticket To
                                            </th>
                                            <th style="width: 10%">
                                                Total Ticket
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default btnClose" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Page-Level Demo Scripts - Tables - Use for reference -->
    <script type="text/javascript" src="/js/plugins/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="/js/plugins/dataTables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var table = $('#example').DataTable({
                responsive: true
            });



            $("#dataTables-example").on("click", ".clsView", function () {
                var _id = $(this).data("id");
                $.ajax({
                    type: "POST",
                    url: "/DistributionList.aspx/GetSalesData",
                    data: "{tid: '" + _id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var objSaleData = JSON.parse(response.d);

                        $("#tblData > tbody").html("");

                        var data = "";
                        $.each(objSaleData.TicketDistList, function (idx, obj) {
                            //data = "<tr><td>" + obj.strCreatedDate + "</td><td> " + obj.DistUserName + "</td><td>" + obj.LastDistUserName + "</td><td>" + obj.FromTicket + "</td><td>" + obj.ToTicket + "</td><td>" + obj.TotalTickets + "</td></tr>";
                            data = "<tr><td>" + obj.strCreatedDate + "</td><td>" + obj.LastDistUserName + "</td><td>" + obj.FromTicket + "</td><td>" + obj.ToTicket + "</td><td>" + obj.TotalTickets + "</td></tr>";
                            $("#tblData > tbody").append(data);
                        });

                        $('#ModalSalesDetails').modal('show');

                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });


            });

            $('#dataTables-example').dataTable({
                "pageLength": 10,
                "bSort": true,
                "order": [[0, "desc"]],
                "columns": [null, null, null, null, null, null, null, null, null, { "orderable": false}]
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
