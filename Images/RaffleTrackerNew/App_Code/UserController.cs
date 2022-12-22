using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaffleModel;


/// <summary>
/// Summary description for UserController
/// </summary>
public class UserController
{

    //
    // TODO: Add constructor logic here
    //

    #region [DECLRATION]
    RaffleEntities _entities = new RaffleEntities();
    #endregion


    public User GetSystemUserLogin(string UserName, string Password)
    {

        var _user = (from _q in _entities.Users.AsQueryable()
                     where _q.username.ToLower().Equals(UserName.ToLower()) && _q.password.Equals(Password)
                     select _q).FirstOrDefault();

        return _user;

    }


    public DIST_USERS GetAdminLogin(string UserName, string Password)
    {

        var _user = (from _q in _entities.DIST_USERS.AsQueryable()
                     where _q.UserName.ToLower().Equals(UserName.ToLower()) && _q.password.Equals(Password) &&  (_q.YearID==UserSession.Inst.CurrentLoginID)
                     select _q).FirstOrDefault();

        return _user;

    }

    //public int AddPatientTrans(UserTransaction userTransObj)
    //{
    //    try
    //    {
    //        User userObj = _entities.Users.Where(x => x.User_Pk == userTransObj.user_Fk).FirstOrDefault();
    //        if (userObj != null)
    //        {
    //            userObj.BalanceAmount -= userTransObj.grams_amount;
    //            _entities.UserTransactions.Add(userTransObj);
    //            _entities.SaveChanges();

    //            return userTransObj.trans_pk;
    //        }
    //        else
    //        {
    //            return 0;
    //        }
    //    }
    //    catch
    //    {
    //        return 0;
    //    }

    //}


    public List<DIST_USERS> GetUserList()
    {
        List<DIST_USERS> lstUser = _entities.DIST_USERS.ToList();



        return lstUser;
    }

    public List<User> GetAllSystemUserList()
    {
        return (from user in _entities.Users join dist in _entities.DIST_USERS on user.DistUser_Fk equals dist.user_pk where dist.YearID==UserSession.Inst.CurrentLoginID select user).ToList();
    }

    public List<User> GetSystemUserList()
    {
        return _entities.Users.ToList();
    }

    public List<User> GetAdminUserList(int adminUserFk)
    {
        return (from user in _entities.Users join dist in _entities.DIST_USERS on user.DistUser_Fk equals dist.user_pk where dist.parent_userFk == adminUserFk && user.UserType=="ADMIN" select user).ToList();
    }

    
    public int AddUser(DIST_USERS userObj)
    {
        if (_entities.DIST_USERS.Where(X => X.UserName.Trim().ToLower() == userObj.UserName.ToLower().Trim()).FirstOrDefault() == null)
        {
            _entities.DIST_USERS.AddObject(userObj);
            _entities.SaveChanges();
            return userObj.user_pk;
        }
        else
        {
            return 0;
        }
    }
      

    public int AddUser(DIST_USERS userObj, TicketsDistribution objDist)
    {
        if (string.IsNullOrEmpty(userObj.UserName) || _entities.DIST_USERS.Where(X => X.UserName.Trim().ToLower() == userObj.UserName.ToLower().Trim()).FirstOrDefault() == null)
        {
            _entities.DIST_USERS.AddObject(userObj);
            _entities.SaveChanges();

            if (userObj.UserType == "RAFFLE")
            {
                objDist.RAFFLE_FK = userObj.user_pk;
            }
            else if (userObj.UserType == "CHURCH")
            {
                objDist.CHURCH_FK = userObj.user_pk;
            }
            else if (userObj.UserType == "MEMBER")
            {
                objDist.MEMBER_FK = userObj.user_pk;
            }
            else if (userObj.UserType == "MEMBER2MEMBER")
            {
                objDist.MEM2MEM_FK = userObj.user_pk;
            }
            
            objDist.Dist_user_Fk = userObj.user_pk;

            _entities.TicketsDistributions.AddObject(objDist);
            _entities.SaveChanges();

            _entities.DIST_USERS.Where(x => x.user_pk == userObj.user_pk).FirstOrDefault().TicketDist_Fk = Convert.ToInt32(objDist.Tickt_Distr_pk);
            if (userObj.UserType == "RAFFLE")
            {
                _entities.DIST_USERS.Where(x => x.user_pk == userObj.user_pk).FirstOrDefault().RAFFLE_FK = Convert.ToInt32(userObj.user_pk);
            }
            else if (userObj.UserType == "CHURCH")
            {
                _entities.DIST_USERS.Where(x => x.user_pk == userObj.user_pk).FirstOrDefault().CHURCH_FK = Convert.ToInt32(userObj.user_pk);
            }
            else if (userObj.UserType == "MEMBER")
            {
                _entities.DIST_USERS.Where(x => x.user_pk == userObj.user_pk).FirstOrDefault().MEMBER_FK = Convert.ToInt32(userObj.user_pk);
            }
            else if (userObj.UserType == "MEMBER2MEMBER")
            {
                _entities.DIST_USERS.Where(x => x.user_pk == userObj.user_pk).FirstOrDefault().MEM2MEM_FK = Convert.ToInt32(userObj.user_pk);
            }

            _entities.SaveChanges();

            int _fromTicket = Convert.ToInt32(objDist.FromTicket);
            int _toTicket=Convert.ToInt32(objDist.ToTicket);

            List<TicketSold> lstTicketSold = _entities.TicketSolds.Where(x => x.TicketFrom <= _fromTicket && x.TicketTo >= _toTicket).ToList();

            if (lstTicketSold != null && lstTicketSold.Count > 0)
            {
                foreach (TicketSold obj in lstTicketSold)
                {
                    obj.LastDistUserId = userObj.user_pk;
                    obj.LastDistUserName = userObj.name;
                    obj.MEM2MEM_FK = userObj.MEM2MEM_FK;
                    obj.MEMBER_FK = userObj.MEMBER_FK;
                    obj.RAFFLE_FK = userObj.RAFFLE_FK;
                    obj.CHURCH_FK = userObj.CHURCH_FK;
                }
                
                _entities.SaveChanges();
            }




            return userObj.user_pk;
        }
        else
        {
            return 0;
        }
    }


