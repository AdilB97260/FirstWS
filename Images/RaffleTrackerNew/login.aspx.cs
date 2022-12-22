using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.IO;
using System.Net;

public partial class login : System.Web.UI.Page
{

    #region [GLOABAL DECALRATION]

    UserController _usercontroller;
    DIST_USERS _objUser;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
                if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                {
                    Response.Redirect("/ReportViewer.aspx");
                }

                else if (UserSession.Inst.IsUserLoggedIn)
                {
                    if (UserSession.Inst.UserType == "ADMIN")
                    {
                        Response.Redirect("/admin/NewDashBoard.aspx");
                    }
                    else if (UserSession.Inst.UserType == "RAFFLE")
                    {
                        Response.Redirect("/raffle/NewDashBoard.aspx");
                    }
                    else if (UserSession.Inst.UserType == "CHURCH")
                    {
                        Response.Redirect("/church/NewDashBoard.aspx");
                    }
                    else if (UserSession.Inst.UserType == "MEMBER")
                    {
                        Response.Redirect("/member/NewDashBoard.aspx");
                    }
                    else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                    {
                        Response.Redirect("/member2member/NewDashBoard.aspx");
                    }
                }
        }

            divError.Visible = false;
        }
    
        
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            _usercontroller = new UserController();
            _objUser = _usercontroller.GetAdminLogin(txtUsrname.Text, txtPassword.Text);

            User _userObj = _usercontroller.GetSystemUserLogin(txtUsrname.Text, txtPassword.Text);

            if (_userObj != null && _userObj.DistUserType == "REPORTVIEW")
            {
                UserSession.Inst.Login(Convert.ToInt32(_userObj.DistUser_Fk), _userObj.FullName, _userObj.UserType, 0, _userObj.username, _userObj.user_pk);
                UserSession.Inst.SystemUserFK = _userObj.user_pk;
                UserSession.Inst.SystemUserRole = _userObj.DistUserType;
                UserSession.Inst.IsSystemUser = true;
                UserSession.Inst.UserType = _userObj.UserType;

                Response.Redirect("/ReportViewer.aspx");
            }
            

            if (_objUser != null)
            {

                //int ss= _usercontroller.GetSystemUserList().Where(x => x.DistUser_Fk == 0).FirstOrDefault().user_pk;

                int userLoginFk = _usercontroller.GetSystemUserList().Where(x => x.DistUser_Fk == _objUser.user_pk).FirstOrDefault() !=null ?   _usercontroller.GetSystemUserList().Where(x => x.DistUser_Fk == _objUser.user_pk).FirstOrDefault().user_pk : 0;

                UserSession.Inst.Login(_objUser.user_pk, _objUser.name, _objUser.UserType, Convert.ToInt32(_objUser.TicketDist_Fk), _objUser.UserName, userLoginFk);
                UserSession.Inst.IsSystemUser = false;
                if (UserSession.Inst.UserType == "ADMIN")
                {
                    Response.Redirect("/Admin/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "RAFFLE")
                {
                    UserSession.Inst.RafflePK = _objUser.user_pk;
                    Response.Redirect("/Raffle/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    UserSession.Inst.ChurchPK = _objUser.user_pk;
                    Response.Redirect("/Church/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    UserSession.Inst.MemberPK = _objUser.user_pk;
                    Response.Redirect("/Member/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                {
                    UserSession.Inst.Member2MemberPK = _objUser.user_pk;
                    Response.Redirect("/member2member/NewDashBoard.aspx");
                }
            }

            else if (_userObj != null)
            {
                UserSession.Inst.Login(Convert.ToInt32(_userObj.DistUser_Fk), _userObj.FullName, _userObj.UserType, 0, _userObj.username, _userObj.user_pk);
                UserSession.Inst.SystemUserFK = _userObj.user_pk;
                UserSession.Inst.SystemUserRole = _userObj.DistUserType;
                UserSession.Inst.IsSystemUser = true;
                UserSession.Inst.UserType = _userObj.UserType;

                if (UserSession.Inst.UserType == "ADMIN")
                {
                    UserSession.Inst.UserPK = Convert.ToInt32(_userObj.DistUser_Fk);
                    Response.Redirect("/admin/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "RAFFLE")
                {
                    UserSession.Inst.RafflePK = Convert.ToInt32(_userObj.DistUser_Fk);
                    Response.Redirect("/raffle/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    UserSession.Inst.ChurchPK = Convert.ToInt32(_userObj.DistUser_Fk);
                    Response.Redirect("/church/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    UserSession.Inst.MemberPK = Convert.ToInt32(_userObj.DistUser_Fk);
                    Response.Redirect("/member/NewDashBoard.aspx");
                }
                else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                {
                    UserSession.Inst.Member2MemberPK = Convert.ToInt32(_userObj.DistUser_Fk);
                    Response.Redirect("/member2member/NewDashBoard.aspx");
                }

            }
            else
            {
                divError.Visible = true;
                lblErrorMsg.Text = "UserName or Password are incorrect";
                //Utilities.ShowMessage("Username or Password are incorrect", "Wrong Username Or Password !", Page);
            }
        }
   }
    
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception Ex = Server.GetLastError();
        Server.ClearError();
        Response.Redirect("ErrorPage.aspx");
    }

    protected void downloadPermit_click(object sender, EventArgs e)
    {
        string strURL = Server.MapPath("/pdf/2020-Raffle-Permit.pdf");
        FileInfo file = new FileInfo(strURL);
        if (file.Exists)
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + "2020-Raffle-Permit.pdf");
            Response.ContentType = "application/octet-stream";
            //Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            //Response.AddHeader("Content-Length", file.Length.ToString());
            //Response.ContentType = "application/vnd.ms-excel";
            Response.TransmitFile(file.FullName);
            Response.End();

            //string pdfPath = Server.MapPath("~/pdf/2019-Raffle-Permit.pdf");
            //WebClient client = new WebClient();
            //Byte[] buffer = client.DownloadData(pdfPath);
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-length", buffer.Length.ToString());
            //Response.BinaryWrite(buffer);
        }
    }


}