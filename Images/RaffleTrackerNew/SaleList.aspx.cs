using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Web.Services;
using System.Text;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Web.Script.Serialization;

public partial class SaleList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {
            RaffleController _controller = new RaffleController();

            List<TicketSold> _lstSold = _controller.GetTicketSoldList();

            if ((UserSession.Inst.UserType == "ADMIN" && UserSession.Inst.IsSystemUser == false) || (UserSession.Inst.IsSystemUser && (UserSession.Inst.SystemUserRole == "REPORTS")))
            {
                //btnRecordSale.Visible = false;
            }


            if (UserSession.Inst.UserType == "ADMIN" )
            {

                if (UserSession.Inst.Member2MemberPK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a>  <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "View Ticket Sale List");
                }
                else if (UserSession.Inst.MemberPK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "View Ticket Sale List");
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "View Ticket Sale List");
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "View Ticket Sale List");
                }
                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "View Ticket Sale List");
                }


            }


            else if (UserSession.Inst.IsSystemUser)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/SaleList.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/SaleList.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Sold");
                //_lstSold = _lstSold.Where(X => X.LastAccessUserID == UserSession.Inst.UserPK).ToList();

                if (UserSession.Inst.UserType == "RAFFLE")
                {
                    _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "View Ticket Sale List");
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "View Ticket Sale List");
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK).ToList();
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "View Ticket Sale List");
                }
                else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a>  <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "View Ticket Sale List");
                    _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList();
                }


             
            }

            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Sold");
                _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK).ToList();
            }

            else if (UserSession.Inst.UserType == "CHURCH")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Sold");
                _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK).ToList();
            }

            else if (UserSession.Inst.UserType == "MEMBER")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Sold");
                _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK).ToList();
            }

            else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member2member/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member2member/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Sold");
                _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList();
            }


            if (UserSession.Inst.SystemUserRole !=null && UserSession.Inst.SystemUserRole == "VIEWER")
            {
                btnDownloadFile.Visible = false;
                btnMultipleSale.Visible = false;
                btnRecordSale.Visible = false;
            }

            rptSale.DataSource = _lstSold;
            rptSale.DataBind();


            lblTotalTick.Text = _lstSold.Sum(x => x.TicketTotal).ToString();
            lblTotAmt.Text = _lstSold.Sum(x => x.Amount).ToString();

            //lblTotAmt1.Text = lblTotAmt.Text;
            //lblTotTick1.Text = lblTotalTick.Text;
        }
    }

    protected void btn_MultipleSale_Click(object sender, EventArgs e)
    {
        Response.Redirect("/MultipleSales.aspx");
    }


    protected void DownloadFile_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string ExcelPath = string.Format("/Data/ticketsold-{0}.xls", DateTime.Now.ToString("yyyyMMddHHMMss"));

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        HSSFSheet sheet1 = hssfworkbook.CreateSheet("TicketSoldData");

        HSSFCellStyle style1 = hssfworkbook.CreateCellStyle();

        // cell background
        style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index;
        style1.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;

        // font color
        HSSFFont font1 = hssfworkbook.CreateFont();
        font1.Color = NPOI.HSSF.Util.HSSFColor.YELLOW.index;
        style1.SetFont(font1);

        //cell.CellStyle = style1;


        //ICellStyle testeStyle = hssfwb.CreateCellStyle();
        //testeStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Medium;
        //testeStyle.FillForegroundColor = IndexedColors.BrightGreen.Index;
        //testeStyle.FillPattern = FillPattern.SolidForeground;



        sheet1.CreateRow(0).CreateCell(0).CellStyle = style1;
        sheet1.CreateRow(0).CreateCell(1).CellStyle = style1;


        sheet1.CreateRow(0).CreateCell(0).SetCellValue("TimeStamp");
        sheet1.CreateRow(0).CreateCell(1).SetCellValue("GivenTo");
        sheet1.CreateRow(0).CreateCell(2).SetCellValue("Email");
        sheet1.CreateRow(0).CreateCell(3).SetCellValue("Phone");
        sheet1.CreateRow(0).CreateCell(4).SetCellValue("Address");
        sheet1.CreateRow(0).CreateCell(5).SetCellValue("City");
        sheet1.CreateRow(0).CreateCell(6).SetCellValue("State");
        sheet1.CreateRow(0).CreateCell(7).SetCellValue("Last Distributer Name");
        sheet1.CreateRow(0).CreateCell(8).SetCellValue("Collected By");
        sheet1.CreateRow(0).CreateCell(9).SetCellValue("Ticket From");
        sheet1.CreateRow(0).CreateCell(10).SetCellValue("Ticket To");
        sheet1.CreateRow(0).CreateCell(11).SetCellValue("Total Ticket");
        sheet1.CreateRow(0).CreateCell(12).SetCellValue("Amount");

        int row = 1;

        List<TicketSold> _lstSold = new List<TicketSold>();

        RaffleController _controller = new RaffleController();

        //if (!string.IsNullOrEmpty(txtSearch.Text))
        //{
        //    if (UserSession.Inst.UserType == "ADMIN")
        //    {
        //        _lstSold = _controller.GetTicketSoldList().Where(x => x.TicketFrom <= Convert.ToInt32(txtSearch.Text) && x.TicketTo >= Convert.ToInt32(txtSearch.Text)).ToList();
        //    }
        //    else
        //    {
        //        _lstSold = _controller.GetTicketSoldList().Where(x => x.TicketFrom <= Convert.ToInt32(txtSearch.Text) && x.TicketTo >= Convert.ToInt32(txtSearch.Text) && x.LastAccessUserID == Utilities.GetAccessedUserKey()).ToList();
        //    }
        //}
        //else
        //{
        if (UserSession.Inst.UserType == "ADMIN")
        {
            _lstSold = _controller.GetTicketSoldList().ToList();
        }
        else
        {
            _lstSold = _controller.GetTicketSoldList().Where(x => x.LastAccessUserID == Utilities.GetAccessedUserKey()).ToList();
        }




        if (UserSession.Inst.Member2MemberPK > 0)
        {
            _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList();
            
        }
        else if (UserSession.Inst.MemberPK > 0)
        {
            _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK).ToList();

        }
        else if (UserSession.Inst.ChurchPK > 0)
        {
            _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK).ToList();
           
        }
        else if (UserSession.Inst.RafflePK > 0)
        {
            _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK).ToList();
           
        }
        

        //}

        // _lstDetail = _customerProvider.GetCustPrizeDetailList();

        foreach (TicketSold obj in _lstSold)
        {

            sheet1.CreateRow(row).CreateCell(0).SetCellValue(obj.CreatedDate.ToShortDateString());
            sheet1.CreateRow(row).CreateCell(1).SetCellValue(obj.GivenTo);
            sheet1.CreateRow(row).CreateCell(2).SetCellValue(obj.Email);
            sheet1.CreateRow(row).CreateCell(3).SetCellValue(obj.Phone);
            sheet1.CreateRow(row).CreateCell(4).SetCellValue(obj.Address);
            sheet1.CreateRow(row).CreateCell(5).SetCellValue(obj.City);
            sheet1.CreateRow(row).CreateCell(6).SetCellValue(obj.State);
            sheet1.CreateRow(row).CreateCell(7).SetCellValue(obj.LastAccessUserName);
            sheet1.CreateRow(row).CreateCell(8).SetCellValue(obj.Collected_By);
            sheet1.CreateRow(row).CreateCell(9).SetCellValue(obj.TicketFrom);
            sheet1.CreateRow(row).CreateCell(10).SetCellValue(obj.TicketTo);
            sheet1.CreateRow(row).CreateCell(11).SetCellValue(obj.TicketTotal);
            sheet1.CreateRow(row).CreateCell(12).SetCellValue(string.Format("${0}", obj.Amount.Value.ToString("0.00")));
            row++;
        }

        WriteSave(Server.MapPath(ExcelPath), hssfworkbook);
        DownloadFile(ExcelPath);

    }

    private void WriteSave(string filePath, HSSFWorkbook hssfworkbook)
    {
        if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
        FileStream file = new FileStream(filePath, FileMode.Create);
        hssfworkbook.Write(file);
        file.Flush();
        file.Close();
        file.Dispose();

    }

    private void DownloadFile(string FilePath)
    {
        string strURL = Server.MapPath(FilePath);
        FileInfo file = new FileInfo(strURL);
        if (file.Exists)
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/vnd.ms-excel";
            Response.TransmitFile(file.FullName);
            Response.End();
        }

    }

    protected void btn_RecordList_Click(object sernder, EventArgs e)
    {
        Response.Redirect("/TicketSale.aspx");
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(txtSearch.Text))
        {

            return;
        }


        RaffleController _controller = new RaffleController();
        List<TicketSold> _lstSold = new List<TicketSold>();
        if (UserSession.Inst.UserType == "ADMIN")
        {
            _lstSold = _controller.GetTicketSoldList().Where(x => x.TicketFrom <= Convert.ToInt32(txtSearch.Text) && x.TicketTo >= Convert.ToInt32(txtSearch.Text)).ToList();
        }
        else
        {
            _lstSold = _controller.GetTicketSoldList().Where(x => x.TicketFrom <= Convert.ToInt32(txtSearch.Text) && x.TicketTo >= Convert.ToInt32(txtSearch.Text) && x.LastAccessUserID == Utilities.GetAccessedUserKey()).ToList();
        }

        rptSale.DataSource = _lstSold;
        rptSale.DataBind();

        lblTotalTick.Text = _lstSold.Sum(x => x.TicketTotal).ToString();
        lblTotAmt.Text = _lstSold.Sum(x => x.Amount).ToString();

        //lblTotAmt1.Text = lblTotAmt.Text;
        //lblTotTick1.Text = lblTotalTick.Text;

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        RaffleController _controller = new RaffleController();

        List<TicketSold> _lstSold = _controller.GetTicketSoldList();

        if (UserSession.Inst.UserType != "ADMIN")
        {
            _lstSold = _lstSold.Where(x => x.LastAccessUserID == Utilities.GetAccessedUserKey()).ToList();
        }

        rptSale.DataSource = _lstSold;
        rptSale.DataBind();


        lblTotalTick.Text = _lstSold.Sum(x => x.TicketTotal).ToString();
        lblTotAmt.Text = _lstSold.Sum(x => x.Amount).ToString();

        //lblTotAmt1.Text = lblTotAmt.Text;
        //lblTotTick1.Text = lblTotalTick.Text;

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
    public static string GetSalesData(string tid)
    {
        RaffleController _controller = new RaffleController();
        SalesData objSaleData = new SalesData();
        TicketSold obj = _controller.GetTicketSoldObj(Convert.ToInt32(tid));

        objSaleData.CollectionBy = !string.IsNullOrEmpty(obj.Collected_By) ? obj.Collected_By : _controller.GetDistUserObj(obj.LastAccessUserID).name;
        objSaleData.SoldTo = obj.GivenTo;
        objSaleData.SoldDate = obj.CreatedDate.ToShortDateString();
        objSaleData.TicketFrom = obj.TicketFrom.ToString();
        objSaleData.TicketTo = obj.TicketTo.ToString();

        List<TicketsDistribution> lst = _controller.GetTickHistory(Convert.ToInt32(obj.TicketFrom), Convert.ToInt32(obj.TicketTo));
        if (lst != null && lst.Count > 0)
        {
            objSaleData.TicketDistList = lst;
            objSaleData.LastDistName = lst.OrderByDescending(x => x.CreatedDate).FirstOrDefault().LastDistUserName;
        }


        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(objSaleData);
    }

    //protected void lnkView_click(object sender, EventArgs e)
    //{
    //    RaffleController _controller = new RaffleController();
    //    int TickSoldFk = Convert.ToInt32(((LinkButton)(sender)).CommandArgument);

    //    TicketSold obj = _controller.GetTicketSoldObj(TickSoldFk);


    //    lblCollectionBy.Text = !string.IsNullOrEmpty(obj.Collected_By) ? obj.Collected_By : _controller.GetDistUserObj(obj.LastAccessUserID).name;
    //    lblSoldTo.Text = obj.GivenTo;
    //    lblDate.Text = obj.CreatedDate.ToShortDateString();

    //    lblFrom.Text = obj.TicketFrom.ToString();
    //    lblTo.Text = obj.TicketTo.ToString();

    //    List<TicketsDistribution> lst = _controller.GetTickHistory(Convert.ToInt32(obj.TicketFrom), Convert.ToInt32(obj.TicketTo));
    //    if (lst != null && lst.Count > 0)
    //    {
    //        lblLastDistName.Text = lst.OrderByDescending(x => x.CreatedDate).FirstOrDefault().LastDistUserName;
    //    }
    //    rptSaleHist.DataSource = lst;
    //    rptSaleHist.DataBind();

    //    Popup(true);

    //    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
    //    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none","<script>$('#ViewModel').modal('show');</script>", true);
    //    //ClientScript.RegisterStartupScript(this.GetType(), "Pop", "openModal(); return false;", true);
    //}


    //void Popup(bool isDisplay)
    //{
    //    StringBuilder builder = new StringBuilder();
    //    if (isDisplay)
    //    {
    //        builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
    //    }
    //    else
    //    {
    //        builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
    //    }
    //}

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
    protected void btnImportClick(object sender, EventArgs e)
    {
        Response.Redirect("/ImportSalesData.aspx");
    }
    protected void btn_click(object sender, EventArgs e)
    {
        RaffleController _controller = new RaffleController();

        RaffleEntities _entities = new RaffleEntities();

        List<TicketSold> _lstSold = _controller.GetTicketSoldList();



        foreach (TicketSold obj in _lstSold)
        {

            TicketsDistribution objDist = _controller.GetLastTickDist(obj.TicketFrom, obj.TicketTo);

            if (objDist != null)
            {
                TicketSold objSold = _entities.TicketSolds.Where(x => x.TicketSold_fk == obj.TicketSold_fk).FirstOrDefault();

                objSold.LastDistUserId = objDist.LastDistUserFk;
                objSold.LastDistUserName = objDist.LastDistUserName;
                objSold.LastAccessUserID = objDist.LastDistUserFk;
                //objSold.LastAccessUserName
                _entities.SaveChanges();
            }

        }

    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {

        Response.Redirect("/TicketSale.aspx");
    }
    protected void btndistributeSales_Click(object sender, EventArgs e)
    {
        Response.Redirect("/DistributerSales.aspx");
    }
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