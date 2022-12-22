using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class ChurchRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", UserSession.Inst.RaffleObj.name);
            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Home</a></div>");
            }




            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                RaffleController _provider = new RaffleController();

                DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(Request.QueryString["id"]));
                if (objDistUser != null && objDistUser.UserType == "CHURCH")
                {
                    hdnUserId.Value = Request.QueryString["id"];

                    TicketsDistribution TicketDistObj = _provider.GetTicketDistObj(Convert.ToInt32(objDistUser.TicketDist_Fk));
                    if (TicketDistObj != null)
                    {
                        txtUserName.Text = objDistUser.name;
                        txtEmail.Text = objDistUser.email;
                        txtLogin.Enabled = false;
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
                        txtChurchAdmName.Text = objDistUser.ChurchAdminstName;
                        divFromTic.Visible = false;
                        divToTic.Visible = false;
                        divTotTic.Visible = false;
                        ddlChurchType.SelectedValue = objDistUser.ChurchType;



                    }
                }
            }
        }
    }


    protected void btncancel_click(object sender, EventArgs e)
    {
        
            Response.Redirect("/raffle/dashboard.aspx");
        


    }

    protected void btnBack_click(object sender, EventArgs e)
    {
        //if (UserSession.Inst.UserType == "ADMIN")
        //{
        //    Response.Redirect("/admin/dashboard.aspx");
        //}
        //else
        //{
        //    Response.Redirect("/raffle/dashboard.aspx");
        //}

        Response.Redirect("/raffle/dashboard.aspx");

    }



    protected void btnSave_click(object sernder, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/raffle/dashboard.aspx");
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
                objDistUser.LastaccessUserId = UserSession.Inst.RafflePK;
                objDistUser.name = txtUserName.Text;
                objDistUser.phone = txtPhone.Text;
                objDistUser.Address = txtAdd.Text;
                objDistUser.City = txtCity.Text;
                objDistUser.State = txtState.Text;
                objDistUser.Zip = txtZip.Text;
                objDistUser.ChurchType = ddlChurchType.SelectedValue;
                objDistUser.ChurchAdminstName = txtChurchAdmName.Text;

                int result = _controller.UpdateUser(objDistUser);

                if (result > 0)
                {
                    Response.Redirect("/raffle/dashboard.aspx");
                }
            }
        }
        else
        {
            if (_provider.GetDistUserList().Where(x => x.UserName.ToLower() == txtUserName.Text.ToLower() && !string.IsNullOrEmpty(txtUserName.Text)).Count() > 0)
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
            userObj.UserType = "CHURCH";
            userObj.UserName = txtLogin.Text;
            userObj.password = txtPassword.Text;
            userObj.name = txtUserName.Text;
            userObj.createdDate = DateTime.Now;
            userObj.modifiedDate = DateTime.Now;
            userObj.LastaccessUserId = UserSession.Inst.RafflePK;
            userObj.CreatedBy_UserFk = UserSession.Inst.RafflePK;
            userObj.parent_userFk = UserSession.Inst.RafflePK;
            userObj.ChurchAdminstName = txtChurchAdmName.Text;
            userObj.RAFFLE_FK = UserSession.Inst.RafflePK;
            userObj.YearID = UserSession.Inst.CurrentYearID;

            userObj.ChurchType = ddlChurchType.SelectedValue;
            if (userObj.ChurchType == "PRIMARY")
            {
                userObj.TicketRate = 25;
            }
            else
            {
                userObj.TicketRate = ((decimal)12.50);
            }

            TicketsDistribution distObj = new TicketsDistribution();
            distObj.CreatedDate = DateTime.Now;
            distObj.ModifiedDate = DateTime.Now;
            distObj.LastAccessUserID = UserSession.Inst.RafflePK;
            distObj.FromTicket = txtTicketStart.Text;
            distObj.ToTicket = txtTicketEnd.Text;
            distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
            distObj.TotalTickets = Convert.ToInt32(Convert.ToInt32(distObj.ToTicket) - Convert.ToInt32(distObj.FromTicket) + 1);
            userObj.Balance = distObj.TotalTickets;
            distObj.YearID = UserSession.Inst.CurrentYearID;
            int result = _controller.AddUser(userObj, distObj);

            if (result > 0)
            {
                Response.Redirect("/raffle/dashboard.aspx");
            }

            //Utilities.ShowSuccessMessage("User created successfully", "Register user information", Page, "/UserList.aspx");



        }



    }
}