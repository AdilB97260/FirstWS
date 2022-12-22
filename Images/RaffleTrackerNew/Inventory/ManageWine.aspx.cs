using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.IO;

public partial class Inventory_ManageWine : System.Web.UI.Page
{
    InventoryController _inventoryController = new InventoryController();
    protected void Page_Load(object sender, EventArgs e)
    {
        

        
        BindCategory();
        BindWineType();
    }
    private void BindCategory()
    {
        Category objCategory = new Category();
        ddlCategory.DataSource = _inventoryController.GetCategoryList();

        ddlCategory.DataTextField = "Category_name";
        ddlCategory.DataValueField = "Category_Id";

        ddlCategory.DataBind(); 
    }
    private void BindWineType()
    {
        Inventory objInventory = new Inventory();
        ddlWineType.DataSource = _inventoryController.GetInvestoryList();
        ddlWineType.DataTextField = "Wine_name";
        ddlWineType.DataValueField = "Inventory_ID";
        ddlWineType.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            InventoryController _controller = new InventoryController();
            Inventory_Stock objStock = new Inventory_Stock();
            objStock.Location1_Qty = Convert.ToInt32(txtL1Qty.Text);
            objStock.Location2_Qty = Convert.ToInt32(txtL2Qty.Text);
            objStock.Location3_qty = Convert.ToInt32(txtL3Qty.Text);
            int Result = _controller.AddInventoryStock(objStock);
        }
    }
}