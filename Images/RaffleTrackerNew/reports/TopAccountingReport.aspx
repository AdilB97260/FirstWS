<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true" CodeFile="TopAccountingReport.aspx.cs" Inherits="reports_TopAccountingReport" %>

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
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: '/reports/TopAccountingReport.aspx/GetData',
                    data: "{dtF:'" + $("#txtDatePickerFrom").val() + "',dtT:'" + $("#txtDatePickerTo").val() + "',sType:'" + $("#ddlSalesType").val() + "'  }",
                    //data: {},
                    success:
                    function (response) {
                        drawVisualization(response.d);
                    }
                });
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
                    url: '/reports/TopAccountingReport.aspx/GetData',
                    data: "{dtF:'" + $("#txtDatePickerFrom").val() + "',dtT:'" + $("#txtDatePickerTo").val() + "',sType:'" + $("#ddlSalesType").val() + "'  }",
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
            var Expensess = 0;
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            var options = {
                title: 'Profit Chart',
                width: '100%',
                height: '100%',
                is3D: true,
                chartArea: { left: 20, top: 30, width: '100%', height: '100%' },
                pieSliceText: 'value',
                //pieSliceText: 'value-and-percentage',
                //pieStartAngle: 50,
            };

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value]);
                console.log(dataValues[i].ColumnName);
                console.log(dataValues[i].Value);
                if (!dataValues[i].ColumnName.includes("Expences")) {
                    Total += parseInt(dataValues[i].Value);
                    Amount += parseFloat(dataValues[i].Amount);
                }

            }

            for (var i = 0; i < dataValues.length; i++) {
                if (dataValues[i].ColumnName.includes("Expences")) {
                    Expensess += parseFloat(dataValues[i].Amount);
                }
            }

            //$("#lblTotal").text(Total);
            $("#lblAmt").text('$' + Amount);
            $("#lblExp").text('$' + Expensess);

            var formatter = new google.visualization.NumberFormat({ pattern: '$###,###' });
            formatter.format(data, 1);

            new google.visualization.PieChart(document.getElementById('visualization')).draw(data, options);
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
        <div style="background-color: #2f5c84;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px;">
        <div class="col-lg-12 col-md-12" style="padding-top: 0px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important; min-height: 510px">
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
                                <div class="col-lg-2 col-sm-2">
                                    <label>
                                        Sales Type :
                                    </label>
                                    <select id="ddlSalesType" name="ddlSalesType" class="formDropdown1 formcontrol" style="width: 110px">
                                        <option value="NET">Net</option>
                                        <option value="GROSS" selected="selected">Gross</option>
                                    </select>
                                </div>
                                <div class="col-lg-3 col-sm-3" style="padding-top: 23px; margin-left: 20px;">
                                    <input type="button" id="GenRep" class="buttonColorSmall" title="Generate Report"
                                        value="Generate Report" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 20px">
                        <div class="col-lg-12 col-md-12">
                            <div id="visualization" style="width: 100%; height: 100%; min-height: 380px">
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 20px; padding-bottom: 30px; font-weight: bold; font-size: 14px; padding-left: 20px; font-weight: 600">
                        <div class="col-lg-9 col-md-9">
                            <div class="col-lg-12 col-md-12">
                                <div class="col-lg-12 col-md-12" style="color: maroon">
                                    Total Profit : 
                                    <label id="lblAmt" style="font-weight: 600">
                                        $&nbsp;0
                                    </label>
                                </div>

                            </div>
                            <div class="col-lg-12 col-md-12">
                                <div class="col-lg-12 col-md-12" style="color: maroon">
                                    Total Expences  : 
                                    <label id="lblExp" style="font-weight: 600">
                                        $&nbsp;0
                                    </label>
                                </div>

                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3">
                            <div class="col-lg-10 col-md-10">
                                <asp:Button ID="btnSales" runat="server" OnClick="btnSales_Click" Text="View Sales Report" CssClass="buttonColorSmall" />
                            </div>
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


