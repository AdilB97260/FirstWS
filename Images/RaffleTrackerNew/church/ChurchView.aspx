<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="ChurchView.aspx.cs" Inherits="Church_ChurchView" %>

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
                                Church Tickets Distribution To Members
                            </h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="Button1" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" />
                            </span><span style="float: right; padding-right: 10px">
                                <asp:Button Text="Add more tickets for Church" ID="btnAddTicket" runat="server" class="buttonColor"
                                    OnClick="btnAddTicket_Click" /></span>
                            <div class="divider" style="margin-bottom:15px!important">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="row" style="padding-left: 15px;">
                                <div class="col-lg-12" style="padding-left: 0px; padding-top: 5px; padding-bottom:15px; text-align: center">
                                    <div class="col-lg-12" style="padding-left: 0px!important;padding-right: 0px!important">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                                            <div class="col-lg-8" style="padding-left: 0px; padding-top: 5px; text-align: center">
                                                <div style="float: left; text-align: left; padding-top: 8px; vertical-align: middle;
                                                    width: 14%">
                                                    <b>Search Ticket:</b>
                                                    <asp:RequiredFieldValidator ID="reqVal" runat="server" ControlToValidate="txtSearch"
                                                        Display="Dynamic" Text="*" ForeColor="Red" ValidationGroup="VlGreqTicket"></asp:RequiredFieldValidator>
                                                </div>
                                                <div style="float: left; width: 22%">
                                                    <asp:TextBox ID="txtSearch" runat="server" placeholder="Enter Ticket No" class="formcontrol talcenter"
                                                        Width="105" type="number"></asp:TextBox>
                                                </div>
                                                <div style="float: left; width: 15%">
                                                    <asp:Button Text="Search" ID="btnSearch" runat="server" class="buttonColorSmall"
                                                        ValidationGroup="VlGreqTicket" OnClick="btnSearch_Click" />
                                                </div>
                                                <div style="float: left; width: 15%">
                                                    <asp:Button Text="RESET" ID="Button2" runat="server" class="buttonGreySmall" OnClick="btnReset_Click"
                                                        Width="80" />
                                                </div>
                                                <div style="float: right;">
                                                    <asp:Button Text="Download Excel File" ID="btnDownloadFile" runat="server" class="buttonColorSmall"
                                                        OnClick="DownloadFile_Click" Width="180" />
                                                </div>
                                            </div>
                                            <div class="col-lg-4" style="padding-left: 0px; padding-top: 5px; text-align: right">
                                                <b style="color: Maroon">Total Ticket Distribution to Members&nbsp;: </b>&nbsp;&nbsp;<asp:Label
                                                    ID="lblTotChurch" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:Panel ID="pnlSale" runat="server" Visible="false">
                                    <div class="row" style="float: left; padding-bottom: 15px; padding-top: 5px; width: 100%">
                                        <div class="row" style="float: left; background-color: #eae7e7; margin-left: 3%;
                                            width: 97%; padding: 15px">
                                            <span style="padding-left: 30px; color: #31708f; font-weight: bold">Ticket From :</span>
                                            <span style="padding-left: 15px; color: #000; font-weight: bold">
                                                <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label>
                                            </span><span style="padding-left: 30px; color: #31708f; font-weight: bold">Ticket To
                                                :</span> <span style="padding-left: 15px; color: #000; font-weight: bold">
                                                    <asp:Label ID="lblTo" runat="server" Text=""></asp:Label>
                                                </span><span style="float: left; width: 100%; padding-top: 15px; padding-left: 30px;">
                                                    <span style="color: Maroon; font-weight: bold">Last Distributer Name :</span> <span
                                                        style="padding-left: 10px; color: #000; font-weight: bold">
                                                        <asp:Label ID="lblLastDistName" runat="server" Text=""></asp:Label></span> <span
                                                            style="padding-left: 32px; color: Maroon; font-weight: bold">Collection By :</span>
                                                    <span style="padding-left: 10px; color: #000; font-weight: bold">
                                                        <asp:Label ID="lblCollectionBy" runat="server" Text=""></asp:Label></span> <span
                                                            style="padding-left: 32px; color: Maroon; font-weight: bold">Sold To :</span>
                                                    <span style="padding-left: 10px; color: #000; font-weight: bold">
                                                        <asp:Label ID="lblSoldTo" runat="server" Text=""></asp:Label></span> <span style="padding-left: 32px;
                                                            color: Maroon; font-weight: bold">Sold Date :</span> <span style="padding-left: 10px;
                                                                color: #000; font-weight: bold">
                                                                <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></span>
                                            </span>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlInfo" runat="server" Visible="false">
                                    <div class="row" style="float: left; padding-bottom: 15px; width: 100%">
                                        <span style="padding-left: 30px; color: #31708f; font-weight: bold">Ticket has not been
                                            sold yet !</span>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="row">
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
                                                        From Whom Distribution
                                                    </th>
                                                    <th>
                                                        To Whom Distribution
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
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="rptChurch">
                                                    <ItemTemplate>
                                                        <tr class="odd gradeX">
                                                            <td>
                                                                <%# Eval("CreatedDate")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("FromDistUserName")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("ToDistUserName")%>
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
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                    <!-- /.table-responsive -->
                                    <div class="col-lg-12" style="text-align: right; padding-right: 20%">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1>
                                Church Tickets From Raffle
                            </h1>
                            <div class="divider">
                            </div>
                        </div>
                    </div>
                    <div class="panel-body" style="padding-top: 1px">
                        <div class="table-responsive">
                            <table class="table table-striped table-bordered table-hover" id="Table1">
                                <thead>
                                    <tr>
                                        <th>
                                            Time Stamp
                                        </th>
                                        <th>
                                            From Ticket
                                        </th>
                                        <th>
                                            To Ticket
                                        </th>
                                        <th>
                                            Total Ticket
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptDistribution">
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td>
                                                    <%# Eval("CreatedDate")%>
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
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                        <!-- /.table-responsive -->
                        <div class="col-lg-12" style="text-align: right; padding-right: 1%">
                            <b style="color: Maroon">Total Ticket Got from the Raffle&nbsp;: </b>&nbsp;&nbsp;<asp:Label
                                ID="lblTotTick" runat="server" Font-Bold="true" Text="0"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/js/plugins/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="/js/plugins/dataTables/dataTables.bootstrap.js"></script>
    <script>
        $(document).ready(function () {
            var table = $('#example').DataTable({
                responsive: true
            });



            $('#dataTables-example').dataTable({
                "pageLength": 10,
                "bSort": true,
                "columns": [null, null, null, null, null, null]
            });


        });

    </script>
</asp:Content>
