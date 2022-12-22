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

public partial class reports_SaleReport : System.Web.UI.Page
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
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            RaffleController _controller = new RaffleController();


            List<TicketSold> lstData = new List<TicketSold>();

            if (UserSession.Inst.Member2MemberPK > 0)
            {
                lstData = _controller.GetTicketSoldList().Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK ).OrderByDescending(x => x.TicketTotal).ToList();
            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                lstData = _controller.GetTicketSoldList().Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).OrderByDescending(x => x.TicketTotal).ToList();
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                lstData = _controller.GetTicketSoldList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).OrderByDescending(x => x.TicketTotal).ToList();
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                lstData = _controller.GetTicketSoldList().Where(x => x.RAFFLE_FK == UserSession.Inst.RafflePK).OrderByDescending(x => x.TicketTotal).ToList();
            }
            else
            {
                lstData = _controller.GetTicketSoldList().OrderByDescending(x => x.TicketTotal).ToList();
            }

           
            List<TicketSold> _lst = (from x in lstData
                                     group x by x.LastDistUserId into g
                                     select new TicketSold
                                         {
                                             LastDistUserName = g.Max(x => x.LastDistUserName),
                                             LastDistUserId = g.Key,
                                             TicketTotal = g.Sum(x => x.TicketTotal)
                                         }).ToList();

            if (noOfCount > 0)
            {
                int cnt = _lst.Count > noOfCount ? noOfCount : _lst.Count;
                _lst = _lst.Take(cnt).ToList();
            }



            foreach (TicketSold dr in _lst)
            {
                distUserName = dr.LastDistUserName.ToString();
                saleAmout = Convert.ToInt32(dr.TicketTotal);
                dataList.Add(new Data(distUserName, saleAmout));
            }
        }
        catch
        {
            throw;
        }

        return dataList;
    }


    //public DataTable GetChartData()
    //{

    //}


    //public string GGReq = string.Empty;
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    DataTable dt = new DataTable();
    //    dt = GetChartData();

    //    PieGraph pg = new PieGraph();
    //    pg.GraphColors = "5566AA";
    //    pg.GraphHeight = "1251";
    //    pg.GraphWidth = "2500";
    //    pg.GraphTitle = "Welcome to Test Graph";
    //    pg.GraphTitleColor = "112233";
    //    pg.GraphTitleSize = "20";
    //    pg.dtData = dt;

    //    GGReq = pg.GenerateGraph();


    //    this.DataBind();
    //}


    //private void BindGvData()
    //{
    //    //gvData.DataSource = GetChartData();
    //    //gvData.DataBind();
    //}

    //    private void BindChart()
    //    {
    //        DataTable dsChartData = new DataTable();
    //        StringBuilder strScript = new StringBuilder();

    //        try
    //        {
    //            dsChartData = GetChartData();

    //            strScript.Append(@"<script type='text/javascript'>  
    //                    google.load('visualization', '1', {packages: ['corechart']});</script>  
    //  
    //                    <script type='text/javascript'>  
    //                    function drawVisualization() {         
    //                    var data = google.visualization.arrayToDataTable([  
    //                    ['Month', 'Bolivia', 'Ecuador', 'Madagascar', 'Average'],");

    //            foreach (DataRow row in dsChartData.Rows)
    //            {
    //                //strScript.Append("['" + row["Month"] + "'," + row["Bolivia"] + "," + row["Ecuador"] + "," + row["Madagascar"] + "," + row["Avarage"] + "],");
    //                strScript.Append("['" + row["GivenName"] + "']s,");
    //            }
    //            strScript.Remove(strScript.Length - 1, 1);
    //            strScript.Append("]);");

    //            strScript.Append("var options = { title : 'Monthly Coffee Production by Country', vAxis: {title: 'Cups'},  hAxis: {title: 'Month'}, seriesType: 'bars', series: {3: {type: 'area'}} };");
    //            strScript.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
    //            strScript.Append(" </script>");

    //            ltScripts.Text = strScript.ToString();
    //        }
    //        catch
    //        {
    //        }
    //        finally
    //        {
    //            dsChartData.Dispose();
    //            strScript.Clear();
    //        }
    //    }



    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
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