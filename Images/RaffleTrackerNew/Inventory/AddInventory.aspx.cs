using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.IO;

public partial class Inventory_AddInventory : System.Web.UI.Page
{
    InventoryController _inventoryController = new InventoryController();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            BindCategory();
            BindBottleSize();
            BindHeading();
            BindSubHeading();


            
            
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                hdnInventoryID.Value = Request.QueryString["id"];
            }

            if (hdnInventoryID.Value != "")
            {

                InventoryController _controller = new InventoryController();
                Inventory objInventory = _controller.GetInventoryId(Convert.ToInt32(hdnInventoryID.Value));
                Inventory_Stock objInvStock = _controller.GetInventoryStockId(Convert.ToInt32(hdnInventoryID.Value));
                txtWineName.Text = objInventory.Wine_name;
                txtBarcode.Text = objInventory.Barcode;
                txtRecNo.Text = Convert.ToString(objInventory.RecNo);
                ddlCategory.Text = Convert.ToString(objInventory.Category_ID);
                txtDescription.Text = objInventory.Description;         
                ddlBottleSizeId.Text = Convert.ToString(objInventory.BottleSize_ID);
                txtBottleprice.Text = objInventory.BottlePrize;
                txtWholesale.Text = Convert.ToString(objInventory.Cost_Wholesale);
                ddlHeadingId.SelectedValue = Convert.ToString(objInventory.Heading_ID);
                ddlSubHeadingId.SelectedValue = Convert.ToString(objInventory.SubHeading_ID);

                if (objInvStock != null)
                {
                    txtLoc1Qty.Text = Convert.ToString(objInvStock.Location1_Qty);
                    txtLoc2Qty.Text = Convert.ToString(objInvStock.Location2_Qty);
                    txtLoc3Qty.Text = Convert.ToString(objInvStock.Location3_qty);
                    txtLocName.Text = objInvStock.Location1_name;
                    txtLoc2Name.Text = objInvStock.Location2_name;
                    txtLoc3Name.Text = objInvStock.Location3_name;
                }
            }

            if (Session["Inventory"] != null)
            {
                Inventory objInventory = new Inventory();
                Inventory_Stock objStock = new Inventory_Stock();
                objInventory = (Inventory)Session["Inventory"];
                txtWineName.Text = objInventory.Wine_name;
                txtBarcode.Text = objInventory.Barcode;
                txtRecNo.Text = Convert.ToString(objInventory.RecNo);
                ddlCategory.Text = Convert.ToString(objInventory.Category_ID);
                txtDescription.Text = objInventory.Description;
                ddlBottleSizeId.SelectedValue = Convert.ToString(objInventory.BottleSize_ID);
                txtBottleprice.Text = objInventory.BottlePrize;
                ddlHeadingId.SelectedValue = Convert.ToString(objInventory.Heading_ID);
                ddlSubHeadingId.SelectedValue = Convert.ToString(objInventory.SubHeading_ID);
                txtWholesale.Text = Convert.ToString(objInventory.Cost_Wholesale);
               
                Session["Inventory"] = null;
            }

            if (Request.QueryString != null && Request.QueryString["cat"] != null)
            {
                string catID = Request.QueryString["cat"];

                ddlCategory.SelectedValue = catID;
               
            }
            if (Request.QueryString != null && Request.QueryString["bot"] != null)
            {
                string botID = Request.QueryString["bot"];

                ddlBottleSizeId.SelectedValue = botID;

            }
            if (Request.QueryString != null && Request.QueryString["head"] != null)
            {
                string headID = Request.QueryString["head"];

                ddlHeadingId.SelectedValue = headID;

            }
            if (Request.QueryString != null && Request.QueryString["sub"] != null)
            {
                string subID = Request.QueryString["sub"];

                ddlSubHeadingId.SelectedValue = subID;

            }
            

        }



    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (Page.IsValid)
        {

            if (hdnInventoryID.Value != "")
            {
                InventoryController _controller = new InventoryController();
                Inventory objInv = new Inventory();
                Inventory_Stock objInvStock = new Inventory_Stock();
                objInv.Inventory_ID = Convert.ToInt32(hdnInventoryID.Value);
                objInv.Wine_name = txtWineName.Text;
                objInv.Barcode = txtBarcode.Text;
                if(!string.IsNullOrEmpty(txtRecNo.Text))
                {
                    objInv.RecNo = Convert.ToInt32(txtRecNo.Text);
                }
                objInv.Category_ID = Convert.ToInt32(ddlCategory.SelectedValue);
                objInv.Description = txtDescription.Text;
                objInv.BottleSize_ID = Convert.ToInt32(ddlBottleSizeId.SelectedValue);
                objInv.BottlePrize = txtBottleprice.Text;
                objInv.Cost_Wholesale = !string.IsNullOrEmpty(txtWholesale.Text) ? Convert.ToDecimal(txtWholesale.Text) : 0;
                objInv.Heading_ID = Convert.ToInt32(ddlHeadingId.SelectedValue);
                if (!string.IsNullOrEmpty(ddlSubHeadingId.SelectedValue))
                {
                    objInv.SubHeading_ID = Convert.ToInt32(ddlSubHeadingId.SelectedValue);
                }
                if (txtLocName.Text != "")
                {
                    objInvStock.Location1_name = txtLocName.Text;
                }
                if (txtLoc2Name.Text != "")
                {
                    objInvStock.Location2_name = txtLoc2Name.Text;
                }
                if (txtLoc3Name.Text != "")
                {
                    objInvStock.Location3_name = txtLoc3Name.Text;
                }
                if (txtLoc1Qty.Text != "")
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



                bool result = _controller.UpdateInventory(objInv, objInvStock);

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
            else
            {
                InventoryController _controller = new InventoryController();
                Inventory objInv = new Inventory();
                Inventory_Stock objInvStock = new Inventory_Stock();
                objInv.Wine_name = txtWineName.Text;
                objInv.Barcode = txtBarcode.Text;
                objInv.RecNo = !string.IsNullOrEmpty(txtRecNo.Text) ? Convert.ToInt32(txtRecNo.Text) : 0;
                objInv.Category_ID = Convert.ToInt32(ddlCategory.SelectedValue);
                objInv.Description = txtDescription.Text;
                objInv.BottleSize_ID = Convert.ToInt32(ddlBottleSizeId.SelectedValue);
                objInv.BottlePrize = txtBottleprice.Text;
                objInv.Cost_Wholesale = !string.IsNullOrEmpty(txtWholesale.Text) ? Convert.ToDecimal(txtWholesale.Text) : 0;
                objInv.Heading_ID = Convert.ToInt32(ddlHeadingId.SelectedValue);
                objInv.SubHeading_ID =   !string.IsNullOrEmpty(ddlSubHeadingId.SelectedValue) ?  Convert.ToInt32(ddlSubHeadingId.SelectedValue) : 0;
                objInv.CreatedDate = System.DateTime.Now;
                objInv.ModifiedDate = System.DateTime.Now;
               

                objInvStock.Location1_Qty = txtLoc1Qty.Text != "" ? Convert.ToInt32(txtLoc1Qty.Text) : 0;
                objInvStock.Location2_Qty = txtLoc2Qty.Text != "" ? Convert.ToInt32(txtLoc2Qty.Text) : 0;
                objInvStock.Location3_qty = txtLoc3Qty.Text != "" ? Convert.ToInt32(txtLoc3Qty.Text) : 0;
                if (txtLocName.Text != "")
                {
                    objInvStock.Location1_name = txtLocName.Text;
                }
                if (txtLoc2Name.Text != "")
                {
                    objInvStock.Location2_name = txtLoc2Name.Text;
                }
                if (txtLoc3Name.Text != "")
                {
                    objInvStock.Location3_name = txtLoc3Name.Text;
                }

                int result = _controller.AddInventory(objInv, objInvStock);

                if (result > 0)
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

    }
    private void BindCategory()
    {
        Category objCategory = new Category();
        ddlCategory.DataSource = _inventoryController.GetCategoryList();
        ddlCategory.DataTextField = "Category_name";
        ddlCategory.DataValueField = "Category_Id";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, new ListItem { Text = "Select Category", Value = "", Selected = true });
    }
    private void BindBottleSize()
    {
        BottleSize objBottleSize = new BottleSize();
        ddlBottleSizeId.DataSource = _inventoryController.GetBottlesizelist();
        ddlBottleSizeId.DataTextField = "BottleSizeName";
        ddlBottleSizeId.DataValueField = "BottleSizeId";
        ddlBottleSizeId.DataBind();
        ddlBottleSizeId.Items.Insert(0, new ListItem { Text = "Select BottleSize", Value = "", Selected = true });
    }
    private void BindHeading()
    {
        Heading objHeading = new Heading();
        ddlHeadingId.DataSource = _inventoryController.Getheadinglist();

        ddlHeadingId.DataTextField = "headingName";
        ddlHeadingId.DataValueField = "HeadingId";
        ddlHeadingId.DataBind();
        ddlHeadingId.Items.Insert(0, new ListItem { Text = "Select Heading", Value = "", Selected = true });
    }

    private void BindSubHeading()
    {
        SubHeading objSubHeading = new SubHeading();
        ddlSubHeadingId.DataSource = _inventoryController.GetSubHeadingList();

        ddlSubHeadingId.DataTextField = "SubHeadingName";
        ddlSubHeadingId.DataValueField = "SubHeadingId";
        ddlSubHeadingId.DataBind();
        ddlSubHeadingId.Items.Insert(0, new ListItem { Text = "Select SubHeading", Value = "", Selected = true });
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventorySearch.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("InventorySearch.aspx");
    }
    protected void BtnAddBottleSize_Click(object sender, EventArgs e)
    {
        BindValueFromSession();
        if (hdnInventoryID.Value != "")
        {
            Response.Redirect("AddBottleSize.aspx?m=inv&id=" + hdnInventoryID.Value);
        }
        else
        {
            Response.Redirect("AddBottleSize.aspx");
        }
        
    }
    private void BindValueFromSession()
    {
        Inventory objInventory = new Inventory();
       
        objInventory.Wine_name = txtWineName.Text;
        objInventory.Barcode = txtBarcode.Text;
        objInventory.RecNo = Convert.ToInt32(txtRecNo.Text);
        objInventory.Category_ID = Convert.ToInt32(ddlCategory.SelectedValue);
        objInventory.Description = txtDescription.Text;
        objInventory.BottleSize_ID = Convert.ToInt32(ddlBottleSizeId.SelectedValue);
        objInventory.BottlePrize = txtBottleprice.Text;
        objInventory.Heading_ID = Convert.ToInt32(ddlSubHeadingId.SelectedValue);
        objInventory.SubHeading_ID = Convert.ToInt32(ddlSubHeadingId.SelectedValue);
       

        if (!string.IsNullOrEmpty(txtWholesale.Text))
        {
            objInventory.Cost_Wholesale = Convert.ToDecimal(txtWholesale.Text);
        }
        
        objInventory.Heading_ID = Convert.ToInt32(ddlHeadingId.SelectedValue);
        objInventory.SubHeading_ID = Convert.ToInt32(ddlSubHeadingId.SelectedValue);
        Session["Inventory"] = objInventory;
        
       
    }
    protected void BtnaddHeading_Click(object sender, EventArgs e)
    {
        BindValueFromSession();
        if (hdnInventoryID.Value != "")
        {
            Response.Redirect("HeadingMaster.aspx?m=inv&id=" + hdnInventoryID.Value);
        }
        else
        {
            Response.Redirect("HeadingMaster.aspx");
        }
    }
    protected void btnSubHeading_Click(object sender, EventArgs e)
    {
        BindValueFromSession();
        if (hdnInventoryID.Value != "")
        {
            Response.Redirect("SubHeadingMaster.aspx?m=inv&id=" + hdnInventoryID.Value);
        }
        else
        {
            Response.Redirect("SubHeadingMaster.aspx");
        }
    }
    protected void btnCategory_Click(object sender, EventArgs e)
    {
        BindValueFromSession();
        if (hdnInventoryID.Value != "")
        {
            Response.Redirect("ManageCategory.aspx?m=inv&id=" + hdnInventoryID.Value);
        }
        else
        {
            Response.Redirect("ManageCategory.aspx");
        }
    }
}