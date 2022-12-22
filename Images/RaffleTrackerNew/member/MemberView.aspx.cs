using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;
using System.IO;
using NPOI.HSSF.UserModel;

public partial class Member_MemberView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        
            RaffleController _provider = new RaffleController();
            DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(UserSession.Inst.MemberPK));
            if (objDistUser != null)
            {
                if (UserSession.Inst.UserType == "ADMIN")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/raffle/dashboard.aspx' class='btn btn-primary'>Raffle({0}) </a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({1})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({2})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", UserSession.Inst.RaffleObj.name, UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "Edit Member Information");

                }
                else if (UserSession.Inst.UserType == "RAFFLE")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/raffle/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/raffle/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/church/dashboard.aspx' class='btn btn-primary'>Church({0})</a>  <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({1})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.ChurchObj.name, UserSession.Inst.MemberObj.name, "Edit Member Information");
                }
                else if (UserSession.Inst.UserType == "CHURCH")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/church/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/church/dashboard.aspx' class='btn btn-primary'>Home</a> <a href='/member/dashboard.aspx' class='btn btn-primary'>Member({0})</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", UserSession.Inst.MemberObj.name, "Edit Member Information");
                }
                else if (UserSession.Inst.UserType == "MEMBER")
                {
                    ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/member/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/member/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Edit Member Information");
                }


                RaffleController _controller = new RaffleController();
                List<TicketsDistribution> distList = _controller.GetTicketDistributionList().Where(x => x.Dist_user_Fk == objDistUser.user_pk).OrderBy(x => x.CreatedDate).ToList();
                rptDistribution.DataSource = distList;
                rptDistribution.DataBind();

                lblTotTick.Text = distList.Sum(x => x.TotalTickets).ToString();


                //List<TicketsDistribution> distChurchList = _controller.GetTickeDistributionList(objDistUser.user_pk).OrderBy(x => x.CreatedDate).ToList();
                //rptChurch.DataSource = distChurchList;
                //rptChurch.DataBind();

                //lblTotChurch.Text = distChurchList.Sum(x => x.TotalTickets).ToString();

                List<TicketsDistribution> distChurchList = _controller.GetTickeDistributionDetailList();

                //distChurchList = distChurchList.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();

                distChurchList = distChurchList.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.CHURCH_FK != null && X.MEMBER_FK != null && X.LastAccessUserID == UserSession.Inst.MemberPK).ToList();

                rptChurch.DataSource = distChurchList;
                rptChurch.DataBind();

                lblTotChurch.Text = distChurchList.Sum(x => x.TotalTickets).ToString();
                
            
        }
    }

    protected void btncancel_click(object sender, EventArgs e)
    {
        Response.Redirect("/member/dashboard.aspx");
    }

    protected void btnAddTicket_Click(object sernder, EventArgs e)
    {

        Response.Redirect("/DistributionForm.aspx?id=" + UserSession.Inst.MemberPK);
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

        _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.CHURCH_FK != null && X.MEMBER_FK != null && X.LastAccessUserID == UserSession.Inst.MemberPK).ToList();

        //if (UserSession.Inst.UserType == "RAFFLE")
        //{
        //    _lstDist = _lstDist.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "CHURCH")
        //{
        //    _lstDist = _lstDist.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "MEMBER")
        //{
        //    _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        //{
        //    _lstDist = _lstDist.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "ADMIN")
        //{
        //    _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}

        rptChurch.DataSource = _lstDist;
        rptChurch.DataBind();


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
            _lstSold = _lstSold.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            _lstSold = _lstSold.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        }
        else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            _lstSold = _lstSold.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        }
        else if (UserSession.Inst.UserType == "ADMIN")
        {
            _lstSold = _lstSold.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        }

        rptChurch.DataSource = _lstSold;
        rptChurch.DataBind();

        pnlSale.Visible = false;
        pnlInfo.Visible = false;
        txtSearch.Text = "";


    }


    protected void DownloadFile_Click(object sender, EventArgs e)
    {
        string guid = Guid.NewGuid().ToString();
        string ExcelPath = string.Format("/Data/distribution-{0}.xls", DateTime.Now.ToString("yyyyMMddHHMMss"));

        HSSFWorkbook hssfworkbook = new HSSFWorkbook();
        HSSFSheet sheet1 = hssfworkbook.CreateSheet("TicketSoldData");

        sheet1.CreateRow(0).CreateCell(0).SetCellValue("TimeStamp");
        sheet1.CreateRow(0).CreateCell(1).SetCellValue("From Whom Distribution");
        sheet1.CreateRow(0).CreateCell(2).SetCellValue("To Whom Distribution");
        sheet1.CreateRow(0).CreateCell(3).SetCellValue("Ticket From");
        sheet1.CreateRow(0).CreateCell(4).SetCellValue("Ticket To");
        sheet1.CreateRow(0).CreateCell(5).SetCellValue("Total Ticket");

        int row = 1;


        RaffleController _controller = new RaffleController();
        List<TicketsDistribution> _lstDist = new List<TicketsDistribution>();

        if (!string.IsNullOrEmpty(txtSearch.Text))
        {
            _lstDist = _controller.GetTickeDistributionDetailList().Where(x => x.FTick <= Convert.ToInt32(txtSearch.Text) && x.TTick >= Convert.ToInt32(txtSearch.Text)).ToList();
        }
        else
        {
            _lstDist = _controller.GetTickeDistributionDetailList();
        }

        _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.CHURCH_FK != null && X.MEMBER_FK != null && X.LastAccessUserID == UserSession.Inst.MemberPK).ToList();

        //if (UserSession.Inst.UserType == "RAFFLE")
        //{
        //    _lstDist = _lstDist.Where(X => X.RAFFLE_FK == UserSession.Inst.RafflePK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "CHURCH")
        //{
        //    _lstDist = _lstDist.Where(X => X.CHURCH_FK == UserSession.Inst.ChurchPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "MEMBER")
        //{
        //    _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        //{
        //    _lstDist = _lstDist.Where(X => X.MEM2MEM_FK == UserSession.Inst.Member2MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}
        //else if (UserSession.Inst.UserType == "ADMIN")
        //{
        //    _lstDist = _lstDist.Where(X => X.MEMBER_FK == UserSession.Inst.MemberPK && X.Dist_user_Fk != UserSession.Inst.MemberPK).ToList();
        //}




        foreach (TicketsDistribution obj in _lstDist)
        {

            sheet1.CreateRow(row).CreateCell(0).SetCellValue(obj.CreatedDate.ToShortDateString());
            sheet1.CreateRow(row).CreateCell(1).SetCellValue(obj.FromDistUserName);
            sheet1.CreateRow(row).CreateCell(2).SetCellValue(obj.ToDistUserName);
            sheet1.CreateRow(row).CreateCell(3).SetCellValue(obj.FromTicket);
            sheet1.CreateRow(row).CreateCell(4).SetCellValue(obj.ToTicket);
            sheet1.CreateRow(row).CreateCell(5).SetCellValue(obj.TotalTickets);
            row++;
        }

        WriteSave(Server.MapPath(ExcelPath), hssfworkbook);
        DownloadFile(ExcelPath);

    }

    private void WriteSave(string filePath, HSSFWorkbook hssfworkbook)
    {
        if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
        FileStream file = new FileStream(filePath, FileMode.Create);
        hssfworkbook.Write(file);
        file.Flush();
        file.Close();
        file.Dispose();

    }

    private void DownloadFile(string FilePath)
    {
        string strURL = Server.MapPath(FilePath);
        FileInfo file = new FileInfo(strURL);
        if (file.Exists)
        {
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/vnd.ms-excel";
            Response.TransmitFile(file.FullName);
            Response.End();
        }

    }

}