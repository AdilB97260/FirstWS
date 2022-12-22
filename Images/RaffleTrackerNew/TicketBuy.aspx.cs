using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TicketBuy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ltrBredCrumb.Text = "Parish Raffle";
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/TicketBuy.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><span class='btn' style='cursor:auto; padding-top:10px!important; color:#e4dfdf'>{0}</span></div>", "Parish Raffle Buy Ticket");
        }
    }

    [WebMethod]
    public static string SaveTicketDetail(string tickets)
    {
        List<PurchaseTickets> lstTickets = new List<PurchaseTickets>();
        JavaScriptSerializer oJS = new JavaScriptSerializer();
        lstTickets = oJS.Deserialize<List<PurchaseTickets>>(tickets);
        UserSession.Inst.lstPurchaseTicket = lstTickets;
        return "True";
    }
}

