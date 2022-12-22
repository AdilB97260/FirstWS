using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inventory_UpdateStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        divError.Visible = false;

        if (!Page.IsPostBack)
        {
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                hdnInventoryID.Value = Request.QueryString["id"];
                dvSearch.Visible = false;
            }
            if (hdnInventoryID.Value != "")
            {
                InventoryController _controller = new InventoryController();
                GetInventoryByIdResult objinvResult = _controller.GetInventoryWithStock(Convert.ToInt32(hdnInventoryID.Value));

                if (objinvResult != null)
                {
                    lblWineName.Text = objinvResult.Wine_name;
                    lblBottleSize.Text = Convert.ToString(objinvResult.BottleSizeName);
                    txtQty1.Text = Convert.ToString(objinvResult.Location1_Qty);
                    txtQty2.Text = Convert.ToString(objinvResult.Location2_Qty);
                    txtQty3.Text = Convert.ToString(objinvResult.Location3_qty);
                    txtloc1Name.Text = objinvResult.Location1_name;
                    txtLoc2Name.Text = objinvResult.Location2_name;
                    txtloc3Name.Text = objinvResult.Location3_name;
                   
                }

            }
        }
    }
    protected void btnAddInv_Click(object sender, EventArgs e)
    {

        lblErrorMsg.Text = "";
        divError.Visible = false;

        Inventory_Stock objStock = new Inventory_Stock();
        InventoryController _controller = new InventoryController();

        if (txtQty1.Text != "")
        {
            objStock.Location1_Qty = Convert.ToInt32(txtQty1.Text);
        }
        if (txtQty2.Text != "")
        {
            objStock.Location2_Qty = Convert.ToInt32(txtQty2.Text);
        }
        if (txtQty3.Text != "")
        {
            objStock.Location3_qty = Convert.ToInt32(txtQty3.Text);
        }
        if(txtloc1Name.Text !="")
        {
            objStock.Location1_name = txtloc1Name.Text;
        }
        if (txtLoc2Name.Text != "")
        {
            objStock.Location2_name = txtLoc2Name.Text;
        }
        if (txtloc3Name.Text != "")
        {
            objStock.Location3_name = txtloc3Name.Text;
        }

        objStock.Inventory_ID = Convert.ToInt32(hdnInventoryID.Value);

        bool result = _controller.UpdateManageStock(objStock);
        if (result == true)
        {
            Response.Redirect("InventorySearch.aspx");
        }
        else
        {
            lblErrorMsg.Text = "Failed ! Record saved failed.";
            divError.Visible = true;
        }

        
    }
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventorySearch.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        InventoryController _InventoryController = new InventoryController();
        GetrecSearchListResult objRecList = _InventoryController.GetRecSearchList(txtRecNo.Text.Trim() !="" ? Convert.ToInt32(txtRecNo.Text):0, txtBarcode.Text);
        if (objRecList != null)
        {
            txtloc1Name.Text = objRecList.Location1_name;
            txtLoc2Name.Text = objRecList.Location2_name;
            txtloc3Name.Text = objRecList.Location3_name;
            txtQty1.Text = Convert.ToString(objRecList.Location1_Qty);
            txtQty2.Text = Convert.ToString(objRecList.Location2_Qty);
            txtQty3.Text = Convert.ToString(objRecList.Location3_qty);
            lblWineName.Text = objRecList.Wine_name;
            lblBottleSize.Text = objRecList.BottleSizeName;
            hdnInventoryID.Value = Convert.ToString(objRecList.Inventory_ID);
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        txtloc1Name.Text = "";
        txtLoc2Name.Text = "";
        txtloc3Name.Text = "";
        txtQty1.Text = "";
        txtQty2.Text = "";
        txtQty3.Text = "";
        lblWineName.Text = "";
        lblBottleSize.Text = "";
        txtBarcode.Text = "";
        txtRecNo.Text = "";

    }
}