    public int UpdateUser(DIST_USERS userObj)
    {
        DIST_USERS _userObj = _entities.DIST_USERS.Where(X => X.user_pk == userObj.user_pk).FirstOrDefault();
        if (_userObj != null)
        {
            _userObj.name = userObj.name;
            _userObj.phone = userObj.phone;
            _userObj.Address = userObj.Address;
            _userObj.mobile = userObj.mobile;
            _userObj.modifiedDate = DateTime.Now;
            _userObj.email = userObj.email;
            _userObj.UserName = userObj.UserName;
            _userObj.ChurchAdminstName = userObj.ChurchAdminstName;
            _userObj.TicketRate = userObj.TicketRate;
            _userObj.Zip = userObj.Zip;
            _userObj.State = userObj.State;
            _userObj.City = userObj.City;
            _userObj.ChurchType = userObj.ChurchType;
            if (!string.IsNullOrEmpty(userObj.password))
            {
                _userObj.password = userObj.password;
            }
            else
            {
                if (string.IsNullOrEmpty(userObj.UserName))
                {
                    _userObj.password = userObj.password;
                }
            }
            _entities.SaveChanges();
            return _userObj.user_pk;
        }

        return 0;
    }

    public int UpdatePassword(string strPass, int UserPk)
    {
        DIST_USERS _userObj = _entities.DIST_USERS.Where(X => X.user_pk == UserPk).FirstOrDefault();
        if (_userObj != null)
        {
            _userObj.password = strPass;
            _entities.SaveChanges();
            return _userObj.user_pk;
        }

        return 0;

    }

    public void UpdateAllChurchMemberTicketRate(int churchFk, decimal rate)
    {
        List<DIST_USERS> lstDistUser = _entities.DIST_USERS.Where(x => x.CHURCH_FK == churchFk).ToList();

        foreach (DIST_USERS obj in lstDistUser)
        {
            obj.TicketRate = rate;
        }
        _entities.SaveChanges();

    }

    public int UpdateUser(DIST_USERS userObj, TicketsDistribution objDist)
    {
        DIST_USERS _userObj = _entities.DIST_USERS.Where(X => X.user_pk == userObj.user_pk).FirstOrDefault();
        if (_userObj != null)
        {
            _userObj.name = userObj.name;
            _userObj.phone = userObj.phone;
            _userObj.Address = userObj.Address;
            _userObj.mobile = userObj.mobile;
            _userObj.modifiedDate = DateTime.Now;
            if (!string.IsNullOrEmpty(userObj.password))
            {
                _userObj.password = userObj.password;
            }

            TicketsDistribution _distObj = _entities.TicketsDistributions.Where(X => X.Tickt_Distr_pk == _userObj.TicketDist_Fk).FirstOrDefault();
            if (_distObj != null)
            {
                _distObj.LastAccessUserID = objDist.LastAccessUserID;
                _distObj.FromTicket = objDist.FromTicket;
                _distObj.ToTicket = objDist.ToTicket;
                _distObj.TotalTickets = Convert.ToInt32(objDist.TotalTickets);
            }

            _entities.SaveChanges();

            return _userObj.user_pk;
        }

        return 0;

    }





}