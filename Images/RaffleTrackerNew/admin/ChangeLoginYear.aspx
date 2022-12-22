<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="ChangeLoginYear.aspx.cs" Inherits="admin_ChangeLoginYear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 500px">
        <div class="col-lg-12 col-md-12" style="padding-top: 0px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1 style="padding-top: 10px; padding-bottom: 15px">
                                Change Login year of Raffle</h1>
                            <span style="float: right;"></span>
                            <div class="divider">
                            </div>
                            <!-- start login form -->
                            <div class="login-form" style="min-height: 400px">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12">
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
                                                                Is Login Year
                                                            </th>
                                                            
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptRaffle" OnItemCommand="rptRaffle_ItemCommand">
                                                            <ItemTemplate>
                                                                <tr class="odd gradeX">
                                                                    <td>
                                                                        <%# Eval("RaffleYear_ID")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("RaffleYear1")%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkButton" runat="server" Text='<%# Convert.ToBoolean(Eval("IsLoginYear"))==true ? "Yes" : "No" %>' CommandArgument=' <%# Eval("RaffleYear_ID")%>' ></asp:LinkButton>
                                                                        
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
</asp:Content>
