using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using RaffleModel;
using System.Data;

public partial class reports_SalesDateRange : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }


        if (!Page.IsPostBack)
        {

            if (Request.QueryString !=null &&  Request.QueryString["r"] ==null)
            {
                hdnRedirect.Value = "1";
            }

            if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Sales By Date Range Repot");
            }

            else
            {


                if (UserSession.Inst.Member2MemberPK > 0)
                {
                    hdnUserPk.Value = UserSession.Inst.Member2MemberPK.ToString();
                    hdnUserType.Value = UserSession.Inst.UserType;
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "Sales By Date Range Repot");
                }
                else if (UserSession.Inst.MemberPK > 0)
                {
                    hdnUserPk.Value = UserSession.Inst.MemberPK.ToString();
                    hdnUserType.Value = UserSession.Inst.UserType;
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "Sales By Date Range Repot");
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    hdnUserPk.Value = UserSession.Inst.ChurchPK.ToString();
                    hdnUserType.Value = UserSession.Inst.UserType;
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "Sales By Date Range Repot");
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    hdnUserPk.Value = UserSession.Inst.RafflePK.ToString();
                    hdnUserType.Value = UserSession.Inst.UserType;
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "Sales By Date Range Repot");
                }
                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Sales By Date Range Repot");
                }
            }
        }

    }


    [WebMethod]
    public static List<DIST_USERS> GetSalesByData(string sType1)
    {
        RaffleController _controller = new RaffleController();
        List<DIST_USERS> lstDistUser = new List<DIST_USERS>();
        if (sType1 == "RAFFLE")
        {
            lstDistUser = _controller.GetDistUserList(UserSession.Inst.CurrentYearID).Where(x => x.UserType == "RAFFLE").ToList();
        }
        else if (sType1 == "CHURCH")
        {
            lstDistUser = _controller.GetDistUserList(UserSession.Inst.CurrentYearID).Where(x => x.UserType == "CHURCH").ToList();
        }
        else if (sType1 == "MEMBER")
        {
            lstDistUser = _controller.GetDistUserList(UserSession.Inst.CurrentYearID).Where(x => x.UserType == "MEMBER").ToList();
        }
        return lstDistUser;
    }


    [WebMethod]
    public static List<Data> GetData(string dtF, string dtT, string sType1, string sType2, string sType)
    {

        List<Data> dataList = new List<Data>();
        string distUserName = "";
        int ticketTotal = 0;
        decimal amount = 0;

        DateTime fDate = Convert.ToDateTime(dtF);
        DateTime tDate = Convert.ToDateTime(dtT);

        DataSet dsData = new DataSet();
        try
        {
            RaffleController _controller = new RaffleController();

            List<TicketSold> lstData = new List<TicketSold>();
            List<TicketSold> _lst = new List<TicketSold>();

            if (sType1 == "RAFFLE")
            {
                if (UserSession.Inst.UserType == "RAFFLE" && UserSession.Inst.RafflePK > 0)
                {
                    lstData = _controller.GetTicketSoldListSalesBy("RAFFLE", sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate && x.RAFFLE_FK == UserSession.Inst.RafflePK).OrderByDescending(x => x.TicketTotal).ToList();
                }
                else if (UserSession.Inst.UserType == "CHURCH" && UserSession.Inst.ChurchPK > 0)
                {
                    lstData = _controller.GetTicketSoldListSalesBy("CHURCH", sType).Where(x => x.CreatedDate.Date >= fDate.Date && x.CreatedDate.Date <= tDate.Date && x.CHURCH_FK != null && x.CHURCH_FK == UserSession.Inst.ChurchPK).OrderByDescending(x => x.TicketTotal).ToList();
                }
                else if (UserSession.Inst.UserType == "MEMBER" && UserSession.Inst.MemberPK > 0)
                {
                    lstData = _controller.GetTicketSoldListSalesBy("MEMBER", sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate && x.MEMBER_FK != null && x.MEMBER_FK == UserSession.Inst.MemberPK).OrderByDescending(x => x.TicketTotal).ToList();
                }
                else
                {
                    lstData = _controller.GetTicketSoldListSalesBy("RAFFLE", sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate).OrderByDescending(x => x.TicketTotal).ToList();
                }

            }
            else if (sType1 == "CHURCH")
            {

                if (UserSession.Inst.UserType == "CHURCH" && UserSession.Inst.ChurchPK > 0)
                {
                    lstData = _controller.GetTicketSoldListSalesBy("CHURCH", sType).Where(x => x.CreatedDate.Date >= fDate.Date && x.CreatedDate.Date <= tDate.Date && x.CHURCH_FK != null && x.CHURCH_FK == UserSession.Inst.ChurchPK).OrderByDescending(x => x.TicketTotal).ToList();
                }
                else if (UserSession.Inst.UserType == "MEMBER" && UserSession.Inst.MemberPK > 0)
                {
                    lstData = _controller.GetTicketSoldListSalesBy("MEMBER", sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate && x.MEMBER_FK != null && x.MEMBER_FK == UserSession.Inst.MemberPK).OrderByDescending(x => x.TicketTotal).ToList();
                }
                else
                {
                    lstData = _controller.GetTicketSoldListSalesBy("CHURCH", sType).Where(x => x.CreatedDate.Date >= fDate.Date && x.CreatedDate.Date <= tDate.Date && x.CHURCH_FK != null).OrderByDescending(x => x.TicketTotal).ToList();
                }

            }
            else if (sType1 == "MEMBER")
            {

                if (UserSession.Inst.UserType == "CHURCH" && UserSession.Inst.ChurchPK > 0)
                {
                    lstData = _controller.GetTicketSoldListSalesBy("MEMBER", sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate && x.MEMBER_FK != null && x.CHURCH_FK == UserSession.Inst.ChurchPK).OrderByDescending(x => x.TicketTotal).ToList();
                }
                else
                {
                    lstData = _controller.GetTicketSoldListSalesBy("MEMBER", sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate && x.MEMBER_FK != null).OrderByDescending(x => x.TicketTotal).ToList();
                }

            }
            else
            {
                lstData = _controller.GetTicketSoldListSalesBy(sType1, sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate).OrderByDescending(x => x.TicketTotal).ToList();
            }

            //if (UserSession.Inst.Member2MemberPK > 0)
            //{
            //    lstData = _controller.GetTicketSoldList().Where(x => x.CreatedDate >= fDate && x.CreatedDate <= tDate && x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).OrderByDescending(x => x.TicketTotal).ToList();
            //}
            //else if (UserSession.Inst.MemberPK > 0)
            //{
            //    lstData = _controller.GetTicketSoldList().Where(x => x.CreatedDate >= fDate && x.CreatedDate <= tDate && x.MEMBER_FK == UserSession.Inst.MemberPK).OrderByDescending(x => x.TicketTotal).ToList();
            //}
            //else if (UserSession.Inst.ChurchPK > 0)
            //{
            //    lstData = _controller.GetTicketSoldList().Where(x => x.CreatedDate >= fDate && x.CreatedDate <= tDate &&  x.CHURCH_FK == UserSession.Inst.ChurchPK).OrderByDescending(x => x.TicketTotal).ToList();
            //}
            //else if (UserSession.Inst.RafflePK > 0)
            //{
            //    lstData = _controller.GetTicketSoldList().Where(x => x.CreatedDate >= fDate && x.CreatedDate <= tDate &&  x.RAFFLE_FK == UserSession.Inst.RafflePK).OrderByDescending(x => x.TicketTotal).ToList();
            //}
            //else
            //{
            //    lstData = _controller.GetTicketSoldList().Where(x => x.CreatedDate >= fDate && x.CreatedDate <= tDate).OrderByDescending(x => x.TicketTotal).ToList();
            //}




            //if (noOfCount > 0)
            //{
            //    int cnt = _lst.Count > noOfCount ? noOfCount : _lst.Count;
            //    _lst = _lst.Take(cnt).ToList();
            //}



            //foreach (TicketSold dr in _lst)
            //{
            //    distUserName = dr.LastDistUserName.ToString();
            //    saleAmout = Convert.ToInt32(dr.TicketTotal);
            //    dataList.Add(new Data(distUserName, saleAmout));
            //}



            if (sType1 == "RAFFLE")
            {
                _lst = (from x in lstData
                        group x by x.RAFFLE_FK into g
                        select new TicketSold
                        {
                            SalesBy = g.Max(x => x.SalesBy),
                            RAFFLE_FK = g.Key,
                            TicketTotal = g.Sum(x => x.TicketTotal),
                            Amount = g.Sum(x => x.Amount)
                        }).OrderByDescending(x => x.TicketTotal).ToList();
            }
            else if (sType1 == "CHURCH")
            {
                _lst = (from x in lstData
                        group x by x.CHURCH_FK into g
                        select new TicketSold
                        {
                            SalesBy = g.Max(x => x.SalesBy),
                            RAFFLE_FK = g.Key,
                            TicketTotal = g.Sum(x => x.TicketTotal),
                            Amount = g.Sum(x => x.Amount)
                        }).OrderByDescending(x => x.TicketTotal).ToList();
            }
            else if (sType1 == "MEMBER")
            {
                _lst = (from x in lstData
                        group x by x.MEMBER_FK into g
                        select new TicketSold
                        {
                            SalesBy = g.Max(x => x.SalesBy),
                            RAFFLE_FK = g.Key,
                            TicketTotal = g.Sum(x => x.TicketTotal),
                            Amount = g.Sum(x => x.Amount)
                        }).OrderByDescending(x => x.TicketTotal).ToList();
            }

            if (sType2 != "" && sType2 != "0")
            {
                int cnt = _lst.Count > Convert.ToInt32(sType2) ? Convert.ToInt32(sType2) : _lst.Count;
                _lst = _lst.Take(cnt).ToList();
            }

            decimal percentage = 0;
            decimal TotalTicket = _lst.Sum(x => x.TicketTotal);
            foreach (TicketSold dr in _lst)
            {
                percentage = Convert.ToDecimal((dr.TicketTotal * 100) / TotalTicket);
                distUserName = dr.SalesBy + "  -  " + Math.Round(percentage, 1).ToString() + "%  -  " + dr.TicketTotal.ToString() + " tix  -  $" + dr.Amount.Value.ToString("0");
                ticketTotal = Convert.ToInt32(dr.TicketTotal);
                amount = Convert.ToInt32(dr.Amount);



                dataList.Add(new Data(distUserName, ticketTotal, amount));
            }
        }
        catch
        {
            throw;
        }

        return dataList;
    }

    public class Data
    {
        public string ColumnName = "";
        public int Value = 0;
        public decimal Amount = 0;

        public Data(string columnName, int value, decimal amt)
        {
            ColumnName = columnName;
            Value = value;
            Amount = amt;
        }
    }
    protected void btnSales_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Reports/TopAccountingReport.aspx");
    }
}