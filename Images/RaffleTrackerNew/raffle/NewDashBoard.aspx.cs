using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class raffle_NewDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!UserSession.Inst.IsUserLoggedIn)
            {
                Response.Redirect("/login.aspx");
            }

            if (UserSession.Inst.RafflePK <= 0)
            {
                if (UserSession.Inst.UserPK > 0 && UserSession.Inst.UserType == "ADMIN")
                {
                    Response.Redirect("/admin/NewDashBoard.aspx");
                }
                else
                {
                    UserSession.Inst.Logout();
                    Response.Redirect("/login.aspx");
                }
            }

            if (UserSession.Inst.IsSystemUser && UserSession.Inst.UserType != "RAFFLE" && UserSession.Inst.UserType != "ADMIN")
            {
                Response.Redirect("/login.aspx");
            }

        }

        if (!IsPostBack)
        {
            RaffleController _controller = new RaffleController();
            //reffleDashboard

            lblTotSalesticket.Text = _controller.GetReffleTotalSalesTicekt();
            lblTotSalesAmmount.Text = _controller.GetReffleTotalSalesAmmount();
            lblTotDistriBution.Text = _controller.GetReffleTotaldisribution();

            List<TicketsDistribution> _lstSold = _controller.GetReffleDistributerList();

            rptDistribution.DataSource = _lstSold;
            rptDistribution.DataBind();

            List<TicketSold> _lstSell = _controller.GetReffeleSellList();
            rptSell.DataSource = _lstSell;
            rptSell.DataBind();
            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/NewDashBoard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Raffle");
            }
            else
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Home");
            }



            ddlRaffleList.DataTextField = "name";
            ddlRaffleList.DataValueField = "user_pk";
            ddlRaffleList.DataSource = _controller.GetDistUserList(UserSession.Inst.CurrentLoginID).Where(x => x.UserType == "CHURCH").OrderBy(x => x.name);
            ddlRaffleList.DataBind();
            ddlRaffleList.Items.Insert(0, new ListItem { Text = "Select Church", Value = "0", Selected = true });

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

    protected void btnSell_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SaleList.aspx");
    }

    protected void ddlRaffleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRaffleList.SelectedValue) > 0)
        {
            UserSession.Inst.ChurchObj = null;
            UserSession.Inst.ChurchPK = Convert.ToInt32(ddlRaffleList.SelectedValue);
            Response.Redirect("/church/NewDashBoard.aspx");
        }
    }
}