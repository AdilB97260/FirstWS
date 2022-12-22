using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class Inventory_ManageCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblErrorMsg.Text = "";
            divError.Visible = false;

            InventoryController _Controller = new InventoryController();
            Category objCategory = new Category();
            objCategory.Category_Name = txtName.Text;
            int Result = _Controller.AddCategory(objCategory);
            if (Result > 0)
            {
                if (Request.QueryString != null && Request.QueryString["m"] != null && Request.QueryString["m"] == "inv")
                {
                    int id = Request.QueryString != null && Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
                    Response.Redirect("AddInventory.aspx?id=" + id + "&cat=" + Result);
                }
                else
                {

                    Response.Redirect("AddInventory.aspx?cat = " + Result);
                }
            }
            else if (Result==0)
            {
                lblErrorMsg.Text = "Duplicate category found. !";
                divError.Visible = true;
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        
            if (Request.QueryString != null && Request.QueryString["m"] != null && Request.QueryString["m"] == "inv")
            {
                int id = Request.QueryString != null && Request.QueryString["id"] != null ? Convert.ToInt32(Request.QueryString["id"]) : 0;
                Response.Redirect("AddInventory.aspx?id=" + id);
            }
            else
            {

                Response.Redirect("AddInventory.aspx");
            }
        
    }
}