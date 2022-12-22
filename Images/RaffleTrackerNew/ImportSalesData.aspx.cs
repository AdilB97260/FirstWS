using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using RaffleModel;
using NPOI.HSSF.UserModel;

public partial class ImportSalesData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptFailedData.DataSource = new List<ImportSalesData>();
            rptFailedData.DataBind();
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

    protected void btnImport_Click(object sender, EventArgs e)
    {

        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, "/SaleList.aspx");
            return;
        }

        if (fldCSVFile.HasFile && fldCSVFile.PostedFile.FileName != "")
        {
            string fileName = fldCSVFile.PostedFile.FileName;

            if (Path.GetExtension(fileName).ToUpper() == ".CSV" || Path.GetExtension(fileName).ToUpper() == ".XLSX" || Path.GetExtension(fileName).ToUpper() == ".XLS")
            {
                TicketSold objSold = new TicketSold();
                RaffleController _controller = new RaffleController();

                TicketsDistribution objDist = new TicketsDistribution();

                List<ImportSalesData> lstImportSaleData = new List<ImportSalesData>();
                ImportSalesData objImportSaleData = new ImportSalesData();
                string path1 = string.Format("{0}/{1}", Server.MapPath("~/data"), fileName);

                if (System.IO.File.Exists(path1))
                {
                    System.IO.File.Delete(path1);
                }

                DataTable dt = new DataTable();

                fldCSVFile.PostedFile.SaveAs(path1);

                if (Path.GetExtension(fileName).ToUpper() == ".CSV")
                {
                    dt = ConvertCSVtoDataTable(path1);
                }
                else if (Path.GetExtension(fileName).ToUpper() == ".XLS")
                {
                    string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    dt = ConvertXSLXtoDataTable(path1, connString);
                }
                else if (Path.GetExtension(fileName).ToUpper() == ".XLSX")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    dt = ConvertXSLXtoDataTable(path1, connString);
                }


                int result = 0;

                int successRec = 0;

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        objImportSaleData = new ImportSalesData();
                        objImportSaleData.TotalTicket = Convert.ToInt32(dr["Total Ticket"]);
                        objImportSaleData.FromTicket = Convert.ToInt32(dr["Ticket Number Start"]);
                        objImportSaleData.ToTicket = Convert.ToInt32(dr["Ticket Number End"]);
                        objImportSaleData.GivenTo = Convert.ToString(dr["Ticket(s) sold to"]);
                        objImportSaleData.Email = Convert.ToString(dr["Email address of buyer"]);
                        objImportSaleData.City = Convert.ToString(dr["City of buyer"]);
                        objImportSaleData.State = Convert.ToString(dr["State of buyer"]);
                        objImportSaleData.Phone = Convert.ToString(dr["Phone Number of buyer"]);
                        objImportSaleData.Address = Convert.ToString(dr["Street address of buyer"]);
                        objImportSaleData.Zip = dr["zip of buyer"] !=null ? Convert.ToString(dr["zip of buyer"]) : "";
                        lstImportSaleData.Add(objImportSaleData);
                    }
                }


                List<ImportSalesDataStatus> lstFailedData = new List<ImportSalesDataStatus>();
                ImportSalesDataStatus objFailed = new ImportSalesDataStatus();

                if (lstImportSaleData.Count > 0)
                {
                    foreach (ImportSalesData obj in lstImportSaleData)
                    {

                        objFailed = new ImportSalesDataStatus();

                        if (_controller.IsTicketSold(Convert.ToInt32(obj.FromTicket), Convert.ToInt32(obj.ToTicket), 0))
                        {
                            objFailed.FromTicket = obj.FromTicket;
                            objFailed.ToTicket = obj.ToTicket;
                            objFailed.GivenTo = obj.GivenTo;
                            objFailed.TotalTicket = obj.TotalTicket;
                            objFailed.Status = "FAILED";
                            objFailed.ErrorMsg = "Ticked already been sold or Ticket already entered into the system. !";
                            lstFailedData.Add(objFailed);
                        }
                        else
                        {

                            objSold = new TicketSold();

                            objDist = new TicketsDistribution();

                            objDist = _controller.GetLastTickDist(Convert.ToInt32(obj.FromTicket), Convert.ToInt32(obj.ToTicket));

                            if (objDist != null)
                            {
                                objSold.LastDistUserId = Convert.ToInt32(objDist.LastDistUserFk);
                                objSold.LastDistUserName = objDist.LastDistUserName;

                                objSold.LastAccessUserID = Convert.ToInt32(objDist.LastDistUserFk);
                                objSold.LastAccessUserName = objDist.LastDistUserName;


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
                                    //}
                                }


                                objSold.GivenTo = obj.GivenTo;
                                objSold.Address = obj.Address;
                                objSold.City = obj.City;
                                objSold.State = obj.State;
                                objSold.CreatedDate = DateTime.Now;
                                objSold.Email = obj.Email;
                                objSold.Phone = obj.Phone;
                                objSold.Zip = obj.Zip;
                                objSold.TicketFrom = Convert.ToInt32(obj.FromTicket);
                                objSold.TicketTo = Convert.ToInt32(obj.ToTicket);
                                objSold.TicketTotal = Convert.ToInt32(objSold.TicketTo - objSold.TicketFrom + 1);
                                objSold.Collected_By = UserSession.Inst.UserName;

                                if (objD != null && objD.TicketRate != null)
                                {
                                    objSold.Amount = Convert.ToDecimal(objSold.TicketTotal * objD.TicketRate);
                                }
                                else
                                {
                                    objSold.Amount = Convert.ToDecimal(objSold.TicketTotal * 25);
                                }


                                objSold.TicketSold_fk = 0;
                                result = _controller.AddTicketSold(objSold);

                                if (result < 0)
                                {
                                    objFailed.FromTicket = obj.FromTicket;
                                    objFailed.ToTicket = obj.ToTicket;
                                    objFailed.GivenTo = obj.GivenTo;
                                    objFailed.TotalTicket = obj.TotalTicket;
                                    objFailed.Status = "FAILED";
                                    objFailed.ErrorMsg = "Data mismatched !";
                                    lstFailedData.Add(objFailed);

                                }
                                else
                                {
                                    successRec++;
                                }
                            }

                        }

                    }

                    rptFailedData.DataSource = lstFailedData;
                    rptFailedData.DataBind();


                    if (successRec > 0)
                    {
                        pnlSuccess.Visible = true;
                        lblSuccessRec.Text = successRec.ToString();
                    }
                }
            }
        }
    }

    protected void btnDownloadTemplate_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string ExcelPath = string.Format("/Data/import-Salesdata-template.xls");

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        HSSFSheet sheet1 = hssfworkbook.CreateSheet("SalesData");

        HSSFCellStyle style1 = hssfworkbook.CreateCellStyle();

        // cell background
        style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BLUE.index;
        style1.FillPattern = HSSFCellStyle.SOLID_FOREGROUND;

        // font color
        HSSFFont font1 = hssfworkbook.CreateFont();
        font1.Color = NPOI.HSSF.Util.HSSFColor.YELLOW.index;
        style1.SetFont(font1);

        sheet1.CreateRow(0).CreateCell(0).CellStyle = style1;
        sheet1.CreateRow(0).CreateCell(1).CellStyle = style1;


        sheet1.CreateRow(0).CreateCell(0).SetCellValue("Total Ticket");
        sheet1.CreateRow(0).CreateCell(1).SetCellValue("Ticket Number Start");
        sheet1.CreateRow(0).CreateCell(2).SetCellValue("Ticket Number End");
        sheet1.CreateRow(0).CreateCell(3).SetCellValue("Ticket(s) sold to");
        sheet1.CreateRow(0).CreateCell(4).SetCellValue("Email address of buyer");
        sheet1.CreateRow(0).CreateCell(5).SetCellValue("City of buyer");
        sheet1.CreateRow(0).CreateCell(6).SetCellValue("State of buyer");
        sheet1.CreateRow(0).CreateCell(7).SetCellValue("Phone Number of buyer");
        sheet1.CreateRow(0).CreateCell(8).SetCellValue("Street address of buyer");
        


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


    public static DataTable ConvertCSVtoDataTable(string strFilePath)
    {
        DataTable dt = new DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }

            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                if (rows.Length > 1)
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i].Trim();
                    }
                    dt.Rows.Add(dr);
                }
            }

        }


        return dt;
    }

    public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
    {
        OleDbConnection oledbConn = new OleDbConnection(connString);
        DataTable dt = new DataTable();
        try
        {

            oledbConn.Open();
            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [SalesData$x]", oledbConn))
            {
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                oleda.Fill(ds);

                dt = ds.Tables[0];
            }
        }
        catch(Exception ex)
        {
        }
        finally
        {

            oledbConn.Close();
        }

        return dt;

    }


}



public partial class ImportSalesData
{
    public int TotalTicket { get; set; }
    public int FromTicket { get; set; }
    public int ToTicket { get; set; }
    public string GivenTo { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Phone { get; set; }

}


public partial class ImportSalesDataStatus
{
    public int TotalTicket { get; set; }
    public int FromTicket { get; set; }
    public int ToTicket { get; set; }
    public string GivenTo { get; set; }
    public string Status { get; set; }
    public string ErrorMsg { get; set; }


}