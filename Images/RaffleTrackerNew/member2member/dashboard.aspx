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
        <div class="row" style="padding-top: 50px; text-align: center;">
            <div class="col-lg-12 col-md-12">
                <a href="/member2member/EditMember.aspx" style="text-align: center; font-size: 20px;
                    font-weight: bold; color: #428bca; font-family: Sans-Serif">
                    <%= UserSession.Inst.Member2MemberObj.name %>
                </a>
            </div>
        </div>
        <div class="formBlock" style="margin-top: 70px; min-height: 400px;">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="col-lg-1 col-md-1">
                        &nbsp;
                    </div>
                    <div class="col-lg-10 col-md-10">
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <% if (!UserSession.Inst.IsSystemUser || (UserSession.Inst.IsSystemUser && UserSession.Inst.SystemUserRole != "USER" && UserSession.Inst.SystemUserRole != "VIEWER"))
                               { %>
                            <a href="/TicketSale.aspx?db=1" title="Enter Sales" style="text-align: center">
                                <img src="/images/enter-sale.jpg" alt="Enter User Sales" title="Enter User Sales"
                                    height="150" width="150" />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Enter User Sales</strong></label>
                            </a>
                            <%} %>
                            &nbsp;
                        </div>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/SaleList.aspx" title="View Ticket Sale" style="text-align: center">
                                <img src="/images/imgSale.png" alt="View Ticket Sales" title="View Ticket Sales"
                                    height="110" width="110" />
                                <br />
                                <br />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">View Sales</strong></label>
                            </a>
                        </div>
                        <% if (!UserSession.Inst.IsSystemUser || (UserSession.Inst.IsSystemUser && UserSession.Inst.SystemUserRole != "USER" && UserSession.Inst.SystemUserRole != "VIEWER"))
                           { %>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/member2member/MemberView.aspx" title="Distribute User Ticket" style="text-align: center">
                                <img src="/images/enter-distribution.png" alt="Distribute User Ticket" title="Distribute User Ticket"
                                    height="130" width="130" />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Distribute User Ticket</strong></label>
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
</asp:Content>
