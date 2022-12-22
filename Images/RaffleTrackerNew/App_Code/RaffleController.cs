using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaffleModel;
using System.Text;


/// <summary>
/// Summary description for RaffleController
/// </summary>
public class RaffleController
{
    #region [DECLRATION]
    RaffleEntities _entities = new RaffleEntities();
    #endregion

    public RaffleController()
    {
        //
        // TODO: Add constructor logic here
        //









    }


    public int AddDistUser(DIST_USERS distributionObj, string userTye)
    {
        _entities.DIST_USERS.AddObject(distributionObj);
        _entities.SaveChanges();

        if (userTye == "RAFFLE")
        {
            distributionObj.RAFFLE_FK = distributionObj.user_pk;

        }
        else if (userTye == "CHURCH")
        {
            distributionObj.CHURCH_FK = distributionObj.user_pk;
        }
        else if (userTye == "MEMBER")
        {
            distributionObj.MEMBER_FK = distributionObj.user_pk;
        }
        else if (userTye == "MEMBER2MEMBER")
        {
            distributionObj.MEM2MEM_FK = distributionObj.user_pk;
        }

        _entities.SaveChanges();
        return distributionObj.user_pk;
    }


    public int AddDistUser(DIST_USERS distributionObj)
    {
        _entities.DIST_USERS.AddObject(distributionObj);
        _entities.SaveChanges();
        return distributionObj.user_pk;
    }




    public int AddRaffle(RaffleYear objRaffle, int currentYear)
    {
        foreach (RaffleYear obj in _entities.RaffleYears)
        {
            obj.IsCurrentYear = false;
            obj.IsLoginYear = false;

        }

        _entities.RaffleYears.AddObject(objRaffle);
        _entities.SaveChanges();
        int raffleId = objRaffle.RaffleYear_ID;


        DIST_USERS objPrevObj = _entities.DIST_USERS.Where(x => x.UserType == "RAFFLE" && x.YearID == currentYear).FirstOrDefault();

        DIST_USERS objDistUser = new DIST_USERS();

        objDistUser.Address = objPrevObj.Address;
        objDistUser.City = objPrevObj.City;
        objDistUser.CreatedBy_UserFk = objPrevObj.CreatedBy_UserFk;
        objDistUser.createdDate = DateTime.Now;
        objDistUser.email = objPrevObj.email;
        objDistUser.FullAddress = objPrevObj.FullAddress;
        objDistUser.LastaccessUserId = objPrevObj.LastaccessUserId;
        objDistUser.mobile = objPrevObj.mobile;
        objDistUser.modifiedDate = DateTime.Now;
        objDistUser.name = "Connecticut Parish Raffle " + objRaffle.RaffleYear1;
        objDistUser.parent_userFk = 1;
        objDistUser.password = objPrevObj.password;
        objDistUser.phone = objPrevObj.phone;
        objDistUser.RAFFLE_FK = 0;
        objDistUser.State = objPrevObj.State;
        objDistUser.status = objPrevObj.status;
        objDistUser.UserName = objPrevObj.UserName;
        objDistUser.UserType = objPrevObj.UserType;
        objDistUser.Zip = objPrevObj.Zip;
        objDistUser.YearID = raffleId;

        _entities.DIST_USERS.AddObject(objDistUser);
        _entities.SaveChanges();

        objDistUser.RAFFLE_FK = objDistUser.user_pk;
        _entities.SaveChanges();

        return raffleId;

    }



    public void ChangeRaffleLoginYear(int raffleId)
    {
        foreach (RaffleYear obj in _entities.RaffleYears)
        {
            obj.IsLoginYear = false;
        }

        RaffleYear objRaffle = _entities.RaffleYears.Where(x => x.RaffleYear_ID == raffleId).FirstOrDefault();
        objRaffle.IsLoginYear = true;
        _entities.SaveChanges();
    }


    public RaffleYear GetCurrentYear()
    {
        return _entities.RaffleYears.Where(x => x.IsCurrentYear).FirstOrDefault();
    }


    public int GetCurrentYearID()
    {
        return _entities.RaffleYears.Max(x => x.RaffleYear_ID);
    }


    public RaffleYear CurrentLoginYear()
    {
        return _entities.RaffleYears.Where(x => x.IsLoginYear).FirstOrDefault();
    }


    public bool IsUserExist(string userName, int userKey, string userType)
    {
        bool RetValue = false;

        if (userType == "ADMIN")
        {

            if (userKey > 0)
            {
                if (_entities.DIST_USERS.Any(x => x.UserName.ToLower().Trim() == userName && x.user_pk != userKey && x.YearID == UserSession.Inst.CurrentRaffleYearID))
                {
                    RetValue = true;
                }
            }
            else
            {
                if (_entities.DIST_USERS.Any(x => x.UserName.ToLower().Trim() == userName && x.YearID == UserSession.Inst.CurrentRaffleYearID))
                {
                    RetValue = true;
                }
            }
        }
        else
        {
            if (userKey > 0)
            {
                //var tmp = (from m1 in _entities.Users join m2 in _entities.DIST_USERS on m1.DistUser_Fk equals m2.user_pk where m1.username.ToLower().Trim() == userName && m1.user_pk != userKey && m2.YearID==UserSession.Inst.CurrentLoginID select m1);
                var tmp = (from m1 in _entities.DIST_USERS join m2 in _entities.Users on m1.user_pk equals m2.DistUser_Fk where m1.YearID == UserSession.Inst.CurrentRaffleYearID && m2.username.ToLower().Trim() == userName && m1.user_pk != userKey select m2);
                if (tmp != null && tmp.FirstOrDefault() !=null)
                {
                    RetValue = true;
                }
            }
            else
            {
                var tmp = (from m1 in _entities.Users join m2 in _entities.DIST_USERS on m1.DistUser_Fk equals m2.user_pk where m1.username.ToLower().Trim() == userName && m2.YearID == UserSession.Inst.CurrentRaffleYearID select m1);
                if (tmp != null && tmp.FirstOrDefault() !=null)
                {
                    RetValue = true;
                }

            }
        }

        return RetValue;
    }

    

    public int AddTicketDistribution(TicketsDistribution distributionObj, DIST_USERS objUser)
    {
        DIST_USERS userObj = _entities.DIST_USERS.Where(X => X.user_pk == objUser.user_pk).FirstOrDefault();
        userObj.Balance = Convert.ToInt32(objUser.Balance);

        distributionObj.YearID = UserSession.Inst.CurrentRaffleYearID;
        _entities.TicketsDistributions.AddObject(distributionObj);
        _entities.SaveChanges();



        int _fromTicket = Convert.ToInt32(distributionObj.FromTicket);
        int _toTicket = Convert.ToInt32(distributionObj.ToTicket);

        List<TicketSold> lstTicketSold = _entities.TicketSolds.Where(x => x.TicketFrom <= _fromTicket && x.TicketTo >= _toTicket && x.YearID == UserSession.Inst.CurrentRaffleYearID).ToList();

        if (lstTicketSold != null && lstTicketSold.Count > 0)
        {
            foreach (TicketSold obj in lstTicketSold)
            {
                obj.LastDistUserId = userObj.user_pk;
                obj.LastDistUserName = userObj.name;
                obj.LastAccessUserID = userObj.user_pk;
                obj.MEM2MEM_FK = userObj.MEM2MEM_FK;
                obj.MEMBER_FK = userObj.MEMBER_FK;
                obj.RAFFLE_FK = userObj.RAFFLE_FK;
                obj.CHURCH_FK = userObj.CHURCH_FK;
            }
            _entities.SaveChanges();
        }

        return distributionObj.Tickt_Distr_pk;
    }


