using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

public partial class UserRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RaffleController _provider = new RaffleController();


            if (UserSession.Inst.UserType == "ADMIN")
            {
                if (UserSession.Inst.MemberPK > 0)
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><a href='/member/dashboard.aspx' class='btn btn-primary'>Church({2})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, UserSession.Inst.MemberObj, "Manage Member Users");

                    ddlRoleType.Items.Add(new ListItem { Text = "Viewer", Value = "VIEWER", Selected = true });

                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, "Manage Church Users");

                    ddlRoleType.Items.Add(new ListItem { Text = "Church Admin", Value = "CHURCHADMIN", Selected = true });
                    ddlRoleType.Items.Add(new ListItem { Text = "User", Value = "USER", Selected = false });
                    ddlRoleType.Items.Add(new ListItem { Text = "Viewer", Value = "VIEWER", Selected = false });
                    ddlRoleType.Items.Add(new ListItem { Text = "Report Viewer Only", Value = "REPORTVIEW", Selected = false });
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "Manage Raffle Users");
                    ddlRoleType.Items.Add(new ListItem { Text = "Raffle Admin", Value = "RAFFLEADMIN", Selected = true });
                    ddlRoleType.Items.Add(new ListItem { Text = "Report Viewer Only", Value = "REPORTVIEW", Selected = false });
                }
                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/Admin/Dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Manage Super Admin Users");
                    ddlRoleType.Items.Add(new ListItem { Text = "Super Admin", Value = "SUPERADMIN", Selected = true });
                    ddlRoleType.Items.Add(new ListItem { Text = "Report Viewer Only", Value = "REPORTVIEW", Selected = false });
                }

            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "Manage Raffle Users");
                ddlRoleType.Items.Add(new ListItem { Text = "Super Admin", Value = "SUPERADMIN", Selected = true });
                ddlRoleType.Items.Add(new ListItem { Text = "Report Viewer Only", Value = "REPORTVIEW", Selected = false });
            }
            else if (UserSession.Inst.UserType == "CHURCH")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, "Manage Church Users");
                ddlRoleType.Items.Add(new ListItem { Text = "Church Admin", Value = "CHURCHADMIN", Selected = true });
                ddlRoleType.Items.Add(new ListItem { Text = "User", Value = "USER", Selected = false });
                ddlRoleType.Items.Add(new ListItem { Text = "Viewer", Value = "VIEWER", Selected = false });
                ddlRoleType.Items.Add(new ListItem { Text = "Report Viewer Only", Value = "REPORTVIEW", Selected = false });
            }
            else if (UserSession.Inst.UserType == "MEMBER")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a> <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a><a href='/member/dashboard.aspx' class='btn btn-primary'>Church({2})</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj, UserSession.Inst.MemberObj, "Manage Member Users");
                ddlRoleType.Items.Add(new ListItem { Text = "Viewer", Value = "VIEWER", Selected = true });
                ddlRoleType.Items.Add(new ListItem { Text = "Report Viewer Only", Value = "REPORTVIEW", Selected = false });
            }

            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                User objUser = _provider.GetUserObj(Convert.ToInt32(Request.QueryString["id"]));
                if (objUser != null)
                {
                    txtName.Text = objUser.FullName;
                    txtEmail.Text = objUser.Email;
                    txtPhone.Text = objUser.userPhone;
                    txtPassword.Text = objUser.password;
                    txtAdd.Text = objUser.UserAddress;
                    txtLogin.Text = objUser.username;
                    ddlRoleType.SelectedValue = objUser.DistUserType;
                    hdnUserId.Value = objUser.user_pk.ToString();


                    List<PagePermission> lstPermission = _provider.GetUserPagePermissionByUserID(objUser.user_pk);

                    if (lstPermission != null)
                    {
                        foreach (PagePermission obj in lstPermission.Where(x => x.PageView == true).ToList())
                        {
                            for (int i = 0; i < lstReport.Items.Count; i++)
                            {
                                if (obj.pageID == lstReport.Items[i].Value)
                                {
                                    lstReport.Items[i].Selected = true;
                                    break;
                                }
                            }
                        }
                    }

                }
            }
        }
    }


    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("/UserList.aspx");

    }

    protected void btnSave_click(object sernder, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/UserRegistration.aspx");
            return;
        }


        //string message = "";
        //foreach (ListItem item in lstReport.Items)
        //{
        //    if (item.Selected)
        //    {
        //        message += item.Text + " " + item.Value + "\\n";
        //    }
        //}
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);


        RaffleController _provider = new RaffleController();

        if (!string.IsNullOrEmpty(hdnUserId.Value) && Convert.ToInt32(hdnUserId.Value) > 0)
        {
            User objUser = _provider.GetUserObj(Convert.ToInt32(Request.QueryString["id"]));
            if (objUser != null)
            {
                if (_provider.IsUserExist(txtLogin.Text.ToLower().Trim(), Convert.ToInt32(objUser.DistUser_Fk), ddlRoleType.SelectedValue == "SUPERADMIN" ? "ADMIN" : ""))
                {
                    divError.Visible = true;
                    lblErrorMsg.Text = "Login User Name is already exists ! Please enter different Login User Name for login to system";
                    return;
                }
            }
        }
        else
        {
            if (_provider.IsUserExist(txtLogin.Text.ToLower().Trim(), 0, ""))
            {
                divError.Visible = true;
                lblErrorMsg.Text = "Login User Name is already exists ! Please enter different Login User Name for login to system";
                return;
            }

        }


        if (!string.IsNullOrEmpty(hdnUserId.Value))
        {
            User userObj = _provider.GetUserObj(Convert.ToInt32(hdnUserId.Value));
            if (userObj != null)
            {
                userObj.Email = txtEmail.Text;
                userObj.UserAddress = txtAdd.Text;
                userObj.userPhone = txtPhone.Text;
                userObj.password = txtPassword.Text;
                userObj.username = txtLogin.Text;
                userObj.FullName = txtName.Text;
                userObj.DistUserType = ddlRoleType.SelectedValue;
                userObj.ModifiedDate = DateTime.Now;

                if (UserSession.Inst.Member2MemberPK > 0)
                {
                    userObj.DistUser_Fk = UserSession.Inst.Member2MemberPK;
                    userObj.UserType = "MEMBER2MEMBER";
                }
                else if (UserSession.Inst.MemberPK > 0)
                {
                    userObj.DistUser_Fk = UserSession.Inst.MemberPK;
                    userObj.UserType = "MEMBER";
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    userObj.DistUser_Fk = UserSession.Inst.ChurchPK;
                    userObj.UserType = "CHURCH";
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    userObj.DistUser_Fk = UserSession.Inst.RafflePK;
                    userObj.UserType = "RAFFLE";
                }
                else if (UserSession.Inst.UserPK > 0)
                {

                    if (userObj.UserType == "ADMIN")
                    {
                        DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(userObj.DistUser_Fk));
                        if (objDistUser != null)
                        {
                            objDistUser.UserName = txtLogin.Text; ;
                            objDistUser.password = txtPassword.Text;
                            objDistUser.name = txtName.Text;
                            objDistUser.Address = txtAdd.Text;
                            objDistUser.phone = txtPhone.Text;
                            objDistUser.email = txtEmail.Text;
                            objDistUser.mobile = txtPhone.Text;
                            objDistUser.modifiedDate = DateTime.Now;
                            UserController _controller = new UserController();
                            _controller.UpdateUser(objDistUser);
                        }
                    }
                }



                List<PagePermission> lstPagePermission = new List<PagePermission>();
                PagePermission objPagePermission = null;

                foreach (ListItem item in lstReport.Items)
                {

                    objPagePermission = new PagePermission();
                    objPagePermission.CreatedDate = DateTime.Now;
                    objPagePermission.ModifiedDate = DateTime.Now;
                    objPagePermission.PageDelete = false;
                    objPagePermission.PageEdit = false;
                    objPagePermission.PageInsert = false;
                    objPagePermission.PageView = item.Selected ? true : false;
                    objPagePermission.pageName = item.Text;
                    objPagePermission.pageID = item.Value;
                    objPagePermission.userID = userObj.user_pk;
                    lstPagePermission.Add(objPagePermission);


                }


                _provider.AddUpdateUserPermission(lstPagePermission);

                int result = _provider.UpdateUser(userObj);

                if (result > 0)
                {
                    Response.Redirect("/UserList.aspx");
                }

            }
        }
        else
        {
            User userObj = new User();
            userObj.Email = txtEmail.Text;
            userObj.UserAddress = txtAdd.Text;
            userObj.userPhone = txtPhone.Text;
            userObj.DistUserType = ddlRoleType.SelectedValue;
            userObj.password = txtPassword.Text;
            userObj.username = txtLogin.Text;
            userObj.FullName = txtName.Text;
            userObj.CreatedDate = DateTime.Now;
            userObj.ModifiedDate = DateTime.Now;

            if (UserSession.Inst.Member2MemberPK > 0)
            {
                userObj.DistUser_Fk = UserSession.Inst.Member2MemberPK;
                userObj.UserType = "MEMBER2MEMBER";
            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                userObj.DistUser_Fk = UserSession.Inst.MemberPK;
                userObj.UserType = "MEMBER";
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                userObj.DistUser_Fk = UserSession.Inst.ChurchPK;
                userObj.UserType = "CHURCH";
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                userObj.DistUser_Fk = UserSession.Inst.RafflePK;
                userObj.UserType = "RAFFLE";
            }
            else if (UserSession.Inst.UserPK > 0)
            {
                userObj.DistUser_Fk = UserSession.Inst.UserPK;
                userObj.UserType = "ADMIN";
            }

            int result = 0;

            if (ddlRoleType.SelectedValue == "SUPERADMIN")
            {
                DIST_USERS objDistUser = new DIST_USERS();

                objDistUser.UserName = txtLogin.Text; ;
                objDistUser.UserType = "ADMIN";
                objDistUser.password = txtPassword.Text;
                objDistUser.name = txtName.Text;
                objDistUser.Address = txtAdd.Text;
                objDistUser.phone = txtPhone.Text;
                objDistUser.parent_userFk = UserSession.Inst.UserPK;
                objDistUser.email = txtEmail.Text;
                objDistUser.mobile = txtPhone.Text;

                objDistUser.createdDate = DateTime.Now;
                objDistUser.modifiedDate = DateTime.Now;
                objDistUser.CreatedBy_UserFk = 1;
                objDistUser.Balance = 25000;
                objDistUser.YearID = UserSession.Inst.CurrentRaffleYearID;
                objDistUser.TicketRate = 25;


                result = _provider.AddDistUser(objDistUser);

                userObj.DistUser_Fk = result;

                result = _provider.AddUser(userObj);



            }
            else
            {
                result = _provider.AddUser(userObj);
            }
            if (result > 0)
            {

                Response.Redirect("/UserList.aspx");
            }
        }

    }


    //[WebMethod]
    //public static string GetUserList(string id)
    //{
    //    if (id == "0")
    //    {
    //        id = "1";
    //    }

    //    RaffleController _controller = new RaffleController();
    //    List<DIST_USERS> userList = _controller.GetDistUserNameList().Where(x => x.UserType == id).ToList();
    //    return JsonConvert.SerializeObject(userList);
    //}

    //[WebMethod]
    //public static string GetUserDetails(string id)
    //{
    //    RaffleController _provider= new RaffleController();
    //    User objUser = _provider.GetUserObj(Convert.ToInt32(id));
    //    JavaScriptSerializer serializer = new JavaScriptSerializer();
    //    return serializer.Serialize(objUser).ToString();
    //}

}