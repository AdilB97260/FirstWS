using RaffleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ExpencessController
/// </summary>
public class ExpencessController
{
    #region [DECLRATION]
    RaffleEntities _entities = new RaffleEntities();
    #endregion

    public ExpencessController()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public List<Expencess> GetAllExpencessList(int yearID)
    {
        return _entities.Expencesses.Where(x => x.year_id == yearID).ToList().OrderByDescending(x => x.expences_date).ToList();
    }

    public int AddExpenses(Expencess objExp)
    {
        try
        {
            _entities.Expencesses.AddObject(objExp);
            _entities.SaveChanges();
            return objExp.expences_id;
        }
        catch(Exception ex)
        {
            return 0;
        }
    }

    public int UpdateExpenses(Expencess objExp)
    {
        try
        {
            Expencess _obj = _entities.Expencesses.Where(x => x.expences_id == objExp.expences_id).FirstOrDefault();
            if (_obj != null)
            {
                _obj.expences_name = objExp.expences_name;
                _obj.given_to = objExp.given_to;
                _obj.expences_date = objExp.expences_date;
                _obj.Amount = objExp.Amount;
                _obj.Category = objExp.Category;
                _obj.description = objExp.description;
                if(!string.IsNullOrEmpty(objExp.Receipt))
                {
                    _obj.Receipt = objExp.Receipt;
                }
                _entities.SaveChanges();
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 0;
        }
    }

    public Expencess GetExpencess(int id)
    {
        return _entities.Expencesses.Where(x => x.expences_id == id).FirstOrDefault();
    }

}