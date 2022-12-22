<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="GenerateNewYearRaffle.aspx.cs" Inherits="admin_GenerateNewYearRaffle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 500px">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1 style="padding-top: 10px; padding-bottom: 15px">
                                Generate new year Raffle</h1>
                            <span style="float: right;"></span>
                            <div class="divider">
                            </div>
                            <!-- start login form -->
                            <div class="login-form" style="min-height: 400px">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-12 col-md-12 col-sm-12" style="padding-top: 50px; padding-left: 30px">
                                                <div style="float: left; text-align: left; padding-top: 8px; vertical-align: middle; 
                                                    font-size: 14px; width: 25%">
                                                    <b>Enter the Year for Generate Raffle:</b>
                                                      <asp:RequiredFieldValidator ID="reqVal" runat="server" ControlToValidate="txtYear"
                                                        Display="Dynamic" Text="*" ForeColor="Red" ValidationGroup="VldReqYear" Font-Size="Larger"></asp:RequiredFieldValidator>
                                                </div>
                                                <div style="float: left; width: 14%">
                                                    <asp:TextBox ID="txtYear" runat="server" placeholder="Enter Ticket No" class="formcontrol talcenter"
                                                        Width="125" MaxLength="4"  type="number" ValidationGroup="VldReqYear"></asp:TextBox>
                                                  
                                                </div>
                                                <div style="float: left; width: 14%">
                                                    <asp:Button Text="Generate" ID="btnGenerate" runat="server" class="buttonColorSmall"
                                                        ValidationGroup="VldReqYear" OnClick="btnGenerate_click" />
                                                </div>
                                                <div style="float: left; padding-left: 20px; padding-top: 10px;">
                                                    <asp:Label ID="lblError" runat="server" Text="You had already created year !" ForeColor="Red"
                                                        Font-Size="Larger" Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" style="padding: 50px 30px">
                                            <div class="table-responsive">
                                                <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                                    id="dataTables-example">
                                                    <thead>
                                                        <tr>
                                                            <th>
                                                                ID
                                                            </th>
                                                            <th>
                                                                RaffleYear
                                                            </th>
                                                            <th>
                                                                Is Current Year
                                                            </th>
                                                            <th>
                                                                Created Date
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptRaffle">
                                                            <ItemTemplate>
                                                                <tr class="odd gradeX">
                                                                    <td>
                                                                        <%# Eval("RaffleYear_ID")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RaffleYear1")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("IsCurrentYear")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("CreatedDate")%>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
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
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
    </div>
</asp:Content>
