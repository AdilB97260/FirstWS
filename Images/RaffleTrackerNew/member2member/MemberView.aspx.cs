using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class Member_MemberView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        
            RaffleController _provider = new RaffleController();
            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.Member2MemberPK));
            if (objDistUser != null)
            {
                if (UserSession.Inst.UserType == "ADMIN")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2})</a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "User");

                }
                else if (UserSession.Inst.UserType == "RAFFLE")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({0})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({1})</a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({2})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "User");
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({0})</a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({1})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.Member2MemberObj.name, "User");
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({0})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.Member2MemberObj.name, "User");
                }


                //txtEmail.Text = objDistUser.email;
                //txtAdd.Text = objDistUser.Address;
                ////txtMobileNo.Text = objDistUser.mobile;
                //txtPhone.Text = objDistUser.phone;
                //txtUserName.Text = objDistUser.name;
                //txtState.Text = objDistUser.State;
                //txtCity.Text = objDistUser.City;
                //txtZip.Text = objDistUser.Zip;
                //txtTotTicket.Text = Convert.ToInt32(objDistUser.Balance).ToString();
                //txtLogin.Text = objDistUser.UserName;
                //txtPassword.Text = objDistUser.password;




                RaffleController _controller = new RaffleController();
                List<TicketsDistribution> distList = _controller.GetTicketDistributionList().Where(x => x.Dist_user_Fk == objDistUser.user_pk).OrderBy(x => x.CreatedDate).ToList();
                rptDistribution.DataSource = distList;
                rptDistribution.DataBind();

                lblTotTick.Text = distList.Sum(x => x.TotalTickets).ToString();


                //List<TicketSold> _saleList = _controller.GetTicketSoldList().Where(x => x.MEM2MEM_FK == objDistUser.user_pk).OrderByDescending(x => x.CreatedDate).ToList();
                //rptSaleList.DataSource = _saleList;
                //rptSaleList.DataBind();
                //lblSoldTicket.Text = _saleList.Sum(x => x.TicketTotal).ToString();
                //lblSoldAmt.Text = _saleList.Sum(x => x.Amount).ToString();

            
        }
    }

    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("/member2member/dashboard.aspx");
    }

    protected void btnAddTicket_Click(object sernder, EventArgs e)
    {

        Response.Redirect("/DistributionForm.aspx?id=" + UserSession.Inst.Member2MemberPK);
    }
}