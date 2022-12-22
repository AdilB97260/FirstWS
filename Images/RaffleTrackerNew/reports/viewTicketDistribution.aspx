<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="viewTicketDistribution.aspx.cs" Inherits="reports_viewTicketDistribution" %>

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
                        <div class="col-lg-12 col-md-12" style="padding-left: 0px; padding-right: 0px;">
                            <h1 style="padding-top: 10px; padding-bottom: 0px; text-transform: inherit">
                                View Ticket Distribution</h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" /></span>
                            <div class="divider" style="margin-top: 1px; margin-bottom: 5px!important">
                            </div>
                            <div style="clear: both">
                            </div>
                            <!-- /.col-lg-12 -->
                        </div>
                        <div class="row" style="padding-left: 15px;">
                            <div class="col-lg-7" style="padding-left: 0px; padding-top: 5px; text-align: center">
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
                                            <asp:Repeater runat="server" ID="rptSale">
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
                                                        <%--  <td>
                                                                <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandArgument='<%# Eval("Tickt_Distr_pk").ToString() %>'
                                                                    ForeColor="Maroon" OnClick="lnkView_click"></asp:LinkButton>
                                                            </td>--%>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- /.table-responsive -->
                            </div>
                        </div>
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
