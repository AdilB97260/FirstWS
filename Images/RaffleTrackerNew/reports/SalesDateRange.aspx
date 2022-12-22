<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="SalesDateRange.aspx.cs" Inherits="reports_SalesDateRange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <%--<link href="../css/jquery-ui.min.css" rel="stylesheet" type="text/css" />--%>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript">      </script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script type="text/javascript">
        $(function () {

            $(document).ready(function () {
                
                if ($("#hdnRedirect").val() != "" && $("#hdnRedirect").val() == "1") {
                    $.ajax({
                        type: 'POST',
                        dataType: 'json',
                        contentType: 'application/json',
                        url: '/reports/SalesDateRange.aspx/GetData',
                        data: "{dtF:'" + $("#txtDatePickerFrom").val() + "',dtT:'" + $("#txtDatePickerTo").val() + "',sType1:'" + $("#ddlSalesBy1").val() + "',sType2:'" + $("#ddlSalesBy2").val() + "',sType:'" + $("#ddlSalesType").val() + "'  }",
                        //data: {},
                        success:
                        function (response) {
                            drawVisualization(response.d);
                        },
                    });
                }
            });

            $(window).load(function () {

                if ($("#hdnUserType").val() != "" && $("#hdnUserType").val() == "CHURCH" && $("#hdnUserPk").val() != "") {

                    $("#ddlSalesBy1").html("");
                    $("#ddlSalesBy1").append($('<option value="CHURCH" selected="selected">Church</option>'));
                    $("#ddlSalesBy1").append($('<option value="MEMBER">Member</option>'));
                    //$("#ddlSalesBy2").attr("disabled", "disabled");
                    $("#ddlSalesBy1").val("CHURCH");
                    $("#ddlSalesBy1").attr("disabled", "disabled");
                    $("#ddlSalesBy2").attr("disabled", "disabled");
                }


                

            });

            $('#txtDatePickerFrom').datepicker(
            {
                dateFormat: 'mm/dd/yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:2100'
            });
            $('#txtDatePickerTo').datepicker(
            {
                dateFormat: 'mm/dd/yy',
                changeMonth: true,
                changeYear: true,
                yearRange: '1950:2100'
            });


            $("#txtDatePickerFrom").val($.datepicker.formatDate("mm/dd/yy", new Date("2018-01-01")));
            $("#txtDatePickerTo").val($.datepicker.formatDate("mm/dd/yy", new Date()));

            $("#GenRep").click(function () {

                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: '/reports/SalesDateRange.aspx/GetData',
                    data: "{dtF:'" + $("#txtDatePickerFrom").val() + "',dtT:'" + $("#txtDatePickerTo").val() + "',sType1:'" + $("#ddlSalesBy1").val() + "',sType2:'" + $("#ddlSalesBy2").val() + "',sType:'" + $("#ddlSalesType").val() + "'  }",
                    //data: {},
                    success:
                    function (response) {
                        drawVisualization(response.d);
                    }
                });


            });




        });

        function drawVisualization(dataValues) {
            var Total = 0;
            var Amount = 0;
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            var options = {
                title: 'Sales By Date Range',
                is3D: true,
                chartArea: { left: 20, top: 30, width: '90%', height: '75%' }
            };

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value]);
                Total += parseInt(dataValues[i].Value);
                Amount += parseFloat(dataValues[i].Amount);
            }

            $("#lblTotal").text(Total);
            $("#lblAmt").text('$' + Amount);

            new google.visualization.PieChart(document.getElementById('visualization')).draw(data, options);
        }

    </script>
    <script type="text/javascript">

        function getSalesByList() {
            var salesBy = $("#ddlSalesBy1").val();
            var salesType = $("#ddlSalesType").val();

            $.ajax
            ({
                url: '/reports/SalesDateRange.aspx/GetSalesByData',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: "{sType1:'" + salesBy + "'}",
                success:
                    function (response) {
                        $("#ddlSalesBy2").html("");
                        $("#ddlSalesBy2").append($('<option></option>').val("").html("ALL"));
                        $.each(response.d, function (i, obj) {
                            $("#ddlSalesBy2").append($('<option></option>').val(obj.user_pk).html(obj.name));
                        });
                    }
            });
        }  
    </script>
    <script type="text/javascript">
        $(document).ajaxStart(function () {
            $('#overlay').show();
        }).ajaxSuccess(function () {
            $('#overlay').hide();
        }).ajaxComplete(function () {
            $('#overlay').hide();
        }).ajaxError(function () {
            $('#overlay').hide();
        }).ajaxStop(function () {
            $('#overlay').hide();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <asp:HiddenField ID="hdnUserType" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnUserPk" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnRedirect" runat="server" Value="1" ClientIDMode="Static" />
        <div style="background-color: #2f5c84;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 0px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;
                    min-height: 510px">
                    <div class="row" style="padding-top: 20px;">
                        <div class="col-lg-12 col-sm-12">
                            <div class="col-lg-2 col-sm-2">
                                <label>
                                    Sales From Date :
                                </label>
                                <input type="text" id="txtDatePickerFrom" />
                            </div>
                            <div class="col-lg-2 col-sm-2">
                                <label>
                                    Sales To Date :
                                </label>
                                <input type="text" id="txtDatePickerTo" />
                            </div>
                            <div class="col-lg-8 col-sm-8">
                                <div class="col-lg-3 col-sm-3">
                                    <label>
                                        Sales By :
                                    </label>
                                    <select id="ddlSalesBy1" name="ddlSalesBy1" class="formDropdown1 formcontrol" style="width: 140px">
                                        <option value="RAFFLE">Raffle</option>
                                        <option value="CHURCH" selected="selected">Church</option>
                                        <option value="MEMBER">Member</option>
                                    </select>
                                </div>
                                <div class="col-lg-2 col-sm-2">
                                    <label>
                                        Top Sales :
                                    </label>
                                    <br />
                                    <select id="ddlSalesBy2" class="formDropdown1 formcontrol" style="width: 100px">
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="5">5</option>
                                        <option value="10">10</option>
                                        <option value="15">15</option>
                                        <option value="20">20</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                        <option value="500">500</option>
                                        <option value="0" selected="selected">All</option>
                                    </select>
                                </div>
                                <div class="col-lg-2 col-sm-2">
                                    <label>
                                        Sales Type :
                                    </label>
                                    <select id="ddlSalesType" name="ddlSalesType" class="formDropdown1 formcontrol" style="width: 110px">
                                        <option value="NET" selected="selected">Net</option>
                                        <option value="GROSS">Gross</option>
                                    </select>
                                </div>
                                <div class="col-lg-3 col-sm-3" style="padding-top: 23px; margin-left:20px;">
                                    <input type="button" id="GenRep" class="buttonColorSmall" title="Generate Report"
                                        value="Generate Report" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 20px">
                        <div class="col-lg-12 col-md-12">
                            <div id="visualization" style="width: 100%; height: 80%; min-height: 380px">
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 20px; padding-bottom: 30px; font-weight: bold;
                        font-size: 14px; padding-left: 20px;">
                        <div class="col-lg-4 col-md-4">
                            <div class="col-lg-6 col-md-6">
                                Total Sold Ticket :
                            </div>
                            <div class="col-lg-6 col-md-6" style="text-align: left">
                                <label id="lblTotal" style="font-weight: bold">
                                    0
                                </label>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4">
                            <div class="col-lg-6 col-md-6">
                                Total Dollars :
                            </div>
                            <div class="col-lg-6 col-md-6" style="text-align: left">
                                <label id="lblAmt" style="font-weight: bold">
                                    $&nbsp;0
                                </label>
                            </div>
                        </div>
                         <div class="col-lg-3 col-md-3">
                                <asp:Button ID="btnSales" runat="server" OnClick="btnSales_Click" Text="View Top Accounting Report" CssClass="buttonColorSmall" />
                            </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="overlay" style="display: none">
        <img id="loading-image" src="/images/wait.GIF" alt="Loading..." />
    </div>
</asp:Content>
