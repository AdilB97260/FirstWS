using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WinnerList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if(!Page.IsPostBack)
        {
            ltrBredCrumb.Text = "Raffle - 2019 Winner List";
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/login.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>","Raffle 2021 - Winner List ");
        }
    }
}