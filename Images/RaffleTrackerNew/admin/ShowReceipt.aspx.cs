using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ShowReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string val = Request.QueryString["val"];
        if (!string.IsNullOrEmpty(val))
        {
            string path = Server.MapPath("/pdf/") + val;
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(path);

            if (buffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", buffer.Length.ToString());
                Response.BinaryWrite(buffer);
            }
        }
    }
}