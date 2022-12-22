using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Processing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["proSteps1"] == null || Convert.ToString(Session["proSteps1"]) != "YES")
        {
            Response.Redirect("/payment.aspx");
        }
        else
        {
            Session["proSteps1"] = null;
            Session["proSteps2"] = "YES";
        }
    }
    
}

