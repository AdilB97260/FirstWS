using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class admin_NewDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!UserSession.Inst.IsUserLoggedIn)
            {
                Response.Redirect("/login.aspx");
            }

            if (UserSession.Inst.UserPK <= 0 && UserSession.Inst.UserType == "ADMIN")
            {
                UserSession.Inst.Logout();
                Response.Redirect("/login.aspx");
            }

            if (UserSession.Inst.IsUserLoggedIn && UserSession.Inst.UserType != "ADMIN")
            {
                Response.Redirect("/login.aspx");
            }

        }

        if (!Page.IsPostBack)
        {
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Home");


            RaffleController _controller = new RaffleController();


            lblTotSalesticket.Text = _controller.GetTotalSalesTicekt();
            lblTotSalesAmmount.Text = _controller.GetTotalSalesAmmount();
            lblTotDistriBution.Text = _controller.GetTotaldisribution();

            List<TicketsDistribution> _lstSold = _controller.GetTop5DistributerList();

            rptDistribution.DataSource = _lstSold;
            rptDistribution.DataBind();

            ddlRaffleList.DataTextField = "name";
            ddlRaffleList.DataValueField = "user_pk";
            ddlRaffleList.DataSource = _controller.GetDistUserList(UserSession.Inst.CurrentLoginID).Where(x => x.UserType == "RAFFLE");
            ddlRaffleList.DataBind();

            ddlRaffleList.Items.Insert(0, new ListItem { Text = "Select Raffle", Value = "0", Selected = true });

            List<TicketSold> _lstSell = _controller.GetTop5SellList();
            rptSell.DataSource = _lstSell;
            rptSell.DataBind();
            UserSession.Inst.RafflePK = 0;
            UserSession.Inst.RaffleObj = null;
            UserSession.Inst.ChurchPK = 0;
            UserSession.Inst.ChurchObj = null;
            UserSession.Inst.MemberPK = 0;
            UserSession.Inst.MemberObj = null;
            UserSession.Inst.Member2MemberPK = 0;
            UserSession.Inst.Member2MemberObj = null;

        }
    }

    protected void btnDistribution_Click(object sender, EventArgs e)
    {
        Response.Redirect("/DistributionList.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void SalesList_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SaleList.aspx");
    }

    protected void ddlRaffleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRaffleList.SelectedValue) > 0)
        {
            UserSession.Inst.RaffleObj = null;
            UserSession.Inst.RafflePK = Convert.ToInt32(ddlRaffleList.SelectedValue);
            Response.Redirect("/raffle/NewDashBoard.aspx", false);
        }
    }
}