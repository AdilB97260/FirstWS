using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using RaffleModel;
using System.Data;

public partial class reports_TopAccountingReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (UserSession.Inst.UserType =="ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "Top Accounting Report");
            }
        }

    }

    [WebMethod]
    public static List<Data> GetData(string dtF, string dtT, string sType)
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
            ExpencessController _expController = new ExpencessController();

            List<TicketSold> lstData = new List<TicketSold>();
            List<TicketSold> _lst = new List<TicketSold>();
            List<TicketSold> _lst2 = new List<TicketSold>();

            //lstData = _controller.GetTicketSoldListByGross(sType).Where(x => x.CreatedDate.Date >= fDate && x.CreatedDate.Date <= tDate).ToList();


            lstData = _controller.GetTicketSoldListSalesBy("CHURCH", "GROSS").Where(x => x.CreatedDate.Date >= fDate.Date && x.CreatedDate.Date <= tDate.Date && x.CHURCH_FK != null).OrderByDescending(x => x.TicketTotal).ToList();


            _lst = (from x in lstData
                    group x by x.CHURCH_FK into g
                    select new TicketSold
                    {
                        SalesBy = g.Max(x => x.SalesBy),
                        RAFFLE_FK = g.Key,
                        TicketTotal = g.Sum(x => x.TicketTotal),
                        Amount = g.Sum(x => x.Amount)
                    }).OrderByDescending(x => x.TicketTotal).ToList();


            TicketSold objStRochSales = _lst.Where(X => X.SalesBy.Contains("St. Roch")).FirstOrDefault();

            List<Expencess> _lstExp = _expController.GetAllExpencessList(UserSession.Inst.CurrentYearID);

            decimal totalExp = Convert.ToDecimal(_lstExp.Sum(x => x.Amount));

            if (objStRochSales != null)
            {

                if (sType == "GROSS")
                {
                    decimal totalSalesAmount = Convert.ToDecimal(_lst.Sum(x => x.Amount));
                    decimal stRockAmount = Convert.ToDecimal(objStRochSales.Amount);
                    decimal OtherAmount = totalSalesAmount - stRockAmount;


                    dataList.Add(new Data("St Roch Gross Profit", Convert.ToInt32(stRockAmount), Convert.ToInt32(stRockAmount)));
                    dataList.Add(new Data("Gross Profit Other Parishes", Convert.ToInt32(OtherAmount), Convert.ToInt32(OtherAmount)));
                    dataList.Add(new Data("Expences", Convert.ToInt32(totalExp), Convert.ToInt32(totalExp)));
                }
                else
                {
                    decimal totalSalesAmount = Convert.ToDecimal(_lst.Sum(x => x.Amount));
                    decimal stRockAmount = Convert.ToDecimal(objStRochSales.Amount);
                    decimal OtherAmount = totalSalesAmount - stRockAmount;


                    stRockAmount = Convert.ToDecimal(objStRochSales.Amount) + (OtherAmount / 2) - totalExp;
                    OtherAmount = OtherAmount / 2;

                    dataList.Add(new Data("St Roch Net Profit", Convert.ToInt32(stRockAmount), Convert.ToInt32(stRockAmount)));
                    dataList.Add(new Data("Net Profit Other Parishes", Convert.ToInt32(OtherAmount), Convert.ToInt32(OtherAmount)));
                    dataList.Add(new Data("Expences", Convert.ToInt32(totalExp), Convert.ToInt32(totalExp)));

                }



            }


            //if (objStRochDist != null)
            //{

            //    //decimal stRockTotalTicket = lstData.Where(x => x.CHURCH_FK==objStRochDist.user_pk).Sum(x => x.TicketTotal);
            //    decimal totalSalesAmount = Convert.ToDecimal(lstData.Sum(x => x.Amount));
            //    decimal stRockAmount = Convert.ToDecimal(lstData.Where(x => x.CHURCH_FK==objStRochDist.user_pk).Sum(x => x.Amount));

            //    decimal OtherTotalTicket = lstData.Where(x => x.CHURCH_FK!=objStRochDist.user_pk).Sum(x => x.TicketTotal);
            //    decimal OtherAmount = totalSalesAmount- stRockAmount;

            //    decimal totalGrossOther = sType=="GROSS" ? OtherAmount : (OtherAmount / 2);
            //    decimal totalGrossStRoch = stRockAmount;  //stRockAmount + totalGrossOther;

            //    decimal NetProfitStRoch = totalGrossStRoch;  //(totalGrossStRoch - totalExp);
            //    decimal NetOther = totalGrossOther;

                

                
            //    dataList.Add(new Data("St Roch Net Profit", Convert.ToInt32(NetProfitStRoch), Convert.ToInt32(NetProfitStRoch)));
            //    dataList.Add(new Data("Net Profit Other Parishes", Convert.ToInt32(NetOther), Convert.ToInt32(NetOther)));
            //    dataList.Add(new Data("Expences", Convert.ToInt32(totalExp), Convert.ToInt32(totalExp)));

            //}


           

            //_lst1 = (from x in lstData
            //        where x.LastDistUserName.StartsWith("St. Roch")
            //        group x by x.LastDistUserName into g
            //        select new TicketSold
            //        {
            //            SalesBy = g.Max(x => x.SalesBy),
            //            TicketTotal = g.Sum(x => x.TicketTotal),
            //            Amount = g.Sum(x => x.Amount)
            //        }).ToList();

            //_lst2 = (from x in lstData
            //         where !x.LastDistUserName.StartsWith("St. Roch")
            //         group x by x.LastDistUserName into g
            //         select new TicketSold
            //         {
            //             SalesBy = g.Max(x => x.SalesBy),
            //             TicketTotal = g.Sum(x => x.TicketTotal),
            //             Amount = g.Sum(x => x.Amount)
            //         }).ToList();
            

            //if (sType2 != "" && sType2 != "0")
            //{
            //    int cnt = _lst.Count > Convert.ToInt32(sType2) ? Convert.ToInt32(sType2) : _lst.Count;
            //    _lst = _lst.Take(cnt).ToList();
            //}

            //decimal percentage = 0;
            //decimal TotalTicketRoch = _lst1.Sum(x => x.TicketTotal);
            //decimal TotalTicketOther = _lst2.Sum(x => x.TicketTotal);
            //foreach (TicketSold dr in _lst1)
            //{
            //    percentage = Convert.ToDecimal((dr.TicketTotal * 100) / TotalTicketRoch);
            //    distUserName = dr.SalesBy + "  -  " + Math.Round(percentage, 1).ToString() + "%  -  " + dr.TicketTotal.ToString() + " tix  -  $" + dr.Amount.Value.ToString("0");
            //    ticketTotal = Convert.ToInt32(dr.TicketTotal);
            //    amount = Convert.ToInt32(dr.Amount);
            //    dataList.Add(new Data(distUserName, ticketTotal, amount));
            //}
            //foreach (TicketSold dr in _lst2)
            //{
            //    percentage = Convert.ToDecimal((dr.TicketTotal * 100) / TotalTicketOther);
            //    distUserName = dr.SalesBy + "  -  " + Math.Round(percentage, 1).ToString() + "%  -  " + dr.TicketTotal.ToString() + " tix  -  $" + dr.Amount.Value.ToString("0");
            //    ticketTotal = Convert.ToInt32(dr.TicketTotal);
            //    amount = Convert.ToInt32(dr.Amount);
            //    dataList.Add(new Data(distUserName, ticketTotal, amount));
            //}
        }
        catch(Exception ex)
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
        Response.Redirect("/Reports/SalesDateRange.aspx?r=1");
    }
}