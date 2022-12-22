<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="EditRaffle.aspx.cs" Inherits="Raffle_EditRaffle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript">




        function DeleteUser(id) {
            jConfirm('Are you sure want to Delete Raffle ?', 'Confirmation', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "/raffle/EditRaffle.aspx/DeleteUser",
                        data: "{tid: '" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d == "true") {
                                jAlert("Raffle deleted successfully.", "Delete Successs", true, "/admin/dashboard.aspx");
                                //window.location.href = window.location.href;
                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                        error: function (response) {
                            //alert(response.d);
                        }
                    });
                }

                return false;
            });
        }



        function DeleteDistribution(id) {
            jConfirm('Are you sure want to delete ticket distribution ?', 'Confirmation', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "/raffle/EditRaffle.aspx/DeleteDistribution",
                        data: "{tid: '" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d == "true") {
                                jAlert("Ticket distribution deleted successfully.", "Delete Successs", true, window.location.href);
                            }
                            else {
                                jAlert("You can not delete this distribution becuase ticket has been distributed to other user.", "Delete Failed");
                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                        error: function (response) {
                            //alert(response.d);
                        }
                    });
                }

                return false;
            });
        }

        function btnSaveEdit() {
            $.ajax({
                type: "POST",
                url: "/raffle/EditRaffle.aspx/EditTicket",
                data: "{ticketFrom:" + $("#main_txtTicketStart").val() + ",toTicket:" + $("#main_txtTicketEnd").val() + ",TotTicket:" + $("#main_txtTotTicket").val() + ",distFkey:" + $("#main_hdnDistFkey").val() + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == "1") {
                        jAlert("Record saved successfully!.", "Saved Record", true, window.location.href);
                    }
                    else if (response.d == "0") {
                        jAlert("Ticket number is overlapping !. Records saved failed.", "Record saved Failed!.");
                        return false;
                    }
                },
                failure: function (response) {
                    alert("Record failed to save ! contact to Administrator");
                },
                error: function (response) {
                    //alert(response.d);
                }
            });

        }


        function DeleteSale(id) {
            jConfirm('Are you sure for delete Sold Ticket  ?', 'Confirmation', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "/Raffle/EditRaffle.aspx/DeleteSale",
                        data: "{tid: '" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d == "true") {
                                jAlert("Sold ticket deleted successfully.", "Delete Successs", true, window.location.href);
                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        },
                        error: function (response) {
                            //alert(response.d);
                        }
                    });
                }

                return false;
            });
        }


        function ShowEditPopup() {
            $('#<%=pnlEditpopup.ClientID %>').show();
        }
        function HideEditPopup() {
            $('#<%=pnlEditpopup.ClientID %>').hide();
        }
        $(".btnClose").live('click', function () {
            HideEditPopup();
        });

        $(".close").live('click', function () {
            HideEditPopup();
        });

        function ValidateEditForm() {
            var IsValid = true;
            var arrControls = ["txtTicketEnd", "txtTicketStart", "txtTotTicket"];
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

        function ValidateForm() {


            var IsValid = true;
            var arrControls = ["txtUserName"];

            $.each(arrControls, function (index, value) {

                var _controlName = "#main_" + value;

                if ($(_controlName).val() == "") {
                    IsValid = false;
                    $(_controlName).closest("div").addClass("has-error");
                }
                else if ($(_controlName).val() == "0") {
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
                    $("#main_txtTotTicket").val($("#main_txtTicketEnd").val() - $("#main_txtTicketStart").val() + 1)
                }
            });

            $("#main_txtTicketStart").blur(function () {
                if ($("#main_txtTicketEnd").val() != "0" && parseInt($("#main_txtTicketEnd").val()) > 0) {
                    if (parseInt($("#main_txtTicketEnd").val()) < parseInt($("#main_txtTicketStart").val())) {
                        jAlert("Invalid Ticket numbers", "Error in validation");
                        return false;
                    }
                    else {
                        $("#main_txtTotTicket").val($("#main_txtTicketEnd").val() - $("#main_txtTicketStart").val() + 1)
                    }
                }
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #2f5c84;">
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
                                Raffle View</h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" />
                            </span><span style="float: right; padding-right: 10px; padding-top: 8px">
                                <%--<asp:Button Text="Add more ticket for Raffle" ID="btnAddTicket" runat="server" class="buttonColor"
                                    OnClick="btnAddTicket_Click" />--%>
                                <a href="javascript:;" class="buttonColor" id="adelete" onclick="DeleteUser(<%= UserSession.Inst.RafflePK %>)"
                                    style="text-decoration: none; margin-top: 5px">Delete Raffle</a>&nbsp;&nbsp;
                            </span>
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
                                                        Name Of Raffle
                                                    </label>
                                                    <span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>
                                                    <asp:TextBox runat="server" ID="txtUserName" placeholder="Enter Name Of Raffle" class="formcontrol"
                                                        MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Email</label>
                                                    <%--<span style="color: red; padding: 3px 0 0 3px; font-size: 12px;">*</span>--%>
                                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Enter Email" class="formcontrol"
                                                        MaxLength="100" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        User Name (For Login)</label>
                                                    <asp:TextBox runat="server" ID="txtLogin" Text="" placeholder="Enter User name for login"
                                                        class="formcontrol" MaxLength="50" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Password</label>
                                                    <asp:TextBox runat="server" ID="txtPassword" Text="" placeholder="Enter Password"
                                                        class="formcontrol" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Address</label>
                                                    <asp:TextBox runat="server" ID="txtAdd" TextMode="SingleLine" placeholder="Enter Full Address"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        City</label>
                                                    <asp:TextBox runat="server" ID="txtCity" TextMode="SingleLine" placeholder="Enter City"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        State</label>
                                                    <asp:TextBox runat="server" ID="txtState" TextMode="SingleLine" placeholder="Enter State"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Zip</label>
                                                    <asp:TextBox runat="server" ID="txtZip" TextMode="SingleLine" placeholder="Enter Zip"
                                                        class="formcontrol" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <div class="formBlock">
                                                    <label>
                                                        Phone Number</label>
                                                    <asp:TextBox runat="server" ID="txtPhone" Text="" placeholder="Enter Phone Number"
                                                        class="formcontrol" MaxLength="50" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-7 col-md-7 col-sm-7">
                                                <div class="formBlock" style="padding-top: 15px;">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 ">
                                                            <asp:Button Text="Save" ID="btnSave" runat="server" CssClass="buttonColor" OnClientClick="return ValidateForm()"
                                                                OnClick="btnSave_click" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4">
                                                            <asp:Button Text="Cancel" ID="Button1" runat="server" CssClass="buttonGrey xlarge"
                                                                OnClick="btncancel_click" />
                                                        </div>
                                                        <asp:HiddenField ID="hdnUserId" runat="server" />
                                                    </div>
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
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <h1>
                                Tickets Distributed To Parish</h1>
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
                                                <% if (UserSession.Inst.UserType == "ADMIN")
                                                   { %>
                                                <th>
                                                    Action
                                                </th>
                                                <%} %>
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
                                                        <% if (UserSession.Inst.UserType == "ADMIN")
                                                           { %>
                                                        <td>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="DISTRIBUTION"
                                                                CommandArgument='<%# Eval("Tickt_Distr_pk").ToString() %>' ForeColor="Maroon"
                                                                OnClick="lnkEdit_click"></asp:LinkButton>
                                                            &nbsp;&nbsp; &nbsp;&nbsp; <a href="javascript:;" id="adelete" style="color: Maroon"
                                                                onclick="DeleteDistribution('<%# Eval("Tickt_Distr_pk").ToString() %>')">Delete</a>&nbsp;&nbsp;
                                                        </td>
                                                        <%} %>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- /.table-responsive -->
                                <div class="col-lg-12" style="text-align: right; padding-right: 35%">
                                    <b style="color: Maroon">Total Ticket Distribution to Raffle&nbsp;: </b>&nbsp;&nbsp;<asp:Label
                                        ID="lblTotTick" runat="server" Font-Bold="true"></asp:Label>
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
                                Tickets Distributed to churches by Parish</h1>
                            <div class="divider">
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="panel-body" style="padding-top: 1px">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                    id="dataTables-example1">
                                    <thead>
                                        <tr>
                                            <th>
                                                Time Stamp
                                            </th>
                                            <th>
                                                Church Name
                                            </th>
                                            <th>
                                                From Ticket
                                            </th>
                                            <th>
                                                Ticket To
                                            </th>
                                            <th>
                                                Total Ticket
                                            </th>
                                            <th>
                                                Action
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptChurch">
                                            <ItemTemplate>
                                                <tr class="odd gradeX">
                                                    <td>
                                                        <%# Eval("createdDate")%>
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
                                                    <td style="width:15%">
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="DISTRIBUTIONCHURCH"
                                                            CommandArgument='<%# Eval("Tickt_Distr_pk").ToString() %>' ForeColor="Maroon"
                                                            OnClick="lnkEdit_click"></asp:LinkButton>
                                                        &nbsp;&nbsp; &nbsp;&nbsp; <a href="javascript:;" id="adelete" style="color: Maroon"
                                                            onclick="DeleteDistribution('<%# Eval("Tickt_Distr_pk").ToString() %>')">Delete</a>&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                            <!-- /.table-responsive -->
                            <div class="col-lg-12" style="text-align: right; padding-right: 42%">
                                <b style="color: Maroon">Total Ticket Distribution to Churches&nbsp;: </b>&nbsp;&nbsp;<asp:Label
                                    ID="lblTotChurch" runat="server" Font-Bold="true"></asp:Label>
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
                                Tickets Sold By Parish</h1>
                            <div class="divider">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                        id="dataTables-example">
                                        <thead>
                                            <tr>
                                                 <th style="min-width:125px">
                                                    Time Stamp
                                                </th>
                                                <th>
                                                    Given To
                                                </th>
                                                <th style="min-width:120px">
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
                                                <th>
                                                    Action
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater runat="server" ID="rptSaleList">
                                                <ItemTemplate>
                                                    <tr class="odd gradeX">
                                                        <td>
                                                            <%# Eval("StrTicketSoldDate")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("GivenTo")%>
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
                                                        <td>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="SOLD" CommandArgument='<%# Eval("TicketSold_fk").ToString() %>'
                                                                ForeColor="Maroon" OnClick="lnkEdit_click"></asp:LinkButton>
                                                            &nbsp;&nbsp; <a href="javascript:;" id="adelete" style="color: Maroon" onclick="DeleteSale('<%# Eval("TicketSold_fk").ToString() %>')">
                                                                Delete</a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- /.table-responsive -->
                                <div class="col-lg-12" style="text-align: center; padding-right: 2%; padding-top: 10px;">
                                    <b style="color: Maroon; padding-right: 1%">Total Ticket Sold By Member&nbsp;: </b>
                                    <b style="padding-right: 2%">
                                        <asp:Label ID="lblSoldTicket" runat="server" Font-Bold="true"></asp:Label></b>
                                    <b style="padding-left: 3%">&nbsp;Amount&nbsp; $<asp:Label ID="lblSoldAmt" runat="server"
                                        Font-Bold="true"></asp:Label></b>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlEditpopup" runat="server" BackColor="White" Style="z-index: 111;
        position: fixed; left: 0px; top: 0px; z-index: 112; opacity: 0.95; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=40)';
        filter: alpha(opacity=40); background-color: gray; display: none; width: 100%;
        height: 100%;">
        <div style="position: relative; width: 60%; left: 20%; top: 15%; opacity: none; z-index: 10;">
            <asp:HiddenField ID="hdnDistFkey" Value="" runat="server" />
            <div class="modal-content" style="background: white;">
                <div class="modal-header" style="background-color: #428bca">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title" id="myModalLabel">
                        <asp:Literal ID="ltrRFCResiName" runat="server" Text="" />
                        <b style="color: #001">Edit Ticket Distribution</b></h4>
                </div>
                <div class="modal-body row" style="padding: 20px">
                    <div class="form-group">
                        <div class="row" style="float: left; padding-left: 42px; padding-top: 5px; font-weight: bold;
                            width: 100%">
                            <div class="col-lg-12 col-md-12">
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="formBlock">
                                        <label>
                                            Ticket Number Start:
                                        </label>
                                        <asp:TextBox runat="server" ID="txtTicketStart" placeholder="Ticket Number Start"
                                            type="number" class="formcontrol" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="formBlock">
                                        <label>
                                            Ticket Number End:</label>
                                        <asp:TextBox runat="server" ID="txtTicketEnd" placeholder="Ticket Number End" class="formcontrol"
                                            type="number" MaxLength="100"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="formBlock">
                                        <label>
                                            Total Ticket(s):</label>
                                        <asp:TextBox runat="server" ID="txtTotTicket" placeholder="Total Ticket" Enabled="false"
                                            class="formcontrol"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="float: left; padding-left: 14px; padding-top: 5px; font-weight: bold;
                                width: 100%">
                                <div class="col-lg-12 col-md-12">
                                    <div class="col-lg-3 col-md-3 ">
                                        <input type="button" value="Save" title="Save" class="buttonColor" onclick="btnSaveEdit();" />
                                        <%-- <asp:Button Text="Save" ID="btnEditRaffleDist" runat="server" CssClass="buttonColor"
                                            OnClientClick="return ValidateEditForm()" OnClick="btnSaveEdit_click" />--%>
                                    </div>
                                    <div class="col-lg-4 col-md-4 ">
                                        <button type="button" class="btn buttonGrey xlarge btnClose" value="Cancel" title="Cancel"
                                            style="padding-left: 30px!important">
                                            Cancel</button>
                                    </div>
                                    <div class="col-lg-5 col-md-5 ">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
    </asp:Panel>
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
                "columns": [null, null, null, null, null, null, null, { "orderable": false}]
            });


            $('#dataTables-example1').dataTable({
                "pageLength": 10,
                "bSort": true,
                "columns": [null, null, null, null, null, { "orderable": false}]
            });

        });

    </script>
</asp:Content>
