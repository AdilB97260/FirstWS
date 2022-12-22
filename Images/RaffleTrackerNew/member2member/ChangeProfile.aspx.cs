using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class ChangeProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member2member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a>  <a href='/member2member/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", UserSession.Inst.Member2MemberObj.name);


            RaffleController _provider = new RaffleController();
            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.MemberPK));

            if (objDistUser != null)
            {
                txtUserName.Text = objDistUser.name;
                txtEmail.Text = objDistUser.email;
                txtCity.Text = objDistUser.City;
                txtState.Text = objDistUser.State;
                txtZip.Text = objDistUser.Zip;
                txtPhone.Text = objDistUser.phone;
                txtAdd.Text = objDistUser.Address;
                txtUserName.Text = objDistUser.UserName;
            }
        }


    }


    protected void btnSave_click(object sernder, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/member2member/dashboard.aspx");
            return;
        }
        
        UserController _controller = new UserController();
        RaffleController _provider = new RaffleController();

         DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.MemberPK));
         if (objDistUser != null)
         {
             objDistUser.email = txtEmail.Text;
             objDistUser.LastaccessUserId = UserSession.Inst.UserPK;
             objDistUser.name = txtUserName.Text;
             objDistUser.phone = txtPhone.Text;
             objDistUser.Address = txtAdd.Text;
             objDistUser.City = txtCity.Text;
             objDistUser.State = txtState.Text;
             objDistUser.Zip = txtZip.Text;

             int result = _controller.UpdateUser(objDistUser);

             if (result > 0)
             {
                 divError.Visible = false;
                 divMsg.Visible = true;
                 lblMsg.Text = "Profile information changed successfully !";
             }
             else
             {

                 divError.Visible = true;
                 lblErrorMsg.Text = "Profile information saved failed ! ";
             }

         }


    }

    protected void btnSavePass_click(object sernder, EventArgs e)
    {
        UserController _controller = new UserController();
        RaffleController _provider = new RaffleController();


        divSuccpass.Visible = false;
        lblSuccPass.Text = "";
        divError.Visible = false;
        divMsg.Visible = false;
        lblErrPass.Visible = false;
        if (txtNewPass.Text != txtConfirmPass.Text)
        {
            divErrPass.Visible = true;
            lblErrPass.Visible = true;
            lblErrPass.Text = "Confirm password does not match with new password !";
            return;
        }

        if (_controller.GetAdminLogin(txtEmail.Text, txtOldPassword.Text) != null)
        {
            _controller.UpdatePassword(txtConfirmPass.Text, UserSession.Inst.RafflePK);

            lblErrorMsg.Text = "";
            lblErrPass.Text = "";
            divError.Visible = false;
            divErrPass.Visible = false;
            divSuccpass.Visible = true;
            lblErrPass.Visible = false;
            lblSuccPass.Text = "Password changed successfully ! ";
        }
        else
        {
            divErrPass.Visible = true;
            lblErrPass.Visible = true;
            lblErrPass.Text = "Old password is wrong ! Please enter correct password";

        }

    }





    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("/member2member/dashboard.aspx");

    }
}