<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white;">
        <div class="row" style="padding-top: 40px; text-align: center;">
            <div class="col-lg-12 col-md-12">
                <a href="/church/EditChurch.aspx" style="text-align: center; font-size: 20px; font-weight: bold; color: #428bca; font-family: Sans-Serif">
                    <%= UserSession.Inst.ChurchObj.name %>
                </a>
            </div>
        </div>
        <div class="formBlock" style="margin-top: 40px; min-height: 440px;">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="col-lg-1 col-md-1">
                        &nbsp;
                    </div>
                    <div class="col-lg-10 col-md-10">
                        <% if (!UserSession.Inst.IsSystemUser || (UserSession.Inst.IsSystemUser && UserSession.Inst.SystemUserRole != "USER" && UserSession.Inst.SystemUserRole != "VIEWER"))
                           { %>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/TicketSale.aspx?db=1" title="Enter Sales" style="text-align: center">
                                <img src="/images/enter-sale.jpg" alt="Enter Sales" title="Enter Sales" height="120"
                                    width="130" />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Enter Sales</strong></label>
                            </a>
                        </div>
                        <%} %>

                         <% if (!UserSession.Inst.IsSystemUser || (UserSession.Inst.IsSystemUser && UserSession.Inst.SystemUserRole != "USER" && UserSession.Inst.SystemUserRole != "VIEWER"))
                           { %>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/UserList.aspx" data-toggle="tooltip" data-original-title="Manage Users"
                                style="text-align: center">
                                <img src="/images/img1.jpg" alt="Manage Users" title="Add, Edit or View System Users"
                                    height="100" width="100" />
                                <br />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Manage Church Users</strong>
                                </label>
                            </a>
                        </div>
                        <%} %>

                        <% if (!UserSession.Inst.IsSystemUser || (UserSession.Inst.IsSystemUser && UserSession.Inst.SystemUserRole != "USER" && UserSession.Inst.SystemUserRole != "VIEWER"))
                           { %>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/DistributerSales.aspx" data-toggle="tooltip" data-original-title="Register new User and distribute ticket"
                                style="text-align: center">
                                <img src="/images/tixsalesiconautopopulate.jpg" alt="Enter Sales V2.0" title="Enter Sales V2.0 Auto Populate"
                                    height="180" width="200" />
                                <br />

                            </a>
                        </div>
                        <%} %>
                       

                    </div>

                    <div class="col-lg-1 col-md-1">
                        &nbsp;
                    </div>


                    <div class="col-lg-12 col-md-12">
                             <div class="col-lg-1 col-md-1">
                                &nbsp;
                             </div>

                             <div class="col-lg-10 col-md-10">
                                <div class="col-lg-4 col-md-4"  style="text-align: center; padding-bottom: 50px;">
                                     <a href="/SaleList.aspx" title="View Ticket Sale" style="text-align: center">
                                        <img src="/images/imgSale.png" alt="View Ticket Sales" title="View Ticket Sales"
                                            height="100" width="100" />
                                        <br />
                                        <br />
                                        <label>
                                            <strong style="font-size: 16px;">View Sales</strong></label>
                                    </a>
                                   </div>
                                <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-top: 20px; font-size: 18px; font-weight: bold; vertical-align: middle">
                                    <asp:DropDownList ID="ddlRaffleList" runat="server" CssClass="formDropdown1 formcontrol"
                                        OnSelectedIndexChanged="ddlRaffleList_selectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Select Raffle" Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <% if (!UserSession.Inst.IsSystemUser || (UserSession.Inst.IsSystemUser && UserSession.Inst.SystemUserRole != "USER" && UserSession.Inst.SystemUserRole != "VIEWER"))
                                   { %>
                                <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                                    <a href="/church/ChurchView.aspx" title="Distribute Raffle Ticket" style="text-align: center">
                                        <img src="/images/enter-distribution.png" alt="Distribute Raffle Ticket" title="Distribute Raffle Ticket"
                                            height="120" width="120" />
                                        <br />
                                        <label>
                                            <strong style="font-size: 16px;">Distribute Ticket</strong></label>
                                    </a>
                                </div>
                                <%} %>
                                 </div>
                           <div class="col-lg-1 col-md-1">
                                &nbsp;
                            </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
