using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Failed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["payfailed"] != null)
        {
            Session["payfailed"] = null;
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/TicketBuy.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a style=padding-top:10px!important;' href='/TicketBuy.aspx' class='btn btn-primary'>{0}</a> <span class='btn' style='cursor:auto; padding-top:10px!important; color:#e4dfdf'>{1}</span></div>", "Home", "Payment Failed!");
        }
        else
        {
            Response.Redirect("/TicketBuy.aspx");
        }
    }
}