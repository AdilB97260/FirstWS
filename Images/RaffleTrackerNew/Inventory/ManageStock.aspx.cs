using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.IO;

public partial class Inventory_ManageStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                hdnInventoryID.Value = Request.QueryString["id"];
            }
            if (hdnInventoryID.Value != "")
            {
                InventoryController _controller = new InventoryController();
                GetInventoryByIdResult objinvResult = _controller.GetInventoryWithStock(Convert.ToInt32(hdnInventoryID.Value));

                if (objinvResult != null)
                {
                    lblWineName.Text = objinvResult.Wine_name;
                    lblBarcode.Text = objinvResult.BarCode;
                    lblBottleSize.Text = Convert.ToString(objinvResult.BottleSize_ID);
                    txtLoc1Qty.Text = Convert.ToString(objinvResult.Location1_Qty);
                    txtLoc2Qty.Text = Convert.ToString(objinvResult.Location2_Qty);
                    txtLoc3Qty.Text = Convert.ToString(objinvResult.Location3_qty);
                }
                
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
           
                InventoryController _controller = new InventoryController();
               // Inventory objInv = new Inventory();
                Inventory_Stock objInvStock = new Inventory_Stock();
               /* objInv.Wine_name = lblWineName.Text;
                objInv.Barcode = lblBarcode.Text;
                objInv.BottleSize = lblBottleSize.Text;*/
               if(txtLoc1Qty.Text !=null)
                {
                    objInvStock.Location1_Qty = Convert.ToInt32(txtLoc1Qty.Text);
                }
                if (txtLoc2Qty.Text != "")
                {
                    objInvStock.Location2_Qty = Convert.ToInt32(txtLoc2Qty.Text);
                }
                if (txtLoc3Qty.Text != "")
                {
                    objInvStock.Location3_qty = Convert.ToInt32(txtLoc3Qty.Text);
                }

                objInvStock.Inventory_ID = Convert.ToInt32(hdnInventoryID.Value);

                bool result = _controller.UpdateManageStock( objInvStock);
                if (result == true)
                {
                    Response.Redirect("InventorySearch.aspx");
                }
                else
                {
                    lblErrorMsg.Text = "Failed ! Record saved failed.";
                    lblErrorMsg.Visible = true;
                }
            
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventorySearch.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventorySearch.aspx");
    }
}