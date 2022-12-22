using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using RaffleModel;

/// <summary>
/// Summary description for UsserSession
/// </summary>
public class UserSession
{

    #region [GLOBAL DECLARATION]

    static UserSession _nonWebSession;
    RaffleController _controller = new RaffleController();
    
    protected int _userPkey;
    protected int _rafflePkey;
    protected int _churchPK;
    protected int _memberPK;
    protected int _ticketDistPk;
    protected int _member2MemberPK;

    protected DIST_USERS _raffleObj;
    protected DIST_USERS _chruchObj;
    protected DIST_USERS _memberObj;
    protected DIST_USERS _member2memberObj;
    protected DIST_USERS _adminObj;
    protected TicketsDistribution _ticketDistributionObj;
    protected List<PagePermission> _pagePermissionList;
    protected User userObj;
    protected RaffleYear _currentYearRaffle;
    protected RaffleYear _currentYearLogin;
    protected int _currentRaffleID;

    #endregion

    #region Property

    public int UserPK
    {
        get
        {
            return _userPkey;
        }
        set
        {
            _userPkey = value;
        }
    }

    public int RafflePK
    {
        get
        {
            return _rafflePkey;
        }
        set
        {
            _rafflePkey = value;
        }
    }

    public int ChurchPK
    {
        get
        {
            return _churchPK;
        }
        set
        {
            _churchPK = value;
        }
    }

    public int MemberPK
    {
        get
        {
            return _memberPK;
        }
        set
        {
            _memberPK = value;
        }
    }

    public int Member2MemberPK
    {
        get
        {
            return _member2MemberPK;
        }
        set
        {
            _member2MemberPK = value;
        }
    }

    public int TicketDistPk
    {
        get
        {
            return _ticketDistPk;
        }
        set
        {
            _ticketDistPk = value;
        }
    }
    

    public string Name { get; set; }

    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public string UserType { get; set; }

    public string SystemUserRole { get; set; }

    public int SystemUserFK { get; set; }

    public bool IsSystemUser { get; set; }
    public int LoginedUserFK { get; set; }
    
    public tblBillingInformation BillingInfo { get;set;}

    public CardInfo CardInfo { get; set; }

    public List<PurchaseTickets> lstPurchaseTicket { get; set; }

    public string PurchaseChurchName { get; set; }

    public DIST_USERS AdminObj
    {
        get
        {
            if (_adminObj == null)
            {
                if (UserPK > 0)
                {
                    _controller = new RaffleController();
                    _adminObj = _controller.GetDistUserObj(UserPK);
                }
            }

            return _adminObj;
        }
        set
        {
            _adminObj = value;
        }
    }

    public DIST_USERS RaffleObj
    {
        get
        {
            if (_raffleObj == null)
            {
                if (_rafflePkey > 0)
                {
                    _controller= new RaffleController();
                    _raffleObj= _controller.GetDistUserObj(_rafflePkey);
                }
            }

            return _raffleObj;
        }
        set
        {
            _raffleObj = value;
        }
    }

    public DIST_USERS ChurchObj
    {
        get
        {
            if (_chruchObj == null)
            {
                if (_churchPK > 0)
                {
                    _controller = new RaffleController();
                    _chruchObj= _controller.GetDistUserObj(_churchPK);
                }
            }

            return _chruchObj;
        }
        set
        {
            _chruchObj = value;
        }
    }

    public DIST_USERS MemberObj
    {
        get
        {
            if (_memberObj == null)
            {
                if (_memberPK > 0)
                {
                    _controller = new RaffleController();
                    _memberObj = _controller.GetDistUserObj(_memberPK);
                }
            }

            return _memberObj;
        }
        set
        {
            _memberObj = value;
        }
    }

    public DIST_USERS Member2MemberObj
    {
        get
        {
            if (_member2memberObj == null)
            {
                if (_member2MemberPK > 0)
                {
                    _controller = new RaffleController();
                    _member2memberObj = _controller.GetDistUserObj(_member2MemberPK);
                }
            }

            return _member2memberObj;
        }
        set
        {
            _member2memberObj = value;
        }
    }


    public TicketsDistribution TicketDistributionObj
    {
        get
        {
            if (_ticketDistributionObj == null)
            {
                if (UserPK > 0)
                {
                    _controller = new RaffleController();
                    _ticketDistributionObj = _controller.GetTicketDistObj(UserPK);
                }
            }

            return _ticketDistributionObj;
        }
        set
        {
            _ticketDistributionObj = value;
        }
    }


    public User ReportUserObj
    {
        get
        {
            if (userObj == null)
            {
                if (LoginedUserFK > 0)
                {
                    _controller = new RaffleController();
                    userObj = _controller.GetUserObj(LoginedUserFK);
                }
            }

            return userObj;
        }
        set
        {
            userObj = value;
        }
    }


