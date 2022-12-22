using RaffleModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ManageExpencess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (UserSession.Inst.UserType == "ADMIN")
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn btn-primary'>Manage Expenses</span></div>", "Manage Expenses");
            }

            txtExpDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                hdnUserId.Value = Request.QueryString["id"];

                ExpencessController _provider = new ExpencessController();

                Expencess objExp = _provider.GetExpencess(Convert.ToInt32(Request.QueryString["id"]));
                if (objExp != null)
                {
                    txtAmount.Text = Convert.ToString((objExp.Amount).ToString("0.00"));
                    txtDesc.Text = objExp.description;
                    txtExpensesName.Text = objExp.expences_name;
                    txtToWhomeGiven.Text = objExp.given_to;
                    txtExpDate.Text = objExp.expences_date.ToString("MM/dd/yyyy");
                }

            }

        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/expencesList.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        if (Page.IsValid)
        {

            ExpencessController _controller = new ExpencessController();

            string uploadMessage = "";
            string path = "";

            if (oFile.Value != "")
            {

                path = UploadAttachment(ref uploadMessage);
            }
            
            if (hdnUserId.Value != "")
            {

                
                
                    Expencess objExpencess = new Expencess();
                    objExpencess.expences_id = Convert.ToInt32(hdnUserId.Value);
                    objExpencess.expences_date = Convert.ToDateTime(txtExpDate.Text);
                    objExpencess.Amount = Convert.ToDecimal(txtAmount.Text);
                    objExpencess.description = txtDesc.Text;
                    objExpencess.expences_name = txtExpensesName.Text;
                    objExpencess.given_to = txtToWhomeGiven.Text;
                    objExpencess.Category = ddlCategory.SelectedValue;

                    if(!string.IsNullOrEmpty(path))
                    {
                        objExpencess.Receipt = path;
                    }

                    int result = _controller.UpdateExpenses(objExpencess);

                    if (result > 0)
                    {

                        Response.Redirect("/admin/expencesList.aspx");

                    }

                
            }
            else
            {
                Expencess objExpencess = new Expencess();
                objExpencess.expences_date = Convert.ToDateTime(txtExpDate.Text);
                objExpencess.Amount = Convert.ToDecimal(txtAmount.Text);
                objExpencess.description = txtDesc.Text;
                objExpencess.expences_name = txtExpensesName.Text;
                objExpencess.given_to = txtToWhomeGiven.Text;
                objExpencess.year_id = UserSession.Inst.CurrentRaffleYearID;
                objExpencess.Category = ddlCategory.SelectedValue;
                if (!string.IsNullOrEmpty(path))
                {
                    objExpencess.Receipt = path;
                }

                int result = _controller.AddExpenses(objExpencess);

                if (result > 0)
                {

                    Response.Redirect("/admin/expencesList.aspx");

                }
            }

        }
    }

    private string UploadAttachment(ref string message)
    {

        string[] validFileTypes = { "pdf"};
        string ext = System.IO.Path.GetExtension(oFile.PostedFile.FileName);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        


        string strFileName;
        string strFilePath;
        string strFolder;
        strFolder = Server.MapPath("/pdf");
        // Retrieve the name of the file that is posted.
        strFileName = oFile.PostedFile.FileName;
        strFileName = Path.GetFileName(strFileName);
        if (oFile.Value != "" && isValidFile)
        {
            // Create the folder if it does not exist.
            if (!Directory.Exists(strFolder))
            {
                Directory.CreateDirectory(strFolder);
            }
            // Save the uploaded file to the server.
            strFilePath = strFolder + "/" + strFileName;
            if (File.Exists(strFilePath))
            {
                message = strFileName + " already exists on the server!";
            }
            else
            {
                oFile.PostedFile.SaveAs(strFilePath);
                message = strFileName + " has been successfully uploaded.";
            }
        }

        return strFileName;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/expencesList.aspx");
    }
}