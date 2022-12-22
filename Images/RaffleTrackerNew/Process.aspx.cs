using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using Stripe;
using Stripe.Infrastructure;

public partial class Process : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["proSteps2"] == null || Convert.ToString(Session["proSteps2"]) != "YES")
            {
                Response.Redirect("/payment.aspx");
            }
            else
            {
                Session["proSteps2"] = null;
            }
        }

    }

    protected override void OnLoadComplete(EventArgs e)
    {
        if (Session["paymentInfo"] != null)
        {
            PaymentInfo payInfoObj = (PaymentInfo)Session["paymentInfo"];
            Session["paymentInfo"] = null;
            
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


            //var customerService = new Stripe.CustomerService();
            //Customer stripeCustomer = customerService.Create(new Stripe.CustomerCreateOptions
            //{
            //    Email = UserSession.Inst.BillingInfo.email,
            //    Source = newToken.Id
            //}); ;

            var myCharge = new Stripe.ChargeCreateOptions();
            // always set these properties
            myCharge.Amount = Convert.ToInt64(UserSession.Inst.CardInfo.Amount * 100);
            myCharge.Currency = "USD";
            myCharge.ReceiptEmail = UserSession.Inst.BillingInfo.email;
            myCharge.Description = "Total Ticket Purchase :- " + UserSession.Inst.lstPurchaseTicket.ToList().Sum(x => Convert.ToInt32(x.totTicket));
            myCharge.Source = newToken.Id;
            myCharge.Capture = true;
            // myCharge.Customer = stripeCustomer.Id;
            var chargeService = new Stripe.ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);

            if (stripeCharge.Status == "succeeded")
            {
                PaymentController _controller = new PaymentController();

                StringBuilder chrName = new StringBuilder();

                string[] output = UserSession.Inst.PurchaseChurchName.Split(' ');
                foreach (string s in output)
                {
                    chrName.Append(s[0].ToString().ToLower());
                }

                string confirmNum = chrName.ToString() + (Convert.ToInt32(UserSession.Inst.lstPurchaseTicket[0].ticketFrom) * Convert.ToInt32(UserSession.Inst.lstPurchaseTicket[0].totTicket)).ToString() + DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString();

                payInfoObj.confirmation_no = confirmNum;
                payInfoObj.stire_payment_id = stripeCharge.Id;
                _controller.UpdatePaymentInfo(payInfoObj);
                Session["payconfirmation"] = payInfoObj.confirmation_no;
                Session["paysuccess"] = payInfoObj.billingInfoID;
                Response.Redirect("/success.aspx");
            }
            else
            {
                PaymentController _controller = new PaymentController();
                payInfoObj.payment_error = stripeCharge.FailureMessage;
                _controller.UpdatePaymentInfo(payInfoObj);
                Session["payfailed"] = "YES";
                Response.Redirect("/failed.aspx");
            }
        }
    }

    public static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}