using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using RaffleModel;
using System.Web.Script.Serialization;

public partial class DistributionForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString != null && Request.QueryString["id"] != null)
            {
                hdnUserId.Value = Request.QueryString["id"];
                RaffleController _provider = new RaffleController();
                DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(hdnUserId.Value));

                //txtCreated.Text = DateTime.Now.ToString("MM/dd/yyyy HH:MM");

                if (objDistUser != null)
                {
                    lblDistName.Text = "Distribute more tickets to " + objDistUser.name;
                    txtDistName.Text = objDistUser.name;
                    txtEmail.Text = objDistUser.email;

                    if (UserSession.Inst.UserType == "ADMIN")
                    {
                        if (UserSession.Inst.Member2MemberPK > 0)
                        {
                            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a><a href={0} class='btn btn-primary'>{1}</a><a href={2} class='btn btn-primary'>{3}</a><a href={4} class='btn btn-primary'>{5}</a><a href={6} class='btn btn-primary'>{7}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{8}</span></div>", "/raffle/dashboard.aspx", string.Format("Raffle({0})", UserSession.Inst.RaffleObj.name), "/church/dashboard.aspx", string.Format("Church({0})", UserSession.Inst.ChurchObj.name), "/member/dashboard.aspx", string.Format("User({0})", UserSession.Inst.MemberObj.name), "/member2member/dashboard.aspx", string.Format("User({0})", UserSession.Inst.Member2MemberObj.name), "Add more ticket to User");
                        }
                        else if (UserSession.Inst.MemberPK > 0)
                        {
                            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a><a href={0} class='btn btn-primary'>{1}</a><a href={2} class='btn btn-primary'>{3}</a><a href={4} class='btn btn-primary'>{5}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{6}</span></div>", "/raffle/dashboard.aspx", string.Format("Raffle({0})", UserSession.Inst.RaffleObj.name), "/church/dashboard.aspx", string.Format("Church({0})", UserSession.Inst.ChurchObj.name), "/member/dashboard.aspx", string.Format("User({0})", UserSession.Inst.MemberObj.name), "Add more ticket to User");
                        }
                        else if (UserSession.Inst.ChurchPK > 0)
                        {
                            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a><a href={0} class='btn btn-primary'>{1}</a><a href={2} class='btn btn-primary'>{3}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", "/raffle/dashboard.aspx", string.Format("Raffle({0})",UserSession.Inst.RaffleObj.name), "/church/dashboard.aspx", string.Format("Church({0})",UserSession.Inst.ChurchObj.name), "Add more ticket to Church");
                        }
                        else if (UserSession.Inst.RafflePK > 0)
                        {
                            ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/admin/dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href='/admin/dashboard.aspx' class='btn btn-primary'>Home</a><a href={0} class='btn btn-primary'>{1}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{2}</span></div>", "/raffle/dashboard.aspx", string.Format("Raffle({0})", UserSession.Inst.RaffleObj.name), "Add more ticket to Raffle");
                        }
                        
                    }
                    else if (UserSession.Inst.UserType == "RAFFLE")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href={0} class='btn btn-primary'><i class='glyphicon glyphicon-home'></i><a href={1} class='btn btn-primary'>{2}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{3}</span></div>", "/Raffle/Dashboard.aspx", "/Raffle/ViewDistribution.aspx", "RAFFLE-" + UserSession.Inst.RaffleObj.name, "CHURCH-" + UserSession.Inst.ChurchObj.name);
                    }
                    else if (UserSession.Inst.UserType == "CHURCH")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href='/Church/Dashboard.aspx' class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href={0} class='btn btn-primary'>{1}</a><a href={2} class='btn btn-primary'>{3}</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{4}</span></div>", "/Raffle/Dashboard.aspx", "Raffle", "/Church/Dashboard.aspx", "Church", UserSession.Inst.ChurchObj.name);
                    }
                    else if (UserSession.Inst.UserType == "MEMBER")
                    {
                        ltrBredCrumb.Text = string.Format("<div class='btn-group btn-breadcrumb'><a href={0} class='btn btn-primary'><i class='glyphicon glyphicon-home'></i></a><a href={0} class='btn btn-primary'>Member</a><span class='btn' style='cursor:auto; color:#e4dfdf'>{1}</span></div>", "/Member/Dashboard.asxp?id=" + UserSession.Inst.MemberPK, UserSession.Inst.MemberObj.name);
                    }
                }

            }
        }


    }

    protected void btncancel_click(object sender, EventArgs e)
    {
        if (UserSession.Inst.UserType == "ADMIN")
        {
            Response.Redirect("/raffle/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "RAFFLE")
        {
            Response.Redirect("/raffle/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            Response.Redirect("/church/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            Response.Redirect("/member/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            Response.Redirect("/member2member/dashboard.aspx");
        }
    }

    public string RedirectString()
    {
        if (UserSession.Inst.UserType == "ADMIN")
        {
            return ("/raffle/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "RAFFLE")
        {
            return ("/raffle/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "CHURCH")
        {
            return ("/church/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "MEMBER")
        {
            return ("/member/dashboard.aspx");
        }
        else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
        {
            return ("/member2member/dashboard.aspx");
        }
        else
        {
            return ("/distributionForm.aspx");
        }
    }

    protected void btnSave_click(object sernder, EventArgs e)
    {
        if (UserSession.Inst.CurrentLoginID != UserSession.Inst.CurrentRaffleYearID)
        {
            Utilities.ShowSuccessMessage("You are logined with previous year Raffle So you can not make the sales entry. Login first to current year then enter the sales entry.", "Opps! Sales entered on previous year Raffle ", Page, RedirectString());
            return;
        }


        if (!string.IsNullOrEmpty(hdnUserId.Value))
        {
            RaffleController _provider = new RaffleController();

            //if(_provider.GetTicketDistributionList().Any(x=> Convert.ToInt32(txtTicketStart.Text) < Convert.ToInt32(x.ToTicket)  || Convert.ToInt32(txtTicketEnd.Text) <= Convert.ToInt32(x.FromTicket)))
            // {


            int Start = Convert.ToInt32(txtTicketStart.Text);
            int End = Convert.ToInt32(txtTicketEnd.Text);

            List<TicketsDistribution> _list = _provider.GetTicketDistributionList().Where(X => X.Dist_user_Fk == Convert.ToInt32(hdnUserId.Value)).ToList();

            divError.Visible = false;

            int MinStartValue = _list.Where(x => Convert.ToInt32(x.ToTicket) > Start).Count() > 0 ? _list.Where(x => Convert.ToInt32(x.ToTicket) > Start).Min(x => Convert.ToInt32(x.FromTicket)) : 0;
            int MaxStartValue = _list.Where(x => Convert.ToInt32(x.FromTicket) < Start).Count() > 0 ? _list.Where(x => Convert.ToInt32(x.FromTicket) < Start).Max(x => Convert.ToInt32(x.ToTicket)) : 0;


            if (Start <= End)
            {
                if (!_list.Any(x => Convert.ToInt32(x.FromTicket) == Start) && !_list.Any(x => Convert.ToInt32(x.ToTicket) == End))
                {
                    if (Start > MaxStartValue && (Start < MinStartValue  || MinStartValue == 0))
                    {
                        DIST_USERS objDistUser = _provider.GetDistUserObj(Convert.ToInt32(hdnUserId.Value));
                        if (objDistUser != null)
                        {
                            TicketsDistribution distObj = new TicketsDistribution();
                            distObj.CreatedDate = DateTime.Now;
                            distObj.ModifiedDate = DateTime.Now;
                            distObj.LastAccessUserID = UserSession.Inst.UserPK;
                            distObj.FromTicket = txtTicketStart.Text;
                            distObj.ToTicket = txtTicketEnd.Text;
                            distObj.TotalTickets = Convert.ToInt32(Convert.ToInt32(distObj.ToTicket) - Convert.ToInt32(distObj.FromTicket) + 1);
                            distObj.Dist_user_Fk = objDistUser.user_pk;
                            objDistUser.Balance += distObj.TotalTickets;

                            if (UserSession.Inst.UserType == "ADMIN")
                            {
                                if (UserSession.Inst.Member2MemberPK > 0)
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                    distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
                                    distObj.MEMBER_FK = UserSession.Inst.MemberPK;
                                    distObj.MEM2MEM_FK = UserSession.Inst.Member2MemberPK;
                                }
                                else if (UserSession.Inst.MemberPK > 0)
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                    distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
                                    distObj.MEMBER_FK = UserSession.Inst.MemberPK;
                                }
                                else if (UserSession.Inst.ChurchPK > 0)
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                    distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
                                }
                                else if (UserSession.Inst.RafflePK > 0)
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                }
                                
                            }
                            
                            else
                            {
                                if (UserSession.Inst.UserType == "RAFFLE")
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                }
                                else if (UserSession.Inst.UserType == "CHURCH")
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                    distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
                                }
                                else if (UserSession.Inst.UserType == "MEMBER")
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                    distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
                                    distObj.MEMBER_FK = UserSession.Inst.MemberPK;
                                }
                                else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                                {
                                    distObj.RAFFLE_FK = UserSession.Inst.RafflePK;
                                    distObj.CHURCH_FK = UserSession.Inst.ChurchPK;
                                    distObj.MEMBER_FK = UserSession.Inst.MemberPK;
                                    distObj.MEM2MEM_FK = UserSession.Inst.Member2MemberPK;
                                }
                            }


                            int result = _provider.AddTicketDistribution(distObj, objDistUser);
                            if (result > 0)
                            {
                                if (UserSession.Inst.UserType == "ADMIN")
                                {
                                    //Response.Redirect("Admin/RaffleList.aspx");
                                    if (UserSession.Inst.Member2MemberPK > 0)
                                    {
                                        Response.Redirect("/member2member/Dashboard.aspx");
                                    }
                                    if (UserSession.Inst.MemberPK > 0)
                                    {
                                        Response.Redirect("/member/Dashboard.aspx");
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
                                        Response.Redirect("/admin/dashboard.aspx");
                                    }
                                }
                                else if (UserSession.Inst.UserType == "RAFFLE")
                                {
                                    Response.Redirect("/raffle/dashboard.aspx");
                                }
                                else if (UserSession.Inst.UserType == "CHURCH")
                                {
                                    Response.Redirect("/church/dashboard.aspx");
                                }
                                else if (UserSession.Inst.UserType == "MEMBER")
                                {
                                    Response.Redirect("/member/dashboard.aspx");
                                }
                                else if (UserSession.Inst.UserType == "MEMBER2MEMBER")
                                {
                                    Response.Redirect("/member2member/dashboard.aspx");
                                }
                            }
                        }
                    }
                    else
                    {
                        lblErrorMsg.Text = "You have entered wrong series number!";
                        divError.Visible = true;
                        return;
                    }

                }
                else
                {
                    lblErrorMsg.Text = "You have entered wrong series number!";
                    divError.Visible = true;
                    return;
                }
            }
            else
            {
                lblErrorMsg.Text = "Start number should be less then End number";
                divError.Visible = true;
                return;
            }
        }
    }


}