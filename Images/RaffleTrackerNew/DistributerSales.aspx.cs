using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using RaffleModel;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class DistributerSales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {





            if (UserSession.Inst.Member2MemberPK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "View Ticket Sale List");

                hdnRate.Value = UserSession.Inst.ChurchObj.TicketRate.HasValue ? UserSession.Inst.ChurchObj.TicketRate.Value.ToString() : "25";

            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "View Ticket Sale List");
                hdnRate.Value = UserSession.Inst.ChurchObj.TicketRate.HasValue ? UserSession.Inst.ChurchObj.TicketRate.Value.ToString() : "25";
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "View Ticket Sale List");
                hdnRate.Value = UserSession.Inst.ChurchObj.TicketRate.HasValue ? UserSession.Inst.ChurchObj.TicketRate.Value.ToString() : "25";
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "View Ticket Sale List");
                hdnRate.Value = UserSession.Inst.RaffleObj.TicketRate.HasValue ? UserSession.Inst.RaffleObj.TicketRate.Value.ToString() : "25";
            }
            else
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Ticket Sales Entry");
                hdnRate.Value = "25";
            }


            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {

                RaffleController _provider = new RaffleController();

                TicketSold objSold = _provider.GetTicketSoldList().Where(x => x.TicketSold_fk == Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
                if (objSold != null)
                {
                    txtGiveTo.Text = objSold.GivenTo;
                    txtEmail.Text = objSold.Email;
                    txtPhone.Text = objSold.Phone;
                    txtTotTicket.Text = objSold.TicketTotal.ToString();
                    txtTicketStart.Text = objSold.TicketFrom.ToString();
                    txtTicketEnd.Text = objSold.TicketTo.ToString();
                    txtAdd.Text = objSold.Address;
                    txtAmount.Text = Convert.ToInt32(objSold.Amount).ToString();
                    txtState.Text = objSold.State;
                    txtCity.Text = objSold.City;
                    txtZip.Text = objSold.Zip;
                }
            }
        }
    }

    protected void btncancel_click(object sender, EventArgs e)
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

    protected void btnSave_click(object sender, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/SaleList.aspx");
            return;
        }



        TicketSold objSold = new TicketSold();
        RaffleController _controller = new RaffleController();

        lblErrorMsg.Text = "";
        divError.Visible = false;

        if (Request.QueryString != null && Request.QueryString["id"] != null)
        {


            if (_controller.IsTicketSold(Convert.ToInt32(txtTicketStart.Text), Convert.ToInt32(txtTicketEnd.Text), Convert.ToInt32(Request.QueryString["id"])))
            {
                lblErrorMsg.Text = "Ticket has already been sold by someone !";
                divError.Visible = true;
                return;
            }

            objSold = _controller.GetTicketSoldList().Where(X => X.TicketSold_fk == Convert.ToInt32(Request.QueryString["id"])).FirstOrDefault();
            if (objSold == null)
            {
                objSold = new TicketSold();
            }





            objSold.GivenTo = txtGiveTo.Text;
            objSold.Collected_By = UserSession.Inst.Name;
            objSold.CreatedDate = DateTime.Now;
            objSold.Email = txtEmail.Text;
            objSold.LastDistUserId = Convert.ToInt32(hdnLastDist.Value);
            objSold.LastDistUserName = _controller.DistUserName(Convert.ToInt32(hdnLastDist.Value));

            if (UserSession.Inst.UserType == "CHURCH")
            {
                objSold.LastAccessUserID = UserSession.Inst.ChurchPK;
                objSold.LastAccessUserName = UserSession.Inst.ChurchObj.name;
            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                objSold.LastAccessUserID = UserSession.Inst.RafflePK;
                objSold.LastAccessUserName = UserSession.Inst.RaffleObj.name;
            }
            else if (UserSession.Inst.UserType == "MEMBER")
            {
                objSold.LastAccessUserID = UserSession.Inst.MemberPK;
                objSold.LastAccessUserName = UserSession.Inst.MemberObj.name;
            }
            else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
            {
                objSold.LastAccessUserID = UserSession.Inst.Member2MemberPK;
                objSold.LastAccessUserName = UserSession.Inst.Member2MemberObj.name;
            }
            else if (UserSession.Inst.UserType == "ADMIN")
            {
                if (UserSession.Inst.Member2MemberPK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.Member2MemberPK;
                    objSold.LastAccessUserName = UserSession.Inst.Member2MemberObj.name;
                }
                else if (UserSession.Inst.MemberPK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.MemberPK;
                    objSold.LastAccessUserName = UserSession.Inst.MemberObj.name;
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.ChurchPK;
                    objSold.LastAccessUserName = UserSession.Inst.ChurchObj.name;
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.RafflePK;
                    objSold.LastAccessUserName = UserSession.Inst.RaffleObj.name;
                }
                //else
                //{
                //    objSold.LastAccessUserID = Convert.ToInt32(objSold.LastDistUserId);
                //    objSold.LastAccessUserName = objSold.LastDistUserName;
            }


            if (UserSession.Inst.Member2MemberPK > 0)
            {
                objSold.MEM2MEM_FK = UserSession.Inst.Member2MemberPK;
            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                objSold.MEMBER_FK = UserSession.Inst.MemberPK;
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                objSold.CHURCH_FK = UserSession.Inst.ChurchPK;
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                objSold.RAFFLE_FK = UserSession.Inst.RafflePK;
            }




            DIST_USERS objD = _controller.GetDistUserObj(Convert.ToInt32(objSold.LastDistUserId));

            if (objD != null)
            {
                if (objD.UserType == "RAFFLE")
                {
                    objSold.RAFFLE_FK = objD.user_pk;
                }
                else if (objD.UserType == "CHURCH")
                {
                    objSold.CHURCH_FK = objD.user_pk;
                    DIST_USERS objD1 = _controller.GetDistUserObj(Convert.ToInt32(objD.CreatedBy_UserFk));
                    if (objD1 != null)
                    {
                        objSold.RAFFLE_FK = objD1.user_pk;
                    }
                }
                else if (objD.UserType == "MEMBER")
                {
                    objSold.MEMBER_FK = objD.user_pk;

                    DIST_USERS objD1 = _controller.GetDistUserObj(Convert.ToInt32(objD.CreatedBy_UserFk));
                    objSold.CHURCH_FK = objD1.user_pk;

                    DIST_USERS objD0 = _controller.GetDistUserObj(Convert.ToInt32(objD1.CreatedBy_UserFk));
                    objSold.RAFFLE_FK = objD0.user_pk;
                }
                else if (objD.UserType == "MEMBER2MEMBER")
                {
                    objSold.MEM2MEM_FK = objD.user_pk;

                    DIST_USERS objD2 = _controller.GetDistUserObj(Convert.ToInt32(objD.CreatedBy_UserFk));
                    objSold.MEMBER_FK = objD2.user_pk;

                    DIST_USERS objD1 = _controller.GetDistUserObj(Convert.ToInt32(objD2.CreatedBy_UserFk));
                    objSold.CHURCH_FK = objD1.user_pk;

                    DIST_USERS objD0 = _controller.GetDistUserObj(Convert.ToInt32(objD1.CreatedBy_UserFk));
                    objSold.RAFFLE_FK = objD0.user_pk;
                }
                //}
            }


            objSold.Phone = txtPhone.Text;
            objSold.TicketFrom = Convert.ToInt32(txtTicketStart.Text);
            objSold.TicketTo = Convert.ToInt32(txtTicketEnd.Text);
            objSold.TicketTotal = Convert.ToInt32(objSold.TicketTo - objSold.TicketFrom + 1);

            if (objD != null && objD.TicketRate != null)
            {
                objSold.Amount = Convert.ToInt32(objSold.TicketTotal * objD.TicketRate);
            }
            else
            {
                objSold.Amount = Convert.ToInt32(objSold.TicketTotal * 25);
            }
            objSold.City = txtCity.Text;
            objSold.State = txtState.Text;
            objSold.Phone = txtPhone.Text;

            int result = _controller.UpdateTicketSold(objSold);

            if (result > 0)
            {
                if (Request.QueryString != null && Request.QueryString["db"] != null)
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
                        Response.Redirect("/admin/dashboard.aspx");
                    }

                }
                else
                {
                    Response.Redirect("/SaleList.aspx");
                }
            }


        }
        else
        {

            if (_controller.IsTicketSold(Convert.ToInt32(txtTicketStart.Text), Convert.ToInt32(txtTicketEnd.Text), 0))
            {
                lblErrorMsg.Text = "Ticket has already been sold by someone !";
                divError.Visible = true;
                return;
            }

            objSold.GivenTo = txtGiveTo.Text;
            objSold.Collected_By = UserSession.Inst.Name;
            objSold.CreatedDate = DateTime.Now;
            objSold.Email = txtEmail.Text;
            objSold.LastDistUserId = Convert.ToInt32(hdnLastDist.Value);
            objSold.LastDistUserName = _controller.DistUserName(Convert.ToInt32(hdnLastDist.Value));

            if (UserSession.Inst.UserType == "CHURCH")
            {
                objSold.LastAccessUserID = UserSession.Inst.ChurchPK;
                objSold.LastAccessUserName = UserSession.Inst.ChurchObj.name;
            }
            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                objSold.LastAccessUserID = UserSession.Inst.RafflePK;
                objSold.LastAccessUserName = UserSession.Inst.RaffleObj.name;
            }
            else if (UserSession.Inst.UserType == "MEMBER")
            {
                objSold.LastAccessUserID = UserSession.Inst.MemberPK;
                objSold.LastAccessUserName = UserSession.Inst.MemberObj.name;
            }
            else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
            {
                objSold.LastAccessUserID = UserSession.Inst.Member2MemberPK;
                objSold.LastAccessUserName = UserSession.Inst.Member2MemberObj.name;
            }
            else if (UserSession.Inst.UserType == "ADMIN")
            {
                if (UserSession.Inst.Member2MemberPK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.Member2MemberPK;
                    objSold.LastAccessUserName = UserSession.Inst.Member2MemberObj.name;
                }
                else if (UserSession.Inst.MemberPK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.MemberPK;
                    objSold.LastAccessUserName = UserSession.Inst.MemberObj.name;
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.ChurchPK;
                    objSold.LastAccessUserName = UserSession.Inst.ChurchObj.name;
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    objSold.LastAccessUserID = UserSession.Inst.RafflePK;
                    objSold.LastAccessUserName = UserSession.Inst.RaffleObj.name;
                }
            }





            if (UserSession.Inst.Member2MemberPK > 0)
            {
                objSold.MEM2MEM_FK = UserSession.Inst.Member2MemberPK;
            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                objSold.MEMBER_FK = UserSession.Inst.MemberPK;
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                objSold.CHURCH_FK = UserSession.Inst.ChurchPK;
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                objSold.RAFFLE_FK = UserSession.Inst.RafflePK;
            }


            DIST_USERS objD = _controller.GetDistUserObj(Convert.ToInt32(objSold.LastDistUserId));

            if (objD != null)
            {
                if (objD.UserType == "RAFFLE")
                {
                    objSold.RAFFLE_FK = objD.user_pk;
                }
                else if (objD.UserType == "CHURCH")
                {
                    objSold.CHURCH_FK = objD.user_pk;
                    DIST_USERS objD1 = _controller.GetDistUserObj(Convert.ToInt32(objD.CreatedBy_UserFk));
                    if (objD1 != null)
                    {
                        objSold.RAFFLE_FK = objD1.user_pk;
                    }
                }
                else if (objD.UserType == "MEMBER")
                {
                    objSold.MEMBER_FK = objD.user_pk;

                    DIST_USERS objD1 = _controller.GetDistUserObj(Convert.ToInt32(objD.CreatedBy_UserFk));
                    objSold.CHURCH_FK = objD1.user_pk;

                    DIST_USERS objD0 = _controller.GetDistUserObj(Convert.ToInt32(objD1.CreatedBy_UserFk));
                    objSold.RAFFLE_FK = objD0.user_pk;
                }
                else if (objD.UserType == "MEMBER2MEMBER")
                {
                    objSold.MEM2MEM_FK = objD.user_pk;

                    DIST_USERS objD2 = _controller.GetDistUserObj(Convert.ToInt32(objD.CreatedBy_UserFk));
                    objSold.MEMBER_FK = objD2.user_pk;

                    DIST_USERS objD1 = _controller.GetDistUserObj(Convert.ToInt32(objD2.CreatedBy_UserFk));
                    objSold.CHURCH_FK = objD1.user_pk;

                    DIST_USERS objD0 = _controller.GetDistUserObj(Convert.ToInt32(objD1.CreatedBy_UserFk));
                    objSold.RAFFLE_FK = objD0.user_pk;

                }
            }


            objSold.Phone = txtPhone.Text;
            objSold.TicketFrom = Convert.ToInt32(txtTicketStart.Text);
            objSold.TicketTo = Convert.ToInt32(txtTicketEnd.Text);
            objSold.TicketTotal = Convert.ToInt32(objSold.TicketTo - objSold.TicketFrom + 1);

            if (objD != null && objD.TicketRate != null)
            {
                objSold.Amount = Convert.ToDecimal(objSold.TicketTotal * objD.TicketRate);
            }
            else
            {
                objSold.Amount = Convert.ToDecimal(objSold.TicketTotal * 25);
            }

            objSold.City = txtCity.Text;
            objSold.State = txtState.Text;
            objSold.Phone = txtPhone.Text;

            int result = _controller.AddTicketSold(objSold);

            if (result > 0)
            {
                if (Request.QueryString != null && Request.QueryString["db"] != null)
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
                        Response.Redirect("/admin/dashboard.aspx");
                    }

                }
                else
                {
                    Response.Redirect("/SaleList.aspx");
                }
            }

        }


    }

    [WebMethod]
    public static string GetDistUser(string ftick, string ttick)
    {
        string result = string.Empty;
        RaffleController _controller = new RaffleController();
        if (!string.IsNullOrEmpty(ftick) && !string.IsNullOrEmpty(ttick))
        {
            TicketsDistribution lstTickDist = _controller.GetLastTickDist(Convert.ToInt32(ftick), Convert.ToInt32(ttick));
            return JsonConvert.SerializeObject(lstTickDist);
        }

        return null;
    }
}