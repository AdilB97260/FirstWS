<%@ Page Title="" Language="C#" MasterPageFile="~/PaymentSite.master" AutoEventWireup="true" CodeFile="TicketBuy.aspx.cs" Inherits="TicketBuy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server" Text=""> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 400px; border: 1px solid #808080">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div style="width: 100%; text-align: center; padding-top: 2%">
                            <h1>Welcome to Your Parish Raffle
                            </h1>
                        </div>

                        <div class="form form-horizontal">
                            <h5 style="padding-left: 16%; padding-top: 20px; font-size: 14px!important"><b>Enter one or more ticket numbers below</b>
                            </h5>

                            <div class="form-group" style="padding: 0; margin: 0; width: 80%; text-align: right;">
                                <div style="width: 20%; float: right">
                                    <input type="button" class="BtnAdd btn btn-sm btn-primary btn-outline btn-add" value="Add More Ticket" />
                                </div>
                            </div>
                            <div class="form-group" style="padding-left: 17%; width: 82%">
                                <table id="ticketList" class="table table-striped table-bordered table-hover dataTable no-footer" role="grid" width="100%">
                                    <thead>
                                        <th id="lblFrom">Ticket No From #</th>
                                        <th id="lblTo">Ticket No To #</th>
                                        <th id="lblTotTicket">No Of Tickets</th>
                                        <th id="lblTotAmt">Amount</th>
                                        <th>Action</th>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <td colspan="3" style="text-align: right; padding-right: 15px; font-weight: 600; font-size: 14px">Total Amount </td>
                                            <td colspan="2" style="font-weight: 600; font-size: 14px; padding-left: 15px;">$<label id="lblTAmt">0.00</label>
                                            </td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>

                            <div class="form-group">
                                <div class="col-lg-10" style="margin-left: 15%; color: red; font-family: 'montserratregular', 'sans-serif'; font-size: 14px; font-weight: 400">
                                    (If you are only buying one ticket please enter the ticket # in the "From" and "To" box above)
                                </div>
                            </div>


                            <div class="form-group" style="margin-top: 5%">
                                <div class="col-sm-2 col-sm-offset-5">
                                    <input type="button" value="Buy Tickets" title="Buy Tickets" class="btn btn-outline btn-primary" onclick="return validateSave();" />
                                </div>
                            </div>
                        </div>

                        <div style="color: #4a4a4a; width: 100%; text-align: center; padding-top: 2%">
                            <h3>Thank you for participating
                            </h3>
                        </div>

                       <%-- <div style="color: #4a4a4a; width: 100%; text-align: center; padding-bottom: 2%; font-size: 22px; font-weight: 600; font-family: Arial;">

                            <div class="blink_me" style="float:left; text-align:center; width:100%; padding-bottom:30px;"><a href="Draw.aspx" title="View Raffle Live">View Live Raffle </a></div>

                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        var seq = 1;

        function Loading(input) {
            if (input == true) {
                $('div.splash').css('display', 'block');
            }
            else {
                $('div.splash').css('display', 'none');
            }
        }

        $(document).ready(function () {
            addRow();

            $(".BtnAdd").on("click", addRow);
            $("#ticketList").on("click", ".BtnDelete", deleteRow);

            $("#ticketList").on("focusout", ".fromAmt", validationFrom);
            $("#ticketList").on("focusout", ".toAmt", validationTo);

        });

        function deleteRow() {

            var numItems = $('.ticketAmt').length

            if (numItems > 1) {
                var par = $(this).parent().parent();
                par.next().find("input[name=fromAmt]").val();
                par.remove();

                var tAmt = 0;
                $(".ticketAmt").each(function (index, val) {
                    tAmt = tAmt + parseInt($(val).val());
                });

                $("#lblTAmt").text(tAmt);
            }
        };

        function validateSave() {
            var valid = true;
            $(".vldTicket1").each(function () {
                if ($(this).val() == '') {
                    $(this).addClass("error");
                    valid = false;
                }
                else {
                    $(this).removeClass("error");
                }
            });

            if (valid == true) {
                Save();
            }

            return valid
        }

        function addRow() {

            var valid = true;
            if (parseInt(seq) > 1) {
                if ($(this).parent().parent().find("input[name=toAmt]").val() == '') {
                    $(this).parent().parent().find("input[name=toAmt]").addClass("error");
                    valid = false;
                }
                else {
                    $(this).parent().parent().find("input[name=toAmt]").removeClass("error");
                }
                if ($(this).parent().parent().find("input[name=fromAmt]").val() == '') {
                    $(this).parent().parent().find("input[name=fromAmt]").addClass("error");
                    valid = false;
                }
                else {
                    $(this).parent().parent().find("input[name=fromAmt]").removeClass("error");
                }
            }

            if (valid == true) {

                var htmlTax =
                    '<tr class="ticketRange">' +
                    '<td><input type="text" name="fromAmt" value="" placeholder="Ticket From No" class="fromAmt vldTicket vldTicket1 form-control" /></td>' +
                    '<td><input type="text" name="toAmt" value="" placeholder="Ticket To No" class="toAmt vldTicket vldTicket1 form-control" /></td>' +
                    '<td><input type="text" name="totTicket" data-id=' + seq + ' value="" disabled class="totTicket vldTicket form-control" /></td>' +
                    '<td><input type="text" name="ticketAmt" data-id=' + seq + ' value="" disabled class="ticketAmt vldTicket form-control" /></td>' +
                    //'<td><input type="button" class="BtnAdd btn btn-sm btn-primary btn-outline btn-add" value="Add More Ticket" /></td>' +
                    '<td><img src="images/delete.png" alt="Delete Row" height="32" class="BtnDelete" style="cursor:pointer" >' +
                    '</tr>'


                $(htmlTax).appendTo($("#ticketList"))

                //var newInput = '';
                //newInput = $("<input type='button' class='BtnDelete btn btn-sm btn-danger btn-delete' value='Delete Ticket Row' />");

                //$(this).parent().parent().find(".BtnAdd").after(newInput);
                //$(this).parent().parent().find(".BtnAdd").remove();
                seq++;
            }

        };

        function validationTo() {

            if ($(this).parent().parent().find("input[name=toAmt]").val() == '') {
                $(this).parent().parent().find("input[name=toAmt]").addClass("error");
                valid = false;
            }
            else if ($(this).parent().parent().find("input[name=toAmt]").val() != '' && parseFloat($(this).parent().parent().find("input[name=toAmt]").val()) < parseFloat($(this).parent().parent().find("input[name=fromAmt]").val())) {
                $(this).parent().parent().find("input[name=toAmt]").addClass("error");
            }
            else {
                $(this).parent().parent().find("input[name=toAmt]").removeClass("error");
                var _totTicket = (parseInt($(this).parent().parent().find("input[name=toAmt]").val()) - parseInt($(this).parent().parent().find("input[name=fromAmt]").val())) + 1;
                $(this).parent().parent().find("input[name=totTicket]").val(_totTicket)
                $(this).parent().parent().find("input[name=ticketAmt]").val(_totTicket * 25);
                var tAmt = 0;
                $(".ticketAmt").each(function (index, val) {
                    tAmt = tAmt + parseInt($(val).val());
                });

                $("#lblTAmt").text(tAmt);
            }
        }

        function validationFrom() {
            if ($(this).parent().parent().find("input[name=fromAmt]").val() == '') {
                $(this).parent().parent().find("input[name=fromAmt]").addClass("error");
                valid = false;
            }
            else if ($(this).parent().parent().find("input[name=fromAmt]").val() != '' && parseFloat($(this).parent().parent().find("input[name=fromAmt]").val()) > parseFloat($(this).parent().parent().find("input[name=toAmt]").val())) {
                $(this).parent().parent().find("input[name=fromAmt]").addClass("error");
            }
            else {
                $(this).parent().parent().find("input[name=fromAmt]").removeClass("error");
            }

            var tAmt = 0;
            $(".ticketAmt").each(function (index, val) {
                if ($(val).val() != '') {
                    tAmt = tAmt + parseInt($(val).val());
                }
            });

            $("#lblTAmt").text(tAmt);

        }

        function Save() {
            var arrTickets = new Array();
            $(".ticketRange").each(function () {
                var objTicketDetail = {
                    ticketFrom: $(this).find(".fromAmt").val(),
                    ticketTo: $(this).find(".toAmt").val(),
                    totTicket: $(this).find(".totTicket").val(),
                    totalAmount: $(this).find(".ticketAmt").val(),
                }
                if (parseFloat(objTicketDetail.ticketFrom) > 0 && parseFloat(objTicketDetail.ticketTo) > 0)
                    arrTickets.push(objTicketDetail);

            });

            var ticketData = JSON.stringify(arrTickets);

            $.ajax({
                type: "POST",
                url: "TicketBuy.aspx/SaveTicketDetail",
                dataType: "json",
                data: JSON.stringify({ tickets: ticketData }),
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    if (result.d == "True") {
                        location.href = "BillingInformation.aspx";
                    }
                    else {
                        //ShowAlertError("Oops, something went wrong");
                    }
                },
                error: function (result) {
                    //ShowAlertError("Oops, something went wrong");
                }

            });
        }

    </script>

</asp:Content>

