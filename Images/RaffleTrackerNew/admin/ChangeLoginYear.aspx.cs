using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ChangeLoginYear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a>  <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Change Login Year");

            RaffleController _conroller = new RaffleController();
            rptRaffle.DataSource = _conroller.GetRaffleYearList(); 
            rptRaffle.DataBind();

        }
    }

    protected void rptRaffle_ItemCommand(Object sender, CommandEventArgs e)
    {
        int raffleId = Convert.ToInt32(e.CommandArgument);
        RaffleController _controller = new RaffleController();
        _controller.ChangeRaffleLoginYear(raffleId);
        RaffleController _conroller = new RaffleController();
        rptRaffle.DataSource = _conroller.GetRaffleYearList();
        rptRaffle.DataBind();

        UserSession.Inst.CurrenLoginRaffle = null;
        
    }
}