    public int DeleteTicketDistribution(int id)
    {
        //_entities.DIST_USERS.Where(X => X.user_pk == objUser.user_pk).FirstOrDefault().Balance = Convert.ToInt32(objUser.Balance);
        TicketsDistribution objTicket = _entities.TicketsDistributions.Where(x => x.Tickt_Distr_pk == id).FirstOrDefault();
        if (objTicket != null)
        {
            _entities.TicketsDistributions.DeleteObject(objTicket);
            //DIST_USERS objUser = _entities.DIST_USERS.Where(X => X.TicketDist_Fk == id).FirstOrDefault();
            //if (objUser != null)
            //{
            //    _entities.DIST_USERS.DeleteObject(objUser);
            //}
            _entities.SaveChanges();
            return 1;
        }
        return 0;
    }

    public int DeleteSale(int id)
    {
        //_entities.DIST_USERS.Where(X => X.user_pk == objUser.user_pk).FirstOrDefault().Balance = Convert.ToInt32(objUser.Balance);
        TicketSold objTicket = _entities.TicketSolds.Where(x => x.TicketSold_fk == id).FirstOrDefault();
        if (objTicket != null)
        {
            _entities.TicketSolds.DeleteObject(objTicket);
            _entities.SaveChanges();
            return 1;
        }
        return 0;
    }

    public int DeleteSystemUser(int id)
    {
        User _userObj = _entities.Users.Where(X => X.user_pk == id).FirstOrDefault();
        if (_userObj != null)
        {
            _entities.DeleteObject(_userObj);
            _entities.SaveChanges();
            return 1;
        }
        return 0;
    }

    //public int DeleteDistribution(int id)
    //{
    //    TicketsDistribution objTicket = _entities.TicketsDistributions.Where(x => x.Tickt_Distr_pk == id).FirstOrDefault();
    //    if (objTicket != null)
    //    {
    //        _entities.TicketsDistributions.DeleteObject(objTicket);
    //        _entities.SaveChanges();
    //        return 1;
    //    }
    //    return 0;
    //}

    public int DeleteUser(int id)
    {
        //_entities.DIST_USERS.Where(X => X.user_pk == objUser.user_pk).FirstOrDefault().Balance = Convert.ToInt32(objUser.Balance);
        DIST_USERS objUser = _entities.DIST_USERS.Where(x => x.user_pk == id).FirstOrDefault();
        if (objUser != null)
        {

            if (objUser.UserType == "RAFFLE")
            {
                List<DIST_USERS> lstDistUser = _entities.DIST_USERS.Where(x => x.RAFFLE_FK == objUser.user_pk).ToList();

                if (lstDistUser != null)
                {
                    foreach (DIST_USERS obj in lstDistUser)
                    {
                        _entities.DIST_USERS.DeleteObject(obj);

                        List<TicketsDistribution> lstTicketDist = _entities.TicketsDistributions.Where(x => x.Dist_user_Fk == obj.user_pk).ToList();

                        if (lstTicketDist != null)
                        {
                            foreach (TicketsDistribution obj1 in lstTicketDist)
                            {
                                _entities.TicketsDistributions.DeleteObject(obj1);
                            }
                        }

                    }

                    _entities.SaveChanges();
                }

            }
            else if (objUser.UserType == "CHURCH")
            {
                List<DIST_USERS> lstDistUser = _entities.DIST_USERS.Where(x => x.CHURCH_FK == objUser.user_pk).ToList();

                if (lstDistUser != null)
                {
                    foreach (DIST_USERS obj in lstDistUser)
                    {
                        _entities.DIST_USERS.DeleteObject(obj);

                        List<TicketsDistribution> lstTicketDist = _entities.TicketsDistributions.Where(x => x.Dist_user_Fk == obj.user_pk).ToList();

                        if (lstTicketDist != null)
                        {
                            foreach (TicketsDistribution obj1 in lstTicketDist)
                            {
                                _entities.TicketsDistributions.DeleteObject(obj1);
                            }
                        }

                    }

                    _entities.SaveChanges();
                }

            }
            else if (objUser.UserType == "MEMBER")
            {
                List<DIST_USERS> lstDistUser = _entities.DIST_USERS.Where(x => x.MEMBER_FK == objUser.user_pk).ToList();

                if (lstDistUser != null)
                {
                    foreach (DIST_USERS obj in lstDistUser)
                    {
                        _entities.DIST_USERS.DeleteObject(obj);

                        List<TicketsDistribution> lstTicketDist = _entities.TicketsDistributions.Where(x => x.Dist_user_Fk == obj.user_pk).ToList();

                        if (lstTicketDist != null)
                        {
                            foreach (TicketsDistribution obj1 in lstTicketDist)
                            {
                                _entities.TicketsDistributions.DeleteObject(obj1);
                            }
                        }

                    }

                    _entities.SaveChanges();
                }
            }
            else if (objUser.UserType == "MEMBER2MEMBER")
            {
                List<DIST_USERS> lstDistUser = _entities.DIST_USERS.Where(x => x.MEM2MEM_FK == objUser.user_pk).ToList();

                if (lstDistUser != null)
                {
                    foreach (DIST_USERS obj in lstDistUser)
                    {
                        _entities.DIST_USERS.DeleteObject(obj);

                        List<TicketsDistribution> lstTicketDist = _entities.TicketsDistributions.Where(x => x.Dist_user_Fk == obj.user_pk).ToList();

                        if (lstTicketDist != null)
                        {
                            foreach (TicketsDistribution obj1 in lstTicketDist)
                            {
                                _entities.TicketsDistributions.DeleteObject(obj1);
                            }
                        }

                    }

                    _entities.SaveChanges();
                }
            }
                        

            _entities.DIST_USERS.DeleteObject(objUser);
            _entities.SaveChanges();
            return 1;
        }
        return 0;
    }

