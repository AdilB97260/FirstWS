using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RaffleModel;

public partial class admin_ImportPreviousYearData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!UserSession.Inst.IsUserLoggedIn)
        {
            Response.Redirect("/login.aspx");
        }

        if (!IsPostBack)
        {
            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a>  <a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a> <span class='btn' style='cursor:auto; color:#e4dfdf'>{0}</span></div>", "Importing previous year churches");

            

            ///DIST_USERS raffleOvb = _conroller.GetDistUserList(UserSession.Inst.CurrentYearID).Where(x => x.UserType == "RAFFLE" && x.user_pk != Convert.ToInt32(ddlRaffleList.SelectedValue)).OrderByDescending(x => x.YearID).FirstOrDefault();



            BindData();

            RaffleController _conroller = new RaffleController();


            ddlRaffleList.DataSource = _conroller.GetDistUserList(UserSession.Inst.CurrentLoginID).Where(x => x.UserType == "RAFFLE");
            ddlRaffleList.DataTextField = "name";
            ddlRaffleList.DataValueField = "user_pk";
            ddlRaffleList.DataBind();
            ddlRaffleList.Items.Insert(0, new ListItem{Text="Select Raffle",Value="0", Selected=true});

            ddlRaffleList.SelectedValue = _conroller.GetDistUserList(UserSession.Inst.CurrentLoginID).Where(x => x.UserType == "RAFFLE").OrderByDescending(x => x.user_pk).FirstOrDefault().user_pk.ToString();

            ddlRaffleList.Enabled = false;

        }
    }


    protected void BindData()
    {

            RaffleController _conroller = new RaffleController();
            RaffleYear objRaffleYear = _conroller.GetRaffleYearList().Where(x => x.RaffleYear_ID != UserSession.Inst.CurrentYearID).OrderByDescending(x => x.RaffleYear_ID).FirstOrDefault();
            List<DIST_USERS> tmpdistUserPrevLst = _conroller.GetDistUserList(objRaffleYear.RaffleYear_ID).Where(x => x.UserType == "CHURCH").ToList();

            List<DIST_USERS> tmpdistUserCurrentLst = _conroller.GetDistUserList(UserSession.Inst.CurrentLoginID).Where(x => x.UserType == "CHURCH").ToList();

            List<DIST_USERS> distUserLst = new List<DIST_USERS>();

            foreach (DIST_USERS obj in tmpdistUserPrevLst)
            {
                if (!tmpdistUserCurrentLst.Any(x=> x.name.Trim() == obj.name.Trim()))
                {
                    distUserLst.Add(obj);
                }
            }


            rptRaffle.DataSource = distUserLst.OrderBy(x => x.name);
            rptRaffle.DataBind();

    }


    protected void btnImportData_Click(object sender, EventArgs e)
    {
        lblError.Visible = false;
        if (ddlRaffleList.SelectedValue == "0")
        {
            lblError.Visible = true;
            lblError.Text = "Select Raffle First for Importing data";
            return;
        }

        bool isSelected = false;
        foreach (RepeaterItem item in rptRaffle.Items)
        {
            CheckBox chkItem = item.FindControl("chkSelect") as CheckBox;
            HiddenField hdnFld = item.FindControl("hdnId") as HiddenField;

            if (chkItem.Checked)
            {
                isSelected = true;
                break;
            }
        }

        if (isSelected == false)
        {
            lblError.Visible = true;
            lblError.Text = "Select atleast one church for importing data.";
            return;
        }

        RaffleController _conroller = new RaffleController();
        //DIST_USERS raffleOvb = _conroller.GetDistUserList(UserSession.Inst.CurrentLoginID).Where(x => x.UserType == "RAFFLE" && x.user_pk != Convert.ToInt32(ddlRaffleList.SelectedValue)).OrderByDescending(x=> x.YearID).FirstOrDefault();


        //List<DIST_USERS> tmpdistUserPrevLst = _conroller.GetDistUserList().Where(x => x.UserType == "CHURCH" && x.YearID == raffleOvb.YearID).ToList();

        RaffleYear objRaffleYear = _conroller.GetRaffleYearList().Where(x => x.RaffleYear_ID != UserSession.Inst.CurrentYearID).OrderByDescending(x => x.RaffleYear_ID).FirstOrDefault();
        List<DIST_USERS> distUserLst = _conroller.GetDistUserList(objRaffleYear.RaffleYear_ID);


        DIST_USERS objDistUser = null;
        DIST_USERS objPrevDistUser = null;

        foreach (RepeaterItem item in rptRaffle.Items)
        {
            CheckBox chkItem = item.FindControl("chkSelect") as CheckBox;
            HiddenField hdnFld = item.FindControl("hdnId") as HiddenField;

            if (chkItem.Checked)
            {
                objPrevDistUser = distUserLst.Where(x => x.user_pk == Convert.ToInt32(hdnFld.Value)).FirstOrDefault();
                objDistUser = new DIST_USERS();

                objDistUser.Address = objPrevDistUser.Address;
                objDistUser.ChurchAdminstName = objPrevDistUser.ChurchAdminstName;
                objDistUser.City = objPrevDistUser.City;
                objDistUser.CreatedBy_UserFk = objPrevDistUser.CreatedBy_UserFk;
                objDistUser.createdDate = DateTime.Now;
                objDistUser.email = objPrevDistUser.email;
                objDistUser.FullAddress = objPrevDistUser.email;
                objDistUser.LastaccessUserId = objPrevDistUser.LastaccessUserId;
                objDistUser.mobile = objPrevDistUser.mobile;
                objDistUser.modifiedDate = DateTime.Now;
                objDistUser.name = objPrevDistUser.name;
                objDistUser.parent_userFk = Convert.ToInt32(ddlRaffleList.SelectedValue);
                objDistUser.password = objPrevDistUser.password;
                objDistUser.phone = objPrevDistUser.phone;
                objDistUser.RAFFLE_FK = Convert.ToInt32(ddlRaffleList.SelectedValue);
                objDistUser.State = objPrevDistUser.State;
                objDistUser.status = objPrevDistUser.status;
                objDistUser.TicketRate = objPrevDistUser.TicketRate;
                objDistUser.UserName = objPrevDistUser.UserName;
                objDistUser.UserType = objPrevDistUser.UserType;
                objDistUser.YearID = Convert.ToInt32(UserSession.Inst.CurrentYearID);
                objDistUser.Zip = objPrevDistUser.Zip;

                _conroller.AddDistUser(objDistUser);
            }
        }

        BindData();

    }
}