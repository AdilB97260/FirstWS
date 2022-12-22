using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using RaffleModel;

public partial class UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (UserSession.Inst.IsUserLoggedIn)
            {

                UserController _controller = new UserController();

                if (UserSession.Inst.UserType == "ADMIN")
                {
                    if (UserSession.Inst.MemberPK > 0)
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><a href='/member/dashboard.aspx' class='btn btn-primary'>Church({2})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, UserSession.Inst.MemberObj, "Manage Member Users");
                        rptEmpList.DataSource = _controller.GetAllSystemUserList().Where(x => x.UserType == "MEMBER" && x.DistUser_Fk == UserSession.Inst.MemberPK).ToList();
                        rptEmpList.DataBind();
                    }
                    else if (UserSession.Inst.ChurchPK > 0)
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name,UserSession.Inst.ChurchObj, "Manage Church Users");
                        rptEmpList.DataSource = _controller.GetAllSystemUserList().Where(x => x.UserType == "CHURCH" && x.DistUser_Fk == UserSession.Inst.ChurchPK).ToList();
                        rptEmpList.DataBind();
                    }
                    else if (UserSession.Inst.RafflePK > 0)
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "Manage Raffle Users");
                        rptEmpList.DataSource = _controller.GetAllSystemUserList().Where(x => x.UserType == "RAFFLE" && x.DistUser_Fk == UserSession.Inst.RafflePK).ToList();
                        rptEmpList.DataBind();
                    }
                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/Admin/Dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Manage Super Admin Users");

                        RaffleEntities _entities = new RaffleEntities();

                        rptEmpList.DataSource = _controller.GetAdminUserList(UserSession.Inst.UserPK);
                        //rptEmpList.DataSource = _controller.GetSystemUserList().Where(x => x.UserType == "ADMIN" && x.DistUser_Fk == UserSession.Inst.UserPK).ToList();
                        rptEmpList.DataBind();
                    }

                }
                else if (UserSession.Inst.UserType == "RAFFLE")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "Manage Raffle Users");
                    rptEmpList.DataSource = _controller.GetAllSystemUserList().Where(x => x.UserType == "RAFFLE" && x.DistUser_Fk == UserSession.Inst.RafflePK).ToList();
                    rptEmpList.DataBind();
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, "Manage Church Users");
                    rptEmpList.DataSource = _controller.GetAllSystemUserList().Where(x => x.UserType == "CHURCH" && x.DistUser_Fk == UserSession.Inst.ChurchPK).ToList();
                    rptEmpList.DataBind();
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><a href='/member/dashboard.aspx' class='btn btn-primary'>Church({2})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, UserSession.Inst.MemberObj, "Manage Member Users");
                    rptEmpList.DataSource = _controller.GetAllSystemUserList().Where(x => x.UserType == "MEMBER" && x.DistUser_Fk == UserSession.Inst.MemberPK).ToList();
                    rptEmpList.DataBind();
                }
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }
    }

    protected void btnAddUser_Click(object sernder, EventArgs e)
    {
        Response.Redirect("/UserRegistration.aspx");
    }


    protected void btncancel_click(object sernder, EventArgs e)
    {
        if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            Response.Redirect("/member2member/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            Response.Redirect("/Member/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            Response.Redirect("/Church/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "RAFFLE")
        {
            Response.Redirect("/Raffle/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "ADMIN")
        {
            if (UserSession.Inst.Member2MemberPK > 0)
            {
                Response.Redirect("/member2member/dashboard.aspx");
            }
            else if (UserSession.Inst.MemberPK > 0)
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
            else
            {
                Response.Redirect("/admin/Dashboard.aspx");
            }
        }
    }

    [WebMethod]
    public static string DeleteUser(string tid)
    {
        string result = string.Empty;
        RaffleController _controller = new RaffleController();
        int ret = _controller.DeleteSystemUser(Convert.ToInt32(tid));
        result = ret > 0 ? "true" : "false";
        return result;
    }


}