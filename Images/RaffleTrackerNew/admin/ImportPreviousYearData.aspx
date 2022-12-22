<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="ImportPreviousYearData.aspx.cs" Inherits="admin_ImportPreviousYearData" %>

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
                                        <div class="row" style="padding: 10px 15px">
                                            <div class="col-lg-12 col-md-12 col-sd-12" style="text-align: center; float: left;
                                                padding-top: 10px; font-size: 18px; font-weight: bold; vertical-align: middle;
                                                padding-bottom: 15px">
                                                <div style="float: left; min-width: 305px">
                                                    <asp:DropDownList ID="ddlRaffleList" runat="server" CssClass="formDropdown1 formcontrol">
                                                        <asp:ListItem Text="Select Raffle" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div style="float: left; padding-left: 15px;">
                                                    <asp:Button ID="btnImportData" runat="server" class="buttonColor1" Text="Import Selected Data" ValidationGroup="vldSelectRaffe" OnClick="btnImportData_Click" />
                                                </div>
                                                <div style="float: left; padding-left: 15px; padding-top:10px">
                                                    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Size="14px"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row" style="padding: 0px 30px">
                                                <div class="table-responsive">
                                                    <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                                        id="dataTables-example">
                                                        <thead>
                                                            <tr>
                                                                <th>
                                                                    Select
                                                                </th>
                                                                <th>
                                                                    Name
                                                                </th>
                                                                <th>
                                                                    Email
                                                                </th>
                                                                <th>
                                                                    Phone
                                                                </th>
                                                                <th>
                                                                    mobile
                                                                </th>
                                                                <th>
                                                                    Address
                                                                </th>
                                                                <th>
                                                                    City
                                                                </th>
                                                                <th>
                                                                    State
                                                                </th>
                                                                <th>
                                                                    Postal Code
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater runat="server" ID="rptRaffle">
                                                                <ItemTemplate>
                                                                    <tr class="odd gradeX">
                                                                        <td>
                                                                            <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("user_pk")%>' />
                                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("name")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("email")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("phone")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("mobile")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("Address")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("City")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("State")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("Zip")%>
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
