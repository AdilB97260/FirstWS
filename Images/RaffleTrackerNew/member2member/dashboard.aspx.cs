using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!UserSession.Inst.IsUserLoggedIn)
            {
                Response.Redirect("/login.aspx");
            }

            if (UserSession.Inst.Member2MemberPK <= 0)
            {
                if (UserSession.Inst.MemberPK > 0)
                {
                    Response.Redirect("/member/dashboard.aspx");
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    Response.Redirect("/church/dashboard.aspx");
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    Response.Redirect("/raffle/dashboard.aspx");
                }
                else if (UserSession.Inst.UserPK > 0 && UserSession.Inst.UserType == "ADMIN")
                {
                    Response.Redirect("/admin/dashboard.aspx");
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

            if (UserSession.Inst.UserType == "MEMBER2MEMBER")
            {
                UserSession.Inst.RaffleObj = null;
                UserSession.Inst.ChurchObj = null;
                UserSession.Inst.MemberObj = null;

                UserSession.Inst.MemberPK = _controller.GetDistUserObj(UserSession.Inst.Member2MemberPK).CreatedBy_UserFk;
                UserSession.Inst.ChurchPK = _controller.GetDistUserObj(UserSession.Inst.MemberPK).CreatedBy_UserFk;
                UserSession.Inst.RafflePK = _controller.GetDistUserObj(UserSession.Inst.ChurchPK).CreatedBy_UserFk;
            }
            
            
            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "User");
            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({0})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({1})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "User");
            }
            else if (UserSession.Inst.UserType == "CHURCH")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({0})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.MemberObj.name, "User");
            }
            else if (UserSession.Inst.UserType == "MEMBER")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", UserSession.Inst.MemberObj.name);
            }
            else
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member2member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member2member/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", UserSession.Inst.Member2MemberObj.name);
            }
        }
    }
        

}