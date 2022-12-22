using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class RaffleRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }


        if (!IsPostBack)
        {

            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/Admin/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/Admin/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Raffle Registration");

            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                hdnUserId.Value = Request.QueryString["id"];

                RaffleController _provider = new RaffleController();

                DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(Request.QueryString["id"]));
                if (objDistUser != null)
                {
                    TicketsDistribution TicketDistObj = _provider.GetTicketDistObj(Convert.ToInt32(objDistUser.TicketDist_Fk));
                    if (TicketDistObj != null)
                    {
                        txtUserName.Text = objDistUser.name;
                        txtEmail.Text = objDistUser.email;
                        txtEmail.Enabled = true;
                        txtLoginUserName.Text = objDistUser.UserName;
                        txtPhone.Text = objDistUser.phone;
                        txtCity.Text = objDistUser.City;
                        txtState.Text = objDistUser.State;
                        txtZip.Text = objDistUser.Zip;
                        txtTotTicket.Text = TicketDistObj.TotalTickets.ToString();
                        txtTicketStart.Text = TicketDistObj.FromTicket.ToString();
                        txtTicketEnd.Text = TicketDistObj.TotalTickets.ToString();
                        txtPassword.Text = objDistUser.password;
                        //txtMobileNo.Text = objDistUser.mobile;
                        txtAdd.Text = objDistUser.Address;
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
        //if (Request.QueryString["db"] != null)
        //{
            Response.Redirect("/admin/dashboard.aspx");
        //}
        //else
        //{
        //    Response.Redirect("/admin/RaffleList.aspx");
        //}

    }

    protected void btnBack_click(object sender, EventArgs e)
    {
        //if (Request.QueryString["db"] != null)
        //{
            Response.Redirect("/admin/dashboard.aspx");
        //}
        //else
        //{
        //    Response.Redirect("/admin/RaffleList.aspx");
        //}

    }

    protected void btnSave_click(object sernder, EventArgs e)
    {

        RaffleController _provider = new RaffleController();
        UserController _controller = new UserController();

        if (!string.IsNullOrEmpty(hdnUserId.Value))
        {

            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(hdnUserId.Value));
            if (objDistUser != null)
            {
                objDistUser.LastaccessUserId = UserSession.Inst.UserPK;
                objDistUser.name = txtUserName.Text;
                objDistUser.phone = txtPhone.Text;
                objDistUser.email = txtEmail.Text;
                objDistUser.Address = txtAdd.Text;
                objDistUser.City = txtCity.Text;
                objDistUser.State = txtState.Text;
                objDistUser.Zip = txtZip.Text;
                int result = _controller.UpdateUser(objDistUser);

                if (result > 0)
                {
                    //if (Request.QueryString["db"] != null)
                    //{
                        Response.Redirect("/admin/dashboard.aspx");
                    //}
                    //else
                    //{
                    //    Response.Redirect("/admin/RaffleList.aspx");
                    //}
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
            userObj.UserType = "RAFFLE";
            userObj.UserName = txtLoginUserName.Text;
            userObj.password = txtPassword.Text;
            userObj.name = txtUserName.Text;
            userObj.createdDate = DateTime.Now;
            userObj.modifiedDate = DateTime.Now;
            userObj.LastaccessUserId = UserSession.Inst.UserPK;
            userObj.CreatedBy_UserFk = UserSession.Inst.UserPK;
            userObj.parent_userFk = UserSession.Inst.UserPK;
            userObj.TicketRate = 25;

            TicketsDistribution distObj = new TicketsDistribution();
            distObj.CreatedDate = DateTime.Now;
            distObj.ModifiedDate = DateTime.Now;
            distObj.LastAccessUserID = UserSession.Inst.UserPK;
            distObj.FromTicket = txtTicketStart.Text;
            distObj.ToTicket = txtTicketEnd.Text;
            distObj.TotalTickets = Convert.ToInt32(Convert.ToInt32(distObj.ToTicket) - Convert.ToInt32(distObj.FromTicket) + 1);
            userObj.Balance = distObj.TotalTickets;

            int result = _controller.AddUser(userObj, distObj);
            if (result > 0)
            {
                //if (Request.QueryString["db"] != null)
                //{
                    Response.Redirect("/admin/dashboard.aspx");
                //}
                //else
                //{
                //    Response.Redirect("/admin/RaffleList.aspx");
                //}
            }

            //Utilities.ShowSuccessMessage("User created successfully", "Register user information", Page, "/UserList.aspx");



        }

    }
}