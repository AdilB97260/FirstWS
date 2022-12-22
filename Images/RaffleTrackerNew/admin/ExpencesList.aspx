<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true" CodeFile="ExpencesList.aspx.cs" Inherits="admin_ExpencesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript" src="/js/plugins/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="/js/plugins/dataTables/dataTables.bootstrap.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#dataTables-example').dataTable({
                "pageLength": 10,
                "bSort": true,
                "order": [[0, "desc"]],
                "columns": [null, null, null, null, null,null, { "orderable": false }, { "orderable": false }],
                "footerCallback": function (row, data, start, end, display) {
                    var api = this.api(), data;

                    // Remove the formatting to get integer data for summation
                    var intVal = function (i) {
                        return typeof i === 'string' ?
                            i.replace(/[\$,]/g, '') * 1 :
                            typeof i === 'number' ?
                            i : 0;
                    };

                    // Total over all pages
                    total = api
                        .column(4)
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    // Total over this page
                    pageTotal = api
                        .column(4, { page: 'current' })
                        .data()
                        .reduce(function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                    // Update footer
                    $(api.column(4).footer()).html(
                        'Page Total $' + pageTotal.toFixed(2) + ' &nbsp;&nbsp;&nbsp;&nbsp;        Gross Total ( $' + total.toFixed(2) + ')'
                    );
                }
            });
        });


        //        $(document).ready(function () { $('#example').DataTable({ "order": [[3, "desc"]] }); });



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
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 540px">
        <div class="col-lg-12 col-md-12">
            <div class="row">
                <div style="background-color: #428bca;">
                    <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
                </div>
            </div>

            <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                <div id="overlay" style="display: none">
                    <img id="loading-image" src="images/wait.GIF" alt="Loading..." />
                </div>

                <div class="row">
                    <div class="col-lg-12 col-md-12" style="padding-left: 0px; padding-right: 0px; padding-top: 15px;">
                        <h1 style="padding-bottom: 0px; text-transform: inherit">Expenses List</h1>
                        <span style="float: right;">
                            <asp:Button Text="Cancel" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                OnClick="btncancel_Click" /></span>
                        <span style="float: right; padding-right: 10px;">
                            <asp:Button Text="Add Expenses" ID="btnExpences" runat="server" CssClass="buttonColor"
                                OnClick="btnExpences_Click" /></span>

                    </div>

                </div>
                <div class="row">
                    <div class="table-responsive" style="padding-bottom:15px; font-family: 'montserratregular', sans-serif;
    font-size: 12px;">
                        <table class="table table-striped table-bordered table-hover" id="dataTables-example" style="width: 100%; padding-top: 15px;">
                            <thead>
                                <tr>
                                    <th style="width: 14%">Expenses Date
                                    </th>
                                    <th>
                                        Category
                                    </th>
                                    <th>Expenses Name
                                    </th>
                                    <th>To Whome Given
                                    </th>
                                    <th style="width: 10%">Amount
                                    </th>
                                    <th>Description
                                    </th>
                                    <th>Receipt
                                    </th>
                                    <th style="width: 7%">Action
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rptExp">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td style="min-width: 110px">
                                                <%# Eval("expences_date", "{0: MM/dd/yyyy}")%>
                                            </td>
                                            <td>
                                                <%# Eval("category")%>
                                            </td>
                                            <td>
                                                <%# Eval("expences_name")%>
                                            </td>
                                            <td>
                                                <%# Eval("given_to")%>
                                            </td>
                                            <td>
                                                <%#string.Format("{0:n2}",Eval("Amount")) %>
                                            </td>
                                           
                                            <td>
                                                <%# Eval("description")%>
                                            </td>
                                             <td>
                                                <asp:LinkButton runat="server" CommandName="ShowFile" CommandArgument='<%# Eval("receipt") %>' ><%# Eval("receipt")%></asp:LinkButton>
                                            </td>
                                            <td style="min-width: 30px">
                                                <a href="/admin/ManageExpencess.aspx?id=<%# Eval("Inventory_ID").ToString() %>" class='btn btn-xs btn-go'>
                                                    <span style='font-size: 11px; color: Maroon'>Edit</span> </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="7" style="text-align: left; color:maroon"></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>


            </div>


        </div>
    </div>
</asp:Content>

