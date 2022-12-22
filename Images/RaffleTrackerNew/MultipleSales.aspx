<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="MultipleSales.aspx.cs" Inherits="MultipleSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript">

        function ValidateForm() {
            var IsValid = true;
            var arrControls = ["txtTicketEnd", "txtTicketStart", "txtTotTicket", "txtGiveTo"];
            $.each(arrControls, function (index, value) {
                var _controlName = "#main_" + value;
                if ($(_controlName).val() == "") {

                    IsValid = false;
                    $(_controlName).closest("div").addClass("has-error");
                }
                else
                    $(_controlName).closest("div").removeClass("has-error");

            });


            return IsValid;
        }


        $(function () {
            $("#main_txtTicketEnd").blur(function () {
                if (parseInt($("#main_txtTicketEnd").val()) < parseInt($("#main_txtTicketStart").val())) {
                    jAlert("Invalid Ticket numbers", "Error in validation");
                    return false;
                }
                else {
                    $("#main_txtTotTicket").val(parseInt($("#main_txtTicketEnd").val() - $("#main_txtTicketStart").val()) + 1)
                    $("#main_txtAmount").val(parseFloat($("#main_txtTotTicket").val()) * parseFloat($("#main_hdnRate").val()));

                    $.ajax({
                        type: "POST",
                        url: "/TicketSale.aspx/GetDistUser",
                        data: "{ftick: '" + $("#main_txtTicketStart").val() + "',ttick: '" + $("#main_txtTicketEnd").val() + "' }",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //if (eval(response.d).length > 0) {
                            var obj = JSON.parse(response.d);
                            //alert(obj.LastDistUserName);

                            $("#main_hdnLastDist").val(obj.LastDistUserFk);
                            $("#main_hdnLastDistName").val(obj.LastDistUserName);

                            // }
                        },
                        failure: function (response) {
                            alert("Last Distribution User Not found");
                        },
                        error: function (response) {
                            alert(response.d);
                        }
                    });



                }
            });
        });
           

    </script>
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
                            <h1>
                                Multiple Ticket Sale Entry Form</h1>
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
                            <div class="login-form" style="margin-bottom: 15px!important">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12" style="padding-bottom: 20px; font-weight: bolder;
                                        font-size: 12px">
                                        Tracking of sold tickets
                                    </div>
                                    <div class="col-lg-12 col-md-12">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Ticket(s) sold to:
                                                    </label>
                                                    <asp:TextBox runat="server" ID="txtGiveTo" placeholder="Enter Name of sold to" class="formcontrol"
                                                        MaxLength="100"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email address of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter  Email address of buyer"
                                                        type="Email" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Street address of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtAdd" placeholder="Enter Street address of buyer"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        City of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtCity" placeholder="Enter City of buyer" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        State of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtState" placeholder="Enter State of buyer" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Zip of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtZip" placeholder="Enter Zip of buyer" class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Phone Number of buyer:</label>
                                                    <asp:TextBox runat="server" ID="txtPhone" placeholder="Enter Phone Number of buyer"
                                                        class="formcontrol"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnRate" runat="server" Value="25" />
                                        </div>
                                    </div>
                                    <!-- end login form -->
                                </div>
                                <!-- end col -->
                            </div>
                            <div class="login-form">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12">
                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="formBlock">
                                                <label>
                                                    Ticket From
                                                </label>
                                                <asp:TextBox runat="server" ID="txtTicketStart" placeholder="Ticket No Start" type="Number"
                                                    class="formcontrol" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="formBlock">
                                                <label>
                                                    Ticket To
                                                </label>
                                                <asp:TextBox runat="server" ID="txtTicketEnd" placeholder="Ticket No End" type="Number"
                                                    class="formcontrol" MaxLength="6"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="formBlock">
                                                <label>
                                                    Total Ticket
                                                </label>
                                                <asp:TextBox runat="server" ID="txtTotTicket" placeholder="Total Ticket(s)" type="Number"
                                                    class="formcontrol" disabled="disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-2 col-md-2 col-sm-2">
                                            <div class="formBlock">
                                                <label>
                                                    Amount
                                                </label>
                                                <asp:TextBox runat="server" ID="txtAmount" placeholder="Amount" type="Number" class="formcontrol"
                                                    disabled="disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <div class="formBlock" style="padding-top: 22px">
                                                <div class="col-lg-6 col-md-6 ">
                                                    <asp:Button Text="Add" ID="btnAdd" runat="server" CssClass="buttonColor" OnClick="btnAdd_Click"
                                                        OnClientClick="return ValidateForm()" />
                                                </div>
                                                <div class="col-lg-6 col-md-6 ">
                                                    <asp:Button Text="Reset" ID="btnNo" runat="server" CssClass="buttonGrey xlarge" OnClick="btnReset_click" />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnLastDist" runat="server" />
                                        <asp:HiddenField ID="hdnLastDistName" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12" style="padding-top: 20px">
                                        <div class="panel-body" style="padding-top: 0px!important">
                                            <div class="table-responsive">
                                                <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                                    id="dataTables-example">
                                                    <thead style="background-color: #428bca">
                                                        <tr>
                                                            <th>
                                                                Given To
                                                            </th>
                                                            <th>
                                                                Last Distributer Name
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
                                                            <td>
                                                                Action
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater runat="server" ID="rptSales">
                                                            <ItemTemplate>
                                                                <tr class="odd gradeX">
                                                                    <td>
                                                                        <%# Eval("GivenTo")%>
                                                                    </td>
                                                                    <td>
                                                                        <%# Eval("LastDistUserName")%>
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
                                                                    <td>
                                                                         <asp:LinkButton ID="lnkRemove" runat="server" Text="Remove" CommandName="REMOVE"
                                                                          CommandArgument='<%# Eval("TicketSold_fk").ToString() %>' ForeColor="Maroon"
                                                                            OnClick="lnkRemove_click"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <!-- /.table-responsive -->
                                            <div class="col-lg-12" style="text-align: right; padding-right: 20%">
                                                <div class="col-lg-7 col-md-7 col-sm-7">
                                                    <div class="formBlock" style="padding-top: 15px; float:right">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-md-4 ">
                                                                <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClick="btnSave_click"
                                                                     />
                                                            </div>
                                                            <div class="col-lg-4 col-md-4">
                                                                <asp:Button Text="Cancel" ID="btnCancel" runat="server" CssClass="buttonGrey xlarge"
                                                                    OnClick="btncancel_click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<asp:GridView ID="grvSales" AutoGenerateColumns="false" runat="server" CellPadding="4"
                                            CssClass="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                            ForeColor="#000">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundField HeaderStyle-Width="120px" HeaderText="Given To" DataField="GivenTo" />
                                                <asp:BoundField HeaderStyle-Width="120px" HeaderText="LastDistUserName" DataField="LastDistUserName" />
                                                <asp:BoundField HeaderStyle-Width="120px" HeaderText="Ticket From" DataField="TicketFrom" />
                                                <asp:BoundField HeaderStyle-Width="120px" HeaderText="Ticket To" DataField="TicketTo" />
                                                <asp:BoundField HeaderStyle-Width="120px" HeaderText="Total Ticket" DataField="TicketTotal" />
                                                <asp:BoundField HeaderStyle-Width="120px" HeaderText="Amount" DataField="Amount" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <HeaderStyle BackColor="#428bca" Font-Bold="True" ForeColor="Black" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end row -->
                        <!-- end form -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end login form -->
</asp:Content>
