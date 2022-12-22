using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ExpencesList : System.Web.UI.Page
{

    public ExpencessController _expController = new ExpencessController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }


        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a></div>", "Expences List");

        if (!IsPostBack)
        {
            if (UserSession.Inst.UserType == "ADMIN")
            {

               

                List<Expencess> lstExpences = _expController.GetAllExpencessList(UserSession.Inst.CurrentRaffleYearID);
                rptExp.DataSource = lstExpences;
                rptExp.DataBind();

                //decimal total = lstExpences.Sum(p => Convert.ToDecimal(p.Amount));
                //lblTotal.Text = total.ToString("0.00"); 
                //(rptExp.Controls[rptExp.Controls.Count - 1].Controls[0].FindControl("lblTotal") as Label).Text = total.ToString("0.00");

            }
        }
    }


    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "ShowFile")
        {
            //e.CommandArgument --> contain the erid value
            //Do something

            if (!string.IsNullOrEmpty(Convert.ToString(e.CommandArgument)))
            {

                Response.Write("<script>window.open ('ShowReceipt.aspx?val=" + e.CommandArgument + "','_blank');</script>");
            }
        }
            
    }

    protected void btnExpences_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/manageExpencess.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("/admin/dashboard.aspx");
    }
}