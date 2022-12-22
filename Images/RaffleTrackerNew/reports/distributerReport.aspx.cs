using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using RaffleModel;
using System.Reflection;
using System.Web.Services;

public partial class reports_distributerReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }


        if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
        {
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Sales By Distributer Repot");
        }

        else
        {

            if (UserSession.Inst.Member2MemberPK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "Sales By Distributer Repot");
            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "Sales By Distributer Repot");
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "Sales By Distributer Repot");
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "Sales By Distributer Repot");
            }
            else
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Sales By Distributer Repot");
            }

        }

        //if (UserSession.Inst.UserType == "ADMIN")
        //{
        //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Sales By Distributer Repot");
        //}
        //else if (UserSession.Inst.UserType == "RAFFLE")
        //{
        //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Sales By Distributer Repot");
        //}
        //else if (UserSession.Inst.UserType == "CHURCH")
        //{
        //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Sales By Distributer Repot");
        //}
        //else if (UserSession.Inst.UserType == "MEMBER")
        //{
        //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Sales By Distributer Repot");
        //}
        //else
        //{
        //    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member2member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Sales By Distributer Repot");
        //}



    }


    [WebMethod]
    public static List<Data> GetData(int noOfCount)
    {

        List<Data> dataList = new List<Data>();
        string distUserName = "";
        int saleAmout = 0;

        DataSet dsData = new DataSet();
        try
        {
            RaffleController _controller = new RaffleController();


            List<TicketsDistribution> lstData = new List<TicketsDistribution>();

            if (UserSession.Inst.MemberPK > 0)
            {
                lstData = _controller.GetTickeDistributionDetailList().Where(x => x.MEMBER_FK==UserSession.Inst.MemberPK && x.Dist_user_Fk != x.MEMBER_FK).OrderByDescending(x => x.TotalTickets).ToList();
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                lstData = _controller.GetTickeDistributionDetailList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK && x.Dist_user_Fk != x.CHURCH_FK).OrderByDescending(x => x.TotalTickets).ToList();
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                lstData = _controller.GetTickeDistributionDetailList().Where(x => x.RAFFLE_FK == UserSession.Inst.RafflePK && x.Dist_user_Fk != x.RAFFLE_FK).OrderByDescending(x => x.TotalTickets).ToList();
            }
            else
            {
                lstData = _controller.GetTickeDistributionDetailList().OrderByDescending(x => x.TotalTickets).ToList();
            }

            lstData = lstData.OrderByDescending(x => x.TotalTickets).ToList();

            List<TicketsDistribution> _lst = (from x in lstData
                                              group x by x.Dist_user_Fk into g
                                              select new TicketsDistribution
                                              {
                                                  ToDistUserName = g.Max(x => x.ToDistUserName),
                                                  Dist_user_Fk = g.Key,
                                                  TotalTickets = g.Sum(x => x.TotalTickets)
                                              }).OrderByDescending(x => x.TotalTickets).ToList();

            if (noOfCount > 0)
            {
                int cnt = _lst.Count > noOfCount ? noOfCount : _lst.Count;
                _lst = _lst.Take(cnt).ToList();
            }



            foreach (TicketsDistribution dr in _lst)
            {
                distUserName = dr.ToDistUserName.ToString();
                saleAmout = Convert.ToInt32(dr.TotalTickets);
                dataList.Add(new Data(distUserName, saleAmout));
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

        public Data(string columnName, int value)
        {
            ColumnName = columnName;
            Value = value;
        }
    }
}