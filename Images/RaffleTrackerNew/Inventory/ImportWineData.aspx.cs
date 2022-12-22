using System;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Web.Services;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Data.OleDb;

public partial class Inventory_ImportWineData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnDownloadTemplate_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string ExcelPath = string.Format("/data/Invenstory-Stock-{0}.xls", DateTime.Now.ToString("yyyyMMddHHMMss"));

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        HSSFSheet sheet1 = hssfworkbook.CreateSheet("InventoryData");

        sheet1.CreateRow(0).CreateCell(0).SetCellValue("RecNo");
        sheet1.CreateRow(0).CreateCell(1).SetCellValue("WineListCategory");
        sheet1.CreateRow(0).CreateCell(2).SetCellValue("wineListLineNo");
        sheet1.CreateRow(0).CreateCell(3).SetCellValue("Description");
        sheet1.CreateRow(0).CreateCell(4).SetCellValue("BottleSize");
        sheet1.CreateRow(0).CreateCell(5).SetCellValue("BottlePrice");
        sheet1.CreateRow(0).CreateCell(6).SetCellValue("CostWholesale");
        sheet1.CreateRow(0).CreateCell(7).SetCellValue("Barcode");
        sheet1.CreateRow(0).CreateCell(8).SetCellValue("Category");
        sheet1.CreateRow(0).CreateCell(9).SetCellValue("Heading");
        sheet1.CreateRow(0).CreateCell(10).SetCellValue("SubHeading");
        sheet1.CreateRow(0).CreateCell(11).SetCellValue("AlphaSort");
        sheet1.CreateRow(0).CreateCell(12).SetCellValue("Loc1");
        sheet1.CreateRow(0).CreateCell(13).SetCellValue("Count1");
        sheet1.CreateRow(0).CreateCell(14).SetCellValue("Loc2");
        sheet1.CreateRow(0).CreateCell(15).SetCellValue("Count2");
        sheet1.CreateRow(0).CreateCell(16).SetCellValue("Loc3");
        sheet1.CreateRow(0).CreateCell(17).SetCellValue("Count3");

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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventorySearch.aspx");
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (fldCSVFile.HasFile && fldCSVFile.PostedFile.FileName != "")
        {
            string fileName = fldCSVFile.PostedFile.FileName;
            if (Path.GetExtension(fileName).ToUpper() == ".CSV" || Path.GetExtension(fileName).ToUpper() == ".XLSX" || Path.GetExtension(fileName).ToUpper() == ".XLS")
            {
                InventoryController _Controller = new InventoryController();
                Inventory objInv = new Inventory();

                List<ImportWineData> lstimportWinedata = new List<ImportWineData>();
                ImportWineData objImportWineData = new ImportWineData();
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
                        objImportWineData = new ImportWineData();
                        if (objImportWineData != null)
                        {

                            objImportWineData.RecNo = Convert.ToString(dr["RecNo"]);
                            objImportWineData.WineListCategory = Convert.ToString(dr["WineListCategory"]);
                            objImportWineData.WineListlineNo = Convert.ToString(dr["WineListLineNo"]);
                            objImportWineData.Description = Convert.ToString(dr["Description"]);
                            objImportWineData.BottleSize = Convert.ToString(dr["BottleSize"]);
                            objImportWineData.BottlePrize = Convert.ToString(dr["BottlePrice"]);
                            objImportWineData.CostWholeSale = Convert.ToString(dr["CostWholeSale"]);
                            objImportWineData.Barcode = Convert.ToString(dr["BarCode"]);
                            objImportWineData.Category = Convert.ToString(dr["Category"]);
                            objImportWineData.Heading = Convert.ToString(dr["Heading"]);
                            objImportWineData.SubHeading = Convert.ToString(dr["SubHeading"]);
                            objImportWineData.Alphasort = Convert.ToString(dr["Alphasort"]);
                            objImportWineData.Loc1 = Convert.ToString(dr["Loc1"]);
                            objImportWineData.Count1 = Convert.ToString(dr["Count1"]);
                            objImportWineData.Loc2 = Convert.ToString(dr["Loc2"]);
                            objImportWineData.Count2 = Convert.ToString(dr["Count2"]);
                            objImportWineData.Loc3 = Convert.ToString(dr["Loc3"]);
                            //objImportWineData.Count3 = Convert.ToString(dr["Count3"]);
                            lstimportWinedata.Add(objImportWineData);
                        }
                    }

                }
                List<ImportWineDataStutas> lstFailedData = new List<ImportWineDataStutas>();
                ImportWineDataStutas objFailed = new ImportWineDataStutas();

                List<BottleSize> lstBottleSize = _Controller.GetBottlesizelist();
                List<Category> lstCategory = _Controller.GetCategoryList();
                List<Heading> lstHeading = _Controller.Getheadinglist();
                List<SubHeading> lstSubHeading = _Controller.GetSubHeadingList();

                if (lstimportWinedata.Count > 0)
                {
                    foreach (ImportWineData obj in lstimportWinedata)
                    {

                        Inventory objInventory = new Inventory();

                        if (!string.IsNullOrEmpty(obj.RecNo))
                        {
                            objInventory.RecNo = Convert.ToInt32(obj.RecNo);
                        }
                        if (!string.IsNullOrEmpty(obj.WineListCategory))
                        {
                            objInventory.WinelistCategory = Convert.ToDecimal(obj.WineListCategory);
                        }
                        objInventory.WineListLineNo = obj.WineListlineNo;
                        objInventory.Description = obj.Description;
                        objInventory.Wine_name = obj.Description;

                        if (lstBottleSize.Any(x => x.BottleSizeName.ToLower().Trim() == obj.BottleSize.ToLower().Trim()))
                        {
                            objInventory.BottleSize_ID = lstBottleSize.Where(x => x.BottleSizeName.ToLower().Trim() == obj.BottleSize.ToLower().Trim()).FirstOrDefault().BottleSizeId;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(obj.BottleSize))
                            {
                                int bottleId = _Controller.AddBottleSize(new BottleSize { BottleSizeName = obj.BottleSize });
                                objInventory.BottleSize_ID = bottleId;
                            }
                        }
                        objInventory.BottlePrize = obj.BottlePrize;
                        if (!string.IsNullOrEmpty(obj.CostWholeSale))
                        {
                            objInventory.Cost_Wholesale = Convert.ToDecimal(obj.CostWholeSale);
                        }
                        objInventory.Barcode = obj.Barcode;
                        if (lstCategory.Any(x => x.Category_Name.ToLower().Trim() == obj.Category.ToLower().Trim()))
                        {
                            objInventory.Category_ID = lstCategory.Where(x => x.Category_Name.ToLower().Trim() == obj.Category.ToLower().Trim()).FirstOrDefault().Category_ID;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(obj.Category))
                            {
                                int catId = _Controller.AddCategory(new Category { Category_Name = obj.Category });
                                objInventory.Category_ID = catId;
                            }
                        }
                        if (lstHeading.Any(x => x.HeadingName.ToLower().Trim() == obj.Heading.ToLower().Trim()))
                        {
                            objInventory.Heading_ID = lstHeading.Where(x => x.HeadingName.ToLower().Trim() == obj.Heading.ToLower().Trim()).FirstOrDefault().HeadingId;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(obj.Heading))
                            {
                                int headId = _Controller.AddHeading(new Heading { HeadingName = obj.Heading });
                                objInventory.Heading_ID = headId;
                            }
                        }

                        if (lstSubHeading.Any(x => x.SubHeadingName.ToLower().Trim() == obj.SubHeading.ToLower().Trim()))
                        {
                            objInventory.SubHeading_ID = lstSubHeading.Where(x => x.SubHeadingName.ToLower().Trim() == obj.SubHeading.ToLower().Trim()).FirstOrDefault().SubHeadingId;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(obj.SubHeading))
                            {
                                int subHeadId = _Controller.AddSubHeading(new SubHeading { SubHeadingName = obj.SubHeading });
                                objInventory.SubHeading_ID = subHeadId;
                            }
                        }
                        objInventory.AlphaSort = obj.Alphasort;
                        objInventory.Loc1 = obj.Loc1;
                        objInventory.Count1 = obj.Count1;
                        objInventory.Loc2 = obj.Loc2;
                        objInventory.Count2 = obj.Count2;
                        objInventory.Count3 = obj.Count3;
                        objInventory.CreatedDate = DateTime.Now;
                        objInventory.ModifiedDate = DateTime.Now;


                        int Result = _Controller.AddInventoryImport(objInventory);

                        if (Result > 0)
                        {
                            objFailed = new ImportWineDataStutas();
                            objFailed.WineName = obj.Description;
                            objFailed.Category = obj.Category;
                            objFailed.BottleSize = obj.BottleSize;
                            objFailed.Message = " Added or Updated Successfully. !";
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
            using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [InventoryData$]", oledbConn))
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
}

public partial class ImportWineData
{
    public string RecNo { get; set; }
    public string WineListCategory { get; set; }
    public string WineListlineNo { get; set; }
    public string Description { get; set; }
    public string WineName { get; set; }
    public string BottleSize { get; set; }
    public string BottlePrize { get; set; }
    public string CostWholeSale { get; set; }
    public string Barcode { get; set; }
    public string Category { get; set; }
    public string Heading { get; set; }
    public string SubHeading { get; set; }
    public string Alphasort { get; set; }
    public string Loc1 { get; set; }
    public string Count1 { get; set; }
    public string Loc2 { get; set; }
    public string Count2 { get; set; }
    public string Loc3 { get; set; }
    public string Count3 { get; set; }
}

public partial class ImportWineDataStutas
{
    public string WineName { get; set; }
    public string BottleSize { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    
}