<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="admin_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server" Text="TTT"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white;">
        <div class="row" style="padding-top: 50px; text-align: center;">
            <div class="col-lg-12 col-md-12">
                <span style="text-align: center; font-size: 20px; font-weight: bold;
                    color: #428bca; font-family: Sans-Serif">System Level (All access) </span>
            </div>
        </div>
        <div class="formBlock" style="margin-top: 50px; min-height: 420px;">
            <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="col-lg-1 col-md-1">
                        &nbsp;
                    </div>
                    <div class="col-lg-10 col-md-10">
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/admin/RaffleRegistration.aspx" data-toggle="tooltip" data-original-title="Register new Raffle and distribute ticket"
                                style="text-align: center">
                                <img src="/images/Raffle.jpg" alt="Register new Raffle and distribute ticket" title="Register new Raffle and distribute ticket"
                                    height="100" width="100" />
                                <br />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Register Raffle</strong>
                                </label>
                            </a>
                        </div>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/UserList.aspx" data-toggle="tooltip" data-original-title="Manage Users"
                                style="text-align: center">
                                <img src="/images/img1.jpg" alt="Manage Users" title="Add, Edit or View System Users"
                                    height="100" width="100" />
                                <br />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Manage Super Admin Users</strong>
                                </label>
                            </a>
                        </div>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 50px;">
                            <a href="/SaleList.aspx" title="Ticket Sale" style="text-align: center">
                                <img src="/images/imgSale.png" alt="Record Ticket Sales" title="Record Ticket Sales"
                                    height="100" width="100" />
                                <br />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">View All Sales</strong></label>
                            </a>
                        </div>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 30px;">
                            <a href="/ImportDistributionData.aspx" title="Import Distribution" style="text-align: center">
                                <img src="/images/enter-distribution.png" alt="Import Distribution" title="Import Distribution"
                                    height="110" width="110" />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">Import Distribution</strong></label>
                            </a>
                            &nbsp;
                        </div>


                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-top: 30px;
                            font-size: 18px; font-weight: bold; vertical-align: middle">
                            <asp:DropDownList ID="ddlRaffleList" runat="server" CssClass="formDropdown1 formcontrol" OnSelectedIndexChanged="ddlRaffleList_selectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Select Raffle" Value="0" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-4 col-md-04 col-sd-04" style="text-align: center; padding-bottom: 30px;">
                             <a href="/DistributionList.aspx" title="View All Distributio" style="text-align: center">
                                <img src="/images/img65.png" alt="View All Distribution" title="View All Distribution"
                                    height="90" width="90" />
                                <br />
                                <br />
                                <label>
                                    <strong style="font-size: 16px;">View All Distribution</strong></label>
                            </a>
                            &nbsp;
                        </div>
                    </div>
                    <div class="col-lg-1 col-md-1">
                        &nbsp;
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
