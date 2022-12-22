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

public partial class ImportDistributionData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rptFailedData.DataSource = new List<ImportDistData>();
            rptFailedData.DataBind();
        }
    }


    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/Dashboard.aspx");
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        List<ImportDistDataStatus> lstFailedData = new List<ImportDistDataStatus>();
        ImportDistDataStatus objFailed = new ImportDistDataStatus();

        pnlSuccess.Visible = false;
        lblSuccessRec.Text = "";
        
        
        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the distribution entry. Login first to current year then enter the distribution entry.", "Opps! Distribution entered on previous year Raffle ", Page, "/SaleList.aspx");
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

                List<ImportDistData> lstImportSaleData = new List<ImportDistData>();
                ImportDistData objImportDistData = new ImportDistData();
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

                int successRec = 0;

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                            objImportDistData = new ImportDistData();
                            objImportDistData.TotalTicket = Convert.ToString(dr["Total Tix"]);
                            objImportDistData.FromTicket = Convert.ToString(dr["Start Tix"]);
                            objImportDistData.ToTicket = Convert.ToString(dr["End Tix"]);
                            objImportDistData.Name = Convert.ToString(dr["Name"]);
                            objImportDistData.Address = Convert.ToString(dr["Street Address"]);
                            objImportDistData.City = Convert.ToString(dr["City"]);
                            objImportDistData.State = Convert.ToString(dr["State"]);
                            objImportDistData.Zip = dr["Zip"] != null ? Convert.ToString(dr["Zip"]) : "";
                            objImportDistData.phone = dr["Phone"] != null ? Convert.ToString(dr["Phone"]) : "";

                            if (objImportDistData.TotalTicket != "" && objImportDistData.FromTicket != "" && objImportDistData.ToTicket != "")
                            {

                                lstImportSaleData.Add(objImportDistData);
                            }
                        
                    }
                }


              

                if (lstImportSaleData.Count > 0)
                {
                    DIST_USERS objDistUser = null;
                    TicketsDistribution _objTickDist = null;
                    RaffleController _provider = new RaffleController();

                    foreach (ImportDistData obj in lstImportSaleData)
                    {

                        objFailed = new ImportDistDataStatus();
                        
                        objDistUser = new DIST_USERS();
                        _objTickDist = new TicketsDistribution();

                        

                        if (_provider.GetDistUserObjByName(obj.Name, obj.FromTicket.ToString(), obj.ToTicket.ToString()) == null)
                        {
                            TicketsDistribution objTicketDist = _provider.GetLastTickDist(Convert.ToInt32(obj.FromTicket), Convert.ToInt32(obj.ToTicket));

                            if (objTicketDist != null)
                            {

                                DIST_USERS objDistPrev = _controller.GetDistUserObj(objTicketDist.Dist_user_Fk);

                                if (objDistPrev != null)
                                {
                                    objDistUser.Address = obj.Address;
                                    objDistUser.City = obj.City;
                                    objDistUser.Zip = obj.Zip;
                                    objDistUser.YearID = UserSession.Inst.CurrentRaffleYearID;
                                    objDistUser.UserType = objDistPrev.UserType == "RAFFLE" ? "CHURCH" : objDistPrev.UserType == "CHURCH" ? "MEMBER" : "MEMBER2MEMBER";
                                    objDistUser.RAFFLE_FK = objDistPrev.RAFFLE_FK;
                                    objDistUser.CHURCH_FK = objDistPrev.CHURCH_FK;
                                    objDistUser.MEMBER_FK = objDistPrev.MEMBER_FK;
                                    objDistUser.MEM2MEM_FK = objDistPrev.MEM2MEM_FK;
                                    objDistUser.parent_userFk = objDistPrev.user_pk;
                                    objDistUser.name = obj.Name;
                                    objDistUser.modifiedDate = DateTime.Now;
                                    objDistUser.createdDate = DateTime.Now;
                                    objDistUser.ChurchAdminstName = objDistPrev.ChurchAdminstName;
                                    objDistUser.ChurchType = objDistPrev.ChurchType;
                                    objDistUser.CreatedBy_UserFk = objDistPrev.user_pk;
                                    objDistUser.LastaccessUserId = objDistPrev.user_pk;
                                    objDistUser.State = obj.State;
                                    objDistUser.TicketRate = objDistPrev.TicketRate;
                                    objDistUser.Balance = Convert.ToInt32(obj.TotalTicket);
                                    objDistUser.phone = obj.phone;

                                    _objTickDist.CHURCH_FK = objTicketDist.CHURCH_FK;
                                    _objTickDist.CreatedDate = DateTime.Now;
                                    _objTickDist.Dist_Type = objTicketDist.Dist_Type;
                                    _objTickDist.FromTicket = obj.FromTicket.ToString();
                                    _objTickDist.LastAccessUserID = objDistPrev.user_pk;
                                    _objTickDist.LastDistUserFk = objDistPrev.user_pk;
                                    _objTickDist.LastDistUserName = objDistPrev.name;
                                    _objTickDist.MEM2MEM_FK = objTicketDist.MEM2MEM_FK;
                                    _objTickDist.MEMBER_FK = objTicketDist.MEMBER_FK;
                                    _objTickDist.ModifiedDate = DateTime.Now;
                                    _objTickDist.RAFFLE_FK = objTicketDist.RAFFLE_FK;
                                    _objTickDist.TotalTickets = Convert.ToInt32(obj.TotalTicket);
                                    _objTickDist.ToTicket = obj.ToTicket.ToString();
                                    _objTickDist.YearID = UserSession.Inst.CurrentRaffleYearID;


                                    int userFk = _provider.AddDistUser(objDistUser, objDistUser.UserType);

                                    if (userFk > 0)
                                    {
                                        _objTickDist.RAFFLE_FK = objDistUser.RAFFLE_FK;
                                        _objTickDist.CHURCH_FK = objDistUser.CHURCH_FK;
                                        _objTickDist.MEM2MEM_FK = objDistUser.MEM2MEM_FK;
                                        _objTickDist.MEMBER_FK = objDistUser.MEMBER_FK;
                                        _objTickDist.Dist_user_Fk = userFk;
                                        _provider.AddTicketDistribution(_objTickDist, objDistUser);

                                        successRec++;
                                    }

                                }
                                else
                                {

                                    objFailed.FromTicket = obj.FromTicket;
                                    objFailed.ToTicket = obj.ToTicket;
                                    objFailed.GivenTo = obj.Name;
                                    objFailed.TotalTicket = obj.TotalTicket;
                                    objFailed.Status = "FAILED";
                                    objFailed.ErrorMsg = "Previous Distribution User Not Found !";
                                    lstFailedData.Add(objFailed);

                                    // errror message
                                }

                            }
                            else
                            {
                                objFailed.FromTicket = obj.FromTicket;
                                objFailed.ToTicket = obj.ToTicket;
                                objFailed.GivenTo = obj.Name;
                                objFailed.TotalTicket = obj.TotalTicket;
                                objFailed.Status = "FAILED";
                                objFailed.ErrorMsg = "Previous Ticket Distribution Not Found !";
                                lstFailedData.Add(objFailed);
                            }

                        }
                        else
                        {
                            objFailed.FromTicket = obj.FromTicket;
                            objFailed.ToTicket = obj.ToTicket;
                            objFailed.GivenTo = obj.Name;
                            objFailed.TotalTicket = obj.TotalTicket;
                            objFailed.Status = "FAILED";
                            objFailed.ErrorMsg = "Already entered ticket for same distribution !";
                            lstFailedData.Add(objFailed);
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
        string ExcelPath = string.Format("/Data/import-distributioin-template.xls");

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        HSSFSheet sheet1 = hssfworkbook.CreateSheet("DistributionData");

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


        sheet1.CreateRow(0).CreateCell(0).SetCellValue("Total Tix");
        sheet1.CreateRow(0).CreateCell(1).SetCellValue("Start Tix");
        sheet1.CreateRow(0).CreateCell(2).SetCellValue("End Tix");
        sheet1.CreateRow(0).CreateCell(3).SetCellValue("Name");
        sheet1.CreateRow(0).CreateCell(4).SetCellValue("Street Address");
        sheet1.CreateRow(0).CreateCell(5).SetCellValue("City");
        sheet1.CreateRow(0).CreateCell(6).SetCellValue("State");
        sheet1.CreateRow(0).CreateCell(7).SetCellValue("Zip");
        sheet1.CreateRow(0).CreateCell(8).SetCellValue("Phone");



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
            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [DistributionData$]", oledbConn))
            {
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                DataSet ds = new DataSet();
                oleda.Fill(ds);

                dt = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {

            oledbConn.Close();
        }

        return dt;

    }

    public partial class ImportDistData
    {
        public string TotalTicket { get; set; }
        public string FromTicket { get; set; }
        public string ToTicket { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string phone { get; set; }
    }


    public partial class ImportDistDataStatus
    {
        public string TotalTicket { get; set; }
        public string FromTicket { get; set; }
        public string ToTicket { get; set; }
        public string GivenTo { get; set; }
        public string Status { get; set; }
        public string ErrorMsg { get; set; }
    }
}