using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class MemberRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (UserSession.Inst.ChurchObj != null)
            //{
            //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/Admin/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/Admin/Dashboard.aspx' class='btn btn-primary'>Admin</a> <a href={0} class='btn btn-primary'>{1}</a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", "/Admin/RaffleList.aspx", "Church", UserSession.Inst.ChurchObj.name);
            //}
            //else if (UserSession.Inst.MemberObj != null)
            //{
            //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='{0}' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='{0}' class='btn btn-primary'>Home</a> <a href={0} class='btn btn-primary'>{1}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", "/Member/Dashboard.aspx", "Member (" + UserSession.Inst.MemberObj.name + ")", "Member Registration");
            //}


            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "Register User");
            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Home</a><a href='/church/dashboard.aspx' class='btn btn-primary'>Church({0})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({1})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "Register User");
            }
            else if (UserSession.Inst.UserType == "CHURCH")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/dashboard.aspx' class='btn btn-primary'>Home</a>   <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({0})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.MemberObj.name, "Register User");
            }
            else if (UserSession.Inst.UserType == "MEMBER")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member/dashboard.aspx' class='btn btn-primary'>Home</a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Register User");
            }


            if (Request.QueryString != null && Request.QueryString["id"] != null && Request.QueryString["id"] != "" && Request.QueryString["uType"] == null)
            {
                RaffleController _provider = new RaffleController();
                DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(Request.QueryString["id"]));
                if (objDistUser != null && objDistUser.UserType == "MEMBER")
                {
                    hdnUserId.Value = Request.QueryString["id"];
                    txtEmail.Enabled = false;
                    TicketsDistribution TicketDistObj = _provider.GetTicketDistObj(Convert.ToInt32(objDistUser.TicketDist_Fk));
                    if (TicketDistObj != null)
                    {
                        txtUserName.Text = objDistUser.name;
                        txtEmail.Text = objDistUser.email;
                        txtCity.Text = objDistUser.City;
                        txtState.Text = objDistUser.State;
                        txtZip.Text = objDistUser.Zip;
                        txtPhone.Text = objDistUser.phone;
                        txtTotTicket.Text = TicketDistObj.TotalTickets.ToString();
                        txtTicketStart.Text = TicketDistObj.FromTicket.ToString();
                        txtTicketEnd.Text = TicketDistObj.TotalTickets.ToString();
                        txtPassword.Text = objDistUser.password;
                        txtLogin.Text = objDistUser.UserName;
                        txtAdd.Text = objDistUser.Address;
                        txtEmail.Enabled = true;
                        divFromTic.Visible = false;
                        divToTic.Visible = false;
                        divTotTic.Visible = false;

                    }
                }
            }
        }
    }


    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("/member/dashboard.aspx");
    }

    protected void btnSave_click(object sernder, EventArgs e)
    {
        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/member/dashboard.aspx");
            return;
        }
        
        UserController _controller = new UserController();
        RaffleController _provider = new RaffleController();



        if (!string.IsNullOrEmpty(hdnUserId.Value))
        {

            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(hdnUserId.Value));
            if (objDistUser != null)
            {
                objDistUser.email = txtEmail.Text;
                objDistUser.LastaccessUserId = UserSession.Inst.MemberPK;
                objDistUser.name = txtUserName.Text;
                objDistUser.phone = txtPhone.Text;
                objDistUser.Address = txtAdd.Text;
                objDistUser.City = txtCity.Text;
                objDistUser.State = txtState.Text;
                objDistUser.Zip = txtZip.Text;
                txtEmail.Enabled = true;
                objDistUser.UserName = txtLogin.Text;

                int result = _controller.UpdateUser(objDistUser);

                if (result > 0)
                {

                    Response.Redirect("/member/dashboard.aspx");

                }
            }
        }
        else
        {

            if (_provider.GetDistUserList().Where(x => !string.IsNullOrEmpty(txtLogin.Text) && x.UserName.ToLower() == txtLogin.Text.ToLower()).Count() > 0)
            {
                lblErrorMsg.Text = "User Name is already exits ! Please enter different User Name for login.";
                divError.Visible = true;
                return;
            }

            DIST_USERS userObj = new DIST_USERS();
            userObj.email = txtEmail.Text;
            userObj.Address = txtAdd.Text;
            userObj.City = txtCity.Text;
            userObj.State = txtState.Text;
            userObj.Zip = txtZip.Text;
            userObj.phone = txtPhone.Text;
            userObj.UserType = "MEMBER2MEMBER";
            userObj.UserName = txtLogin.Text;
            userObj.name = txtUserName.Text;
            userObj.password = txtPassword.Text;
            userObj.RAFFLE_FK = UserSession.Inst.RafflePK;
            userObj.CHURCH_FK = UserSession.Inst.ChurchPK;
            userObj.MEMBER_FK = UserSession.Inst.MemberPK;
            userObj.createdDate = DateTime.Now;
            userObj.modifiedDate = DateTime.Now;

            userObj.LastaccessUserId = UserSession.Inst.MemberPK;
            userObj.CreatedBy_UserFk = UserSession.Inst.MemberPK;
            userObj.parent_userFk = UserSession.Inst.ChurchPK;
            userObj.TicketRate = 25;
            userObj.YearID = UserSession.Inst.CurrentYearID;

            TicketsDistribution distObj = new TicketsDistribution();
            distObj.CreatedDate = DateTime.Now;
            distObj.ModifiedDate = DateTime.Now;
            distObj.LastAccessUserID = UserSession.Inst.MemberPK;
            distObj.FromTicket = txtTicketStart.Text;
            distObj.ToTicket = txtTicketEnd.Text;

            distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
            distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
            distObj.MEMBER_FK= UserSession.Inst.MemberPK;
            distObj.YearID = UserSession.Inst.CurrentYearID;

            distObj.TotalTickets = Convert.ToInt32(Convert.ToInt32(distObj.ToTicket) - Convert.ToInt32(distObj.FromTicket) + 1);
            userObj.Balance = distObj.TotalTickets;

            int result = _controller.AddUser(userObj, distObj);
            if (result > 0)
            {
                Response.Redirect("/member/dashboard.aspx");
            }

            //Utilities.ShowSuccessMessage("User created successfully", "Register user information", Page, "/UserList.aspx");



        }

    }

    protected void btnBack_click(object sender, EventArgs e)
    {
        Response.Redirect("/member/dashboard.aspx");
    }
}