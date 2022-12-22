using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory_InventorySearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            InventoryController inv = new InventoryController();
            List<GetWineSearchListResult> inventoryList = inv.GetSearchList("", "");
            rptWine.DataSource = inventoryList;
            rptWine.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        InventoryController inv = new InventoryController();

        List<GetWineSearchListResult> inventoryList = inv.GetSearchList(txtWineName.Text, txtBarcode.Text);

        rptWine.DataSource = inventoryList;
        rptWine.DataBind();
    }
    protected void btnAddInv_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddInventory.aspx");
    }

     [WebMethod]
    public static string DeleteInventory(string tid)
    {
        string result = string.Empty;
        InventoryController _controller = new InventoryController();
        int ret = _controller.DeleteInventory(Convert.ToInt32(tid),Convert.ToInt32(tid));
        result = ret > 0 ? "true" : "false";
        return result;
    }


     protected void btncancel_Click(object sender, EventArgs e)
     {
         InventoryController inv = new InventoryController();
         List<GetWineSearchListResult> inventoryList = new List<GetWineSearchListResult>();
         rptWine.DataSource = inventoryList;
         rptWine.DataBind();
         txtWineName.Text = "";
         txtBarcode.Text = "";
     }

     protected void btnExport_Click(object sender, EventArgs e)
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
     
         int row = 1;


         InventoryController inv = new InventoryController();
         List<GetWineSearchListResult> inventoryList = inv.GetSearchList(txtWineName.Text, txtBarcode.Text);

         foreach (GetWineSearchListResult obj in inventoryList)
         {
             sheet1.CreateRow(row).CreateCell(0).SetCellValue(Convert.ToString(obj.RecNo));
             sheet1.CreateRow(row).CreateCell(1).SetCellValue(Convert.ToString(obj.WineListCategory));
             sheet1.CreateRow(row).CreateCell(2).SetCellValue(obj.WinelistLineNo);
             sheet1.CreateRow(row).CreateCell(3).SetCellValue(obj.Description);
             sheet1.CreateRow(row).CreateCell(4).SetCellValue(obj.BottleSizeName);
             sheet1.CreateRow(row).CreateCell(5).SetCellValue(obj.BottlePrize);
             sheet1.CreateRow(row).CreateCell(6).SetCellValue(Convert.ToString(obj.Cost_Wholesale));
             sheet1.CreateRow(row).CreateCell(7).SetCellValue(obj.BarCode);
             sheet1.CreateRow(row).CreateCell(8).SetCellValue(obj.Category_Name);
             sheet1.CreateRow(row).CreateCell(9).SetCellValue(obj.HeadingName);
             sheet1.CreateRow(row).CreateCell(10).SetCellValue(obj.SubHeadingName);
             sheet1.CreateRow(row).CreateCell(11).SetCellValue((obj.AlphaSort));
             sheet1.CreateRow(row).CreateCell(12).SetCellValue(obj.Location1_name);
             sheet1.CreateRow(row).CreateCell(13).SetCellValue(Convert.ToString(obj.Location1_Qty));
             sheet1.CreateRow(row).CreateCell(14).SetCellValue(obj.Location2_name);
             sheet1.CreateRow(row).CreateCell(15).SetCellValue(Convert.ToString(obj.Location2_Qty));
             sheet1.CreateRow(row).CreateCell(16).SetCellValue(obj.Location3_name);
             sheet1.CreateRow(row).CreateCell(17).SetCellValue(Convert.ToString(obj.Location3_qty));
           
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



     protected void btnImport_Click(object sender, EventArgs e)
     {
         Response.Redirect("ImportWineData.aspx");
     }
     protected void btnStockUpdate_Click(object sender, EventArgs e)
     {
         Response.Redirect("UpdateStock.aspx");
     }
}