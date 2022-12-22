<%@ Page Title="" Language="C#" MasterPageFile="~/PaymentSite.master" AutoEventWireup="true" CodeFile="Failed.aspx.cs" Inherits="Failed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" Runat="Server">
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
                            <h3>Ticket Purchased Failed.
                            </h3>
                        </div>

                        <div style="width: 100%; text-align: left; padding-top: 2%; font-size: 14px; font-weight: 600">
                            <div class="col-lg-12" style="color: #e23636; font-weight: 600; font-size: 22px; text-align: center; padding-top:2%">
                                <img src="images/failed.png" alt="Success" style="height: 50px; margin-left: 3%" /> &nbsp;&nbsp;
                                Your Payment has been Failed!
                            </div>
                        </div>
                       
                        <div class="form form-horizontal" style="text-align:center; margin-top:16%">
                            <asp:Button ID="btnHome" runat="server" Text="Go Home" CssClass="btn btn-outline btn-primary" Width="120" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

