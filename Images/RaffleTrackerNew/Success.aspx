<%@ Page Title="" Language="C#" MasterPageFile="~/PaymentSite.master" AutoEventWireup="true" CodeFile="Success.aspx.cs" Inherits="Success" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .credit-card-box .panel-title {
            display: inline;
            font-weight: bold;
        }

        .credit-card-box .form-control.error {
            border-color: red;
            outline: 0;
            box-shadow: inset 0 1px 1px rgba(0,0,0,0.075),0 0 8px rgba(255,0,0,0.6);
        }

        .credit-card-box label.error {
            font-weight: bold;
            color: red;
            padding: 2px 8px;
            margin-top: 2px;
        }

        .credit-card-box .payment-errors {
            font-weight: bold;
            color: red;
            padding: 2px 8px;
            margin-top: 2px;
        }

        .credit-card-box label {
            display: block;
        }
        /* The old "center div vertically" hack */
        .credit-card-box .display-table {
            display: table;
        }

        .credit-card-box .display-tr {
            display: table-row;
        }

        .credit-card-box .display-td {
            display: table-cell;
            vertical-align: middle;
            width: 50%;
        }
        /* Just looks nicer */
        .credit-card-box .panel-heading img {
            min-width: 180px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div class="row">
        <div style="background-color: #428bca;">
            <asp:Literal ID="ltrBredCrumb" runat="server" Text=""> </asp:Literal>
        </div>
    </div>
    <div class="row" style="background: white; margin-bottom: 10px; min-height: 380px; border: 1px solid #808080">
        <div class="col-lg-12 col-md-12" style="padding-top: 15px">
            <div class="col-lg-12 col-sm-12">
                <div class="filterContent sidebarWidget register-form" style="border: none!important;">
                    <div class="row">
                        <div style="width: 100%; text-align: center; padding-top: 2%">
                            <h3>Thank you for participating in the &nbsp;<asp:Label ID="lblChurchName" runat="server" ></asp:Label> &nbsp; Raffle
                            </h3>
                        </div>

                         <div style="width: 100%; text-align: center; padding-top: 2%;">
                            <h3>YOUR TRANSACTION IS COMPLETE
                            </h3>
                        </div>

                        <div style="width: 100%; text-align: center; padding-top: 2%; color:#464646"">
                            <h3>YOUR CONFIRMATION NUMBER IS &nbsp; <asp:Label ID="lblConfirmation" runat="server" Text="" ForeColor="#47a94e"></asp:Label>
                            </h3>
                        </div>

                        <div class="form form-horizontal">
                            <h5 style="text-align:center; color:#dc4646; padding-top: 0px; font-size: 14px!important">Please write ticket confirmation number on the ticket in the space provided.
                            </h5>
                        </div>

                        <div style="width: 100%; text-align: center; padding-top: 2%; color:#464646"">
                            <h3>Purchase another ticket? Click below.
                            </h3>
                        </div>

                        <div class="form form-horizontal" style="text-align:center; margin-top:1%; margin-bottom:3%">
                            <asp:Button ID="btnHome" runat="server" Text="Buy Tickets" CssClass="btn btn-outline btn-primary" Width="120" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

