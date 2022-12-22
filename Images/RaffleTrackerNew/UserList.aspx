<%@ Page Title="" Language="C#" MasterPageFile="~/RaffleTracker.master" AutoEventWireup="true"
    CodeFile="UserList.aspx.cs" Inherits="UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <script type="text/javascript">
        function DeleteUser(id) {
            jConfirm('Are you sure want to delete Users ?', 'Confirmation', function (r) {
                if (r) {
                    $.ajax({
                        type: "POST",
                        url: "UserList.aspx/DeleteUser",
                        data: "{tid: '" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d == "true") {
                                jAlert("User deleted successfully.", "Delete Successs", true, window.location.href);
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
                            <h1 style="padding-top: 10px; padding-bottom: 0px; text-transform: inherit">
                                Manage Users</h1>
                            <span style="float: right;">
                                <asp:Button Text="Back" ID="btncancel" runat="server" CssClass="buttonGrey xlarge"
                                    OnClick="btncancel_click" /></span> <span style="float: right; padding-right: 10px;">
                                        <asp:Button Text="Add New User" ID="btnAddUser" runat="server" CssClass="buttonColor"
                                            OnClick="btnAddUser_Click" /></span>
                            <div class="divider" style="margin-top: 1px">
                            </div>
                            <div style="clear: both">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <table class="table table-striped table-bordered table-hover nowrap dataTable no-footer dtr-inline collapsed"
                                                id="dataTables-example">
                                                <thead>
                                                    <tr>
                                                        <th>
                                                            Full Name
                                                        </th>
                                                        <th>
                                                            Email
                                                        </th>
                                                        <th>
                                                            User Name
                                                        </th>
                                                        <th>
                                                            User Type
                                                        </th>
                                                        <th>
                                                            Address
                                                        </th>
                                                        <th>
                                                            Phone
                                                        </th>
                                                        <th>
                                                            Action
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptEmpList">
                                                        <ItemTemplate>
                                                            <tr class="odd gradeX">
                                                                <td>
                                                                    <%# Eval("FullName")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("email") %>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("username")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("DistUserType")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("UserAddress")%>
                                                                </td>
                                                                <td>
                                                                    <%# Eval("userPhone")%>
                                                                </td>
                                                                <td>
                                                                    <a href="/UserRegistration.aspx?id=<%# Eval("user_pk").ToString() %>" id="Edit" style="color: Maroon">
                                                                        Edit</a> &nbsp;&nbsp; <a href="javascript:;" id="adelete" style="color: Maroon" onclick="DeleteUser('<%# Eval("user_pk").ToString() %>')">
                                                                            Delete</a>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                        <!-- /.table-responsive -->
                                    </div>
                                    <!-- /.panel-body -->
                                </div>
                                <!-- /.panel -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <script src="/js/plugins/dataTables/jquery.dataTables.js"></script>
    <script src="/js/plugins/dataTables/dataTables.bootstrap.js"></script>
   
    <script>
        $(document).ready(function () {
            var table = $('#example').DataTable({
                responsive: true
            });



            $('#dataTables-example').dataTable({
                "pageLength": 10,
                "bSort": true,
                "columns": [null, null, null, null, null, null, { "orderable": false}]
            });


        });



    </script>
</asp:Content>
