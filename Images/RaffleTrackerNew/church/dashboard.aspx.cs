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

            if (UserSession.Inst.ChurchPK <= 0)
            {
                if (UserSession.Inst.RafflePK > 0)
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
           
        RaffleController _controller = new RaffleController();
        
        if (!IsPostBack)
        {

            if (UserSession.Inst.UserType == "CHURCH")
            {
                UserSession.Inst.RaffleObj=null;
                UserSession.Inst.RafflePK = _controller.GetDistUserObj(UserSession.Inst.ChurchPK).CreatedBy_UserFk;
            }
            
            
            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Church");
            }
            else
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Home");
            }

         

            ddlRaffleList.DataTextField = "name";
            ddlRaffleList.DataValueField = "user_pk";
            ddlRaffleList.DataSource = _controller.GetDistUserList().Where(x => x.UserType == "MEMBER" && x.CreatedBy_UserFk == UserSession.Inst.ChurchPK).OrderBy(x => x.name);
            ddlRaffleList.DataBind();
            ddlRaffleList.Items.Insert(0, new ListItem{Text="Select Member",Value="0", Selected=true});

            UserSession.Inst.Member2MemberPK = 0;
            UserSession.Inst.Member2MemberObj = null;
            UserSession.Inst.MemberPK = 0;
            UserSession.Inst.MemberObj = null;

        }
    }


    protected void ddlRaffleList_selectedIndexChanged(object sender, EventArgs e)
    {

        if (Convert.ToInt32(ddlRaffleList.SelectedValue) > 0)
        {
            UserSession.Inst.MemberObj = null;
            UserSession.Inst.MemberPK = Convert.ToInt32(ddlRaffleList.SelectedValue);
            Response.Redirect("/member/dashboard.aspx");
        }

    }

}