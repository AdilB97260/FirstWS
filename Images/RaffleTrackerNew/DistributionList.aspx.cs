using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Web.Script.Serialization;

public partial class DistributionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }
        else if (UserSession.Inst.UserType != "ADMIN")
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
        }

        if (!IsPostBack)
        {
            RaffleController _controller = new RaffleController();

            List<TicketsDistribution> _lstSold = _controller.GetTickeDistributionList().OrderByDescending(x => x.CreatedDate).ToList();

            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "View Ticket Distribution List");

            rptSale.DataSource = _lstSold;
            rptSale.DataBind();


            lblTotalTick.Text = _lstSold.Sum(x => x.TotalTickets).ToString();
            //lblTotAmt.Text = _lstSold.Sum(x => x.Amount).ToString();

        }

    }




    [WebMethod]
    public static string GetSalesData(string tid)
    {
        RaffleController _controller = new RaffleController();
        SalesData objSaleData = new SalesData();

        TicketsDistribution objDist = _controller.GetTicketDistObj(Convert.ToInt32(tid));
        
        
        //TicketSold obj = _controller.GetTicketSoldObj(Convert.ToInt32(tid));

        //objSaleData.CollectionBy = !string.IsNullOrEmpty(obj.Collected_By) ? obj.Collected_By : _controller.GetDistUserObj(obj.LastAccessUserID).name;
        //objSaleData.SoldTo = obj.GivenTo;
        //objSaleData.SoldDate = obj.CreatedDate.ToShortDateString();
        //objSaleData.TicketFrom = obj.TicketFrom.ToString();
        //objSaleData.TicketTo = obj.TicketTo.ToString();

        List<TicketsDistribution> lst = _controller.GetTickDistHistory(Convert.ToInt32(objDist.FromTicket), Convert.ToInt32(objDist.ToTicket));
        if (lst != null && lst.Count > 0)
        {
            objSaleData.TicketDistList = lst;
            objSaleData.LastDistName = lst.OrderByDescending(x => x.CreatedDate).FirstOrDefault().LastDistUserName;
        }


        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(objSaleData);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(txtSearch.Text))
        {

            return;
        }


        RaffleController _controller = new RaffleController();
        List<TicketsDistribution> _lstSold = new List<TicketsDistribution>();

        _lstSold = _controller.GetTickeDistributionList().Where(x => x.FTick <= Convert.ToInt32(txtSearch.Text) && x.TTick >= Convert.ToInt32(txtSearch.Text)).ToList();

        rptSale.DataSource = _lstSold;
        rptSale.DataBind();

        lblTotalTick.Text = _lstSold.Sum(x => x.TotalTickets).ToString();
        //lblTotAmt.Text = _lstSold.Sum(x => x.Amount).ToString();

    }



    protected void btnReset_Click(object sender, EventArgs e)
    {
        RaffleController _controller = new RaffleController();

        List<TicketsDistribution> _lstSold = _lstSold = _controller.GetTickeDistributionList().OrderByDescending(x => x.CreatedDate).ToList();


        //_lstSold = _lstSold.Where(x => x.LastAccessUserID == Utilities.GetAccessedUserKey()).ToList();


        rptSale.DataSource = _lstSold;
        rptSale.DataBind();


        lblTotalTick.Text = _lstSold.Sum(x => x.TotalTickets).ToString();
        //lblTotAmt.Text = _lstSold.Sum(x => x.Amount).ToString();



    }

    public class SalesData
    {
        public string CollectionBy { get; set; }
        public string SoldTo { get; set; }
        public string SoldDate { get; set; }
        public string TicketFrom { get; set; }
        public string TicketTo { get; set; }
        public string LastDistName { get; set; }

        public List<TicketsDistribution> TicketDistList { get; set; }


    }
}



