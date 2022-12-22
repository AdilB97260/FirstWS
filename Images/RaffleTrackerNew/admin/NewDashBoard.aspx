<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true" CodeFile="NewDashBoard.aspx.cs" Inherits="admin_NewDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <style>
        .widget-content .widget-content-wrapper {
            display: flex;
            flex: 1;
            position: relative;
            align-items: flex-start;
        }

        .text-white {
            color: #fff !important;
        }

        .widget-content .widget-content-right {
            margin-left: auto;
        }

        .widget-content .widget-content-left .widget-heading {
            opacity: .9;
            font-weight: bold;
            font-size: 1.8rem;
        }

        .widget-content .widget-numbers {
            font-weight: bold;
            font-size: 1.8rem;
            display: block;
        }

        .widget-content .widget-numbers {
            font-weight: bold;
            font-size: 1.8rem;
            display: block;
        }

        .widget-content .widget-content-left .widget-subheading {
            opacity: 1.2;
        }


        .card.mb-3 {
        }

        .widget-content {
            padding: 1rem;
            flex-direction: row;
            align-items: center;
        }

        .card {
            box-shadow: 0 0.46875rem 2.1875rem rgba(4,9,20,0.03), 0 0.9375rem 1.40625rem rgba(4,9,20,0.03), 0 0.25rem 0.53125rem rgba(4,9,20,0.05), 0 0.125rem 0.1875rem rgba(4,9,20,0.03);
            border-width: 0;
            transition: all .2s;
        }

        .bg-midnight-bloom {
            background-image: radial-gradient(circle 248px at center, #58488a 0%, #8d84da 47%, #0b2f48 100%) !important;
        }

        .bg-midnight-bloom1 {
            background-image: radial-gradient(circle 248px at center, #517ccc 0%, #415f94 47%, #1c3254 100%) !important
        }

        .card {
            position: relative;
            display: flex;
            flex-direction: column;
            min-width: 0;
            word-wrap: break-word;
            background-color: #fff;
            background-clip: border-box;
            border: 1px solid rgba(26,54,126,0.125);
            border-radius: .25rem;
        }


        .widget-content .widget-content-right {
            margin-left: auto;
        }

        .bg-arielle-smile {
            background-image: radial-gradient(circle 248px at center, #37959a 0%, #549aab 47%, #0b2f48 100%) !important
        }

        .bg-grow-early {
            background-image: linear-gradient(to top, #96666d 0%, #944848 100%) !important
        }

        .card-header, .card-title {
            text-transform: uppercase;
            color: rgba(117, 21, 43, 0.7);
            font-weight: bold;
            font-size: 16px;
        }

        .card-body {
            /*flex: 1 1 auto;*/
            padding: 1.25rem;
        }


        .card.mb-3 {
            margin-bottom: 10px !important;
        }

        .table thead th {
            vertical-align: bottom;
            /*border-bottom: 1px solid #e9ecef;*/
        }

        .table th, .table td {
            padding: .55rem;
            vertical-align: top;
            /*border-top: 1px solid #e9ecef;*/
        }

        table {
            border-collapse: collapse;
        }

        th {
            text-align: inherit;
        }

        .table th, .table td {
            padding: .55rem;
            vertical-align: top;
            /*border-top: 1px solid #e9ecef;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js" type="text/javascript">      </script>
    <script type="text/javascript" src="//www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
    </script>
    <script type="text/javascript">

        $(function () {

            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: '/admin/NewDashBoard.aspx/GetData',
                data: "{noOfCount:" + "0" + "}",
                success:
                    function (response) {
                        drawVisualization(response.d);
                    }
            });


        });

        function drawVisualization(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Column Name');
            data.addColumn('number', 'Column Value');

            var options = {
                titleTextStyle: {
                    fontSize: 16,
                    color: 'Maroon',
                    bold: false
                },
                title: 'Transaction Chart',
                is3D: true,
                chartArea: { left: 20, top: 30, width: '100%', height: '90%' }
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
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['Year', 'Ticket Disribution', 'Ticket Sales'],
                ['1000', 1000, 400],
                ['2000', 1170, 460],
                ['3000', 660, 1120],
                ['4000', 1030, 540]
            ]);

            var options = {
                title: 'Reffle Ticket Traking',
                curveType: 'function',
                legend: { position: 'bottom' }
            };

            var chart = new google.visualization.LineChart(document.getElementById('curve_chart'));

            chart.draw(data, options);
        }
    </script>
    <div class="row">
        <div style="background-color: #2f5c84;">
            <asp:Literal ID="ltrBredCrumb" runat="server" Text="Home/Dashboard"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white;">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="min-height: 480px; border: none!important;">
                    <div class="row" style="padding-top: 20px;">
                        <div class="col-md-4 col-xl-4">
                            <div class="card mb-3 widget-content bg-midnight-bloom1">
                                <div class="widget-content-wrapper text-white">
                                    <div class="widget-content-left" style="float: left">
                                        <div class="widget-heading">Total Sales Ticket</div>
                                        <div class="widget-subheading" style="padding-top: 5px; padding-bottom: 0px; font-weight: bold; font-size: 18px; color: #fff; text-align: center">
                                            <asp:Label ID="lblTotSalesticket" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-xl-4">
                            <div class="card mb-3 widget-content bg-arielle-smile">
                                <div class="widget-content-wrapper text-white">
                                    <div class="widget-content-left" style="float: left">
                                        <div class="widget-heading">Total Sales Ammount</div>
                                        <div class="widget-subheading" style="padding-top: 5px; padding-bottom: 0px; font-weight: bold; font-size: 18px; color: #fff; text-align: center">
                                            <asp:Label ID="lblTotSalesAmmount" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-xl-4">
                            <div class="card mb-3 widget-content bg-grow-early">
                                <div class="widget-content-wrapper text-white">
                                    <div class="widget-content-wrapper text-white">
                                        <div class="widget-content-left" style="float: left">
                                            <div class="widget-heading">Total Ticket Disribution </div>
                                            <div class="widget-subheading" style="padding-top: 5px; padding-bottom: 0px; font-weight: bold; font-size: 18px; color: #fff; text-align: center">
                                                <asp:Label ID="lblTotDistriBution" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="row">

                                <div class="col-lg-3 col-md-03 col-sd-03" style="left: 70px;">
                                    <a href="/admin/RaffleRegistration.aspx" data-toggle="tooltip" data-original-title="Register new Raffle and distribute ticket"
                                        style="text-align: center">
                                        <img src="/images/Raffle.jpg" alt="Register new Raffle and distribute ticket" title="Register new Raffle and distribute ticket"
                                            height="100" width="100" style="padding-left:20px"/>
                                        <br />
                                        <br />
                                        <label>
                                            <strong style="font-size: 16px;">Register Raffle</strong>
                                        </label>
                                    </a>
                                </div>


                                <div class="col-md-6">
                                    <div class="col-lg-12" style="padding-top: 20px">
                                        <div class="card mb-3 widget-content bg-midnight-bloom" style="height: 76px; padding-top: 15px">

                                            <asp:DropDownList ID="ddlRaffleList" runat="server" CssClass="formDropdown1 formcontrol" AutoPostBack="true" OnSelectedIndexChanged="ddlRaffleList_SelectedIndexChanged">
                                                <asp:ListItem Text="Select Raffle" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-3 col-md-03 col-sd-03" style="left: 50px;">
                                    <a href="/ImportDistributionData.aspx" title="Import Distribution" style="text-align: center;">
                                        <img src="/images/enter-distribution.png" alt="Import Distribution" title="Import Distribution"
                                            height="110" width="110" style="padding-left:20px;" />
                                        <br />
                                        <label>
                                            <strong style="font-size: 16px;padding-right:20px; ">Import Distribution</strong></label>
                                    </a>
                                    &nbsp;
                                </div>


                            </div>
                        </div>

                        <div class="col-lg-12" style="padding: 0!important">
                            <div class="row">
                                <div class="col-md-6">

                                    <div class="col-lg-12" style="left: 468px">
                                        <div class="col-lg-6 col-md-6">
                                            <a title="Disribution" href="../DistributionList.aspx" style="font-size: 15px">more</a>
                                        </div>


                                    </div>

                                    <div class="main-card mb-3 card">
                                        <div class="card-body">

                                            <h5 class="card-title" style="margin-top: 0px!important">Latest Ticket Disribution</h5>
                                            <table class="mb-0 table table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>Time Stamp</th>
                                                        <th>Distributor Name</th>
                                                        <th>Ticket From</th>
                                                        <th>Ticket To</th>
                                                        <th>Total Ticket
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptDistribution" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="odd gradeX">
                                                                <td style="min-width: 80px">
                                                                    <%# Eval("strCreatedDate")%>
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
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">

                                        <div class="col-lg-12">

                                            <div class="col-lg-6 col-md-6" style="left: 468px">
                                                <a href="../SaleList.aspx" style="font-size: 15px">more</a>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="main-card mb-3 card">
                                                <div class="card-body">
                                                    <h5 class="card-title" style="margin-top: 0px!important">Latest Ticket Sales</h5>
                                                    <table class="mb-0 table table-striped">
                                                        <thead>
                                                            <tr>
                                                                <th>Time Stamp</th>
                                                                <th>Distributor Name</th>
                                                                <th>Ticket From</th>
                                                                <th>Ticket To</th>
                                                                <th>Total Ticket
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptSell" runat="server">
                                                                <ItemTemplate>
                                                                    <tr class="odd gradeX">
                                                                        <td style="min-width: 110px">
                                                                            <%# Eval("CreatedDate")%>
                                                                        </td>
                                                                        <td>
                                                                            <%# Eval("GivenTo")%>
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
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>



                                        <div class="card-body" style="padding-top: 100px;">

                                            <div id="visualization" style="width: 100%; height: 100%; min-height: 280px"></div>

                                            <div id="curve_chart" style="width: 1000px; height: 300px"></div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
</asp:Content>

