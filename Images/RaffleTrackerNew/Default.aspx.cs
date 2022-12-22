using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ltrBredCrumb.Text = "View Raffle Live";
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/login.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf;padding-top:9px!important; padding-left:9px!important'>{0}</span></div>", "Raffle Ticket Tracking");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("/login.aspx");
    }
    protected void btnWathLive_Click(object sender, EventArgs e)
    {
        Response.Redirect("/draw.aspx");
    }
}