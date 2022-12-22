using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;


public partial class admin_GenerateNewYearRaffle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a>  <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Generate new year Raffle");
            txtYear.Text = DateTime.Today.Year.ToString();

            RaffleController _conroller = new RaffleController();

            List<RaffleYear> _raffleyear = _conroller.GetRaffleYearList();

            rptRaffle.DataSource = _raffleyear;
            rptRaffle.DataBind();

        }
    }


    protected void btnGenerate_click(object sender, EventArgs e)
    {

        lblError.Visible = false;
        
        if (!string.IsNullOrEmpty(txtYear.Text))
        {
            RaffleController _controller = new RaffleController();
            if (_controller.GetRaffleYearList().Any(x => x.RaffleYear1 == txtYear.Text))
            {
                lblError.Text = "You have already generated year ! ";
                lblError.Visible = true;
                return;
            }

            RaffleYear obj = new RaffleYear();
            obj.RaffleYear1 = txtYear.Text;
            obj.IsLoginYear = true;
            obj.IsCurrentYear = true;
            obj.CreatedDate = DateTime.Now;

            int result = _controller.AddRaffle(obj, UserSession.Inst.CurrentYearID);
            if (result > 0)
            {
                rptRaffle.DataSource = _controller.GetRaffleYearList(); 
                rptRaffle.DataBind();

                UserSession.Inst.CurrenYearRaffle = null;
                UserSession.Inst.CurrenLoginRaffle = null;

            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Opps ! Something has wrong. Please contact to administrator !";
            }



        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Please enter year !";
        }

    }

}