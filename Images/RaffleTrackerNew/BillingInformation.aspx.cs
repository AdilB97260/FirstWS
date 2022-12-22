using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BillingInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(UserSession.Inst.lstPurchaseTicket==null)
            {
                Response.Redirect("TicketBuy.aspx");
            }
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/TicketBuy.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a style=padding-top:10px!important;' href='/TicketBuy.aspx' class='btn btn-primary'>{0}</a> <span class='btn' style='cursor:auto; padding-top:10px!important; color:#e4dfdf'>{1}</span></div>", "Home", "Billing Information");
            lblTicketTot.Text = Convert.ToString(UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToInt32(x.totTicket)));
            lblAmount.Text = "$" + Convert.ToString(UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToInt32(x.totalAmount)));

            PaymentController _controller = new PaymentController();
            lblRaffle.Text = _controller.GetDistNameByTicketNumber(Convert.ToInt32(UserSession.Inst.lstPurchaseTicket.FirstOrDefault().ticketFrom), Convert.ToInt32(UserSession.Inst.lstPurchaseTicket.FirstOrDefault().ticketTo));
            UserSession.Inst.PurchaseChurchName = string.Empty;
            UserSession.Inst.PurchaseChurchName = lblRaffle.Text;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(Page.IsValid)
        {
            tblBillingInformation objBilling = new tblBillingInformation();
            objBilling.address = txtAddress.Text;
            objBilling.city = txtCity.Text;
            objBilling.email = txtEmail.Text;
            objBilling.firstname = txtFirstName.Text;
            objBilling.lastname = txtLastName.Text;
            objBilling.phone = txtPhone.Text;
            objBilling.zip = txtZip.Text;
            objBilling.state = txtState.Text;
            UserSession.Inst.BillingInfo = objBilling;
            Response.Redirect("/Payment.aspx");
        }
    }
}