    public List<PagePermission> PagePermissionList
    {
        get
        {
            if (_pagePermissionList == null)
            {
                if (UserPK > 0 && LoginedUserFK > 0)
                {
                    _pagePermissionList = _controller.GetUserPagePermissionByUserID(LoginedUserFK);
                }
            }

            return _pagePermissionList;
        }
        set
        {
            _pagePermissionList = value;
        }
    }

    public bool IsUserLoggedIn
    {
        get { return _userPkey > 0 ? true : false; }
    }


    public int CurrentYearID
    {
        get { return CurrenYearRaffle.RaffleYear_ID; }
    }


     public int CurrentLoginID
    {
        get { return CurrenLoginRaffle.RaffleYear_ID; }
    }


    public RaffleYear CurrenYearRaffle
    {
        get
        {
            if (_currentYearRaffle == null)
            {
                _controller = new RaffleController();
                _currentYearRaffle = _controller.GetCurrentYear();
            }
            return _currentYearRaffle;
        }
        set
        {
            _currentYearRaffle = value;
        }
    }

    public RaffleYear CurrenLoginRaffle
    {
        get
        {
            if (_currentYearLogin == null)
            {
                _controller = new RaffleController();
                _currentYearLogin = _controller.CurrentLoginYear();
            }
            return _currentYearLogin;
        }
        set
        {
            _currentYearLogin = value;
        }
    }


    public int CurrentRaffleYearID
    {
        get
        {
            if (_currentRaffleID == 0)
            {
                _controller = new RaffleController();
                _currentRaffleID = _controller.GetCurrentYearID();
            }
            return _currentRaffleID;
        }
        set
        {
            _currentRaffleID = value;
        }
    }


    #endregion

    #region Constructor
    public UserSession()
    {
        Init();
    }
    #endregion

    #region Method

    public static UserSession Inst
    {
        get
        {

            if (System.Web.HttpContext.Current == null)
            {
                if (_nonWebSession == null)
                {
                    _nonWebSession = new UserSession();
                }
                return _nonWebSession;
            }
            else
            {
                if (HttpContext.Current.Handler is IRequiresSessionState || HttpContext.Current.Handler is IReadOnlySessionState)
                {
                    return (UserSession)HttpContext.Current.Session["AuthorizedUser"];
                }
                else
                {
                    _nonWebSession = new UserSession();
                    return _nonWebSession;
                }
            }
        }

    }

    public static void Create()
    {
        UserSession userSess = (UserSession)HttpContext.Current.Session["AuthorizedUser"];
        if (userSess == null)
            HttpContext.Current.Session["AuthorizedUser"] = new UserSession();
    }

    private void Init()
    {
        if (HttpContext.Current == null)
        {
            HttpContext.Current.Session["AuthorizedUser"] = new UserSession();
            // HttpContext.Current.Session.Timeout = BVeConnect.Data.XmlProvider.SessionTimeOut;
        }
        string httpHost = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToLower();
    }

    public void Logout()
    {
        Init();
        _userPkey = 0;
        HttpContext.Current.Session.RemoveAll();
        HttpContext.Current.Session.Abandon();
    }


    public string ProfileURL()
    {
        string ret = "/";

        if (UserSession.Inst.UserType == "ADMIN")
        {
            ret ="/Admin/ChangeProfile.aspx";
        }
        if (UserSession.Inst.UserType == "CHURCH")
        {
            ret = "/Church/ChangeProfile.aspx";
        }
        if (UserSession.Inst.UserType == "RAFFLE")
        {
            ret = "/Raffle/ChangeProfile.aspx";
        }
        if (UserSession.Inst.UserType == "MEMBER")
        {
            ret = "/Member/ChangeProfile.aspx";
        }

        return ret;

    }

    public void Login(int userPk, string name, string userType, int ticketDistPk, string userName, int loginedUserFK)
    {
        UserPK = userPk;
        Name = name;
        UserType = userType;
        UserName = userName;
        LoginedUserFK = loginedUserFK;
        if (userType == "RAFFLE")
        {
            _rafflePkey = userPk;
        }
        else if (userType == "CHURCH")
        {
            _churchPK = userPk;
        }
        else if (userType == "MEMBER")
        {
            _memberPK = userPk;
        }
        else if (userType == "MEMBER2MEMBER")
        {
            _member2MemberPK = userPk;
        }

        TicketDistPk = ticketDistPk;

        if (loginedUserFK > 0)
        {
            _pagePermissionList = _controller.GetUserPagePermissionByUserID(loginedUserFK);
        }
        else
        {
            _pagePermissionList = new List<PagePermission>();
        }

    }

    #endregion

}

public class CardInfo
{
    public string CardNumber { get; set; }
    public string CardName { get; set; }
    public int CardExpMonth { get; set; }
    public int CardExpYear { get; set; }
    public string CVV2 { get; set; }

    public decimal Amount { get; set; }

}
