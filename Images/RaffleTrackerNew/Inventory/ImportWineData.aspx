<%@ Page Title="" Language="C#" MasterPageFile="~/Inventory/Inventory.master" AutoEventWireup="true" CodeFile="ImportWineData.aspx.cs" Inherits="Inventory_ImportWineData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
     <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server"> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 0px; min-height: 585px">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div class="col-lg-12 col-md-12" style="padding-left: 0px; padding-right: 0px;">
                            <h1 style="padding-top: 10px; padding-bottom: 0px; text-transform: inherit">
                                Import Data</h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="btnBack" runat="server" CssClass="buttonGrey xlarge" OnClick="btnBack_Click"
                                     /></span>
                            <div class="divider" style="margin-top: 1px; margin-bottom: 5px!important">
                            </div>
                            <div style="clear: both">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12" style="padding-top: 5px; text-align: left; padding-top: 30px">
                    <div style="width: 16%; float: left; padding-top: 5px; text-align: right; padding-right: 20px">
                        <b>Import CSV Or Excel File </b>
                    </div>
                    <div style="width: 16%; float: left">
                        <asp:FileUpload ID="fldCSVFile" runat="server" />
                    </div>
                    <div class="col-lg-2" style="text-align: left">
                        <asp:Button ID="btnImport" runat="server" Text="Import Data" class="buttonColorSmall" OnClick="btnImport_Click" />
                    </div>
                    <div class="col-lg-6" style="text-align: right; padding-top: 10px">
                        <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
                            <h4 style="color: Green; font-size: 14px;text-align:left">
                                Total successed import records &nbsp;&nbsp;: &nbsp;
                                <asp:Label ID="lblSuccessRec" runat="server" Text="0"></asp:Label></h4>
                        </asp:Panel>
                        <span style="float:right; padding-right:3px; width:30%">
                            <asp:Button ID="btnDownloadTemplate" runat="server" Text="Download Template" class="buttonColorSmall" OnClick="btnDownloadTemplate_Click"  />
                        </span>

                    </div>
                </div>
            </div>
            <div class="row" style="padding-top: 10px; padding-left: 16px">
                <div class="panel-body" style="padding-top: 0px!important">
                    <asp:Panel ID="pnlFailed" runat="server" Visible="false">
                        <h4 style="color: Red; font-size: 14px">
                            List of failed import records &nbsp;&nbsp;: &nbsp;
                            <asp:Label ID="lblFailedRed" runat="server" Text="0"></asp:Label>
                        </h4>
                    </asp:Panel>
                    <div class="table-responsive" style="width: 97%; text-align: left;">
                        <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                            id="tblResult">
                            <thead>
                                <tr>
                                    <th>
                                       Wine Name
                                    </th>
                                    <th >
                                       Category
                                    </th>
                                    <th>
                                      Bottle Size
                                    </th>
                                    <th>
                                        Message
                                    </th>
                                  
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rptFailedData">
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td>
                                                <%# Eval("WineName")%>
                                            </td>
                                            <td>
                                                <%# Eval("Category")%>
                                            </td>
                                            <td>
                                                <%# Eval("BottleSize")%>
                                            </td>
                                            <td>
                                                <%# Eval("Message")%>
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
    </div>

     <script type="text/javascript" src="/js/plugins/dataTables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="/js/plugins/dataTables/dataTables.bootstrap.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $('#tblResult').dataTable({
                "pageLength": 10,
                "bSort": true,
                "columns": [null, null, null,{ "orderable": false }]
            });


        });


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

