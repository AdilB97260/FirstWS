using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Text;


public partial class Church_EditChurch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        //if (UserSession.Inst.UserType == "CHURCH")
        //{
        //    btnAddTicket.Visible = false;
        //}





        if (!Page.IsPostBack)
        {

            RaffleController _provider = new RaffleController();
            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.ChurchPK));
            if (objDistUser != null)
            {

                if (UserSession.Inst.UserType == "ADMIN")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0})</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "Edit Church information");
                }
                else if (UserSession.Inst.UserType == "RAFFLE")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Home</a><a href='/church/dashboard.aspx' class='btn btn-primary'>Church({0})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", objDistUser.name, "Edit Church information");
                }
                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Edit Church information");
                }


                txtEmail.Text = objDistUser.email;
                txtAdd.Text = objDistUser.Address;
                txtLogin.Text = objDistUser.UserName;
                txtPhone.Text = objDistUser.phone;
                txtUserName.Text = objDistUser.name;
                txtAdminName.Text = objDistUser.ChurchAdminstName;
                txtCity.Text = objDistUser.City;
                txtState.Text = objDistUser.State;
                txtZip.Text = objDistUser.Zip;
                txtPassword.Text = objDistUser.password;
                txtTicketRate.Text = objDistUser.TicketRate.ToString();
                ddlChurchType.SelectedValue = objDistUser.ChurchType;

                RaffleController _controller = new RaffleController();
                List<TicketsDistribution> distList = _controller.GetTicketDistributionList().Where(x => x.Dist_user_Fk == objDistUser.user_pk).OrderBy(x => x.CreatedDate).ToList();
                rptDistribution.DataSource = distList;
                rptDistribution.DataBind();

                lblTotTick.Text = distList.Sum(x => x.TotalTickets).ToString();


                List<TicketsDistribution> distChurchList = _controller.GetTickeDistributionList(objDistUser.user_pk).OrderBy(x => x.CreatedDate).ToList();
                rptChurch.DataSource = distChurchList;
                rptChurch.DataBind();

                lblTotChurch.Text = distChurchList.Sum(x => x.TotalTickets).ToString();



                List<TicketSold> _saleList = _controller.GetTicketSoldList().Where(x => x.CHURCH_FK == objDistUser.user_pk).OrderByDescending(x => x.CreatedDate).ToList();

                rptSaleList.DataSource = _saleList;
                rptSaleList.DataBind();

                lblSoldTicket.Text = _saleList.Sum(x => x.TicketTotal).ToString();
                lblSoldAmt.Text = _saleList.Sum(x => x.Amount).ToString();


            }
            else
            {
                Response.Redirect("/raffle/dashboard.aspx");
            }
        }
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

    protected void btncancel_click(object sender, EventArgs e)
    {

        Response.Redirect("/church/dashboard.aspx");

    }

    protected void btnAddTicket_Click(object sernder, EventArgs e)
    {

        Response.Redirect("/DistributionForm.aspx?id=" + UserSession.Inst.ChurchPK);
    }

    protected void btnSave_click(object sernder, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/church/dashboard.aspx");
            return;
        }
        
        RaffleController _provider = new RaffleController();
        UserController _controller = new UserController();


        DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.ChurchPK));
        if (objDistUser != null)
        {
            decimal previousRate= Convert.ToDecimal(objDistUser.TicketRate);
            objDistUser.name = txtUserName.Text;
            objDistUser.phone = txtPhone.Text;
            objDistUser.email = txtEmail.Text;
            objDistUser.Address = txtAdd.Text;
            objDistUser.City = txtCity.Text;
            objDistUser.State = txtState.Text;
            objDistUser.Zip = txtZip.Text;
            objDistUser.UserName = txtLogin.Text;
            objDistUser.password = txtPassword.Text;
            objDistUser.ChurchAdminstName = txtAdminName.Text;
            objDistUser.TicketRate = Convert.ToDecimal(txtTicketRate.Text);
            objDistUser.ChurchType = ddlChurchType.SelectedValue;

            int result = _controller.UpdateUser(objDistUser);

            if (result > 0)
            {

                if (previousRate != Convert.ToDecimal(txtTicketRate.Text))
                {
                    _controller.UpdateAllChurchMemberTicketRate(objDistUser.user_pk, Convert.ToDecimal(txtTicketRate.Text)); 
                }

                UserSession.Inst.ChurchObj = null;
                Utilities.ShowSuccessMessage("Church information updated successfully ! If you want to update sales by new ticket rate then click on Update Sales as per Ticket button..", "Church information updated sucessfully", Page, "/church/EditChurch.aspx");
                //Response.Redirect("/church/dashboard.aspx");
            }
        }
        else
        {
            Response.Redirect("/raffle/dashboard.aspx");
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
            hdnDistType.Value= Convert.ToInt32(objTicketDist.MEMBER_FK) > 0 ? "MEMBER" : "CHRUCH";
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
        RaffleController _controller = new RaffleController();
        int tickTistFkey = Convert.ToInt32(((LinkButton)(sender)).CommandArgument);

        

        TicketsDistribution objTicketDist = _controller.GetTicketDistObj(tickTistFkey);

        if (objTicketDist != null)
        {
            txtRedesTicketStart.Text = objTicketDist.FromTicket;
            txtResdesTicketEnd.Text = objTicketDist.ToTicket;
            txtResdesTotTicket.Text = objTicketDist.TotalTickets.ToString();

            DIST_USERS obj = _controller.GetDistUserObj(objTicketDist.Dist_user_Fk);

            lblName.Text = obj.name;
            lblPhone.Text = obj.phone;
            
            hdnReDesFkey.Value = tickTistFkey.ToString();
            ResDistrPopup(true);
        }
        



    }


    protected void btnUpdateTicketSale_click(object sender, EventArgs e)
    {
        RaffleController _objController = new RaffleController();
        _objController.UpdateTicketSaleByRate(UserSession.Inst.ChurchPK, Convert.ToDecimal(txtTicketRate.Text));

        Utilities.ShowSuccessMessage("Ticket Rate updated in Sales", "Ticket Rate updated in Sales", Page, "/church/EditChurch.aspx");

    }


    void ResDistrPopup(bool isDisplay)
    {
        StringBuilder builder = new StringBuilder();
        if (isDisplay)
        {
            builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
        }
        else
        {
            builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
        }
    }

    //[WebMethod]
    //public static string DeleteDistribution(string tid)
    //{
    //    string result = string.Empty;
    //    RaffleController _controller = new RaffleController();
    //    int ret = _controller.DeleteTicketDistribution(Convert.ToInt32(tid));
    //    result = ret > 0 ? "true" : "false";
    //    return result;
    //}


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
    public static string EditTicket(int ticketFrom, int toTicket, int TotTicket, int distFkey, string distType)
    {
        RaffleController _provider = new RaffleController();
        UserController _controller = new UserController();

        bool canEdit = _provider.IsTicketDistributionOverlapping(Convert.ToInt32(ticketFrom), Convert.ToInt32(toTicket), UserSession.Inst.ChurchPK, distType);

        if (!canEdit)
        {
            return "0";
        }

        bool isEdit = _provider.UpdateEditTicket(ticketFrom.ToString(), toTicket.ToString(), TotTicket, Convert.ToInt32(distFkey));

        if (isEdit)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }


    [WebMethod]
    public static string ReDistTicket(int ticketFrom, int toTicket, int TotTicket, int distFkey)
    {
        RaffleController _provider = new RaffleController();
        UserController _controller = new UserController();

        TicketsDistribution obj = _provider.GetTicketDistObj(Convert.ToInt32(distFkey));

        if (ticketFrom < Convert.ToInt32(obj.FromTicket) || toTicket > Convert.ToInt32(obj.ToTicket))
        {
            return "0";
        }
        bool isEdit = _provider.UpdateReDestTicket(ticketFrom.ToString(), toTicket.ToString(), TotTicket, Convert.ToInt32(distFkey), UserSession.Inst.ChurchPK, "CHURCH");

        if (isEdit)
        {
            return "1";
        }
        else
        {
            return "-1";
        }
    }


    

}