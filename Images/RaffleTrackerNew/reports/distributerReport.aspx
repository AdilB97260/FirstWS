<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="distributerReport.aspx.cs" Inherits="reports_distributerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript">      </script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script type="text/javascript">

        $(function () {


            $("#GenRep").click(function () {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    url: '/Reports/distributerReport.aspx/GetData',
                    data: "{noOfCount:" + $("#drpCount").val() + "}",
                    success:
                    function (response) {
                        drawVisualization(response.d);
                    }
                });
            });

        });

        function drawVisualization(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            //            var formatter = new google.visualization.NumberFormat({
            //                pattern: '#,###$'
            //            });
            //            formatter.format(data, 1);

            var options = {
                title: 'Sales By Distributer Repot',
                is3D: true,
                //                sliceVisibilityThreshold: .2,
                chartArea: { left: 50, top: 70, width: '100%', height: '75%' }
                //                pieHole: 0.4
                //                vAxis: { title: 'Dollars' },
                //                hAxis: { title: 'Year' }
            };

            for (var i = 0; i < dataValues.length; i++) {
                data.addRow([dataValues[i].ColumnName, dataValues[i].Value]);
            }
            //new google.visualization.PieChart(document.getElementById('visualization')).draw(data, options);

            new google.visualization.PieChart(document.getElementById('visualization')).draw(data, options);


            //var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            //chart.draw(data, options);
        }
 
 
        
    </script>
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
                        <div class="col-lg-12 col-sm-12">
                            <div class="col-lg-3 col-sm-3" style="padding-top: 8px">
                                <label>
                                    Select Top Distributer count
                                </label>
                            </div>
                            <div class="col-lg-2 col-sm-2">
                                <select id="drpCount" class="formDropdown1 formcontrol" style="width: 120px">
                                    <option value="5">5</option>
                                    <option value="10">10</option>
                                    <option value="20">20</option>
                                    <option value="50">50</option>
                                    <option value="100">100</option>
                                    <option value="500">500</option>
                                    <option value="0" selected="selected">All</option>
                                </select>
                            </div>
                            <div class="col-lg-2 col-sm-2" style="padding-top: 0px;">
                                <input type="button" id="GenRep" class="buttonColorSmall" title="Generate Report"
                                    value="Generate Report" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <div id="visualization" style="width: 100%; height: 80%; min-height: 520px">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