    public bool IsTicketSold(int FromTicket, int ToTicket, int userFkey)
    {
        if (userFkey > 0)
        {
            if (_entities.TicketSolds.Where(x => x.TicketFrom <= FromTicket && x.TicketTo >= ToTicket && x.LastAccessUserID != userFkey && x.YearID == UserSession.Inst.CurrentLoginID).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (_entities.TicketSolds.Where(x => x.TicketFrom <= FromTicket && x.TicketTo >= ToTicket && x.YearID == UserSession.Inst.CurrentLoginID).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public int AddTicketSold(TicketSold ticketSoldObj)
    {
        ticketSoldObj.YearID = UserSession.Inst.CurrentRaffleYearID;
        _entities.TicketSolds.AddObject(ticketSoldObj);
        _entities.SaveChanges();
        return ticketSoldObj.TicketSold_fk;
    }


    public void UpdateTicketSaleByRate(int LastDistUserFkey, decimal rate)
    {
        List<TicketSold> lstTicketSold = _entities.TicketSolds.Where(x => x.CHURCH_FK == LastDistUserFkey).ToList();

        foreach (TicketSold obj in lstTicketSold)
        {
            obj.Amount = obj.TicketTotal * rate;
        }
        _entities.SaveChanges();
    }

    public int UpdateTicketSold(TicketSold ticketSoldObj)
    {
        TicketSold objSold = _entities.TicketSolds.Where(X => X.TicketSold_fk == ticketSoldObj.TicketSold_fk).FirstOrDefault();

        if (objSold != null)
        {
            objSold.GivenTo = ticketSoldObj.GivenTo;
            objSold.LastAccessUserID = ticketSoldObj.LastAccessUserID;
            objSold.LastAccessUserName = ticketSoldObj.LastAccessUserName;
            objSold.LastDistUserId = ticketSoldObj.LastDistUserId;
            objSold.LastDistUserName = ticketSoldObj.LastDistUserName;
            objSold.Phone = ticketSoldObj.Phone;
            objSold.TicketFrom = ticketSoldObj.TicketFrom;
            objSold.TicketTo = ticketSoldObj.TicketTo;
            objSold.TicketTotal = ticketSoldObj.TicketTotal;
            objSold.Amount = ticketSoldObj.Amount;
            objSold.Collected_By = ticketSoldObj.Collected_By;
            objSold.Email = ticketSoldObj.Email;
            objSold.City = ticketSoldObj.City;
            objSold.State = ticketSoldObj.State;
            objSold.Zip = ticketSoldObj.Zip;
            _entities.SaveChanges();
        }
        return ticketSoldObj.TicketSold_fk;
    }


    public void UpdateTicketAmount(int USERFk, decimal ticketRate, string userType)
    {
        List<TicketSold> lstTicketSoldList = new List<TicketSold>();
            
        if(userType=="CHURCH")
        {
            lstTicketSoldList = _entities.TicketSolds.Where(x => x.YearID == UserSession.Inst.CurrentRaffleYearID && x.CHURCH_FK == USERFk && x.MEMBER_FK == null).ToList();
        }
        else if(userType=="MEMBER")
        {
            lstTicketSoldList = _entities.TicketSolds.Where(x => x.YearID == UserSession.Inst.CurrentRaffleYearID && x.MEMBER_FK == USERFk).ToList();
        }
        else if(userType=="RAFFLE")
        {
            lstTicketSoldList = _entities.TicketSolds.Where(x => x.YearID == UserSession.Inst.CurrentRaffleYearID && x.RAFFLE_FK == USERFk && x.CHURCH_FK == null).ToList();
        }

        foreach(TicketSold obj in lstTicketSoldList)
        {
            obj.Amount=  obj.TicketTotal * ticketRate;
        }

        _entities.SaveChanges();

    }


    public TicketSold GetTicketSoldObj(int pkey)
    {
        //return _entities.TicketSolds.Where(X => X.TicketSold_fk == pkey).FirstOrDefault();

        return (from t in _entities.TicketSolds.AsEnumerable()
                //join u in _entities.DIST_USERS.AsEnumerable() on t.LastAccessUserID equals u.user_pk
                where t.TicketSold_fk == pkey
                select new TicketSold
                {
                    GivenTo = t.GivenTo,
                    Phone = t.Phone,
                    TicketFrom = t.TicketFrom,
                    TicketTo = t.TicketTo,
                    TicketTotal = t.TicketTotal,
                    Amount = t.Amount,
                    Collected_By = t.Collected_By,
                    Email = t.Email,
                    City = t.City,
                    State = t.State,
                    Zip = t.Zip,
                    LastAccessUserID = t.LastAccessUserID,
                    TicketSold_fk = t.TicketSold_fk,
                    Address = t.Address,
                    //LastAccessUserName  = u.name,
                    CreatedDate = t.CreatedDate.AddHours(3),
                }).FirstOrDefault();


    }

    public List<TicketSold> GetTicketSoldList()
    {
        List<TicketSold> lstChruchList = (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.CHURCH_FK != null)
                                          //join u in _entities.DIST_USERS.AsEnumerable() on t.LastAccessUserID equals u.user_pk
                                          join u in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.CHURCH_FK equals u.user_pk into ps
                                          from u in ps.DefaultIfEmpty()
                                          //select new { Category = c, ProductName = p == null ? "(No products)" : p.ProductName };
                                          select new TicketSold
                                          {
                                              GivenTo = t.GivenTo,
                                              Phone = t.Phone,
                                              TicketFrom = t.TicketFrom,
                                              TicketTo = t.TicketTo,
                                              TicketTotal = t.TicketTotal,
                                              Amount = t.Amount,
                                              Collected_By = t.Collected_By,
                                              Email = t.Email,
                                              City = t.City,
                                              State = t.State,
                                              Zip = t.Zip,
                                              LastAccessUserID = t.LastAccessUserID,
                                              LastDistUserId = t.LastDistUserId,
                                              LastDistUserName = t.LastDistUserName,
                                              LastAccessUserName = t.LastAccessUserName,
                                              TicketSold_fk = t.TicketSold_fk,
                                              Address = t.Address,
                                              CreatedDate = t.CreatedDate.AddHours(3),
                                              StrTicketSoldDate = t.CreatedDate.AddHours(3).ToString("MM/dd/yyyy hh:mm tt"),
                                              MEM2MEM_FK = t.MEM2MEM_FK,
                                              MEMBER_FK = t.MEMBER_FK,
                                              CHURCH_FK = t.CHURCH_FK,
                                              RAFFLE_FK = t.RAFFLE_FK,
                                              YearID = t.YearID,
                                              ChurchName = u !=null && !string.IsNullOrEmpty(u.name) ? u.name : "-"
                                          }).ToList();

        List<TicketSold> lstRaffleList = (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.CHURCH_FK == null && x.RAFFLE_FK != null)
                                          //join u in _entities.DIST_USERS.AsEnumerable() on t.LastAccessUserID equals u.user_pk
                                          join u in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.RAFFLE_FK equals u.user_pk into ps
                                          from u in ps.DefaultIfEmpty()
                                          //select new { Category = c, ProductName = p == null ? "(No products)" : p.ProductName };
                                          select new TicketSold
                                          {
                                              GivenTo = t.GivenTo,
                                              Phone = t.Phone,
                                              TicketFrom = t.TicketFrom,
                                              TicketTo = t.TicketTo,
                                              TicketTotal = t.TicketTotal,
                                              Amount = t.Amount,
                                              Collected_By = t.Collected_By,
                                              Email = t.Email,
                                              City = t.City,
                                              State = t.State,
                                              Zip = t.Zip,
                                              LastAccessUserID = t.LastAccessUserID,
                                              LastDistUserId = t.LastDistUserId,
                                              LastDistUserName = t.LastDistUserName,
                                              LastAccessUserName = t.LastAccessUserName,
                                              TicketSold_fk = t.TicketSold_fk,
                                              Address = t.Address,
                                              CreatedDate = t.CreatedDate.AddHours(3),
                                              StrTicketSoldDate = t.CreatedDate.AddHours(3).ToString("MM/dd/yyyy hh:mm tt"),
                                              MEM2MEM_FK = t.MEM2MEM_FK,
                                              MEMBER_FK = t.MEMBER_FK,
                                              CHURCH_FK = t.CHURCH_FK,
                                              RAFFLE_FK = t.RAFFLE_FK,
                                              YearID = t.YearID,
                                              ChurchName = !string.IsNullOrEmpty(u.name) ? u.name : "-"
                                          }).ToList();


        if (lstRaffleList != null && lstRaffleList.Count > 0)
        {
            lstChruchList.AddRange(lstRaffleList);
        }

        return lstChruchList.OrderByDescending(x => x.CreatedDate).ToList();

           }


    public List<TicketSold> GetTicketSoldListByRaffle(int raffleFk)
    {
        List<TicketSold> lstChruchList = (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.RAFFLE_FK == raffleFk)
                                          //select new { Category = c, ProductName = p == null ? "(No products)" : p.ProductName };
                                          select new TicketSold
                                          {
                                              GivenTo = t.GivenTo,
                                              Phone = t.Phone,
                                              TicketFrom = t.TicketFrom,
                                              TicketTo = t.TicketTo,
                                              TicketTotal = t.TicketTotal,
                                              Amount = t.Amount,
                                              Collected_By = t.Collected_By,
                                              Email = t.Email,
                                              City = t.City,
                                              State = t.State,
                                              Zip = t.Zip,
                                              LastAccessUserID = t.LastAccessUserID,
                                              LastDistUserId = t.LastDistUserId,
                                              LastDistUserName = t.LastDistUserName,
                                              LastAccessUserName = t.LastAccessUserName,
                                              TicketSold_fk = t.TicketSold_fk,
                                              Address = t.Address,
                                              CreatedDate = t.CreatedDate.AddHours(3),
                                              StrTicketSoldDate = t.CreatedDate.AddHours(3).ToString("MM/dd/yyyy HH:MM tt"),
                                              MEM2MEM_FK = t.MEM2MEM_FK,
                                              MEMBER_FK = t.MEMBER_FK,
                                              CHURCH_FK = t.CHURCH_FK,
                                              RAFFLE_FK = t.RAFFLE_FK,
                                              YearID = t.YearID,
                                          }).ToList();


        return lstChruchList;

    }


    public List<TicketSold> GetTicketSoldListByGross(string sType)
    {
        return (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                    select new TicketSold
                    {
                        CHURCH_FK = t.CHURCH_FK,
                        TicketTotal = t.TicketTotal,
                        Amount = Convert.ToDecimal(t.TicketTotal * 25) ,
                        CreatedDate = t.CreatedDate.Date
                    }).ToList();

        
        
    }


    public List<TicketSold> GetTicketSoldListSalesBy(string salesBy, string sType)
    {
        if (salesBy == "RAFFLE")
        {
            return (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                    join r in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.RAFFLE_FK equals r.user_pk
                    select new TicketSold
                    {
                        GivenTo = t.GivenTo,
                        Phone = t.Phone,
                        TicketFrom = t.TicketFrom,
                        TicketTo = t.TicketTo,
                        TicketTotal = t.TicketTotal,
                        Amount = sType == "GROSS" ? Convert.ToDecimal(t.TicketTotal * 25) : sType == "NET" && r.UserType == "CHURCH" && r.ChurchType == "PARTNER" ? Convert.ToDecimal(t.TicketTotal * 12.50) : Convert.ToDecimal(t.TicketTotal * 25),
                        Collected_By = t.Collected_By,
                        Email = t.Email,
                        City = t.City,
                        State = t.State,
                        Zip = t.Zip,
                        LastAccessUserID = t.LastAccessUserID,
                        LastDistUserId = t.LastDistUserId,
                        LastDistUserName = t.LastDistUserName,
                        LastAccessUserName = t.LastAccessUserName,
                        TicketSold_fk = t.TicketSold_fk,
                        Address = t.Address,
                        CreatedDate = t.CreatedDate.Date,
                        MEM2MEM_FK = t.MEM2MEM_FK,
                        MEMBER_FK = t.MEMBER_FK,
                        CHURCH_FK = t.CHURCH_FK,
                        RAFFLE_FK = t.RAFFLE_FK,
                        YearID = t.YearID,
                        SalesBy = r.name
                    }).OrderByDescending(x => x.CreatedDate).ToList();

        }
        else if (salesBy == "CHURCH")
        {
            List<TicketSold> lstChruchList = (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.CHURCH_FK != null)
                                              join c in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.CHURCH_FK equals c.user_pk
                                              select new TicketSold
                                              {
                                                  GivenTo = t.GivenTo,
                                                  Phone = t.Phone,
                                                  TicketFrom = t.TicketFrom,
                                                  TicketTo = t.TicketTo,
                                                  TicketTotal = t.TicketTotal,
                                                  Amount = sType == "GROSS" ? Convert.ToDecimal(t.TicketTotal * 25) : sType == "NET" && c.UserType == "CHURCH" && c.ChurchType == "PARTNER" ? Convert.ToDecimal(t.TicketTotal * 12.50) : Convert.ToDecimal(t.TicketTotal * 25),
                                                  Collected_By = t.Collected_By,
                                                  Email = t.Email,
                                                  City = t.City,
                                                  State = t.State,
                                                  Zip = t.Zip,
                                                  LastAccessUserID = t.LastAccessUserID,
                                                  LastDistUserId = t.LastDistUserId,
                                                  LastDistUserName = t.LastDistUserName,
                                                  LastAccessUserName = t.LastAccessUserName,
                                                  TicketSold_fk = t.TicketSold_fk,
                                                  Address = t.Address,
                                                  CreatedDate = t.CreatedDate.Date,
                                                  MEM2MEM_FK = t.MEM2MEM_FK,
                                                  MEMBER_FK = t.MEMBER_FK,
                                                  CHURCH_FK = t.CHURCH_FK,
                                                  RAFFLE_FK = t.RAFFLE_FK,
                                                  YearID = t.YearID,
                                                  SalesBy = c.name
                                              }).ToList();

            List<TicketSold> lstRaffleList = (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.CHURCH_FK == null)
                                              join c in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.RAFFLE_FK equals c.user_pk
                                              select new TicketSold
                                              {
                                                  GivenTo = t.GivenTo,
                                                  Phone = t.Phone,
                                                  TicketFrom = t.TicketFrom,
                                                  TicketTo = t.TicketTo,
                                                  TicketTotal = t.TicketTotal,
                                                  Amount = sType == "GROSS" ? Convert.ToDecimal(t.TicketTotal * 25) : sType == "NET" && c.UserType == "CHURCH" && c.ChurchType == "PARTNER" ? Convert.ToDecimal(t.TicketTotal * 12.50) : Convert.ToDecimal(t.TicketTotal * 25),
                                                  Collected_By = t.Collected_By,
                                                  Email = t.Email,
                                                  City = t.City,
                                                  State = t.State,
                                                  Zip = t.Zip,
                                                  LastAccessUserID = t.LastAccessUserID,
                                                  LastDistUserId = t.LastDistUserId,
                                                  LastDistUserName = t.LastDistUserName,
                                                  LastAccessUserName = t.LastAccessUserName,
                                                  TicketSold_fk = t.TicketSold_fk,
                                                  Address = t.Address,
                                                  CreatedDate = t.CreatedDate.Date,
                                                  MEM2MEM_FK = t.MEM2MEM_FK,
                                                  MEMBER_FK = t.MEMBER_FK,
                                                  CHURCH_FK = t.CHURCH_FK,
                                                  RAFFLE_FK = t.RAFFLE_FK,
                                                  YearID = t.YearID,
                                                  SalesBy = c.name
                                              }).ToList();

            if (lstRaffleList != null && lstRaffleList.Count > 0)
            {
                lstChruchList.AddRange(lstRaffleList);
            }

            return lstChruchList.OrderByDescending(x => x.CreatedDate).ToList();


        }
        else
        {
            return (from t in _entities.TicketSolds.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                    join m in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.MEMBER_FK equals m.user_pk
                    select new TicketSold
                    {
                        GivenTo = t.GivenTo,
                        Phone = t.Phone,
                        TicketFrom = t.TicketFrom,
                        TicketTo = t.TicketTo,
                        TicketTotal = t.TicketTotal,
                        Amount = sType == "GROSS" ? Convert.ToDecimal(t.TicketTotal * 25) : sType == "NET" && m.UserType == "CHURCH" && m.ChurchType == "PARTNER" ? Convert.ToDecimal(t.TicketTotal * 12.50) : Convert.ToDecimal(t.TicketTotal * 25),
                        Collected_By = t.Collected_By,
                        Email = t.Email,
                        City = t.City,
                        State = t.State,
                        Zip = t.Zip,
                        LastAccessUserID = t.LastAccessUserID,
                        LastDistUserId = t.LastDistUserId,
                        LastDistUserName = t.LastDistUserName,
                        LastAccessUserName = t.LastAccessUserName,
                        TicketSold_fk = t.TicketSold_fk,
                        Address = t.Address,
                        CreatedDate = t.CreatedDate.Date,
                        MEM2MEM_FK = t.MEM2MEM_FK,
                        MEMBER_FK = t.MEMBER_FK,
                        CHURCH_FK = t.CHURCH_FK,
                        RAFFLE_FK = t.RAFFLE_FK,
                        YearID = t.YearID,
                        SalesBy = m.name
                    }).OrderByDescending(x => x.CreatedDate).ToList();

        }
    }



    public TicketsDistribution GetLastTickDist(int FromTicket, int ToTicket)
    {

        List<TicketsDistribution> lstTicketDist = (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                                                   select new TicketsDistribution
                                                   {
                                                       CreatedDate = t.CreatedDate.AddHours(3),
                                                       Dist_user_Fk = t.Dist_user_Fk,
                                                       FromTicket = t.FromTicket,
                                                       LastAccessUserID = t.LastAccessUserID,
                                                       Tickt_Distr_pk = t.Tickt_Distr_pk,
                                                       TotalTickets = t.TotalTickets,
                                                       ToTicket = t.ToTicket,
                                                       FTick = Convert.ToInt32(t.FromTicket),
                                                       TTick = Convert.ToInt32(t.ToTicket),
                                                       CHURCH_FK= t.CHURCH_FK,
                                                       RAFFLE_FK=t.RAFFLE_FK,
                                                       MEMBER_FK=t.MEMBER_FK,
                                                       MEM2MEM_FK=t.MEM2MEM_FK,
                                                       Dist_Type= t.Dist_Type,
                                                       LastDistUserFk= t.LastDistUserFk,
                                                       LastDistUserName= t.LastDistUserName,
                                                       YearID= t.YearID
                                                   }).ToList();


        int ticketDist = 0;
        TicketsDistribution obj = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.YearID == UserSession.Inst.CurrentLoginID).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        ticketDist = obj != null ? obj.Tickt_Distr_pk : 0;


        return (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentRaffleYearID)
                join u in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentRaffleYearID) on t.Dist_user_Fk equals u.user_pk
                where t.Tickt_Distr_pk == ticketDist
                select new TicketsDistribution
                {
                    CreatedDate = t.CreatedDate.AddHours(3),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    Dist_Type = t.Dist_Type,
                    LastDistUserName = u.name,
                    LastDistUserFk = u.user_pk,
                    Address= u.Address,
                   State= u.State,
                   phone= u.phone,
                   Zip= u.Zip,
                   City= u.City,
                   email=u.email,
                    CHURCH_FK = t.CHURCH_FK,
                    RAFFLE_FK = t.RAFFLE_FK,
                    MEMBER_FK = t.MEMBER_FK,
                    MEM2MEM_FK = t.MEM2MEM_FK
                }).FirstOrDefault();
    }


    public List<TicketsDistribution> GetTickHistory(int FromTicket, int ToTicket)
    {

        List<TicketsDistribution> lstTicketDist = (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.CHURCH_FK != null)
                                                   select new TicketsDistribution
                                                   {
                                                       CreatedDate = t.CreatedDate.AddHours(3),
                                                       Dist_user_Fk = t.Dist_user_Fk,
                                                       FromTicket = t.FromTicket,
                                                       LastAccessUserID = t.LastAccessUserID,
                                                       Tickt_Distr_pk = t.Tickt_Distr_pk,
                                                       TotalTickets = t.TotalTickets,
                                                       ToTicket = t.ToTicket,
                                                       FTick = Convert.ToInt32(t.FromTicket),
                                                       TTick = Convert.ToInt32(t.ToTicket),
                                                   }).ToList();

        List<TicketsDistribution> lst = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket).OrderBy(x => x.CreatedDate).ToList();
        return (from t in lst.AsEnumerable()
                join u in _entities.DIST_USERS.AsEnumerable() on t.Dist_user_Fk equals u.user_pk
                join ab in _entities.DIST_USERS.AsEnumerable() on t.LastAccessUserID equals ab.user_pk
                select new TicketsDistribution
                {
                    CreatedDate = t.CreatedDate.AddHours(3),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    LastDistUserName = u.name,
                    LastDistUserFk = u.user_pk,
                    DistUserName = ab.name,
                    strCreatedDate = t.CreatedDate.ToShortDateString(),
                }).ToList();
    }


    public List<TicketsDistribution> GetTickDistHistory(int FromTicket, int ToTicket)
    {

        List<TicketsDistribution> lstTicketDist = (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                                                   select new TicketsDistribution
                                                   {
                                                       CreatedDate = t.CreatedDate.AddHours(3),
                                                       Dist_user_Fk = t.Dist_user_Fk,
                                                       FromTicket = t.FromTicket,
                                                       LastAccessUserID= t.LastAccessUserID,
                                                       //LastAccessUserID = Convert.ToInt32(t.CHURCH_FK) == 0 ? Convert.ToInt32(t.LastAccessUserID) : Convert.ToInt32(t.MEMBER_FK) == 0 ? Convert.ToInt32(t.CHURCH_FK) : Convert.ToInt32(t.MEM2MEM_FK) == 0 ? Convert.ToInt32(t.MEMBER_FK) : Convert.ToInt32(t.LastAccessUserID),
                                                       Tickt_Distr_pk = t.Tickt_Distr_pk,
                                                       TotalTickets = t.TotalTickets,
                                                       ToTicket = t.ToTicket,
                                                       FTick = Convert.ToInt32(t.FromTicket),
                                                       TTick = Convert.ToInt32(t.ToTicket),
                                                   }).ToList();

        List<TicketsDistribution> lst = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket).OrderBy(x => x.CreatedDate).ToList();
        return (from t in lst.AsEnumerable()
                join u in _entities.DIST_USERS.AsEnumerable() on t.Dist_user_Fk equals u.user_pk
                join ab in _entities.DIST_USERS.AsEnumerable() on t.LastAccessUserID equals ab.user_pk
                select new TicketsDistribution
                {
                    CreatedDate = t.CreatedDate.AddHours(3),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    LastDistUserName = u.name,
                    LastDistUserFk = u.user_pk,
                    DistUserName = ab.name,
                    strCreatedDate = t.CreatedDate.ToString("MM/dd/yyyy HH:MM"),
                }).ToList();
    }

    public string DistUserName(int uKey)
    {
        string RetVal = "";
        RetVal = _entities.DIST_USERS.Where(X => X.user_pk == uKey).FirstOrDefault().name;
        return RetVal;
    }

    public List<DIST_USERS> GetDistUserNameList()
    {
        return _entities.DIST_USERS.Where(x => x.YearID == UserSession.Inst.CurrentLoginID).ToList().OrderBy(X => X.name).ToList();
    }

    public List<DIST_USERS> GetDistUserList(int yearID)
    {
        return _entities.DIST_USERS.Where(x => x.YearID == yearID).ToList();

    }

    public List<DIST_USERS> GetDistUserList()
    {

        return (from usr in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                join dist in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on usr.user_pk equals dist.Dist_user_Fk
                //select usr).ToList();
                select new DIST_USERS
                {
                    Address = usr.Address,
                    Balance = usr.Balance,
                    ChurchAdminstName = usr.ChurchAdminstName,
                    CreatedBy_UserFk = usr.CreatedBy_UserFk,
                    createdDate = usr.createdDate.AddHours(3),
                    email = usr.email,
                    LastaccessUserId = usr.LastaccessUserId,
                    mobile = usr.mobile,
                    modifiedDate = usr.modifiedDate.AddHours(3),
                    name = usr.name,
                    parent_userFk = usr.parent_userFk,
                    password = usr.password,
                    UserName = usr.UserName == null ? "" : usr.UserName,
                    phone = usr.phone,
                    TicketDist_Fk = usr.TicketDist_Fk,
                    user_pk = usr.user_pk,
                    UserType = usr.UserType,
                    FromTicket = dist.FromTicket,
                    ToTicket = dist.ToTicket,
                    TotalTicket = dist.TotalTickets,
                    FullAddress = fullAddress(usr.Address, usr.City, usr.State, usr.Zip),
                    YearID = dist.YearID,
                }).OrderByDescending(x => x.createdDate).ToList();
    }


    public string fullAddress(string add, string city, string state, string zip)
    {
        StringBuilder retAdd = new StringBuilder();

        if (!string.IsNullOrEmpty(add))
        {
            retAdd.AppendFormat(add);
            retAdd.AppendFormat(",");
        }
        if (!string.IsNullOrEmpty(city))
        {
            retAdd.AppendFormat(city);
            retAdd.AppendFormat(",");
        }
        if (!string.IsNullOrEmpty(state))
        {
            retAdd.AppendFormat(state);
            retAdd.AppendFormat(",");
        }
        if (!string.IsNullOrEmpty(zip))
        {
            retAdd.AppendFormat(zip);
            retAdd.AppendFormat(",");
        }

        return retAdd.ToString().TrimEnd(',');

    }


    public DIST_USERS GetDistUserObj(int _pkey)
    {
        return _entities.DIST_USERS.Where(x => x.user_pk == _pkey).FirstOrDefault();
    }

    public DIST_USERS GetDistUserObjByName(string name, string fromTicket, string toTicket)
    {
        return (from t in _entities.TicketsDistributions.AsEnumerable()
                join u in _entities.DIST_USERS.AsEnumerable() on t.Dist_user_Fk equals u.user_pk
                where u.name.ToLower().Trim() == name.ToLower().Trim() && t.FromTicket == fromTicket && t.ToTicket == toTicket && t.YearID == UserSession.Inst.CurrentRaffleYearID
                select u).FirstOrDefault();
    }

    public User GetUserObj(int _pkey)
    {
        return _entities.Users.Where(x => x.user_pk == _pkey).FirstOrDefault();
    }

    public int AddUser(User userObj)
    {
        if (_entities.Users.Where(X => X.username.Trim().ToLower() == userObj.username.ToLower().Trim()).FirstOrDefault() == null)
        {
            _entities.Users.AddObject(userObj);
            _entities.SaveChanges();
            return userObj.user_pk;
        }
        else
        {
            return 0;
        }
    }

    public int AddUpdateUserPermission(List<PagePermission> lstPagePermission)
    {
        foreach (PagePermission obj in lstPagePermission)
        {
            PagePermission _pagePerObj = _entities.PagePermissions.Where(X => X.userID == obj.userID && X.pageID == obj.pageID).FirstOrDefault();
            if (_pagePerObj != null)
            {
                _pagePerObj.PageView = obj.PageView;
            }
            else
            {
                _entities.PagePermissions.AddObject(obj);
            }
        }

        _entities.SaveChanges();


        return 1;
    }

    public List<PagePermission> GetUserPagePermissionByUserID(int _userID)
    {
        return _entities.PagePermissions.Where(x => x.userID == _userID).ToList();

    }


    public int UpdateUser(User objUser)
    {
        User _userObj = _entities.Users.Where(X => X.user_pk == objUser.user_pk).FirstOrDefault();
        if (_userObj != null)
        {
            _userObj.FullName = objUser.FullName;
            _userObj.Email = objUser.Email;
            _userObj.UserAddress = objUser.UserAddress;
            _userObj.username = objUser.username;
            _userObj.userPhone = objUser.userPhone;
            _userObj.UserType = objUser.UserType;
            _userObj.ModifiedDate = DateTime.Now;
            _userObj.DistUserType = objUser.DistUserType;
            _userObj.DistUser_Fk = objUser.DistUser_Fk;
            if (!string.IsNullOrEmpty(objUser.password))
            {
                _userObj.password = objUser.password;
            }
            _entities.SaveChanges();
            return 1;
        }
        return 0;
    }


    public TicketsDistribution GetTicketDistObj(int _pkey)
    {
        return _entities.TicketsDistributions.Where(x => x.Tickt_Distr_pk == _pkey).FirstOrDefault();
    }


    public bool UpdateEditTicket(string fromTicket, string toTiceket, int totTicket, int distFkey)
    {
        TicketsDistribution objDist = _entities.TicketsDistributions.Where(x => x.Tickt_Distr_pk == distFkey).FirstOrDefault();
        if (objDist != null)
        {
            objDist.FromTicket = fromTicket;
            objDist.ToTicket = toTiceket;
            objDist.TotalTickets = totTicket;
            _entities.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool UpdateReDestTicket(string fromTicket, string toTiceket, int totTicket, int distFkey, int uKey, string _type)
    {
        TicketsDistribution objDist = _entities.TicketsDistributions.Where(x => x.Tickt_Distr_pk == distFkey).FirstOrDefault();
        if (objDist != null)
        {
            DIST_USERS _OldobjUser = _entities.DIST_USERS.Where(x => x.user_pk == uKey).FirstOrDefault();

            DIST_USERS objUser = _entities.DIST_USERS.Where(x => x.user_pk == _OldobjUser.CreatedBy_UserFk).FirstOrDefault();


            objDist.FromTicket = fromTicket;
            objDist.ToTicket = toTiceket;
            objDist.TotalTickets = totTicket;
            objDist.RAFFLE_FK = objUser.RAFFLE_FK;
            objDist.MEMBER_FK = objUser.MEMBER_FK;
            objDist.MEM2MEM_FK = objUser.MEM2MEM_FK;
            objDist.CHURCH_FK = objUser.CHURCH_FK;
            objDist.Dist_user_Fk = objUser.user_pk;
            objDist.LastAccessUserID = objUser.CreatedBy_UserFk;

            int fTicket = Convert.ToInt32(fromTicket);
            int tTicket = Convert.ToInt32(toTiceket);

            List<TicketSold> lstTicketSold = _entities.TicketSolds.Where(x => x.LastDistUserId == objDist.Dist_user_Fk && x.TicketFrom >= fTicket && x.TicketTo <= tTicket).ToList();

            foreach (TicketSold item in lstTicketSold)
            {
                item.LastDistUserId = objUser.user_pk;
                item.LastDistUserName = objUser.name;
                item.LastAccessUserID = objUser.user_pk;
                item.LastAccessUserName = objUser.name;
                item.RAFFLE_FK = objUser.RAFFLE_FK;
                item.MEMBER_FK = objUser.MEMBER_FK;
                item.MEM2MEM_FK = objUser.MEM2MEM_FK;
                item.CHURCH_FK = objUser.CHURCH_FK;
            }

            _entities.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }



    public List<TicketsDistribution> GetTicketDistributionList()
    {
        return _entities.TicketsDistributions.ToList();
    }

    public List<TicketsDistribution> GetTickeDistributionList(int UserFk)
    {
        return (from t in _entities.TicketsDistributions.AsEnumerable()
                join u in _entities.DIST_USERS.AsEnumerable() on t.Dist_user_Fk equals u.user_pk
                where t.LastAccessUserID == UserFk
                select new TicketsDistribution
                {
                    CreatedDate = t.CreatedDate.AddHours(3),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    DistUserName = u.name,
                    RAFFLE_FK = t.RAFFLE_FK,
                    CHURCH_FK = t.CHURCH_FK,
                    MEMBER_FK = t.MEMBER_FK,
                    MEM2MEM_FK = t.MEM2MEM_FK,
                }).OrderBy(x => x.CreatedDate).ToList();
    }


    public bool DeleteTicketDistribution(int FromTicket, int ToTicket, TicketsDistribution objTickDist)
    {
        List<TicketsDistribution> lstTicketDist = (from t in _entities.TicketsDistributions.AsEnumerable()
                                                   select new TicketsDistribution
                                                   {
                                                       CreatedDate = t.CreatedDate.AddHours(3),
                                                       Dist_user_Fk = t.Dist_user_Fk,
                                                       FromTicket = t.FromTicket,
                                                       LastAccessUserID = t.LastAccessUserID,
                                                       Tickt_Distr_pk = t.Tickt_Distr_pk,
                                                       TotalTickets = t.TotalTickets,
                                                       ToTicket = t.ToTicket,
                                                       FTick = Convert.ToInt32(t.FromTicket),
                                                       TTick = Convert.ToInt32(t.ToTicket),
                                                       RAFFLE_FK = t.RAFFLE_FK,
                                                       CHURCH_FK = t.CHURCH_FK,
                                                       MEMBER_FK = t.MEMBER_FK,
                                                       MEM2MEM_FK = t.MEM2MEM_FK,
                                                   }).ToList();

        DIST_USERS objDistUser = _entities.DIST_USERS.Where(x => x.user_pk == objTickDist.Dist_user_Fk).FirstOrDefault();
        List<TicketsDistribution> lstDist = new List<TicketsDistribution>();

        if (objDistUser.UserType == "RAFFLE")
        {
            lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != objTickDist.Dist_user_Fk && x.RAFFLE_FK == objDistUser.user_pk).ToList();
        }

        else if (objDistUser.UserType == "CHURCH")
        {
            lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != objTickDist.Dist_user_Fk && x.CHURCH_FK == objDistUser.user_pk).ToList();
        }
        else if (objDistUser.UserType == "MEMBER")
        {
            lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != objTickDist.Dist_user_Fk && x.MEMBER_FK == objDistUser.user_pk).ToList();
        }
        else if (objDistUser.UserType == "MEMBER2MEMBER")
        {
            lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != objTickDist.Dist_user_Fk && x.MEM2MEM_FK == objDistUser.user_pk).ToList();
        }


        if (lstDist.Count() > 0)
        {
            return false;
        }
        else
        {
            List<TicketSold> lstTicketSold = _entities.TicketSolds.Where(x => x.LastAccessUserID == objTickDist.Dist_user_Fk).ToList();

            DIST_USERS objParentDist = _entities.DIST_USERS.Where(x => x.user_pk == objDistUser.CreatedBy_UserFk).FirstOrDefault();

            foreach (TicketSold obj in lstTicketSold)
            {

                obj.LastAccessUserID = objParentDist.user_pk;
                obj.RAFFLE_FK = objParentDist.RAFFLE_FK;
                obj.CHURCH_FK = objParentDist.CHURCH_FK;
                obj.MEM2MEM_FK = objParentDist.MEM2MEM_FK;
                obj.MEMBER_FK = objParentDist.MEMBER_FK;
                obj.LastDistUserId = objParentDist.user_pk;
                obj.LastDistUserName = objParentDist.name;
                obj.LastAccessUserName = objParentDist.name;
            }

            foreach (TicketsDistribution obj in lstDist)
            {
                _entities.TicketsDistributions.DeleteObject(obj);
            }

            _entities.TicketsDistributions.DeleteObject(objTickDist);

            _entities.DIST_USERS.DeleteObject(objDistUser);

            _entities.SaveChanges();

            return true;
        }




    }



    public bool IsTicketDistribution(int FromTicket, int ToTicket, int userFkey)
    {
        List<TicketsDistribution> lstTicketDist = (from t in _entities.TicketsDistributions.AsEnumerable()
                                                   select new TicketsDistribution
                                                   {
                                                       CreatedDate = t.CreatedDate.AddHours(3),
                                                       Dist_user_Fk = t.Dist_user_Fk,
                                                       FromTicket = t.FromTicket,
                                                       LastAccessUserID = t.LastAccessUserID,
                                                       Tickt_Distr_pk = t.Tickt_Distr_pk,
                                                       TotalTickets = t.TotalTickets,
                                                       ToTicket = t.ToTicket,
                                                       FTick = Convert.ToInt32(t.FromTicket),
                                                       TTick = Convert.ToInt32(t.ToTicket),
                                                   }).ToList();
        if (userFkey > 0)
        {
            DIST_USERS objDistUser = _entities.DIST_USERS.Where(x => x.user_pk == userFkey).FirstOrDefault();
            List<TicketsDistribution> lstDist = new List<TicketsDistribution>();

            if (objDistUser.UserType == "RAFFLE")
            {
                lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != userFkey && x.RAFFLE_FK == objDistUser.user_pk).ToList();
            }

            else if (objDistUser.UserType == "CHURCH")
            {
                lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != userFkey && x.CHURCH_FK == objDistUser.user_pk).ToList();
            }
            else if (objDistUser.UserType == "MEMBER")
            {
                lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != userFkey && x.MEMBER_FK == objDistUser.user_pk).ToList();
            }
            else if (objDistUser.UserType == "MEMBER2MEMBER")
            {
                lstDist = lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_user_Fk != userFkey && x.MEM2MEM_FK == objDistUser.user_pk).ToList();
            }
            if (lstDist.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }


    public bool IsTicketDistributionOverlapping(int FromTicket, int ToTicket, int userFkey, string userType)
    {
        List<TicketsDistribution> lstTicketDist = (from t in _entities.TicketsDistributions.AsEnumerable()
                                                   join usr in _entities.DIST_USERS.AsEnumerable()
                                                       on t.Dist_user_Fk equals usr.user_pk
                                                   select new TicketsDistribution
                                                   {
                                                       CreatedDate = t.CreatedDate.AddHours(3),
                                                       Dist_user_Fk = t.Dist_user_Fk,
                                                       FromTicket = t.FromTicket,
                                                       LastAccessUserID = t.LastAccessUserID,
                                                       Tickt_Distr_pk = t.Tickt_Distr_pk,
                                                       TotalTickets = t.TotalTickets,
                                                       ToTicket = t.ToTicket,
                                                       FTick = Convert.ToInt32(t.FromTicket),
                                                       TTick = Convert.ToInt32(t.ToTicket),
                                                       RAFFLE_FK = t.RAFFLE_FK,
                                                       CHURCH_FK = t.CHURCH_FK,
                                                       MEMBER_FK = t.MEMBER_FK,
                                                       MEM2MEM_FK = t.MEM2MEM_FK,
                                                       Dist_Type = usr.UserType,
                                                   }).ToList();
        if (userFkey > 0)
        {
            if (lstTicketDist.Where(x => x.FTick >= FromTicket && x.TTick <= ToTicket && x.Dist_user_Fk != userFkey && x.Dist_Type == userType).Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (lstTicketDist.Where(x => x.FTick <= FromTicket && x.TTick >= ToTicket && x.Dist_Type == userType).Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }

    public List<RaffleYear> GetRaffleYearList()
    {
        return _entities.RaffleYears.ToList();
    }


    public List<TicketsDistribution> GetTickeDistributionDetailList()
    {
        return (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                join u in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.Dist_user_Fk equals u.user_pk
                join u1 in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on u.CreatedBy_UserFk equals u1.user_pk
                select new TicketsDistribution
                {
                    CreatedDate = t.CreatedDate.AddHours(3),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    FromDistUserName = u1.name,
                    ToDistUserName = u.name,
                    FTick = Convert.ToInt32(t.FromTicket),
                    TTick = Convert.ToInt32(t.ToTicket),
                    RAFFLE_FK = t.RAFFLE_FK,
                    CHURCH_FK = t.CHURCH_FK,
                    MEMBER_FK = t.MEMBER_FK,
                    MEM2MEM_FK = t.MEM2MEM_FK,
                    YearID = t.YearID,
                }).OrderBy(x => x.CreatedDate).ToList();
    }

    public List<TicketsDistribution> GetTop5DistributerList()
    {
        return _entities.TicketsDistributions.ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }
    public List<TicketsDistribution> GetReffleDistributerList()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.RAFFLE_FK == UserSession.Inst.RafflePK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }

    



    public string GetTotalSalesTicekt()
    {
        return _entities.TicketSolds.ToList().Sum(x => x.TicketTotal).ToString();
    }

    public string GetReffleTotalSalesTicekt()
    {
        return _entities.TicketSolds.ToList().Where(x=>x.RAFFLE_FK==UserSession.Inst.RafflePK).ToList().Sum(x => x.TicketTotal).ToString();
    }

    public string GetTotalSalesAmmount()
    {
        return _entities.TicketSolds.ToList().Sum(x => x.Amount).ToString();
    }

    public string GetReffleTotalSalesAmmount()
    {
        return _entities.TicketSolds.ToList().Where(x=>x.RAFFLE_FK==UserSession.Inst.RafflePK).ToList().Sum(x => x.Amount).ToString();
    }

    public string GetTotaldisribution()
    {
        return _entities.TicketsDistributions.ToList().Sum(x => x.TotalTickets).ToString();
    }
    public string GetReffleTotaldisribution()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.RAFFLE_FK == UserSession.Inst.RafflePK).ToList().Sum(x => x.TotalTickets).ToString();
    }

    public List<TicketSold> GetTop5SellList()
    {
        return _entities.TicketSolds.ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }

    public List<TicketSold> GetReffeleSellList()
    {
        return _entities.TicketSolds.ToList().Where(x=>x.RAFFLE_FK==UserSession.Inst.RafflePK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }

    public List<TicketsDistribution> GetTickeDistributionList()
    {
        return (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID)
                join u in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.Dist_user_Fk equals u.user_pk
                select new TicketsDistribution
                {
                    TicketDistribution_PK= t.Tickt_Distr_pk,
                    CreatedDate = t.CreatedDate.AddHours(3),
                    strCreatedDate=  t.CreatedDate.AddHours(3).ToString("MM/dd/yyyy HH:MM"),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    ToDistUserName = u.name,
                    Address= u.Address,
                    State= u.State,
                    Zip= u.Zip,
                    Amount= Convert.ToDecimal(t.TotalTickets * u.TicketRate),
                    phone= u.phone,
                    RAFFLE_FK = t.RAFFLE_FK,
                    CHURCH_FK = t.CHURCH_FK,
                    MEMBER_FK = t.MEMBER_FK,
                    MEM2MEM_FK = t.MEM2MEM_FK,
                    YearID = t.YearID,
                    FTick = Convert.ToInt32(t.FromTicket),
                    TTick = Convert.ToInt32(t.ToTicket)
                }).OrderBy(x => x.CreatedDate).ToList();
    }

    public List<TicketsDistribution> GetChurchTickeDistributionByRaffle()
    {
        return (from t in _entities.TicketsDistributions.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID && x.MEMBER_FK==null)
                join u in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on t.CHURCH_FK equals u.user_pk
                join u1 in _entities.DIST_USERS.AsEnumerable().Where(x => x.YearID == UserSession.Inst.CurrentLoginID) on u.RAFFLE_FK equals u1.user_pk
                select new TicketsDistribution
                {
                    CreatedDate = t.CreatedDate.AddHours(3),
                    Dist_user_Fk = t.Dist_user_Fk,
                    FromTicket = t.FromTicket,
                    LastAccessUserID = t.LastAccessUserID,
                    Tickt_Distr_pk = t.Tickt_Distr_pk,
                    TotalTickets = t.TotalTickets,
                    ToTicket = t.ToTicket,
                    FromDistUserName = u1.name,
                    ToDistUserName = u.name,
                    FTick = Convert.ToInt32(t.FromTicket),
                    TTick = Convert.ToInt32(t.ToTicket),
                    RAFFLE_FK = t.RAFFLE_FK,
                    CHURCH_FK = t.CHURCH_FK,
                    MEMBER_FK = t.MEMBER_FK,
                    MEM2MEM_FK = t.MEM2MEM_FK,
                    YearID = t.YearID,
                }).OrderBy(x => x.CreatedDate).ToList();
    }

    public string GetChurchTotalSalesTicket()
    {
        return _entities.TicketSolds.ToList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).ToList().Sum(x => x.TicketTotal).ToString();
    }
    public string GetChurchTotalSalesAmmount()
    {
        return _entities.TicketSolds.ToList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).ToList().Sum(x => x.Amount).ToString();
    }
    public string GetChurchTotaldisribution()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).ToList().Sum(x => x.TotalTickets).ToString();
    }
    public List<TicketsDistribution> GetChurchDistributerList()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }

    public List<TicketSold> GetChurchSellList()
    {
        return _entities.TicketSolds.ToList().Where(x => x.CHURCH_FK == UserSession.Inst.ChurchPK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }



    public string GetMemberTotalSalesTicket()
    {
        return _entities.TicketSolds.ToList().Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).ToList().Sum(x => x.TicketTotal).ToString();
    }
    public string GetMemberTotalSalesAmmount()
    {
        return _entities.TicketSolds.ToList().Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).ToList().Sum(x => x.Amount).ToString();
    }
    public string GetMemberTotaldisribution()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).ToList().Sum(x => x.TotalTickets).ToString();
    }
    public List<TicketsDistribution> GetMemberDistributerList()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }

    public List<TicketSold> GetMemberSellList()
    {
        return _entities.TicketSolds.ToList().Where(x => x.MEMBER_FK == UserSession.Inst.MemberPK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }


    public string GetMemberToMemberTotalSalesTicket()
    {
        return _entities.TicketSolds.ToList().Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList().Sum(x => x.TicketTotal).ToString();
    }
    public string GetMemberToMemberTotalSalesAmmount()
    {
        return _entities.TicketSolds.ToList().Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList().Sum(x => x.Amount).ToString();
    }
    public string GetMemberToMemberTotaldisribution()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList().Sum(x => x.TotalTickets).ToString();
    }
    public List<TicketsDistribution> GetMemberToMemberDistributerList()
    {
        return _entities.TicketsDistributions.ToList().Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }

    public List<TicketSold> GetMemberToMemberSellList()
    {
        return _entities.TicketSolds.ToList().Where(x => x.MEM2MEM_FK == UserSession.Inst.Member2MemberPK).ToList().OrderByDescending(x => x.CreatedDate).ToList().Take(5).ToList();
    }




}