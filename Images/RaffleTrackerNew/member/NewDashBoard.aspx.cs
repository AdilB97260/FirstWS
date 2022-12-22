using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class member_NewDashBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!UserSession.Inst.IsUserLoggedIn)
            {
                Response.Redirect("/login.aspx");
            }

            if (UserSession.Inst.MemberPK <= 0)
            {
                if (UserSession.Inst.ChurchPK > 0)
                {
                    Response.Redirect("/church/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    Response.Redirect("/raffle/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserPK > 0 && UserSession.Inst.UserType == "ADMIN")
                {
                    Response.Redirect("/admin/NewDashBoard.aspx");
                }
                else
                {
                    UserSession.Inst.Logout();
                    Response.Redirect("/login.aspx");
                }
            }


        }


        if (!IsPostBack)
        {
            RaffleController _controller = new RaffleController();
            //ChurchDashboard

            lblTotSalesticket.Text = _controller.GetMemberTotalSalesTicket();
            lblTotSalesAmmount.Text = _controller.GetMemberTotalSalesAmmount();
            lblTotDistriBution.Text = _controller.GetMemberTotaldisribution();

            List<TicketsDistribution> _lstSold = _controller.GetMemberDistributerList();

            rptDistribution.DataSource = _lstSold;
            rptDistribution.DataBind();

            List<TicketSold> _lstSell = _controller.GetMemberSellList();
            rptSell.DataSource = _lstSell;
            rptSell.DataBind();

            if (UserSession.Inst.UserType == "MEMBER")
            {
                UserSession.Inst.RaffleObj = null;
                UserSession.Inst.ChurchObj = null;

                UserSession.Inst.ChurchPK = _controller.GetDistUserObj(UserSession.Inst.MemberPK).CreatedBy_UserFk;
                UserSession.Inst.RafflePK = _controller.GetDistUserObj(UserSession.Inst.ChurchPK).CreatedBy_UserFk;
            }


            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/NewDashBoard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/NewDashBoard.aspx' class='btn btn-primary'>Raffle({0}) </a> <a href='/church/NewDashBoard.aspx' class='btn btn-primary'>Church({1})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name);
            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/NewDashBoard.aspx' class='btn btn-primary'>Home</a><a href='/church/NewDashBoard.aspx' class='btn btn-primary'>Church({0})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name);
            }
            else if (UserSession.Inst.UserType == "CHURCH")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/NewDashBoard.aspx' class='btn btn-primary'>Home</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", UserSession.Inst.MemberObj.name);
            }
            else
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/NewDashBoard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Home");
            }



            ddlRaffleList.DataTextField = "name";
            ddlRaffleList.DataValueField = "user_pk";
            ddlRaffleList.DataSource = _controller.GetDistUserList().Where(x => x.UserType == "MEMBER2MEMBER" && x.CreatedBy_UserFk == UserSession.Inst.MemberPK).OrderBy(x => x.name);
            ddlRaffleList.DataBind();
            ddlRaffleList.Items.Insert(0, new ListItem { Text = "Select Users", Value = "0", Selected = true });

            UserSession.Inst.Member2MemberPK = 0;
            UserSession.Inst.Member2MemberObj = null;


        }

    }

    protected void btnDistribution_Click(object sender, EventArgs e)
    {
        Response.Redirect("/DistributionList.aspx");
    }

    protected void btnSell_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SaleList.aspx");
    }

    protected void ddlRaffleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRaffleList.SelectedValue) > 0)
        {
            UserSession.Inst.Member2MemberObj = null;
            UserSession.Inst.Member2MemberPK = Convert.ToInt32(ddlRaffleList.SelectedValue);
            Response.Redirect("/member2member/NewDashBoard.aspx");
        }
    }
}