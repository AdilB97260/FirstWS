<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="MemberView.aspx.cs" Inherits="Member_MemberView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <%--<div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1 style="padding-top: 10px; padding-bottom: 15px">
                                User View</h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" /></span> <span style="float: right; padding-right: 10px">
                                        <asp:Button Text="Add more ticket for Member" ID="btnAddTicket" runat="server" class="buttonColor"
                                            OnClick="btnAddTicket_Click" /></span>
                            <div class="divider">
                            </div>
                            <div class="row" id="divError" runat="server" visible="false">
                                <div class="col-lg-12 col-md-12">
                                    <div class="alertBox error">
                                        <h4>
                                            ERROR! <span>
                                                <asp:Literal ID="lblErrorMsg" runat="server"></asp:Literal></span></h4>
                                    </div>
                                </div>
                            </div>
                            <!-- start login form -->
                            <div class="login-form">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Name Of User
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtUserName" placeholder="Enter Name Of Raffle" class="formcontrol"
                                                        MaxLength="100" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter Email" class="formcontrol"
                                                        MaxLength="100" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        User Name (For Login)</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtLogin" placeholder="Enter User name for login"
                                                        class="formcontrol" MaxLength="100" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        User Name (For Login)</label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtPassword" placeholder="Enter Password" class="formcontrol"
                                                        MaxLength="100" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Address</label>
                                                    <asp:TextBox runat="server" ID="txtAdd" TextMode="SingleLine" placeholder="Enter Full Address"
                                                        class="formcontrol" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        City</label>
                                                    <asp:TextBox runat="server" ID="txtCity" TextMode="SingleLine" placeholder="Enter City"
                                                        class="formcontrol" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        State</label>
                                                    <asp:TextBox runat="server" ID="txtState" TextMode="SingleLine" placeholder="Enter State"
                                                        class="formcontrol" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Zip</label>
                                                    <asp:TextBox runat="server" ID="txtZip" TextMode="SingleLine" placeholder="Enter Zip"
                                                        class="formcontrol" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtPhone" Text="" placeholder="Enter Phone Number"
                                                        class="formcontrol" MaxLength="50" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6" id="divTotTic" runat="server">
                                                <div class="formBlock">
                                                    <label>
                                                        Total Ticket(s):</label>
                                                    <asp:TextBox runat="server" ID="txtTotTicket" Enabled="false" placeholder="Total Ticket"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end login form -->
                                </div>
                                <!-- end col -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1 style="padding-top: 10px; padding-bottom: 15px">
                                User Tickets From Member</h1>
                                <span style="float: right;">
                                <asp:Button Text="Back" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" /></span> <span style="float: right; padding-right: 10px">
                                        <asp:Button Text="Add more tickets for Member" ID="btnAddTicket" runat="server" class="buttonColor"
                                            OnClick="btnAddTicket_Click" /></span>
                            <div class="divider">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
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
                                <div class="col-lg-12" style="text-align: right; padding-right: 22%">
                                    <b style="color: Maroon">Total Ticket Got from the Church&nbsp;: </b>&nbsp;&nbsp;<asp:Label
                                        ID="lblTotTick" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
 <%--   <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1 style="text-transform: none">
                                User Ticket Sold To Friend</h1>
                            <div class="divider">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover" id="dataTables-example">
                                        <thead>
                                            <tr>
                                                <th>
                                                    Time Stamp
                                                </th>
                                                <th>
                                                    Given To
                                                </th>
                                                <th>
                                                    Email
                                                </th>
                                                <th>
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
                                                <th>
                                                    Amount
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptSaleList">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                        <td>
                                                            <%# Eval("CreatedDate")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("GivenTo")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Email")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Phone")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TicketFrom")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TicketTo")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("TicketTotal")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Amount")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-lg-12" style="text-align: right; padding-right: 8%">
                                    <b style="color: Maroon; padding-right: 1%">Total Ticket Sold By User&nbsp;: </b>
                                    <b style="padding-right: 5%">
                                        <asp:Label ID="lblSoldTicket" runat="server" Font-Bold="true"></asp:Label></b>
                                    <b style="padding-left: 6%">&nbsp;$<asp:Label ID="lblSoldAmt" runat="server" Font-Bold="true"></asp:Label></b>
                                </div>
                                <!-- /.table-responsive -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
