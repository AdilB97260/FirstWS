using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using RaffleModel;
public partial class RaffleTracker : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (UserSession.Inst.IsUserLoggedIn)
        {
            //lblUserName.Text = "<strong> Welcome &nbsp;&nbsp;</strong><span style='color:maroon'>" + UserSession.Inst.Name + "</span>";
            lblUserName.Text = "<span style='color:#000'>" + UserSession.Inst.Name + "</span>";


            if (UserSession.Inst.UserType != "ADMIN")
            {
                //liAdmin1.Visible = false;
                liAdmin2.Visible = false;
            }


            if (!Page.IsPostBack)
            {

                if (UserSession.Inst != null && UserSession.Inst.UserType=="ADMIN")
                {
                    SALESDISTREP.Visible = true;
                    SALESDATERANGEREP.Visible = true;
                    DISTREP.Visible = true;
                    TICKDISTREP.Visible = true;
                    AcctRep.Visible = true;
                    report.Visible = true;
                }
                
                else if (UserSession.Inst != null && UserSession.Inst.PagePermissionList != null && UserSession.Inst.PagePermissionList.Count() > 0)
                {
                    bool found = false;
                    foreach (PagePermission obj in UserSession.Inst.PagePermissionList.Where(x=> x.PageView==true))
                    {
                        if (obj.pageID == Utilities.PAGEID.SALESDISTREP.ToString())
                        {
                            SALESDISTREP.Visible = true;
                            found = true;
                        }
                        if (obj.pageID == Utilities.PAGEID.SALESDATERANGEREP.ToString())
                        {
                            SALESDATERANGEREP.Visible = true;
                            found = true;
                        }
                        if (obj.pageID == Utilities.PAGEID.DISTREP.ToString())
                        {
                            DISTREP.Visible = true;
                            found = true;
                        }
                        if (obj.pageID == Utilities.PAGEID.TICKDISTREP.ToString())
                        {
                            TICKDISTREP.Visible = true;
                            found = true;
                        }
                    }
                    
                    report.Visible = found;
                }

            }


            //StringBuilder strMenu = new StringBuilder();

            // if (UserSession.Inst.UserType == "ADMIN")
            // {
            //     strMenu.AppendFormat("<li><a href='/Admin/Dashboard.aspx'>Dashboard</a></li>");
            //     //strMenu.AppendFormat("<li><a href='/Admin/RaffleList.aspx'>Raffle</a></li>");
            //     //strMenu.AppendFormat("<li><a href='/Reports.aspx'>Reports</a></li>");
            // }
            // else if (UserSession.Inst.UserType == "RAFFLE")
            // {
            //     strMenu.AppendFormat("<li><a href='/Raffle/Dashboard.aspx'>Dashboard</a></li>");
            // }
            // else if (UserSession.Inst.UserType == "CHURCH")
            // {
            //     strMenu.AppendFormat("<li><a href='/Church/Dashboard.aspx'>Dashboard</a></li>");
            // }
            // else if (UserSession.Inst.UserType == "MEMBER")
            // {
            //     strMenu.AppendFormat("<li><a href='/Member/Dashboard.aspx'>Dashboard</a></li>");
            // }

            //// strMenu.AppendFormat("<li><a href='/TicketSale.aspx'>Ticket Sales</a></li>");

            // ltMenu.Text = strMenu.ToString();

            //if (UserSession.Inst.UserType == "RAFFLE")
            //{
            //    liDash.InnerHtml = "<a href='/Raffle/Dashboard.aspx'>Dashboard</a>";
            //    //liRegUser.InnerHtml = "<a href='/UserRegistration.aspx'>Registration</a>";
            //}
            //if (UserSession.Inst.UserType == "CHURCH")
            //{
            //    liDash.InnerHtml = "<a href='/Church/Dashboard.aspx'>Dashboard</a>";
            //    //liRegUser.InnerHtml = "<a href='/UserRegistration.aspx'>Registration</a>";
            //}
            //if (UserSession.Inst.UserType == "MEMBER")
            //{
            //    liDash.InnerHtml = "<a href='/Member/Dashboard.aspx'>Dashboard</a>";
            //    //liRegUser.InnerHtml = "<a href='/UserRegistration.aspx'>Registration</a>";
            //}
            //if (UserSession.Inst.UserType == "ADMIN" || UserSession.Inst.UserType == "RAFFLE"  )
            //{
            //    liDist.Visible = true;
            //    liUser.Visible = true;
            //}
        }


        else
        {
            Response.Redirect("/login.aspx");
        }
    }


    protected void btnLogout_Click(object sender, EventArgs e)
    {
        if (UserSession.Inst != null)
        {
            UserSession.Inst.Logout();
        }
        else
        {
            UserSession.Create();
            UserSession.Inst.Logout();
        }

        Response.Redirect("/login.aspx");
    }


}
