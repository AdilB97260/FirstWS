using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Text;


public partial class Raffle_EditRaffle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }


        if (!IsPostBack)
        {

            //if (UserSession.Inst.UserType == "RAFFLE")
            //{
            //    btnAddTicket.Visible = false;
            //}


            RaffleController _provider = new RaffleController();
            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.RafflePK));
            if (objDistUser != null)
            {


                if (UserSession.Inst.UserType == "ADMIN")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle</a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", objDistUser.name);
                }
                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/Raffle/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/Raffle/Dashboard.aspx' class='btn btn-primary'>Raffle</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", UserSession.Inst.UserName);
                }

                txtEmail.Text = objDistUser.email;
                txtAdd.Text = objDistUser.Address;
                txtState.Text = objDistUser.State;
                txtCity.Text = objDistUser.City;
                txtZip.Text = objDistUser.Zip;
                txtPhone.Text = objDistUser.phone;
                txtUserName.Text = objDistUser.name;
                txtLogin.Text = objDistUser.UserName;
                txtPassword.Text = objDistUser.password;

                RaffleController _controller = new RaffleController();
                List<TicketsDistribution> distList = _controller.GetTicketDistributionList().Where(x => x.Dist_user_Fk == objDistUser.user_pk).OrderBy(x => x.CreatedDate).ToList();
                rptDistribution.DataSource = distList;
                rptDistribution.DataBind();

                lblTotTick.Text = distList.Sum(x => x.TotalTickets).ToString();


                List<TicketsDistribution> distChurchList = _controller.GetChurchTickeDistributionByRaffle().OrderBy(x => x.CreatedDate).ToList();
                rptChurch.DataSource = distChurchList;
                rptChurch.DataBind();

                lblTotChurch.Text = distChurchList.Sum(x => x.TotalTickets).ToString();



                List<TicketSold> _saleList = _controller.GetTicketSoldListByRaffle(objDistUser.user_pk).OrderByDescending(x => x.CreatedDate).ToList();

                rptSaleList.DataSource = _saleList;
                rptSaleList.DataBind();

                lblSoldTicket.Text = _saleList.Sum(x => x.TicketTotal).ToString();
                lblSoldAmt.Text = _saleList.Sum(x => x.Amount).ToString();


            }
            else
            {
                Response.Redirect("/admin/dashboard.aspx");

            }
        }
    }

    protected void btncancel_click(object sender, EventArgs e)
    {
       
            Response.Redirect("/raffle/dashboard.aspx");
        
    }

   
    protected void btnSave_click(object sernder, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/raffle/dashboard.aspx");
            return;
        }
        
        RaffleController _provider = new RaffleController();
        UserController _controller = new UserController();

        DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.RafflePK));

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
            objDistUser.UserName = txtLogin.Text;
            objDistUser.password = txtPassword.Text;

            int result = _controller.UpdateUser(objDistUser);

            if (result > 0)
            {
                UserSession.Inst.RaffleObj = null;

                if (UserSession.Inst.UserType == "RAFFLE")
                {
                    Response.Redirect("/raffle/dashboard.aspx");
                }
                else
                {
                    Response.Redirect("/admin/dashboard.aspx");
                }
            }
        }

        else
        {
            if (UserSession.Inst.UserType == "RAFFLE")
            {
                Response.Redirect("/raffle/dashboard.aspx");
            }
            else
            {
                Response.Redirect("/admin/dashboard.aspx");
            }
        }

    }


    protected void lnkEdit_click(object sender, EventArgs e)
    {
        int distFke = Convert.ToInt32(((LinkButton)(sender)).CommandArgument);

        RaffleController _controller = new RaffleController();

        TicketsDistribution objTicketDist = _controller.GetTicketDistObj(distFke);

        if (objTicketDist != null)
        {
            txtTicketStart.Text = objTicketDist.FromTicket;
            txtTicketEnd.Text = objTicketDist.ToTicket;
            txtTotTicket.Text = objTicketDist.TotalTickets.ToString();
            hdnDistFkey.Value = distFke.ToString();
            Popup(true);
        }
    }


    void Popup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowEditPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowEditPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HideEditPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HideEditPopup", builder.ToString());
        }
    }

    protected void lnkRestrobition_click(object sender, EventArgs e)
    {
        UserSession.Inst.RafflePK = Convert.ToInt32(((LinkButton)(sender)).CommandArgument);
        Response.Redirect("/Raffle/EditRaffle.aspx?id=" + UserSession.Inst.RafflePK);
        
    }


    //[WebMethod]
    //public static string DeleteDistribution(string tid)
    //{
    //    string result = string.Empty;
    //    RaffleController _controller = new RaffleController();

    //    TicketsDistribution objTickDist = _controller.GetTicketDistObj(Convert.ToInt32(tid));

    //    bool isTrans = false;

    //    if (objTickDist != null)
    //    {
    //        isTrans = _controller.IsTicketSold(Convert.ToInt32(objTickDist.FromTicket), Convert.ToInt32(objTickDist.ToTicket), objTickDist.Dist_user_Fk);

    //        if (!isTrans)
    //        {
    //            isTrans = _controller.IsTicketDistribution(Convert.ToInt32(objTickDist.FromTicket), Convert.ToInt32(objTickDist.ToTicket), objTickDist.Dist_user_Fk);
    //            if (isTrans)
    //            {
    //                return "false";
    //            }
    //        }
    //        else
    //        {
    //            return "false";
    //        }

    //    }

    //    int ret = _controller.DeleteTicketDistribution(Convert.ToInt32(tid));
    //    result = ret > 0 ? "true" : "false";
    //    return result;
    //}


    [WebMethod]
    public static string DeleteDistribution(string tid)
    {
        string result = string.Empty;
        RaffleController _controller = new RaffleController();

        TicketsDistribution objTickDist = _controller.GetTicketDistObj(Convert.ToInt32(tid));

        bool isTrans = true;

        if (objTickDist != null)
        {

            isTrans = _controller.DeleteTicketDistribution(Convert.ToInt32(objTickDist.FromTicket), Convert.ToInt32(objTickDist.ToTicket), objTickDist);
            return isTrans.ToString().ToLower();
        }
        else
        {
            return "false";
        }

    }


    [WebMethod]
    public static string DeleteSale(string tid)
    {
        string result = string.Empty;
        RaffleController _controller = new RaffleController();
        int ret = _controller.DeleteSale(Convert.ToInt32(tid));
        result = ret > 0 ? "true" : "false";
        return result;
    }


    [WebMethod]
    public static string DeleteUser(string tid)
    {
        string result = string.Empty;
        RaffleController _controller = new RaffleController();
        int ret = _controller.DeleteUser(Convert.ToInt32(tid));
        result = ret > 0 ? "true" : "false";
        return result;
    }


    [WebMethod]
    public static string EditTicket(int ticketFrom, int toTicket, int TotTicket, int distFkey)
    {
        RaffleController _provider = new RaffleController();
        UserController _controller = new UserController();

        bool canEdit = _provider.IsTicketDistributionOverlapping(Convert.ToInt32(ticketFrom), Convert.ToInt32(toTicket), UserSession.Inst.RafflePK, "RAFFLE");

        if (!canEdit)
        {
            return "0";
        }

        bool isEdit = _provider.UpdateEditTicket(ticketFrom.ToString(), toTicket.ToString(), TotTicket,Convert.ToInt32(distFkey));

        if (isEdit)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
}