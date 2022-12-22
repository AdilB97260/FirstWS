using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe;
using System.Configuration;

public partial class Payment : System.Web.UI.Page
{

    #region [GENERAL]
    PaymentController _controller = new PaymentController();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            if (UserSession.Inst.BillingInfo == null || UserSession.Inst.lstPurchaseTicket == null)
            {
                Response.Redirect("TicketBuy.aspx");
            }

            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/TicketBuy.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a style='padding-top:10px!important;' href='/TicketBuy.aspx' class='btn btn-primary'>{0}</a><a style='padding-top:10px!important;' href='/BillingInformation.aspx' class='btn btn-primary'>{1}</a><span class='btn' style='cursor:auto; padding-top:10px!important; color:#e4dfdf'>{2}</span></div>", "Home", "Billing Information", "Payment Details");

            lblTicketTot.Text = Convert.ToString(UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToInt32(x.totTicket)));
            lblAmount.Text = "$" + Convert.ToString(UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToInt32(x.totalAmount)));

            lblFullName.Text = UserSession.Inst.BillingInfo.firstname + " " + UserSession.Inst.BillingInfo.lastname;
            lblCity.Text = UserSession.Inst.BillingInfo.city;
            lblZip.Text = UserSession.Inst.BillingInfo.zip;
            lblPhone.Text = UserSession.Inst.BillingInfo.phone;
            lblEmail.Text = UserSession.Inst.BillingInfo.email;

            lblChurchName.Text = _controller.GetDistNameByTicketNumber(Convert.ToInt32(UserSession.Inst.lstPurchaseTicket.FirstOrDefault().ticketFrom), Convert.ToInt32(UserSession.Inst.lstPurchaseTicket.FirstOrDefault().ticketTo));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {

            CardInfo objCardInfo = new CardInfo();
            objCardInfo.CardExpMonth = Convert.ToInt32(ddlMonth.Value);
            objCardInfo.CardExpYear = Convert.ToInt32(ddlYear.Value);
            objCardInfo.CardName = txtCardName.Text;
            objCardInfo.CardNumber = txtCardNumber.Text;
            objCardInfo.CVV2 = txtCVCode.Text;
            objCardInfo.Amount = UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToDecimal(x.totalAmount));
            UserSession.Inst.CardInfo = objCardInfo;

            try
            {
                StripeConfiguration.SetApiKey(Convert.ToString(ConfigurationManager.AppSettings["StripeApiKey"]));
                StripeConfiguration.ApiKey = Convert.ToString(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Stripe.CreditCardOptions card = new CreditCardOptions();
                card.Name = UserSession.Inst.BillingInfo.firstname + " " + UserSession.Inst.BillingInfo.lastname;
                card.Number = UserSession.Inst.CardInfo.CardNumber;
                card.ExpYear = UserSession.Inst.CardInfo.CardExpYear;
                card.ExpMonth = UserSession.Inst.CardInfo.CardExpMonth;
                card.Cvc = UserSession.Inst.CardInfo.CVV2;
                //Assign Card to Token Object and create Token  
                Stripe.TokenCreateOptions token = new Stripe.TokenCreateOptions();
                token.Card = card;
                Stripe.TokenService serviceToken = new Stripe.TokenService();
                Stripe.Token newToken = serviceToken.Create(token);

                List<tblTicketBuy> lstTicketBuy = new List<tblTicketBuy>();

                int ticketBuyId = _controller.GetTicketBuyerID();

                foreach (PurchaseTickets obj in UserSession.Inst.lstPurchaseTicket)
                {
                    lstTicketBuy.Add(new tblTicketBuy { ticket_from = Convert.ToInt32(obj.ticketFrom), ticket_to = Convert.ToInt32(obj.ticketTo), ticket_buyer_Id = ticketBuyId });
                }

                int tickBuy = _controller.AddTicketBuyer(lstTicketBuy);

                if (tickBuy > 0)
                {
                    string hostName = Dns.GetHostName();
                    string myIP = Convert.ToString(Dns.GetHostByName(hostName).AddressList[0]);
                    tblBillingInformation billingOnfo = UserSession.Inst.BillingInfo;
                    billingOnfo.createdDate = DateTime.Now;
                    billingOnfo.IPAddress = myIP;
                    billingOnfo.ticket_buyer_id = ticketBuyId;
                    int billKey = _controller.AddBillingInfo(billingOnfo);
                    if (billKey > 0)
                    {
                        PaymentInfo payInfoObj = new PaymentInfo();
                        payInfoObj.billingInfoID = billKey;
                        payInfoObj.card_name = txtCardName.Text;
                        payInfoObj.card_number = txtCardNumber.Text;
                        payInfoObj.created_date = DateTime.Now;
                        payInfoObj.modified_date = payInfoObj.created_date;
                        payInfoObj.payment_status = "Pending";
                        payInfoObj.ticket_amount = UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToDecimal(x.totalAmount));
                        payInfoObj.total_ticket = UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToInt32(x.totTicket));
                        payInfoObj.ticket_buyer_id = ticketBuyId;

                        int payId = _controller.AddPaymentInfo(payInfoObj);

                        if (payId > 0)
                        {
                            Session["proSteps1"] = "YES";
                            Session["paymentInfo"] = payInfoObj;

                            Response.Redirect("/Processing.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}