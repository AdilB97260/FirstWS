using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.Text;
using System.Web.Services;

public partial class reports_viewTicketDistribution : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {
            RaffleController _controller = new RaffleController();

            List<TicketsDistribution> _lstSold = _controller.GetTickeDistributionDetailList();

            if ((UserSession.Inst.UserType == "ADMIN" && UserSession.Inst.IsSystemUser == false) || (UserSession.Inst.IsSystemUser && (UserSession.Inst.SystemUserRole == "REPORTS")))
            {
                //btnRecordSale.Visible = false;
            }


            if (UserSession.Inst.UserType == "ADMIN" && UserSession.Inst.IsSystemUser == false)
            {

                if (UserSession.Inst.Member2MemberPK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK && X.Dist_user_Fk != UserSession.Inst.Member2MemberPK).ToList();
                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a>  <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "View Ticket Distribution");
                    }
                }
                else if (UserSession.Inst.MemberPK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "View Ticket Distribution");
                    }
                }
                else if (UserSession.Inst.ChurchPK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK && X.Dist_user_Fk != UserSession.Inst.ChurchPK).ToList();
                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "View Ticket Distribution");
                    }
                }
                else if (UserSession.Inst.RafflePK > 0)
                {
                    _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK && X.Dist_user_Fk != UserSession.Inst.RafflePK).ToList();

                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "View Ticket Distribution");
                    }
                }
                else
                {
                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "View Ticket Sale List");
                    }
                }


            }


            else if (UserSession.Inst.IsSystemUser)
            {
                ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/SaleList.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/SaleList.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Sold");
                //_lstSold = _lstSold.Where(X => X.LastAccessUserID == UserSession.Inst.UserPK).ToList();

                if (UserSession.Inst.UserType == "RAFFLE")
                {
                    _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK && X.Dist_user_Fk != UserSession.Inst.RafflePK).ToList();

                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }
                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", UserSession.Inst.RaffleObj.name, "View Ticket Distribution");
                    }
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK && X.Dist_user_Fk != UserSession.Inst.ChurchPK).ToList();

                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a>  <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, "View Ticket Distribution");
                    }
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();

                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "View Ticket Distribution");
                    }
                }
                else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                {
                    if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                    }

                    else
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a>  <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a>   <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1}) </a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2}) </a>  <a href='/member2member/dashboard.aspx' class='btn btn-primary'>User({3}) </a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, UserSession.Inst.Member2MemberObj.name, "View Ticket Distribution");
                    }
                    _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK && X.Dist_user_Fk != UserSession.Inst.Member2MemberPK).ToList();
                }

            }

            else if (UserSession.Inst.UserType == "RAFFLE")
            {
                if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }

                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }
                _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK && X.Dist_user_Fk != UserSession.Inst.RafflePK).ToList();
            }

            else if (UserSession.Inst.UserType == "CHURCH")
            {
                if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }

                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }
                _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK && X.Dist_user_Fk != UserSession.Inst.ChurchPK).ToList();
            }

            else if (UserSession.Inst.UserType == "MEMBER")
            {
                if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }

                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }
                _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
            }

            else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
            {
                if (UserSession.Inst.ReportUserObj != null && UserSession.Inst.ReportUserObj.DistUserType == "REPORTVIEW")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/ReportViewer.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/ReportViewer.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }

                else
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member2member/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member2member/Dashboard.aspx' class='btn btn-primary'>{0}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "Home", "View Ticket Distribution");
                }
                _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK && X.Dist_user_Fk != UserSession.Inst.Member2MemberPK).ToList();
            }

            rptSale.DataSource = _lstSold;
            rptSale.DataBind();

            pnlSale.Visible = false;
            pnlInfo.Visible = false;
        }
    }


   
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(txtSearch.Text))
        {

            return;
        }


        RaffleController _controller = new RaffleController();
        List<TicketsDistribution> _lstDist = new List<TicketsDistribution>();


        _lstDist = _controller.GetTickeDistributionDetailList().Where(x => x.FTick <= Convert.ToInt32(txtSearch.Text) && x.TTick >= Convert.ToInt32(txtSearch.Text)).ToList();

        
        if (UserSession.Inst.UserType == "RAFFLE")
        {
            _lstDist = _lstDist.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK).ToList();
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            _lstDist = _lstDist.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK).ToList();
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK).ToList();
        }
        else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            _lstDist = _lstDist.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList();
        }

        rptSale.DataSource = _lstDist;
        rptSale.DataBind();


        if (_lstDist.Count > 0)
        {
            
            TicketSold obj = _controller.GetTicketSoldList().Where(x => x.TicketFrom <= Convert.ToInt32(txtSearch.Text) && x.TicketTo >= Convert.ToInt32(txtSearch.Text)).FirstOrDefault();

            if (obj != null)
            {
                pnlSale.Visible = true;
                pnlInfo.Visible = false;
                lblCollectionBy.Text = !string.IsNullOrEmpty(obj.Collected_By) ? obj.Collected_By : _controller.GetDistUserObj(obj.LastAccessUserID).name;
                lblSoldTo.Text = obj.GivenTo;
                lblDate.Text = obj.CreatedDate.ToShortDateString();

                lblFrom.Text = obj.TicketFrom.ToString();
                lblTo.Text = obj.TicketTo.ToString();

                List<TicketsDistribution> lst = _controller.GetTickHistory(Convert.ToInt32(obj.TicketFrom), Convert.ToInt32(obj.TicketTo));
                if (lst != null && lst.Count > 0)
                {
                    lblLastDistName.Text = lst.OrderByDescending(x => x.CreatedDate).FirstOrDefault().LastDistUserName;
                }
            }
            else
            {
                pnlSale.Visible = false;
                pnlInfo.Visible = true;
            }

        }
    }



    protected void btnReset_Click(object sender, EventArgs e)
    {
        RaffleController _controller = new RaffleController();

        List<TicketsDistribution> _lstSold = _controller.GetTickeDistributionDetailList();

        if (UserSession.Inst.UserType == "RAFFLE")
        {
            _lstSold = _lstSold.Where(x => x.RAFFLE_FK == UserSession.Inst.RafflePK).ToList();
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            _lstSold = _lstSold.Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).ToList();
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            _lstSold = _lstSold.Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).ToList();
        }
        else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            _lstSold = _lstSold.Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList();
        }

        rptSale.DataSource = _lstSold;
        rptSale.DataBind();

        pnlSale.Visible = false;
        pnlInfo.Visible = false;
        txtSearch.Text = "";


    }











    protected void btncancel_click(object sender, EventArgs e)
    {
        if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            Response.Redirect("/member2member/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            Response.Redirect("/Member/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            Response.Redirect("/Church/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "RAFFLE")
        {
            Response.Redirect("/Raffle/Dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "ADMIN")
        {
            if (UserSession.Inst.Member2MemberPK > 0)
            {
                Response.Redirect("/member2member/dashboard.aspx");
            }
            else if (UserSession.Inst.MemberPK > 0)
            {
                Response.Redirect("/member/dashboard.aspx");
            }
            else if (UserSession.Inst.ChurchPK > 0)
            {
                Response.Redirect("/church/dashboard.aspx");
            }
            else if (UserSession.Inst.RafflePK > 0)
            {
                Response.Redirect("/raffle/dashboard.aspx");
            }
            else
            {
                Response.Redirect("/admin/Dashboard.aspx");
            }
        }
    }

    protected void btn_click(object sender, EventArgs e)
    {
        RaffleController _controller = new RaffleController();

        RaffleEntities _entities = new RaffleEntities();

        List<TicketSold> _lstSold = _controller.GetTicketSoldList();



        foreach (TicketSold obj in _lstSold)
        {

            TicketsDistribution objDist = _controller.GetLastTickDist(obj.TicketFrom, obj.TicketTo);

            if (objDist != null)
            {
                TicketSold objSold = _entities.TicketSolds.Where(x => x.TicketSold_fk == obj.TicketSold_fk).FirstOrDefault();

                objSold.LastDistUserId = objDist.LastDistUserFk;
                objSold.LastDistUserName = objDist.LastDistUserName;
                objSold.LastAccessUserID = objDist.LastDistUserFk;
                //objSold.LastAccessUserName
                _entities.SaveChanges();
            }

        }

    }

}